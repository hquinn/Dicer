using FluentAssertions;
using Xunit;

namespace Dicer.Tests.Nodes;

public class NumberNodeTests
{
	public class EvaluateTests
	{
		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		public void ShouldReturnPassedInNumber(double number)
		{
			// Arrange
			var sut = new NumberNode(number);

			// Act
			var evaluation = sut.Evaluate(Roller.Max, RoundingStrategy.RoundToFloor);

			// Assert
			evaluation.Result.Should().Be(number);
		}
	}
}