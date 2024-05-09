using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace IntegrationTestingExamples.Tests.MockedPetStoreApi;

public class MockedPetStoreApiServer : IDisposable
{
	private WireMockServer _server;

	public string Url => _server.Url!;

	public void Start()
	{
		_server = WireMockServer.Start();
	}

	// https://jsonplaceholder.typicode.com/users
	// Mocks the call to the API with JSON response
	public void SetupUsers()
	{
		_server.Given(Request.Create()
			.WithPath("/users")
			.UsingGet())
			.RespondWith(Response.Create()
				.WithBodyFromFile("response/users_normal.json")
				.WithHeader("content-type", "application/json; charset=utf-8")
				.WithStatusCode(200)
			);
	}

	public void Dispose()
	{
		_server.Stop();
		_server.Dispose();
	}
}
