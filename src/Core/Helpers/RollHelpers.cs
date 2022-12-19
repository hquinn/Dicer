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
		var filteredRolls = GetKeepRolls(keep, roundingStrategy, rollArray.OrderBy(x => x.Result).ToList());

		return GetUnorderedResult(rollArray, filteredRolls).ToArray();
	}

	internal static IEnumerable<Roll> PickDiceToDiscard(
		this IEnumerable<Roll> rolls,
		NodeResponse? keep,
		IRoundingStrategy roundingStrategy)
	{
		if (keep is null)
		{
			return Array.Empty<Roll>();
		}

		var rollArray = rolls.ToArray();
		var filteredRolls = GetDiscardRolls(keep, roundingStrategy, rollArray.OrderBy(x => x.Result).ToList());

		return GetUnorderedResult(rollArray, filteredRolls).ToArray();
	}

	private static List<Roll> GetKeepRolls(NodeResponse keep, IRoundingStrategy roundingStrategy, IReadOnlyCollection<Roll> rolls)
	{
		//var keepResult = (int)roundingStrategy.Round(keep.Result);

		//if (keepResult >= 0)
		//{
		//	return rolls.Take(keepResult).ToList();
		//}

		//var skip = Math.Max(rolls.Count + keepResult, 0);

		//return rolls.Skip(skip).Take(-keepResult).ToList();

		var keepResult = (int)roundingStrategy.Round(keep.Result);

		if (keepResult >= 0)
		{
			return rolls.Skip(rolls.Count - keepResult).Take(keepResult).ToList();
		}
		
		return rolls.Take(-keepResult).ToList();
	}

	private static List<Roll> GetDiscardRolls(NodeResponse keep, IRoundingStrategy roundingStrategy,
		IReadOnlyCollection<Roll> rolls)
	{
		var keepResult = (int)roundingStrategy.Round(keep.Result);

		if (keepResult >= 0)
		{
			return rolls.Take(rolls.Count - keepResult).ToList();
		}
		
		return rolls.Skip(-keepResult).Take(rolls.Count + keepResult).ToList();
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