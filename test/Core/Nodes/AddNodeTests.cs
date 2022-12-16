using Dicer.Tests.Factories;
using FluentAssertions;
using Xunit;

namespace Dicer.Tests.Nodes;

public class AddNodeTests
{
	public class EvaluateTests
	{
		[Theory]
		[InlineData(1, 1, 2)]
		[InlineData(1, 2, 3)]
		public void ShouldReturnAdditionOfPassedInNodes(double first, double second, double expected)
		{
			// Arrange
			var sut = NodeFactory.CreateAddNode(first, second);

			// Act
			var evaluation = sut.Evaluate(Roller.Max, RoundingStrategy.RoundToFloor);

			// Assert
			evaluation.Result.Should().Be(expected);
		}

		[Fact]
		public void ShouldReturnRollsAlongWithOtherNodes()
		{
			// Arrange
			var sut = NodeFactory.CreateAddNode(1, 2);

			var expected = new[]
			{
				new RollResponse(1, new[] { new Roll(1, 1) }),
				new RollResponse(2, new[] { new Roll(2, 2) })
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
			var sut = NodeFactory.CreateAddNode(3.7, 3.4);
			var expected = 8;

			// Act
			var evaluation = sut.Evaluate(Roller.Max, RoundingStrategy.RoundToCeiling);

			// Assert
			evaluation.Result.Should().Be(expected);
		}
	}
}