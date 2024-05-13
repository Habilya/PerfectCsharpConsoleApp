namespace Users.Api.Tests.Unit.DateTimeTestsp;

public class DateTimeProvider : IDateTimeProvider
{
	public DateTime DateTimeNow => DateTime.Now;
}

public interface IDateTimeProvider
{
	public DateTime DateTimeNow { get; }
}
