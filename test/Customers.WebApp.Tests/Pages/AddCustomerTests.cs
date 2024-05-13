using FluentAssertions;
using Microsoft.Playwright;

namespace Customers.WebApp.Tests.Pages;

[Collection("Test collection")]
public class AddCustomerTests
{
	private readonly SharedTestContext _testContext;

	public AddCustomerTests(SharedTestContext testContext)
	{
		_testContext = testContext;
	}

	[Fact(Timeout = 4000)]
	public async Task Create_ShouldCreateCustomer_WhenDataIsValid()
	{
		// Arrange
		var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
		{
			BaseURL = SharedTestContext.AppUrl
		});

		// Act
		// recommended to insert a customer directly into a database
		var customer = await _testContext.CreateCustomer(page);

		// Assert
		var linkElement = page.Locator("article>p>a").First;
		var link = await linkElement.GetAttributeAsync("href");
		await page.GotoAsync(link!);

		// Page screenshot should be taken FIRST
		// if test fails screenshot is not taken!!
		await _testContext.TakeFullPageSreenShot(page, this.GetType().Name, nameof(Create_ShouldCreateCustomer_WhenDataIsValid));

		(await page.Locator("p[id=fullname-field]").InnerTextAsync()).Should().Be(customer.FullName);
		(await page.Locator("p[id=email-field]").InnerTextAsync()).Should().Be(customer.Email);
		(await page.Locator("p[id=github-username-field]").InnerTextAsync()).Should().Be(customer.GitHubUsername);
		// Note the format @"dd\/MM\/yyyy"
		// Slash is a date delimiter, so that will use the current culture date delimiter.
		// If you want to hard - code it to always use slash.
		(await page.Locator("p[id=dob-field]").InnerTextAsync()).Should().Be(customer.DateOfBirth.ToString(@"dd\/MM\/yyyy"));

		await page.CloseAsync();
	}

	[Fact(Timeout = 3000)]
	public async Task Create_ShowsError_WhenEmailIsInvalid()
	{
		// Arrange
		var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
		{
			BaseURL = SharedTestContext.AppUrl
		});
		await page.GotoAsync("add-customer");
		var customer = _testContext.CustomerGenerator.Generate();

		// Act
		await page.FillAsync("input[id=fullname]", customer.FullName);
		await page.FillAsync("input[id=email]", "[Invalid_Email]");
		await page.FillAsync("input[id=github-username]", customer.GitHubUsername);
		await page.FillAsync("input[id=dob]", customer.DateOfBirth.ToString("yyyy-MM-dd"));

		// Assert
		// Page screenshot should be taken FIRST
		// if test fails screenshot is not taken!!
		await _testContext.TakeFullPageSreenShot(page, this.GetType().Name, nameof(Create_ShowsError_WhenEmailIsInvalid));

		var element = page.Locator("li.validation-message").First;
		var text = await element.InnerTextAsync();
		text.Should().Be("Invalid email format");

		await page.CloseAsync();
	}
}
