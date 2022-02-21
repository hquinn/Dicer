using Dicer.Models;
using Dicer.Tests.Factories;
using FluentAssertions;
using Xunit;

namespace Dicer.Tests.Nodes;

public class DivideNodeTests
{
	public class EvaluateTests
	{
		[Theory]
		[InlineData(1, 1, 1)]
		[InlineData(1, 2, 1)]
		public void ShouldReturnDivisionOfPassedInNodes(double first, double second, double expected)
		{
			// Arrange
			var roller = RollerFactory.CreateEmptyRoller();
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var sut = NodeFactory.CreateDivideNode(first, second);

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
			var sut = NodeFactory.CreateDivideNode(1, 2);

			var expected = new[]
			{
				new RollResponse(1, new[] { new Roll(1, 1) }),
				new RollResponse(2, new[] { new Roll(2, 2) })
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
			var sut = NodeFactory.CreateDivideNode(3.33, 2);
			var expected = 2;

			// Act
			var evaluation = sut.Evaluate(roller, roundingStrategy);

			// Assert
			evaluation.Result.Should().Be(expected);
		}
	}
}