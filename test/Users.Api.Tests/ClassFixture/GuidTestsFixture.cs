namespace Users.Api.Tests.ClassFixture;

public class GuidTestsFixture : IDisposable
{
	public Guid Id { get; } = Guid.NewGuid();

	public GuidTestsFixture()
	{
		// The constructor is also shared between tests
		// Ususally setup code goes here
	}

	public void Dispose()
	{
	}
}
