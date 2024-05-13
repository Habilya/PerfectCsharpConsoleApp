using Xunit.Abstractions;

namespace Users.Api.Tests.Unit.ClassFixture;

public class ClassFixtureBehaviorTests : IClassFixture<GuidTestsFixture>
{
	private readonly ITestOutputHelper _testOutputHelper;
	private readonly GuidTestsFixture _fixture;

	public ClassFixtureBehaviorTests(ITestOutputHelper testOutputHelper, GuidTestsFixture fixture)
	{
		// Constructor is called once per test (but fixture constructor and dispose is called only once)
		_testOutputHelper = testOutputHelper;
		_fixture = fixture;
	}


	// The Guid is different because we will have a new instance of DefaultBehaviorTests class PER TEST (ExampleTest1, ExampleTest2)
	[Fact]
	public void ExampleTest1()
	{
		_testOutputHelper.WriteLine($"The Guid was: {_fixture.Id}");
	}

	[Fact]
	public void ExampleTest2()
	{
		_testOutputHelper.WriteLine($"The Guid was: {_fixture.Id}");
	}
}
