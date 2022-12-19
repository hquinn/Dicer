using Dicer.Tests.Factories;
using FluentAssertions;
using Dicer.Tests.Helpers;
using Xunit;

namespace Dicer.Tests.Rollers;

public class RandomRollerTests
{
	public class RollTests
	{
		private const int DefaultKeep = 3;
		private const int DieSize = 6;

		[Fact]
		public void ShouldReturnListOfRandomRolls()
		{
			RollerTests.ShouldReturnListOfRolls(CreateSut(2), 2);
		}

		[Fact]
		public void ShouldReturnLessRollsBasedOnKeep()
		{
			// Arrange
			var (numDice, dieSize, keep, minimum) = RollerTests.CreateResponses(4, DieSize, DefaultKeep, null);

			var sut = CreateSut(1, 1, 1, 2);
			var expected = RollResponse.CreateResponse(
				DieSize.CreateRolls(1, 1, 2),
				DieSize.CreateRolls(1));

			// Act
			var result = sut.Roll(numDice, dieSize, keep, minimum, RoundingStrategy.RoundToFloor.Create());

			// Assert
			result.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public void ShouldReturnSameAsNumDiceOfKeepIsMore()
		{
			RollerTests.ShouldReturnSameAsNumDiceIfKeepIsMore(CreateSut(1), 1);
		}

		[Fact]
		public void ShouldReturnLowestKeepValuesIfKeepIsNegative()
		{
			// Arrange
			var (numDice, dieSize, keep, minimum) = RollerTests.CreateResponses(4, DieSize, -DefaultKeep, null);

			var sut = CreateSut(1, 1, 1, 2);
			var expected = RollResponse.CreateResponse(
				DieSize.CreateRolls(1, 1, 1),
				DieSize.CreateRolls(2));

			// Act
			var result = sut.Roll(numDice, dieSize, keep, minimum, RoundingStrategy.RoundToFloor.Create());

			// Assert
			result.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public void ShouldReturnSameAsNumDiceIfKeepIsMoreAndKeepIsNegative()
		{
			RollerTests.ShouldReturnSameAsNumDiceIfKeepIsMoreAndKeepIsNegative(CreateSut(1), 1);
		}

		[Theory]
		[InlineData(-4, -DieSize, DefaultKeep, 4, -DieSize, DefaultKeep, 2)]
		[InlineData(4, -DieSize, DefaultKeep, -4, -DieSize, DefaultKeep, -2)]
		[InlineData(-4, DieSize, DefaultKeep, -4, DieSize, DefaultKeep, -2)]
		public void ShouldHandleResultIfNegative(
			int numDiceParam,
			int dieSizeParam,
			int keepParam,
			int numDiceResult,
			int dieSizeResult,
			int keepResult,
			int rollResult)
		{
			RollerTests.ShouldHandleResultIfNegative(
				CreateSut(2), 
				numDiceParam, 
				dieSizeParam, 
				keepParam, 
				numDiceResult, 
				dieSizeResult , 
				keepResult, 
				rollResult);
		}

		[Fact]
		public void ShouldReturnListOfParameterMinRolls()
		{
			RollerTests.ShouldReturnListOfParameterMinRolls(CreateSut(2), 3);
		}

		[Fact]
		public void ShouldNotReturnListOfParameterMinRollsWhenOtherRollsAreHigher()
		{
			RollerTests.ShouldNotReturnListOfParameterMinRollsWhenOtherRollsAreHigher(CreateSut(3), 2, 3);
		}

		[Fact]
		public void ShouldNotReturnListOfParameterMinRollsWhenMinIsGreaterThanDieSize()
		{
			RollerTests.ShouldNotReturnListOfParameterMinRollsWhenMinIsGreaterThanDieSize(CreateSut(2), 2);
		}

		private static RandomRoller CreateSut(params int[] rolls)
		{
			var random = RandomFactory.CreateRandom(rolls);
			var sut = new RandomRoller(random);
			return sut;
		}
	}
}