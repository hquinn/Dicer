using Dicer.Models;
using Dicer.Rollers;
using Dicer.Tests.Factories;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Dicer.Tests.Rollers;

public class RandomRollerTests
{
	public class RollTests
	{
		[Fact]
		public void ShouldReturnListOfRandomRolls()
		{
			// Arrange
			var numDice = NodeResponseFactory.CreateSimpleResponse(3);
			var dieSize = NodeResponseFactory.CreateSimpleResponse(6);
			NodeResponse? keep = null;
			NodeResponse? minimum = null;
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var random = RandomFactory.CreateRandom(2);
			var sut = new RandomRoller(random);
			var expected = new RollResponse(6, Enumerable.Repeat(new Roll(2, 6), 3));

			// Act
			var result = sut.Roll(numDice, dieSize, keep, minimum, roundingStrategy);

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
			NodeResponse? minimum = null;
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var random = RandomFactory.CreateRandom(1, 1, 1, 2);
			var sut = new RandomRoller(random);
			var expected = RollResponseFactory.CreateResponse(4, 6, 1, 1, 2);

			// Act
			var result = sut.Roll(numDice, dieSize, keep, minimum, roundingStrategy);

			// Assert
			result.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public void ShouldReturnSameAsNumDiceofKeepIsMore()
		{
			// Arrange
			var numDice = NodeResponseFactory.CreateSimpleResponse(3);
			var dieSize = NodeResponseFactory.CreateSimpleResponse(6);
			var keep = NodeResponseFactory.CreateSimpleResponse(4);
			NodeResponse? minimum = null;
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var random = RandomFactory.CreateRandom(1, 1, 1);
			var sut = new RandomRoller(random);
			var expected = RollResponseFactory.CreateResponse(3, 6, 1, 1, 1);

			// Act
			var result = sut.Roll(numDice, dieSize, keep, minimum, roundingStrategy);

			// Assert
			result.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public void ShouldReturnLowestKeepValuesIfKeepIsNegative()
		{
			// Arrange
			var numDice = NodeResponseFactory.CreateSimpleResponse(4);
			var dieSize = NodeResponseFactory.CreateSimpleResponse(6);
			var keep = NodeResponseFactory.CreateSimpleResponse(-3);
			NodeResponse? minimum = null;
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var random = RandomFactory.CreateRandom(1, 1, 1, 2);
			var sut = new RandomRoller(random);
			var expected = new RollResponse(3, Enumerable.Repeat(new Roll(1, 6), 3));

			// Act
			var result = sut.Roll(numDice, dieSize, keep, minimum, roundingStrategy);

			// Assert
			result.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public void ShouldReturnSameAsNumDiceofKeepIsMoreAndKeepIsNegative()
		{
			// Arrange
			var numDice = NodeResponseFactory.CreateSimpleResponse(3);
			var dieSize = NodeResponseFactory.CreateSimpleResponse(6);
			var keep = NodeResponseFactory.CreateSimpleResponse(-4);
			NodeResponse? minimum = null;
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var random = RandomFactory.CreateRandom(1, 1, 1);
			var sut = new RandomRoller(random);
			var expected = new RollResponse(3, Enumerable.Repeat(new Roll(1, 6), 3));

			// Act
			var result = sut.Roll(numDice, dieSize, keep, minimum, roundingStrategy);

			// Assert
			result.Should().BeEquivalentTo(expected);
		}

		[Theory]
		[InlineData(-4, -6, 3, 2, -6, 3, 6)]
		[InlineData(4, -6, 3, -2, -6, 3, -6)]
		[InlineData(-4, 6, 3, -2, 6, 3, -6)]
		public void ShouldHandleResultIfNegative(
			int numDiceParam,
			int dieSizeParam,
			int keepParam,
			int numDiceResult,
			int dieSizeResult,
			int keepResult,
			int result)
		{
			// Arrange
			var numDice = NodeResponseFactory.CreateSimpleResponse(numDiceParam);
			var dieSize = NodeResponseFactory.CreateSimpleResponse(dieSizeParam);
			var keep = NodeResponseFactory.CreateSimpleResponse(keepParam);
			NodeResponse? minimum = null;
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var random = RandomFactory.CreateRandom();
			var sut = new RandomRoller(random);

			var expected = new RollResponse(result,
				Enumerable.Repeat(new Roll(numDiceResult, dieSizeResult), keepResult));

			// Act
			var actual = sut.Roll(numDice, dieSize, keep, minimum, roundingStrategy);

			// Assert
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public void ShouldReturnListOfParameterMinRolls()
		{
			// Arrange
			var numDice = NodeResponseFactory.CreateSimpleResponse(3);
			var dieSize = NodeResponseFactory.CreateSimpleResponse(6);
			NodeResponse? keep = null;
			var minimum = NodeResponseFactory.CreateSimpleResponse(3);
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var random = RandomFactory.CreateRandom(2);
			var sut = new RandomRoller(random);
			var expected = new RollResponse(9, Enumerable.Repeat(new Roll(3, 6), 3));

			// Act
			var result = sut.Roll(numDice, dieSize, keep, minimum, roundingStrategy);

			// Assert
			result.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public void ShouldNotReturnListOfParameterMinRollsWhenOtherRollsAreHigher()
		{
			// Arrange
			var numDice = NodeResponseFactory.CreateSimpleResponse(3);
			var dieSize = NodeResponseFactory.CreateSimpleResponse(6);
			NodeResponse? keep = null;
			var minimum = NodeResponseFactory.CreateSimpleResponse(1);
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var random = RandomFactory.CreateRandom(2);
			var sut = new RandomRoller(random);
			var expected = new RollResponse(6, Enumerable.Repeat(new Roll(2, 6), 3));

			// Act
			var result = sut.Roll(numDice, dieSize, keep, minimum, roundingStrategy);

			// Assert
			result.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public void ShouldNotReturnListOfParameterMinRollsWhenMinIsGreaterThanDieSize()
		{
			// Arrange
			var numDice = NodeResponseFactory.CreateSimpleResponse(3);
			var dieSize = NodeResponseFactory.CreateSimpleResponse(6);
			NodeResponse? keep = null;
			var minimum = NodeResponseFactory.CreateSimpleResponse(7);
			var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
			var random = RandomFactory.CreateRandom(2);
			var sut = new RandomRoller(random);
			var expected = new RollResponse(6, Enumerable.Repeat(new Roll(2, 6), 3));

			// Act
			var result = sut.Roll(numDice, dieSize, keep, minimum, roundingStrategy);

			// Assert
			result.Should().BeEquivalentTo(expected);
		}
	}
}