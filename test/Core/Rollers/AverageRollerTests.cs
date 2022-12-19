using Dicer.Tests.Factories;
using Dicer.Tests.Helpers;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Dicer.Tests.Rollers;

public class AverageRollerTests
{
	public class RollTests
	{
		private const int Average = 4;
		private const int DieSize = 6;
		private const int DefaultKeep = 3;

		private AverageRoller Sut => new();

		[Fact]
		public void ShouldReturnListOfAverageRolls()
		{
			RollerTests.ShouldReturnListOfRolls(Sut, Average);
		}

		[Fact]
		public void ShouldReturnLessRollsBasedOnKeep()
		{
			RollerTests.ShouldReturnLessRollsBasedOnKeep(Sut, Average);
		}

		[Fact]
		public void ShouldReturnSameAsNumDiceIfKeepIsMore()
		{
			RollerTests.ShouldReturnSameAsNumDiceIfKeepIsMore(Sut, Average);
		}

		[Fact]
		public void ShouldReturnAbsoluteValueOfKeep()
		{
			RollerTests.ShouldReturnAbsoluteValueOfKeep(Sut, Average);
		}

		[Fact]
		public void ShouldReturnSameAsNumDiceIfKeepIsMoreAndKeepIsNegative()
		{
			RollerTests.ShouldReturnSameAsNumDiceIfKeepIsMoreAndKeepIsNegative(Sut, Average);
		}

		[Theory]
		[InlineData(-4, -DieSize, DefaultKeep, 4, -DieSize, DefaultKeep, Average)]
		[InlineData(4, -DieSize, DefaultKeep, -4, -DieSize, DefaultKeep, -Average)]
		[InlineData(-4, DieSize, DefaultKeep, -4, DieSize, DefaultKeep, -Average)]
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
		public void ShouldReturnListOfParameterMinRolls()
		{
			RollerTests.ShouldReturnListOfParameterMinRolls(Sut, Average + 1);
		}

		[Fact]
		public void ShouldNotReturnListOfParameterMinRollsWhenOtherRollsAreHigher()
		{
			RollerTests.ShouldNotReturnListOfParameterMinRollsWhenOtherRollsAreHigher(Sut, Average - 1, Average);
		}

		[Fact]
		public void ShouldNotReturnListOfParameterMinRollsWhenMinIsGreaterThanDieSize()
		{
			RollerTests.ShouldNotReturnListOfParameterMinRollsWhenMinIsGreaterThanDieSize(Sut, Average);
		}
	}
}