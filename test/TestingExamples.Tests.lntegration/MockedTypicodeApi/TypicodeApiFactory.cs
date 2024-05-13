using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Users.Api;

namespace TestingExamples.Tests.lntegration.MockedTypicodeApi;

public class TypicodeApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
	private readonly MockedTypicodeApiServer _typicodeApiServer = new();

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureLogging(logging => { logging.ClearProviders(); });

		builder.ConfigureTestServices(services =>
		{
			services.RemoveAll(typeof(IHostedService));

			services.AddHttpClient("TypicodeApi", httpClient =>
			{
				httpClient.BaseAddress = new Uri(_typicodeApiServer.Url);
				httpClient.DefaultRequestHeaders.Add(
					HeaderNames.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
				httpClient.DefaultRequestHeaders.Add(
					HeaderNames.UserAgent, $"IntegrationTests-{Environment.MachineName}");
			});
		});
	}

	public async Task InitializeAsync()
	{
		_typicodeApiServer.Start();
		_typicodeApiServer.SetupUsers();
	}

	public new async Task DisposeAsync()
	{
		_typicodeApiServer.Dispose();
	}
}
