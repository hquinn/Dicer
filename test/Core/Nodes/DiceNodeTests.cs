using Dicer.Models;
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
			var roller = RollerFactory.CreateRoller(numDice, dieSize);
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var sut = NodeFactory.CreateDiceNode(numDice, dieSize);

			// Act
			var evaluation = sut.Evaluate(roller, roundingStrategy);

			// Assert
			evaluation.Result.Should().Be(expected);
		}

		[Fact]
		public void ShouldReturnRollsAlongWithOtherNodes()
		{
			// Arrange
			const int numDice = 3;
			const int dieSize = 6;
			var roller = RollerFactory.CreateRoller(numDice, dieSize);
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var sut = NodeFactory.CreateDiceNodeWithRolls(numDice, dieSize);

			var expected = new[]
			{
				new RollResponse(3, new[] { new Roll(3, 6) }),
				new RollResponse(numDice * dieSize, Enumerable.Repeat<Roll>(new(dieSize, dieSize), numDice)),
				new RollResponse(6, new[] { new Roll(6, 6) })
			};

			// Act
			var evaluation = sut.Evaluate(roller, roundingStrategy);

			// Assert
			evaluation.RollResponses.Should().BeEquivalentTo(expected);
		}
	}
}