using System;
using Dicer.Parser.Exceptions;
using FluentAssertions;
using Xunit;
using static Dicer.Parser.Parser;

namespace Dicer.Parser.Tests
{
	public class ParserTests
	{
		[Theory]
		[InlineData("1", "1")]
		[InlineData("2", "2")]
		[InlineData("1+2", "ADD(1,2)")]
		[InlineData("1 + 2", "ADD(1,2)")]
		[InlineData("1 + 2 + 3 + 4 + 6 + 2 + 10", "ADD(ADD(ADD(ADD(ADD(ADD(1,2),3),4),6),2),10)")]
		[InlineData("1 - 2", "SUBTRACT(1,2)")]
		[InlineData("1 - 2 - 3 - 4 - 6 - 2 - 10", "SUBTRACT(SUBTRACT(SUBTRACT(SUBTRACT(SUBTRACT(SUBTRACT(1,2),3),4),6),2),10)")]
		[InlineData("1 + 2 - 3", "SUBTRACT(ADD(1,2),3)")]
		[InlineData("1 * 2 * 3", "MULTIPLY(MULTIPLY(1,2),3)")]
		[InlineData("1 * 2 * 3 * 4 * 6 * 2 * 10", "MULTIPLY(MULTIPLY(MULTIPLY(MULTIPLY(MULTIPLY(MULTIPLY(1,2),3),4),6),2),10)")]
		[InlineData("1 * 2 + 3", "ADD(MULTIPLY(1,2),3)")]
		[InlineData("1 + 2 * 3", "ADD(1,MULTIPLY(2,3))")]
		[InlineData("1 * 2 + 3 * 4", "ADD(MULTIPLY(1,2),MULTIPLY(3,4))")]
		[InlineData("1 / 2 / 3", "DIVIDE(DIVIDE(1,2),3)")]
		[InlineData("1 / 2 / 3 / 4 / 6 / 2 / 10", "DIVIDE(DIVIDE(DIVIDE(DIVIDE(DIVIDE(DIVIDE(1,2),3),4),6),2),10)")]
		[InlineData("1 / 2 + 3", "ADD(DIVIDE(1,2),3)")]
		[InlineData("1 + 2 / 3", "ADD(1,DIVIDE(2,3))")]
		[InlineData("1 / 2 + 3 / 4", "ADD(DIVIDE(1,2),DIVIDE(3,4))")]
		[InlineData("1d6", "DICE(1,6)")]
		[InlineData("1D6", "DICE(1,6)")]
		[InlineData("4D6K3", "DICE(4,6,3)")]
		[InlineData("4D6K3+3", "ADD(DICE(4,6,3),3)")]
		[InlineData("(1+2) * 3", "MULTIPLY(ADD(1,2),3)")]
		[InlineData("(1+(1 + 1)) * 3", "MULTIPLY(ADD(1,ADD(1,1)),3)")]
		[InlineData("-1", "UNARY(1)")]
		[InlineData("-1+2", "ADD(UNARY(1),2)")]
		[InlineData("-(1+2)", "UNARY(ADD(1,2))")]
		[InlineData("+-(1+2)", "UNARY(ADD(1,2))")]
		[InlineData("++(1+2)", "ADD(1,2)")]
		public void ShouldConstructNodesFromValidInputString(string input, string expected)
		{
			// Act
			var actual = Parse(input);

			// Assert
			actual.ToString().Should().Be(expected);
		}

		[Theory]
		[InlineData("4K3")]
		[InlineData("4*(3+1")]
		[InlineData("4**3")]
		public void ShouldThrowExceptionFromInvalidInputString(string input)
		{
			Action action = () => Parse(input);

			action.Should().Throw<ParsingException>();
		}
	}
}