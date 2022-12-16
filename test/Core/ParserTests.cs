using FluentAssertions;
using System;
using Xunit;
using static Dicer.Parser;

namespace Dicer.Tests;

public class ParserTests
{
	[Theory]
	[InlineData("1", "1")]
	[InlineData("2", "2")]
	[InlineData("1.5", "1.5")]
	[InlineData("1+2", "ADD(1,2)")]
	[InlineData("1 + 2", "ADD(1,2)")]
	[InlineData("1 + 2 + 3 + 4 + 6 + 2 + 10", "ADD(ADD(ADD(ADD(ADD(ADD(1,2),3),4),6),2),10)")]
	[InlineData("1 - 2", "SUBTRACT(1,2)")]
	[InlineData("1 - 2 - 3 - 4 - 6 - 2 - 10",
		"SUBTRACT(SUBTRACT(SUBTRACT(SUBTRACT(SUBTRACT(SUBTRACT(1,2),3),4),6),2),10)")]
	[InlineData("1 + 2 - 3", "SUBTRACT(ADD(1,2),3)")]
	[InlineData("1 * 2 * 3", "MULTIPLY(MULTIPLY(1,2),3)")]
	[InlineData("1 * 2 * 3 * 4 * 6 * 2 * 10",
		"MULTIPLY(MULTIPLY(MULTIPLY(MULTIPLY(MULTIPLY(MULTIPLY(1,2),3),4),6),2),10)")]
	[InlineData("1 * 2 + 3", "ADD(MULTIPLY(1,2),3)")]
	[InlineData("1 + 2 * 3", "ADD(1,MULTIPLY(2,3))")]
	[InlineData("1 * 2 + 3 * 4", "ADD(MULTIPLY(1,2),MULTIPLY(3,4))")]
	[InlineData("1 / 2 / 3", "DIVIDE(DIVIDE(1,2),3)")]
	[InlineData("1 / 2 / 3 / 4 / 6 / 2 / 10", "DIVIDE(DIVIDE(DIVIDE(DIVIDE(DIVIDE(DIVIDE(1,2),3),4),6),2),10)")]
	[InlineData("1 / 2 + 3", "ADD(DIVIDE(1,2),3)")]
	[InlineData("1 + 2 / 3", "ADD(1,DIVIDE(2,3))")]
	[InlineData("1 / 2 + 3 / 4", "ADD(DIVIDE(1,2),DIVIDE(3,4))")]
	[InlineData("1d6", "DICE(1,6,,)")]
	[InlineData("1D6", "DICE(1,6,,)")]
	[InlineData("4D6K3", "DICE(4,6,3,)")]
	[InlineData("4D6K3+3", "ADD(DICE(4,6,3,),3)")]
	[InlineData("(1+2) * 3", "MULTIPLY(ADD(1,2),3)")]
	[InlineData("(1+(1 + 1)) * 3", "MULTIPLY(ADD(1,ADD(1,1)),3)")]
	[InlineData("-1", "UNARY(1)")]
	[InlineData("-1+2", "ADD(UNARY(1),2)")]
	[InlineData("-(1+2)", "UNARY(ADD(1,2))")]
	[InlineData("+-(1+2)", "UNARY(ADD(1,2))")]
	[InlineData("++(1+2)", "ADD(1,2)")]
	[InlineData("4D6K-3+3", "ADD(DICE(4,6,UNARY(3),),3)")]
	[InlineData("--1", "UNARY(UNARY(1))")]
	[InlineData("1D6M3", "DICE(1,6,,3)")]
	[InlineData("1D6K3M4", "DICE(1,6,3,4)")]
	[InlineData("1D6M4K3", "DICE(1,6,3,4)")]
	public void ShouldConstructNodesFromValidInputString(string input, string expected)
	{
		// Act
		var actual = Parse(input);

		// Assert
		actual.ToString().Should().Be(expected);
	}

