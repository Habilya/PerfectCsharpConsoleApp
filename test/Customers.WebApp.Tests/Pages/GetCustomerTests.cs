using FluentAssertions;
using Microsoft.Playwright;

namespace Customers.WebApp.Tests.Pages;

[Collection("Test collection")]
public class GetCustomerTests
{
	private readonly SharedTestContext _testContext;

	public GetCustomerTests(SharedTestContext testContext)
	{
		_testContext = testContext;
	}


	[Fact(Timeout = 3000)]
	public async Task Get_ShouldReturnCustomer_WhenCustomerExists()
	{
		// Arrange
		var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
		{
			BaseURL = SharedTestContext.AppUrl
		});
		// recommended to insert a customer directly into a database
		var customer = await _testContext.CreateCustomer(page);

		// Act
		var linkElement = page.Locator("article>p>a").First;
		var link = await linkElement.GetAttributeAsync("href");
		await page.GotoAsync(link!);

		// Assert
		// Page screenshot should be taken FIRST
		// if test fails screenshot is not taken!!
		await _testContext.TakeFullPageSreenShot(page, this.GetType().Name, nameof(Get_ShouldReturnCustomer_WhenCustomerExists));

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
	public async Task Get_ShouldReturnNoCustomer_WhenCustomerDoesntExist()
	{
		// Arrange
		var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
		{
			BaseURL = SharedTestContext.AppUrl
		});
		// Non existent customer url
		var customerUrl = $"{SharedTestContext.AppUrl}/customer/{Guid.NewGuid()}";

		// Act
		await page.GotoAsync(customerUrl);

		// Assert
		// Page screenshot should be taken FIRST
		// if test fails screenshot is not taken!!
		await _testContext.TakeFullPageSreenShot(page, this.GetType().Name, nameof(Get_ShouldReturnNoCustomer_WhenCustomerDoesntExist));

		(await page.Locator("article>p").InnerTextAsync()).Should().Be("No customer found with this id");

		await page.CloseAsync();
	}
}
