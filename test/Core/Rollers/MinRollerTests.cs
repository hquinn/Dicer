using Dicer.Tests.Factories;
using Dicer.Tests.Helpers;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Dicer.Tests.Rollers;

public class MinRollerTests
{
	public class RollTests
	{
		private const int Min = 1;
		private const int DieSize = 6;
		private const int DefaultKeep = 3;

		private MinRoller Sut => new();

		[Fact]
		public void ShouldReturnListOfMinimumRolls()
		{
			RollerTests.ShouldReturnListOfRolls(Sut, Min);
		}

		[Fact]
		public void ShouldReturnLessRollsBasedOnKeep()
		{
			RollerTests.ShouldReturnLessRollsBasedOnKeep(Sut, Min);
		}

		[Fact]
		public void ShouldReturnSameAsNumDiceIfKeepIsMore()
		{
			RollerTests.ShouldReturnSameAsNumDiceIfKeepIsMore(Sut, Min);
		}

		[Fact]
		public void ShouldReturnAbsoluteValueOfKeep()
		{
			RollerTests.ShouldReturnAbsoluteValueOfKeep(Sut, Min);
		}

		[Fact]
		public void ShouldReturnSameAsNumDiceIfKeepIsMoreAndKeepIsNegative()
		{
			RollerTests.ShouldReturnSameAsNumDiceIfKeepIsMoreAndKeepIsNegative(Sut, Min);
		}

		[Theory]
		[InlineData(-4, -DieSize, DefaultKeep, 4, -DieSize, DefaultKeep, Min)]
		[InlineData(4, -DieSize, DefaultKeep, -4, -DieSize, DefaultKeep, -Min)]
		[InlineData(-4, DieSize, DefaultKeep, -4, DieSize, DefaultKeep, -Min)]
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
			RollerTests.ShouldReturnListOfParameterMinRolls(Sut, Min + 1);
		}

		[Fact]
		public void ShouldNotReturnListOfParameterMinRollsWhenMinIsGreaterThanDieSize()
		{
			RollerTests.ShouldNotReturnListOfParameterMinRollsWhenMinIsGreaterThanDieSize(Sut, Min);
		}
	}
}