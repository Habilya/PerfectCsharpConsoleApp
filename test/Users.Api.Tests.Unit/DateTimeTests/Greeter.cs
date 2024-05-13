using Users.Api.Tests.Unit.DateTimeTestsp;

namespace Users.Api.Tests.Unit.DateTimeTests;

public class Greeter
{
	private readonly IDateTimeProvider _dateTimeProvider;

	public Greeter(IDateTimeProvider dateTimeProvider)
	{
		_dateTimeProvider = dateTimeProvider;
	}

	public string GenerateGreetMessage()
	{
		var dateTimeNow = _dateTimeProvider.DateTimeNow;
		return dateTimeNow.Hour switch
		{
			>= 5 and < 12 => "Good morning",
			>= 12 and < 18 => "Good afternoon",
			_ => "Good Evening"
		};
	}
}
