using System;
using System.Collections.Generic;
using System.Linq;

namespace Dicer.Tests.Factories;

internal static class RollFactory
{
	internal static IEnumerable<Roll> CreateRepeatedRolls(this int dieSize, int result, int times)
	{
		return Enumerable.Repeat(new Roll(result, dieSize), times);
	}

	internal static IEnumerable<Roll> CreateRepeatedRollsInverse(this int dieSize, int result, int numDice, int times)
	{
		return Enumerable.Repeat(new Roll(result, dieSize), Math.Abs(numDice) - times);
	}

	internal static IEnumerable<Roll> CreateRolls(this int dieSize, params int[] rolls)
	{
		return rolls.Select(x => new Roll(x, dieSize));
	}
}