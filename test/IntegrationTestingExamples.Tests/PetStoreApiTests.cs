using FluentAssertions;
using IntegrationTestingExamples.Tests.MockedPetStoreApi;

namespace IntegrationTestingExamples.Tests;

public class PetStoreApiTests : IClassFixture<PetStoreApiFactory>
{
	private readonly HttpClient _httpClient;

	public PetStoreApiTests(PetStoreApiFactory apiFactory)
	{
		_httpClient = apiFactory.CreateClient();
	}

	[Fact]
	public async Task Test()
	{
		// Arrange


		// Act
		var response = await _httpClient.GetAsync("users");

		// Assert
		var usersResponse = await response.Content.ReadAsStringAsync();
		usersResponse.Should().NotBeNull();
	}
}
