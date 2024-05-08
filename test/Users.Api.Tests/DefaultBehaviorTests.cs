using Xunit.Abstractions;

namespace Users.Api.Tests.DefaultBehaviorTests;

public class DefaultBehaviorTests
{
	private readonly Guid _id = Guid.NewGuid();
	private readonly ITestOutputHelper _testOutputHelper;

	public DefaultBehaviorTests(ITestOutputHelper testOutputHelper)
	{
		_testOutputHelper = testOutputHelper;
	}


	// The Guid is different because we will have a new instance of DefaultBehaviorTests class PER TEST (ExampleTest1, ExampleTest2)
	[Fact]
	public void ExampleTest1()
	{
		_testOutputHelper.WriteLine($"The Guid was: {_id}");
	}

	[Fact]
	public void ExampleTest2()
	{
		_testOutputHelper.WriteLine($"The Guid was: {_id}");
	}
}
