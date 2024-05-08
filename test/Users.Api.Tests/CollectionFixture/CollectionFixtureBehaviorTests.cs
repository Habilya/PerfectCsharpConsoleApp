using Users.Api.Tests.ClassFixture;
using Xunit.Abstractions;

namespace Users.Api.Tests.CollectionFixture;

// These 2 test classes are sharing the same fixture, which is good to separate test groups

[Collection("Collection fixture to test behavior")]
public class CollectionFixtureBehaviorTests
{
	private readonly ITestOutputHelper _testOutputHelper;
	private readonly GuidTestsFixture _fixture;

	public CollectionFixtureBehaviorTests(ITestOutputHelper testOutputHelper, GuidTestsFixture fixture)
	{
		_testOutputHelper = testOutputHelper;
		_fixture = fixture;
	}


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

[Collection("Collection fixture to test behavior")]
public class CollectionFixtureBehaviorTestsAgain
{
	private readonly ITestOutputHelper _testOutputHelper;
	private readonly GuidTestsFixture _fixture;

	public CollectionFixtureBehaviorTestsAgain(ITestOutputHelper testOutputHelper, GuidTestsFixture fixture)
	{
		_testOutputHelper = testOutputHelper;
		_fixture = fixture;
	}


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
