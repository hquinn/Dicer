using System;
using System.Collections.Generic;
using System.Linq;
using Dicer.Tests.Helpers;

namespace Dicer.Tests.Factories;

public static class RollResponseFactory
{
    public static IReadOnlyCollection<RollResponse> Create(
        int dieSize,
        IReadOnlyCollection<int> rollValues,
        IReadOnlyCollection<int>? discardedValues)
    {
        var rolls = CreateRolls(rollValues, dieSize);
        var discarded = discardedValues is not null ? CreateRolls(discardedValues, dieSize) : Array.Empty<Roll>();

        return RollResponse.CreateResponse(rolls, discarded).AsArray();
    }

    private static IReadOnlyCollection<Roll> CreateRolls(IEnumerable<int> rolls, int dieSize)
    {
        return rolls.Select(x => new Roll(x, dieSize)).ToList().AsReadOnly();
    }
}