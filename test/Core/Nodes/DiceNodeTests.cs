using Dicer.Tests.Factories;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Dicer.Tests.Nodes;

public class DiceNodeTests
{
	public class EvaluateTests
	{
		[Theory]
		[InlineData(3, 6, 18)]
		[InlineData(4, 6, 24)]
		public void ShouldReturnRollResultOfPassedInNodes(int numDice, int dieSize, int expected)
		{
			// Arrange
			var sut = NodeFactory.CreateDiceNode(numDice, dieSize);

			// Act
			var evaluation = sut.Evaluate(Roller.Max, RoundingStrategy.RoundToFloor);

			// Assert
			evaluation.Result.Should().Be(expected);
		}

		[Fact]
		public void ShouldReturnRollsAlongWithOtherNodes()
		{
			// Arrange
			const int numDice = 3;
			const int dieSize = 6;
			var sut = NodeFactory.CreateDiceNodeWithRolls(numDice, dieSize);

			var expected = new[]
			{
				new RollResponse(3, new[] { new Roll(3, 6) }, Enumerable.Empty<Roll>()),
				new RollResponse(numDice * dieSize, Enumerable.Repeat<Roll>(new(dieSize, dieSize), numDice), Enumerable.Empty<Roll>()),
				new RollResponse(6, new[] { new Roll(6, 6) }, Enumerable.Empty<Roll>())
			};

			// Act
			var evaluation = sut.Evaluate(Roller.Max, RoundingStrategy.RoundToFloor);

			// Assert
			evaluation.RollResponses.Should().BeEquivalentTo(expected);
		}
	}
}