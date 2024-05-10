using Users.Api.Models.Typicode;

namespace Users.Api.Services;

public class TypicodeService : ITypicodeService
{
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly HttpClient _httpClient;

	public TypicodeService(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
		_httpClient = _httpClientFactory.CreateClient("TypicodeApi");
	}

	public async Task<IEnumerable<User>> GetAllUsersAsync()
	{
		var response = await _httpClient.GetAsync("/users");
		var responseBody = await response.Content.ReadFromJsonAsync<IEnumerable<User>>();
		return responseBody ?? new List<User>();
	}
}
