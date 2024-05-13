using Bogus;
using Customers.WebApp.Models;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Model.Common;
using Ductus.FluentDocker.Services;
using Microsoft.Playwright;

namespace Customers.WebApp.Tests.Integration;

public class SharedTestContext : IAsyncLifetime
{
	public const string ValidGitHubUserName = "anyuser";
	public const string AppName = "integration-app";
	public const string AppUrl = "https://localhost:7780";

	public GitHubApiServer GitHubApiServer { get; } = new();
	public DateTime TestBeginExecutionTime { get; } = DateTime.Now;
	public Faker<Customer> CustomerGenerator { get; } = new Faker<Customer>()
		.RuleFor(r => r.Email, faker => faker.Person.Email)
		.RuleFor(r => r.FullName, faker => faker.Person.FullName)
		.RuleFor(r => r.GitHubUsername, SharedTestContext.ValidGitHubUserName)
		.RuleFor(r => r.DateOfBirth, faker => DateOnly.FromDateTime(faker.Person.DateOfBirth));


	private static readonly string DockerComposeFile =
		Path.Combine(Directory.GetCurrentDirectory(), (TemplateString)"../../../docker-compose.integration.yml");

	private IPlaywright _playwright;
	public IBrowser Browser { get; private set; }

	private readonly ICompositeService _dockerService = new Builder()
		.UseContainer()
		.UseCompose()
		.FromFile(DockerComposeFile)
		.RemoveOrphans()
		.WaitForHttp(AppName, AppUrl)
		.Build();

	public async Task InitializeAsync()
	{
		GitHubApiServer.Start();
		GitHubApiServer.SetupUser(ValidGitHubUserName);
		_dockerService.Start();

		_playwright = await Playwright.CreateAsync();
		Browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
		{
			//Headless = false,
			SlowMo = 150
		});
	}

	public async Task DisposeAsync()
	{
		await Browser.DisposeAsync();
		_playwright.Dispose();
		_dockerService.Dispose();
		GitHubApiServer.Dispose();
	}

	public async Task TakeFullPageSreenShot(IPage page, string testsClassName, string testMethodName)
	{
		var testsCurrentRunDirectory = Path.Combine("ScreenShots", TestBeginExecutionTime.ToString("yyyy-MM-dd_HH-mm-ss"), testsClassName);
		Directory.CreateDirectory(testsCurrentRunDirectory);

		await page.ScreenshotAsync(new()
		{
			Path = Path.Combine(testsCurrentRunDirectory, $"{testMethodName}.png"),
			FullPage = true,
		});
	}

	public async Task<Customer> CreateCustomer(IPage page)
	{
		await page.GotoAsync("add-customer");
		var customer = CustomerGenerator.Generate();

		await page.FillAsync("input[id=fullname]", customer.FullName);
		await page.FillAsync("input[id=email]", customer.Email);
		await page.FillAsync("input[id=github-username]", customer.GitHubUsername);
		await page.FillAsync("input[id=dob]", customer.DateOfBirth.ToString("yyyy-MM-dd"));

		await page.ClickAsync("button[type=submit]");

		// retrieving the actual id of a customer
		var linkElement = page.Locator("article>p>a").First;
		var link = await linkElement.GetAttributeAsync("href");
		var idInText = link!.Split('/').Last();
		customer.Id = Guid.Parse(idInText);

		return customer;
	}
}
