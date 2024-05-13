using FluentAssertions;
using Microsoft.Playwright;

namespace Customers.WebApp.Tests.Integration.Pages;

[Collection("Test collection")]
public class DeleteCustomerTests
{
	private readonly SharedTestContext _testContext;

	public DeleteCustomerTests(SharedTestContext testContext)
	{
		_testContext = testContext;
	}

	[Fact(Timeout = 5000)]
	public async Task Delete_DeletesCustomer_WhenCustomerExists()
	{
		// Arrange
		var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
		{
			BaseURL = SharedTestContext.AppUrl
		});
		// recommended to insert a customer directly into a database
		var customer = await _testContext.CreateCustomer(page);

		// Act
		await page.GotoAsync($"customer/{customer.Id}");
		page.Dialog += (_, dialog) => dialog.AcceptAsync();
		await page.ClickAsync("button.btn.btn-danger");

		// Assert
		await page.GotoAsync($"customer/{customer.Id}");

		// Page screenshot should be taken FIRST
		// if test fails screenshot is not taken!!
		await _testContext.TakeFullPageSreenShot(page, this.GetType().Name, nameof(Delete_DeletesCustomer_WhenCustomerExists));

		(await page.Locator("article>p").InnerTextAsync()).Should().Be("No customer found with this id");

		await page.CloseAsync();
	}
}
