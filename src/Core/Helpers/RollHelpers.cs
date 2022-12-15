using System;
using System.Collections.Generic;
using System.Linq;

namespace Dicer;

internal static class RollHelpers
{
	internal static IEnumerable<RollResponse> Merge(params IEnumerable<RollResponse>?[] responses)
	{
		return responses
			.Select(x => x ??= Enumerable.Empty<RollResponse>())
			.SelectMany(x => x);
	}

	internal static IEnumerable<Roll> PickDiceToKeep(this IEnumerable<Roll> rolls, NodeResponse? keep, IRoundingStrategy roundingStrategy)
	{
		if (keep is null)
		{
			return rolls.ToArray();
		}

		var rollArray = rolls.ToArray();
		var filteredRolls = GetOrderedKeepRolls(keep, roundingStrategy, rollArray.OrderByDescending(x => x.Result).ToList());

		return GetUnorderedResult(rollArray, filteredRolls).ToArray();
	}

	private static List<Roll> GetOrderedKeepRolls(NodeResponse keep, IRoundingStrategy roundingStrategy, IReadOnlyCollection<Roll> orderedRolls)
	{
		var keepResult = (int)roundingStrategy.Round(keep.Result);

		if (keepResult >= 0)
		{
			return orderedRolls.Take(keepResult).ToList();
		}

		var skip = Math.Max(orderedRolls.Count + keepResult, 0);

		return orderedRolls.Skip(skip).Take(-keepResult).ToList();
	}

	private static IEnumerable<Roll> GetUnorderedResult(IEnumerable<Roll> rolls, ICollection<Roll> orderedRolls)
	{
		foreach (var roll in rolls)
		{
			if (orderedRolls.Contains(roll))
			{
				yield return roll;

				orderedRolls.Remove(roll);
			}
		}
	}
}