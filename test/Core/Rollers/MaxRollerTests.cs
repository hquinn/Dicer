using Dicer.Models;
using Dicer.Rollers;
using Dicer.Tests.Factories;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Dicer.Tests.Rollers;

public class MaxRollerTests
{
	public class RollTests
	{
		[Fact]
		public void ShouldReturnListOfMaximumRolls()
		{
			// Arrange
			var numDice = NodeResponseFactory.CreateSimpleResponse(3);
			var dieSize = NodeResponseFactory.CreateSimpleResponse(6);
			NodeResponse? keep = null;
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var sut = new MaxRoller();
			var expected = new RollResponse(18, Enumerable.Repeat(new Roll(6, 6), 3));

			// Act
			var result = sut.Roll(numDice, dieSize, keep, roundingStrategy);

			// Assert
			result.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public void ShouldReturnLessRollsBasedOnKeep()
		{
			// Arrange
			var numDice = NodeResponseFactory.CreateSimpleResponse(4);
			var dieSize = NodeResponseFactory.CreateSimpleResponse(6);
			var keep = NodeResponseFactory.CreateSimpleResponse(3);
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var sut = new MaxRoller();
			var expected = new RollResponse(18, Enumerable.Repeat(new Roll(6, 6), 3));

			// Act
			var result = sut.Roll(numDice, dieSize, keep, roundingStrategy);

			// Assert
			result.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public void ShouldReturnSameAsNumDiceIfKeepIsMore()
		{
			// Arrange
			var numDice = NodeResponseFactory.CreateSimpleResponse(3);
			var dieSize = NodeResponseFactory.CreateSimpleResponse(6);
			var keep = NodeResponseFactory.CreateSimpleResponse(4);
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var sut = new MaxRoller();
			var expected = new RollResponse(18, Enumerable.Repeat(new Roll(6, 6), 3));

			// Act
			var result = sut.Roll(numDice, dieSize, keep, roundingStrategy);

			// Assert
			result.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public void ShouldReturnAbsoluteValueOfKeep()
		{
			// Arrange
			var numDice = NodeResponseFactory.CreateSimpleResponse(4);
			var dieSize = NodeResponseFactory.CreateSimpleResponse(6);
			var keep = NodeResponseFactory.CreateSimpleResponse(-3);
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var sut = new MaxRoller();
			var expected = new RollResponse(18, Enumerable.Repeat(new Roll(6, 6), 3));

			// Act
			var result = sut.Roll(numDice, dieSize, keep, roundingStrategy);

			// Assert
			result.Should().BeEquivalentTo(expected);
		}

		[Theory]
		[InlineData(-4, -6, 3, 6, -6, 3, 18)]
		[InlineData(4, -6, 3, -6, -6, 3, -18)]
		[InlineData(-4, 6, 3, -6, 6, 3, -18)]
		public void ShouldHandleResultIfNegative(int numDiceParam, int dieSizeParam, int keepParam, int numDiceResult, int dieSizeResult, int keepResult, int result)
		{
			// Arrange
			var numDice = NodeResponseFactory.CreateSimpleResponse(numDiceParam);
			var dieSize = NodeResponseFactory.CreateSimpleResponse(dieSizeParam);
			var keep = NodeResponseFactory.CreateSimpleResponse(keepParam);
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var sut = new MaxRoller();
			var expected = new RollResponse(result, Enumerable.Repeat(new Roll(numDiceResult, dieSizeResult), keepResult));

			// Act
			var actual = sut.Roll(numDice, dieSize, keep, roundingStrategy);

			// Assert
			actual.Should().BeEquivalentTo(expected);
		}
	}
}