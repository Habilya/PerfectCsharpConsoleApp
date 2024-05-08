namespace Users.Api.Tests.DateTimeTestsp;

public class DateTimeProvider : IDateTimeProvider
{
	public DateTime DateTimeNow => DateTime.Now;
}

public interface IDateTimeProvider
{
	public DateTime DateTimeNow { get; }
}
