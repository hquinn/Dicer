using System;
using System.Collections.Generic;
using System.Linq;

namespace Dicer;

/// <summary>
///     Final result for a roll.
/// </summary>
/// <param name="Result">The result of the roll.</param>
/// <param name="Rolls">All the rolls for the response that was kept.</param>
/// <param name="Discarded">All the rolls for the response that was discarded.</param>
public record RollResponse(int Result, IReadOnlyCollection<Roll> Rolls, IReadOnlyCollection<Roll> Discarded)
{
    internal static RollResponse CreateResponse(IReadOnlyCollection<Roll> rolls, IReadOnlyCollection<Roll>? discarded)
    {
        return new RollResponse(rolls.Sum(x => x.Result), rolls, discarded ?? Array.Empty<Roll>());
    }
}