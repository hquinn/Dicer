using Dicer.Models;
using Dicer.Randomizer;
using Dicer.Rounding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dicer.Rollers;

public class RandomRoller : IRoller
{
	private readonly IRandom _random;

	public RandomRoller()
	{
		_random = new DefaultRandom();
	}

	public RandomRoller(IRandom random)
	{
		_random = random;
	}

	public RollResponse Roll(NodeResponse numDice, NodeResponse dieSize, NodeResponse? keep, IRoundingStrategy roundingStrategy)
	{
		var numDiceResult = (int)roundingStrategy.Round(numDice.Result);
		var dieSizeResult = (int)roundingStrategy.Round(dieSize.Result);

		var rolls = RollDice(numDiceResult, dieSizeResult).OrderByDescending(x => x.Result).ToList();
		var pickedRolls = PickDiceToKeep(rolls, keep, roundingStrategy).ToList();

		return new(pickedRolls.Sum(x => x.Result), pickedRolls);
	}

	private IEnumerable<Roll> RollDice(int numDiceResult, int dieSizeResult)
	{
		for (var i = 1; i <= numDiceResult; i++)
		{
			yield return new(_random.RollDice(dieSizeResult), dieSizeResult);
		}
	}

	private static IEnumerable<Roll> PickDiceToKeep(IReadOnlyCollection<Roll> rolls, NodeResponse? keep, IRoundingStrategy roundingStrategy)
	{
		if (keep is null)
		{
			return rolls;
		}

		var keepResult = (int)roundingStrategy.Round(keep.Result);

		if (keepResult >= 0)
		{
			return rolls.Take(keepResult);
		}

		var skip = Math.Max(rolls.Count + keepResult, 0);

		return rolls.Skip(skip).Take(-keepResult);
	}
}