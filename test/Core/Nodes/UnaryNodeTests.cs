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
		var sut = NodeFactory.CreateUnaryNode(node);

		// Act
		var evaluation = sut.Evaluate(Roller.Max, RoundingStrategy.RoundToFloor);

		// Assert
		evaluation.Result.Should().Be(expected);
	}

	[Fact]
	public void ShouldReturnRollsAlongWithOtherNodes()
	{
		// Arrange
		var sut = NodeFactory.CreateUnaryNode(1);

		var expected = new[]
		{
			new RollResponse(1, new[] { new Roll(1, 1) })
		};

		// Act
		var evaluation = sut.Evaluate(Roller.Max, RoundingStrategy.RoundToFloor);

		// Assert
		evaluation.RollResponses.Should().BeEquivalentTo(expected);
	}

	[Fact]
	public void ShouldRoundNodesBasedOnRoundingStrategy()
	{
		// Arrange
		var sut = NodeFactory.CreateUnaryNode(3.7);
		var expected = -4;

		// Act
		var evaluation = sut.Evaluate(Roller.Max, RoundingStrategy.RoundToCeiling);

		// Assert
		evaluation.Result.Should().Be(expected);
	}
}