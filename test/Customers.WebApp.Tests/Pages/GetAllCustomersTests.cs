using FluentAssertions;
using Microsoft.Playwright;

namespace Customers.WebApp.Tests.Pages;

[Collection("Test collection")]
public class GetAllCustomersTests
{
	private readonly SharedTestContext _testContext;

	public GetAllCustomersTests(SharedTestContext testContext)
	{
		_testContext = testContext;
	}


	[Fact(Timeout = 5000)]
	public async Task GetAll_ContainsCustomers_WhenCustomersExists()
	{
		// Arrange
		var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
		{
			BaseURL = SharedTestContext.AppUrl
		});
		// recommended to insert a customer directly into a database
		var customer1 = await _testContext.CreateCustomer(page);
		var customer2 = await _testContext.CreateCustomer(page);

		// Act
		var linkElement = page.Locator("a[href='customers']").First;
		var link = await linkElement.GetAttributeAsync("href");
		await page.GotoAsync(link!);
		// or await page.GotoAsync("/customers");


		// Assert
		// Page screenshot should be taken FIRST
		// if test fails screenshot is not taken!!
		await _testContext.TakeFullPageSreenShot(page, this.GetType().Name, nameof(GetAll_ContainsCustomers_WhenCustomersExists));

		/*
		<table class="table">
		<thead>
		<tr>
		<th scope="col">Fullname</th>
        <th scope="col">Email</th>
        <th scope="col">GitHub Username</th>
        <th scope="col">Date of birth</th>
        <th scope="col">Actions</th>
		</tr>
		</thead>
		<tbody>
			<tr>
				<td>Jeannie Hickle</td>
				<td>Jeannie41@gmail.com</td>
				<td>anyuser</td>
				<td>31/10/1976</td>
				<td><button type="button" class="btn btn-primary">View</button>
					<button type="button" class="btn btn-warning">Edit</button>
					<button type="button" class="btn btn-danger">Delete</button></td>
			</tr>
			<tr>
				<td>Simon Hayes</td>
				<td>Simon88@gmail.com</td>
				<td>anyuser</td>
				<td>13/06/1970</td>
				<td><button type="button" class="btn btn-primary">View</button>
					<button type="button" class="btn btn-warning">Edit</button>
					<button type="button" class="btn btn-danger">Delete</button></td>
			</tr>
		</tbody>
		</table>
		 */

		// Or for strict
		// "tbody > tr:nth-child(1) > td"
		var name = page.Locator("tbody > tr > td")
			.Filter(new LocatorFilterOptions { HasTextString = customer1.FullName });

		(await name.First.InnerTextAsync()).Should().Be(customer1.FullName);

		var name2 = page.Locator("tbody > tr > td")
			.Filter(new LocatorFilterOptions { HasTextString = customer2.FullName });

		(await name2.First.InnerTextAsync()).Should().Be(customer2.FullName);

		await page.CloseAsync();
	}
}
