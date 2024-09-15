using System;
using System.Collections.Generic;
using System.Linq;

namespace Dicer.Helpers;

internal static class RollHelpers
{
    internal static IReadOnlyCollection<RollResponse> Merge(params IReadOnlyCollection<RollResponse>[] responses)
    {
        return responses
            .SelectMany(x => x)
            .ToList()
            .AsReadOnly();
    }

    internal static IReadOnlyCollection<Roll> PickDiceToKeep(
        this IReadOnlyCollection<Roll> rolls,
        ExpressionResponse? keep,
        IRoundingStrategy roundingStrategy)
    {
        if (keep is null) return rolls.ToArray();

        var rollArray = rolls.ToArray();
        var filteredRolls = GetKeepRolls(keep, roundingStrategy, rollArray.OrderBy(x => x.Result).ToList());

        return GetUnorderedResult(rollArray, filteredRolls).ToArray();
    }

    internal static IReadOnlyCollection<Roll> PickDiceToDiscard(
        this IReadOnlyCollection<Roll> rolls,
        ExpressionResponse? keep,
        IRoundingStrategy roundingStrategy)
    {
        if (keep is null) return Array.Empty<Roll>();

        var rollArray = rolls.ToArray();
        var filteredRolls = GetDiscardRolls(keep, roundingStrategy, rollArray.OrderBy(x => x.Result).ToList());

        return GetUnorderedResult(rollArray, filteredRolls).ToArray();
    }

    private static List<Roll> GetKeepRolls(
        ExpressionResponse keep,
        IRoundingStrategy roundingStrategy,
        IReadOnlyCollection<Roll> rolls)
    {
        //var keepResult = (int)roundingStrategy.Round(keep.Result);

        //if (keepResult >= 0)
        //{
        //	return rolls.Take(keepResult).ToList();
        //}

        //var skip = Math.Max(rolls.Count + keepResult, 0);

        //return rolls.Skip(skip).Take(-keepResult).ToList();

        var keepResult = (int)roundingStrategy.Round(keep.Result);

        if (keepResult >= 0) return rolls.Skip(rolls.Count - keepResult).Take(keepResult).ToList();

        return rolls.Take(-keepResult).ToList();
    }

    private static List<Roll> GetDiscardRolls(ExpressionResponse keep, IRoundingStrategy roundingStrategy,
        IReadOnlyCollection<Roll> rolls)
    {
        var keepResult = (int)roundingStrategy.Round(keep.Result);

        if (keepResult >= 0) return rolls.Take(rolls.Count - keepResult).ToList();

        return rolls.Skip(-keepResult).Take(rolls.Count + keepResult).ToList();
    }

    private static IReadOnlyCollection<Roll> GetUnorderedResult(IReadOnlyCollection<Roll> rolls,
        ICollection<Roll> orderedRolls)
    {
        var unorderedRolls = new List<Roll>();

        foreach (var roll in rolls)
            if (orderedRolls.Contains(roll))
            {
                unorderedRolls.Add(roll);
                orderedRolls.Remove(roll);
            }

        return unorderedRolls;
    }
}