	[Theory]
	[InlineData("1", "1", "REPEAT(1,1)")]
	[InlineData("2", "2", "REPEAT(2,2)")]
	[InlineData("1.5", "1", "REPEAT(1.5,1)")]
	[InlineData("1+2", "2", "REPEAT(ADD(1,2),2)")]
	[InlineData("1 + 2", "1+1", "REPEAT(ADD(1,2),ADD(1,1))")]
	[InlineData("1 + 2 + 3 + 4 + 6 + 2 + 10", "2", "REPEAT(ADD(ADD(ADD(ADD(ADD(ADD(1,2),3),4),6),2),10),2)")]
	[InlineData("1 - 2", "3*2", "REPEAT(SUBTRACT(1,2),MULTIPLY(3,2))")]
	[InlineData("1 - 2 - 3 - 4 - 6 - 2 - 10", "4d6k3",
		"REPEAT(SUBTRACT(SUBTRACT(SUBTRACT(SUBTRACT(SUBTRACT(SUBTRACT(1,2),3),4),6),2),10),DICE(4,6,3,))")]
	[InlineData("1 + 2 - 3", "2", "REPEAT(SUBTRACT(ADD(1,2),3),2)")]
	[InlineData("1 * 2 * 3", "2", "REPEAT(MULTIPLY(MULTIPLY(1,2),3),2)")]
	[InlineData("1 * 2 * 3 * 4 * 6 * 2 * 10", "2",
		"REPEAT(MULTIPLY(MULTIPLY(MULTIPLY(MULTIPLY(MULTIPLY(MULTIPLY(1,2),3),4),6),2),10),2)")]
	[InlineData("1 * 2 + 3", "2", "REPEAT(ADD(MULTIPLY(1,2),3),2)")]
	[InlineData("1 + 2 * 3", "2", "REPEAT(ADD(1,MULTIPLY(2,3)),2)")]
	[InlineData("1 * 2 + 3 * 4", "2", "REPEAT(ADD(MULTIPLY(1,2),MULTIPLY(3,4)),2)")]
	[InlineData("1 / 2 / 3", "2", "REPEAT(DIVIDE(DIVIDE(1,2),3),2)")]
	[InlineData("1 / 2 / 3 / 4 / 6 / 2 / 10", "2",
		"REPEAT(DIVIDE(DIVIDE(DIVIDE(DIVIDE(DIVIDE(DIVIDE(1,2),3),4),6),2),10),2)")]
	[InlineData("1 / 2 + 3", "2", "REPEAT(ADD(DIVIDE(1,2),3),2)")]
	[InlineData("1 + 2 / 3", "2", "REPEAT(ADD(1,DIVIDE(2,3)),2)")]
	[InlineData("1 / 2 + 3 / 4", "2", "REPEAT(ADD(DIVIDE(1,2),DIVIDE(3,4)),2)")]
	[InlineData("1d6", "2", "REPEAT(DICE(1,6,,),2)")]
	[InlineData("1D6", "2", "REPEAT(DICE(1,6,,),2)")]
	[InlineData("4D6K3", "2", "REPEAT(DICE(4,6,3,),2)")]
	[InlineData("4D6K3+3", "2", "REPEAT(ADD(DICE(4,6,3,),3),2)")]
	[InlineData("(1+2) * 3", "2", "REPEAT(MULTIPLY(ADD(1,2),3),2)")]
	[InlineData("(1+(1 + 1)) * 3", "2", "REPEAT(MULTIPLY(ADD(1,ADD(1,1)),3),2)")]
	[InlineData("-1", "2", "REPEAT(UNARY(1),2)")]
	[InlineData("-1+2", "2", "REPEAT(ADD(UNARY(1),2),2)")]
	[InlineData("-(1+2)", "2", "REPEAT(UNARY(ADD(1,2)),2)")]
	[InlineData("+-(1+2)", "2", "REPEAT(UNARY(ADD(1,2)),2)")]
	[InlineData("++(1+2)", "2", "REPEAT(ADD(1,2),2)")]
	[InlineData("4D6K-3+3", "2", "REPEAT(ADD(DICE(4,6,UNARY(3),),3),2)")]
	[InlineData("--1", "3", "REPEAT(UNARY(UNARY(1)),3)")]
	[InlineData("1D6K3M4", "2", "REPEAT(DICE(1,6,3,4),2)")]
	[InlineData("1D6M4K3", "2", "REPEAT(DICE(1,6,3,4),2)")]
	public void ShouldConstructRepeatingNodesFromValidInputString(
		string nodeInput,
		string repeatingInput,
		string expected)
	{
		// Act
		var actual = Parse(nodeInput, repeatingInput);

		// Assert
		actual.ToString().Should().Be(expected);
	}

	[Theory]
	[InlineData("4K3")]
	[InlineData("4M3")]
	[InlineData("4*(3+1")]
	[InlineData("4**3")]
	[InlineData("4*3+a")]
	[InlineData("1..5")]
	public void ShouldThrowExceptionFromInvalidInputString(string input)
	{
		Action action = () => Parse(input);

		action.Should().Throw<ParsingException>();
	}
}