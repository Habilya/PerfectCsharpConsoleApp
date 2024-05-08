namespace Users.Api.Tests.LongRunningTests;

public class LongRunningTests
{
	[Fact(Timeout = 2000)]
	public async Task SlowTest()
	{
		// Really Slow test (shouldn't take that long)
		await Task.Delay(10000);
	}
}
