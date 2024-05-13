using FluentAssertions;
using HelloWorldLibrary.BusinessLogic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace HelloWorld.Tests.Unit.BusinessLogic;

public class MessagesTests
{
	[Fact]
	public void Greeting_InEnglish()
	{
		ILogger<Messages> logger = new NullLogger<Messages>();
		Messages messages = new(logger);


		string expected = "Hello World";
		string actual = messages.Greeting("en");

		actual.Should().Be(expected);
	}

	[Fact]
	public void Greeting_InFrench()
	{
		ILogger<Messages> logger = new NullLogger<Messages>();
		Messages messages = new(logger);


		string expected = "Bonjour monde";
		string actual = messages.Greeting("fr");

		actual.Should().Be(expected);
	}

	[Fact]
	public void Greeting_Invalid()
	{
		ILogger<Messages> logger = new NullLogger<Messages>();
		Messages messages = new(logger);


		Action failingRun = () => { messages.Greeting("invalid language"); };

		failingRun.Should().Throw<InvalidOperationException>();
	}
}
