using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Users.Api;

namespace IntegrationTestingExamples.Tests.MockedPetStoreApi;

public class PetStoreApiFactory : WebApplicationFactory<IApiMarker>
{
	private readonly MockedPetStoreApiServer _petStoreApiServer = new();

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureLogging(logging => { logging.ClearProviders(); });

		builder.ConfigureTestServices(services =>
		{
			services.RemoveAll(typeof(IHostedService));

			services.AddHttpClient("PetstoreApi", httpClient =>
			{
				httpClient.BaseAddress = new Uri(_petStoreApiServer.Url);
				httpClient.DefaultRequestHeaders.Add(
					HeaderNames.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
				httpClient.DefaultRequestHeaders.Add(
					HeaderNames.UserAgent, $"IntegrationTests-{Environment.MachineName}");
			});
		});
	}

	public async Task InitializeAsync()
	{
		_petStoreApiServer.Start();
		_petStoreApiServer.SetupUsers();
	}

	public new async Task DisposeAsync()
	{
		_petStoreApiServer.Dispose();
	}
}
