using FluentAssertions;
using IntegrationTestingExamples.Tests.MockedTypicodeApi;

namespace IntegrationTestingExamples.Tests;

public class IntegrationTestsExamples : IClassFixture<TypicodeApiFactory>
{
	private readonly HttpClient _httpClient;

	public IntegrationTestsExamples(TypicodeApiFactory apiFactory)
	{
		_httpClient = apiFactory.CreateClient();
	}

	[Fact]
	public async Task CallToUsersRoute_ShouldNotBeNull_WhenCalledNormally()
	{
		// Arrange


		// Act
		var response = await _httpClient.GetAsync("users");

		// Assert
		var usersResponse = await response.Content.ReadAsStringAsync();
		usersResponse.Should().NotBeNull();
	}

	// This test demonstrates a call to the owned api with mocked external API response
	[Fact]
	public async Task CallToTypicodeusersRoute_ShouldReturnMockedResult_WhenCalledNormally()
	{
		// Arrange
		var expected = "[{\"id\":1,\"name\":\"TestFamilyName TestFirstName\",\"username\":\"TestUserName\",\"email\":\"Sincere@april.biz\",\"address\":{\"street\":\"Kulas Light\",\"suite\":\"Apt. 556\",\"city\":\"Gwenborough\",\"zipcode\":\"92998-3874\",\"geo\":{\"lat\":\"-37.3159\",\"lng\":\"81.1496\"}},\"phone\":\"1-770-736-8031 x56442\",\"website\":\"hildegard.org\",\"company\":{\"name\":\"Romaguera-Crona\",\"catchPhrase\":\"Multi-layered client-server neural-net\",\"bs\":\"harness real-time e-markets\"}}]";

		// Act
		var response = await _httpClient.GetAsync("typicodeusers");

		// Assert
		var usersResponse = await response.Content.ReadAsStringAsync();
		usersResponse.Should().NotBeNull()
			.And.Be(expected);
	}
}
