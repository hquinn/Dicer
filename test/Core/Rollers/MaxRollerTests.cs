using Dicer.Tests.Factories;
using FluentAssertions;
using System.Linq;
using Dicer.Tests.Helpers;
using Xunit;

namespace Dicer.Tests.Rollers;

public class MaxRollerTests
{
	public class RollTests
	{
		private const int Max = 6;
		private const int DieSize = 6;
		private const int DefaultKeep = 3;

		internal MaxRoller Sut => new();

		[Fact]
		public void ShouldReturnListOfMaximumRolls()
		{
			RollerTests.ShouldReturnListOfRolls(Sut, Max);
		}

		[Fact]
		public void ShouldReturnLessRollsBasedOnKeep()
		{
			RollerTests.ShouldReturnLessRollsBasedOnKeep(Sut, Max);
		}

		[Fact]
		public void ShouldReturnSameAsNumDiceIfKeepIsMore()
		{
			RollerTests.ShouldReturnSameAsNumDiceIfKeepIsMore(Sut, Max);
		}

		[Fact]
		public void ShouldReturnAbsoluteValueOfKeep()
		{
			RollerTests.ShouldReturnAbsoluteValueOfKeep(Sut, Max);
		}

		[Fact]
		public void ShouldReturnSameAsNumDiceIfKeepIsMoreAndKeepIsNegative()
		{
			RollerTests.ShouldReturnSameAsNumDiceIfKeepIsMoreAndKeepIsNegative(Sut, Max);
		}

		[Theory]
		[InlineData(-4, -DieSize, DefaultKeep, 4, -DieSize, DefaultKeep, Max)]
		[InlineData(4, -DieSize, DefaultKeep, -4, -DieSize, DefaultKeep, -Max)]
		[InlineData(-4, DieSize, DefaultKeep, -4, DieSize, DefaultKeep, -Max)]
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
				Sut,
				numDiceParam,
				dieSizeParam,
				keepParam,
				numDiceResult,
				dieSizeResult,
				keepResult,
				rollResult);
		}

		[Fact]
		public void ShouldReturnMaxRollsEvenWhenMinimumIsPresent()
		{
			RollerTests.ShouldNotReturnListOfParameterMinRollsWhenOtherRollsAreHigher(Sut, Max - 1, Max);
		}
	}
}