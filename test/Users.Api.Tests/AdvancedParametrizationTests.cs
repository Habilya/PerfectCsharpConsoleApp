using FluentAssertions;
using System.Collections;

namespace Users.Api.Tests.Unit.AdvancedParametrizationTests;

public class AdvancedParametrizationTests
{
	[Theory]
	[InlineData(1, 5, 10, 15)]
	[InlineData(2, 2, 1, 3)]
	[InlineData(3, 5, 0, 5)]
	[InlineData(4, -5, 2, -3)]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters", Justification = "UnitTests with testId")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "UnitTests with testId")]
	public void Add_ShouldAddTwoNumbers_WhenNumbersAreValid(int id, int firstNumber, int secondNumber, int expectedResult)
	{
		// Arrange


		// Act
		var result = firstNumber + secondNumber;

		// Assert
		expectedResult.Should().Be(expectedResult);
	}


	public static IEnumerable<object[]> AddTwoNumbers =>
		new List<object[]>
		{
			new object[] { 1, 5, 10, 15 },
			new object[] { 2, 2, 1, 3 },
			new object[] { 3, 5, 0, 5 },
			new object[] { 4, -5, 2, -3 }
		};

	[Theory]
	[MemberData(nameof(AddTwoNumbers))]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters", Justification = "UnitTests with testId")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "UnitTests with testId")]
	public void Add_ShouldAddTwoNumbers_UsingMemberData(int id, int firstNumber, int secondNumber, int expectedResult)
	{
		// Arrange


		// Act
		var result = firstNumber + secondNumber;

		// Assert
		expectedResult.Should().Be(expectedResult);
	}

	[Theory]
	[ClassData(typeof(CalculatorAddTestData))]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters", Justification = "UnitTests with testId")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "UnitTests with testId")]
	public void Add_ShouldAddTwoNumbers_UsingClassData(int id, int firstNumber, int secondNumber, int expectedResult)
	{
		// Arrange


		// Act
		var result = firstNumber + secondNumber;

		// Assert
		expectedResult.Should().Be(expectedResult);
	}
}

public class CalculatorAddTestData : IEnumerable<object[]>
{
	public IEnumerator<object[]> GetEnumerator()
	{
		yield return new object[] { 1, 5, 10, 15 };
		yield return new object[] { 2, 2, 1, 3 };
		yield return new object[] { 3, 5, 0, 5 };
		yield return new object[] { 4, -5, 2, -3 };
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
