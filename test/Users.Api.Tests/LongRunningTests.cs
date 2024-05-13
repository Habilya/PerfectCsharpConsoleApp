namespace Users.Api.Tests.Unit.LongRunningTests;

public class LongRunningTests
{
	// Timeouts normally are not used in Unit testing.
	// Usually it is more common practice in Integration Testing
	// But in some rare cases it might be useful in unit testing aswell
	[Fact(Timeout = 2000, Skip = "This test normally fails by timeout, therefore skipped and kept as a reference.")]
	public async Task SlowTest()
	{
		// Really Slow test (shouldn't take that long)
		await Task.Delay(10000);
	}
}
