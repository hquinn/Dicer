using Dicer.Tests.Factories;
using FluentAssertions;
using Xunit;

namespace Dicer.Tests.Nodes;

public class UnaryNodeTests
{
	[Theory]
	[InlineData(1, -1)]
	public void ShouldReturnNegationOfPassedInNode(double node, double expected)
	{
		// Arrange
		var roller = RollerFactory.CreateEmptyRoller();
		var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
		var sut = NodeFactory.CreateUnaryNode(node);

		// Act
		var evaluation = sut.Evaluate(roller, roundingStrategy);

		// Assert
		evaluation.Result.Should().Be(expected);
	}

	[Fact]
	public void ShouldReturnRollsAlongWithOtherNodes()
	{
		// Arrange
		var roller = RollerFactory.CreateEmptyRoller();
		var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
		var sut = NodeFactory.CreateUnaryNode(1);

		var expected = new[]
		{
			new RollResponse(1, new[] { new Roll(1, 1) })
		};

		// Act
		var evaluation = sut.Evaluate(roller, roundingStrategy);

		// Assert
		evaluation.RollResponses.Should().BeEquivalentTo(expected);
	}

	[Fact]
	public void ShouldRoundNodesBasedOnRoundingStrategy()
	{
		// Arrange
		var roller = RollerFactory.CreateEmptyRoller();
		var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
		var sut = NodeFactory.CreateUnaryNode(3.7);
		var expected = -4;

		// Act
		var evaluation = sut.Evaluate(roller, roundingStrategy);

		// Assert
		evaluation.Result.Should().Be(expected);
	}
}