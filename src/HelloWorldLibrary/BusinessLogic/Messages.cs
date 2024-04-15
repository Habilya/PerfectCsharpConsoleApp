using HelloWorldLibrary.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HelloWorldLibrary.BusinessLogic;

public class Messages : IMessages
{
	private const string TRANSLATIONS_JSON_FILENAME = "CustomText.json";
	private readonly ILogger<Messages> _log;

	public Messages(ILogger<Messages> log)
	{
		_log = log;
	}

	public string Greeting(string language)
	{
		return LookUpCustomText("Greeting", language);
	}

	private string LookUpCustomText(string key, string language)
	{
		JsonSerializerOptions options = new()
		{
			PropertyNameCaseInsensitive = true,
		};


		try
		{
			List<CustomText>? messageSets = JsonSerializer
			.Deserialize<List<CustomText>>
			(
				File.ReadAllText(TRANSLATIONS_JSON_FILENAME), options
			);

			CustomText? messages = messageSets?
				.Where(q => q.Language.Equals(language))
				.First();

			if (messages is null)
			{
				throw new NullReferenceException($"The specified language was not found in the {TRANSLATIONS_JSON_FILENAME}");
			}

			return messages.Translations[key];
		}
		catch (Exception ex)
		{
			_log.LogError("Unhandled exception", ex);
			throw;
		}
	}
}
