using Bogus;
using Customers.WebApp.Models;
using FluentAssertions;
using Microsoft.Playwright;

namespace Customers.WebApp.Tests.Pages;

[Collection("Test collection")]
public class AddCustomerTests
{
	private readonly SharedTestContext _testContext;

	private readonly Faker<Customer> _customerGenerator = new Faker<Customer>()
		.RuleFor(r => r.Email, faker => faker.Person.Email)
		.RuleFor(r => r.FullName, faker => faker.Person.FullName)
		.RuleFor(r => r.GitHubUsername, SharedTestContext.ValidGitHubUserName)
		.RuleFor(r => r.DateOfBirth, faker => DateOnly.FromDateTime(faker.Person.DateOfBirth));


	public AddCustomerTests(SharedTestContext testContext)
	{
		_testContext = testContext;
	}

	[Fact]
	public async Task Create_ShouldCreateCustomer_WhenDataIsValid()
	{
		// Arrange
		var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
		{
			BaseURL = SharedTestContext.AppUrl
		});
		await page.GotoAsync("add-customer");
		var customer = _customerGenerator.Generate();

		// Act
		await page.FillAsync("input[id=fullname]", customer.FullName);
		await page.FillAsync("input[id=email]", customer.Email);
		await page.FillAsync("input[id=github-username]", customer.GitHubUsername);
		await page.FillAsync("input[id=dob]", customer.DateOfBirth.ToString("yyyy-MM-dd"));

		await page.CheckAsync("button[type=submit]");

		// Assert
		var linkElement = page.Locator("article>p>a").First;
		var link = await linkElement.GetAttributeAsync("href");
		await page.GotoAsync(link!);

		(await page.Locator("p[id=fullname-field]").InnerTextAsync()).Should().Be(customer.FullName);
		(await page.Locator("p[id=email-field]").InnerTextAsync()).Should().Be(customer.Email);
		(await page.Locator("p[id=github-username-field]").InnerTextAsync()).Should().Be(customer.GitHubUsername);
		(await page.Locator("p[id=dob-field]").InnerTextAsync()).Should().Be(customer.DateOfBirth.ToString("yyyy-MM-dd"));

		await page.CloseAsync();
	}
}
