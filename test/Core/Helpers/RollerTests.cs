using System.Linq;
using Dicer.Tests.Factories;
using FluentAssertions;

namespace Dicer.Tests.Helpers;

public class RollerTests
{
	protected const int DefaultKeep = 3;
	protected const int DieSize = 6;

	internal static void ShouldReturnListOfRolls(BaseRoller sut, int roll)
	{
		// Arrange
		var (numDice, dieSize, keep, minimum) = CreateResponses(DefaultKeep, DieSize, null, null);

		var expected = RollResponse.CreateResponse(
			DieSize.CreateRepeatedRolls(roll, DefaultKeep),
			Enumerable.Empty<Roll>());

		// Act
		var result = sut.Roll(numDice, dieSize, keep, minimum, RoundingStrategy.RoundToCeiling.Create());

		// Assert
		result.Should().BeEquivalentTo(expected);
	}

	internal static void ShouldReturnLessRollsBasedOnKeep(BaseRoller sut, int roll)
	{
		// Arrange
		var (numDice, dieSize, keep, minimum) = CreateResponses(4, DieSize, DefaultKeep, null);

		var expected = RollResponse.CreateResponse(
			DieSize.CreateRepeatedRolls(roll, DefaultKeep),
			DieSize.CreateRolls(roll));

		// Act
		var result = sut.Roll(numDice, dieSize, keep, minimum, RoundingStrategy.RoundToCeiling.Create());

		// Assert
		result.Should().BeEquivalentTo(expected);
	}

	internal static void ShouldReturnSameAsNumDiceIfKeepIsMore(BaseRoller sut, int roll)
	{
		// Arrange
		var (numDice, dieSize, keep, minimum) = CreateResponses(3, DieSize, 4, null);

		var expected = RollResponse.CreateResponse(
			DieSize.CreateRepeatedRolls(roll, DefaultKeep),
			Enumerable.Empty<Roll>());

		// Act
		var result = sut.Roll(numDice, dieSize, keep, minimum, RoundingStrategy.RoundToCeiling.Create());

		// Assert
		result.Should().BeEquivalentTo(expected);
	}

	internal static void ShouldReturnAbsoluteValueOfKeep(BaseRoller sut, int roll)
	{
		//Arrange
		var (numDice, dieSize, keep, minimum) = CreateResponses(4, DieSize, -DefaultKeep, null);

		var expected = RollResponse.CreateResponse(
			DieSize.CreateRepeatedRolls(roll, DefaultKeep),
			DieSize.CreateRolls(roll));

		// Act
		var result = sut.Roll(numDice, dieSize, keep, minimum, RoundingStrategy.RoundToCeiling.Create());

		// Assert
		result.Should().BeEquivalentTo(expected);
	}

	internal static void ShouldReturnSameAsNumDiceIfKeepIsMoreAndKeepIsNegative(BaseRoller sut, int roll)
	{
		// Arrange
		var (numDice, dieSize, keep, minimum) = CreateResponses(DefaultKeep, DieSize, -4, null);

		var expected = RollResponse.CreateResponse(
			DieSize.CreateRepeatedRolls(roll, DefaultKeep),
			Enumerable.Empty<Roll>());

		// Act
		var result = sut.Roll(numDice, dieSize, keep, minimum, RoundingStrategy.RoundToCeiling.Create());

		// Assert
		result.Should().BeEquivalentTo(expected);
	}

	internal static void ShouldHandleResultIfNegative(
		BaseRoller sut,
		int numDiceParam,
		int dieSizeParam,
		int keepParam,
		int numDiceResult,
		int dieSizeResult,
		int keepResult,
		int rollResult)
	{
		// Arrange
		var (numDice, dieSize, keep, minimum) = CreateResponses(numDiceParam, dieSizeParam, keepParam, null);

		var expected = RollResponse.CreateResponse(
			dieSizeResult.CreateRepeatedRolls(rollResult, keepResult),
			dieSizeResult.CreateRepeatedRollsInverse(rollResult, numDiceResult, keepResult));

		// Act
		var actual = sut.Roll(numDice, dieSize, keep, minimum, RoundingStrategy.RoundToCeiling.Create());

		// Assert
		actual.Should().BeEquivalentTo(expected);
	}

	internal static void ShouldReturnListOfParameterMinRolls(BaseRoller sut, int min)
	{
		// Arrange
		var (numDice, dieSize, keep, minimum) = CreateResponses(DefaultKeep, DieSize, null, min);

		var expected = RollResponse.CreateResponse(
			DieSize.CreateRepeatedRolls(min, DefaultKeep),
			Enumerable.Empty<Roll>());

		// Act
		var result = sut.Roll(numDice, dieSize, keep, minimum, RoundingStrategy.RoundToFloor.Create());

		// Assert
		result.Should().BeEquivalentTo(expected);
	}

	internal static void ShouldNotReturnListOfParameterMinRollsWhenOtherRollsAreHigher(BaseRoller sut, int min, int roll)
	{
		// Arrange
		var (numDice, dieSize, keep, minimum) = CreateResponses(DefaultKeep, DieSize, null, min);

		var expected = RollResponse.CreateResponse(
			DieSize.CreateRepeatedRolls(roll, DefaultKeep),
			Enumerable.Empty<Roll>());

		// Act
		var result = sut.Roll(numDice, dieSize, keep, minimum, RoundingStrategy.RoundToCeiling.Create());

		// Assert
		result.Should().BeEquivalentTo(expected);
	}

	internal static void ShouldNotReturnListOfParameterMinRollsWhenMinIsGreaterThanDieSize(BaseRoller sut, int roll)
	{
		// Arrange
		var (numDice, dieSize, keep, minimum) = CreateResponses(DefaultKeep, DieSize, null, DieSize + 1);

		var expected = RollResponse.CreateResponse(
			DieSize.CreateRepeatedRolls(roll, DefaultKeep),
			Enumerable.Empty<Roll>());

		// Act
		var result = sut.Roll(numDice, dieSize, keep, minimum, RoundingStrategy.RoundToCeiling.Create());

		// Assert
		result.Should().BeEquivalentTo(expected);
	}

	internal static (NodeResponse? NumDice, NodeResponse? DieSize, NodeResponse? Keep, NodeResponse? Minimum)
		CreateResponses(
			int? numDice,
			int? dieSize,
			int? keep,
			int? minimum)
	{
		var numDiceResult = numDice.HasValue ? NodeResponseFactory.CreateSimpleResponse(numDice.Value) : null;
		var dieSizeResult = dieSize.HasValue ? NodeResponseFactory.CreateSimpleResponse(dieSize.Value) : null;
		var keepResult = keep.HasValue ? NodeResponseFactory.CreateSimpleResponse(keep.Value) : null;
		var minimumResult = minimum.HasValue ? NodeResponseFactory.CreateSimpleResponse(minimum.Value) : null;

		return (numDiceResult, dieSizeResult, keepResult, minimumResult);
	}
}