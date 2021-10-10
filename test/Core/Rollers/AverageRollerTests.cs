using Dicer.Models;
using Dicer.Rollers;
using Dicer.Tests.Factories;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Dicer.Tests.Rollers;

public class AverageRollerTests
{
	public class RollTests
	{
		[Fact]
		public void ShouldReturnListOfAverageRolls()
		{
			// Arrange
			var numDice = NodeResponseFactory.CreateSimpleResponse(3);
			var dieSize = NodeResponseFactory.CreateSimpleResponse(6);
			NodeResponse? keep = null;
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var sut = new AverageRoller();
			var expected = new RollResponse(12, Enumerable.Repeat(new Roll(4, 6), 3));

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
			var sut = new AverageRoller();
			var expected = new RollResponse(12, Enumerable.Repeat(new Roll(4, 6), 3));

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
			var sut = new AverageRoller();
			var expected = new RollResponse(12, Enumerable.Repeat(new Roll(4, 6), 3));

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
			var sut = new AverageRoller();
			var expected = new RollResponse(12, Enumerable.Repeat(new Roll(4, 6), 3));

			// Act
			var result = sut.Roll(numDice, dieSize, keep, roundingStrategy);

			// Assert
			result.Should().BeEquivalentTo(expected);
		}

		[Theory]
		[InlineData(-4, -6, 3, 4, -6, 3, 12)]
		[InlineData(4, -6, 3, -4, -6, 3, -12)]
		[InlineData(-4, 6, 3, -4, 6, 3, -12)]
		public void ShouldHandleResultIfNegative(int numDiceParam, int dieSizeParam, int keepParam, int numDiceResult, int dieSizeResult, int keepResult, int result)
		{
			// Arrange
			var numDice = NodeResponseFactory.CreateSimpleResponse(numDiceParam);
			var dieSize = NodeResponseFactory.CreateSimpleResponse(dieSizeParam);
			var keep = NodeResponseFactory.CreateSimpleResponse(keepParam);
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var sut = new AverageRoller();
			var expected = new RollResponse(result, Enumerable.Repeat(new Roll(numDiceResult, dieSizeResult), keepResult));

			// Act
			var actual = sut.Roll(numDice, dieSize, keep, roundingStrategy);

			// Assert
			actual.Should().BeEquivalentTo(expected);
		}
	}
}