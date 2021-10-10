using Dicer.Models;
using Dicer.Rounding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dicer.Helpers;

internal static class RollHelpers
{
	internal static IEnumerable<RollResponse> Merge(params IEnumerable<RollResponse>?[] responses)
	{
		return responses
			.Select(x => x ??= Enumerable.Empty<RollResponse>())
			.SelectMany(x => x);
	}

	internal static IReadOnlyCollection<Roll> PickDiceToKeep(this IEnumerable<Roll> rolls, NodeResponse? keep, IRoundingStrategy roundingStrategy)
	{
		var orderedRolls = rolls
			.OrderByDescending(x => x.Result)
			.ToList();

		if (keep is null)
		{
			return orderedRolls;
		}

		var keepResult = (int)roundingStrategy.Round(keep.Result);

		if (keepResult >= 0)
		{
			return orderedRolls.Take(keepResult).ToList();
		}

		var skip = Math.Max(orderedRolls.Count + keepResult, 0);

		return orderedRolls.Skip(skip).Take(-keepResult).ToList();
	}
}