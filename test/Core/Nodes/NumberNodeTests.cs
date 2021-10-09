using Dicer.Nodes;
using Dicer.Tests.Factories;
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
			var roller = RollerFactory.CreateEmptyRoller();
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var sut = new NumberNode(number);

			// Act
			var evaluation = sut.Evaluate(roller, roundingStrategy);

			// Assert
			evaluation.Result.Should().Be(number);
		}
	}
}