using FluentAssertions;
using System.Reflection;

namespace Users.Api.Tests.PrivateMemberTests;

public class SomeClassInTheProject
{
	private int GetPDFMergeOrderDepar(string fileName)
	{
		if (fileName.EndsWith("LTR.rtf", StringComparison.InvariantCultureIgnoreCase))
			return 1;

		if (fileName.EndsWith("F11.rtf", StringComparison.InvariantCultureIgnoreCase))
			return 2;

		if (fileName.EndsWith("STM.rof", StringComparison.InvariantCultureIgnoreCase))
			return 3;

		if (fileName.EndsWith("F08.rtf", StringComparison.InvariantCultureIgnoreCase))
			return 4;

		if (fileName.EndsWith("F54.rtf", StringComparison.InvariantCultureIgnoreCase))
			return 5;

		return -1;
	}
}


public class PrivateMemberTests
{
	[Theory]
	[InlineData("1", "B0084DET.txt", -1)]
	[InlineData("2", "B0084F12.rtf", -1)]
	[InlineData("3", "B0084F50a.LNK", -1)]
	[InlineData("4", "B0084F50b.LNK", -1)]
	[InlineData("5", "B0084F50c.LNK", -1)]
	[InlineData("6", "B0084F50d.LNK", -1)]
	[InlineData("7", "B0084IMA.txt", -1)]
	[InlineData("8", "B0084STM.pdf", -1)]
	[InlineData("9", "B0084F04.rtf", -1)]
	[InlineData("10", "B0084F02.LNK", -1)]
	[InlineData("11", "B0084F51.rtf", -1)]

	[InlineData("12", "B0084LTR.rtf", 1)]
	[InlineData("13", "B0084F11.rtf", 2)]
	[InlineData("14", "B0084STM.rof", 3)]
	[InlineData("15", "B0084F08.rtf", 4)]
	[InlineData("16", "B0084F54.rtf", 5)]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters", Justification = "UnitTests with testId")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "UnitTests with testId")]
	public void GetPDFMergeOrderDepar_ShouldMatch_ExpectedFilenameOrder(string id, string input, int expected)
	{
		// Arrange
		// probably will be instantiated as part of the _sut (system under test)
		var classInstance = new SomeClassInTheProject();

		// Private Method info obtained using REFLEXION
		MethodInfo privateMethodGetPDFMergeOrderDepar = typeof(Users.Api.Tests.PrivateMemberTests.SomeClassInTheProject)
			.GetMethod("GetPDFMergeOrderDepar", BindingFlags.NonPublic | BindingFlags.Instance);

		object[] methodParameters = new object[1] { input };


		// Act
		var actual = privateMethodGetPDFMergeOrderDepar.Invoke(classInstance, methodParameters);

		// Assert
		actual.Should().Be(expected);
	}
}
