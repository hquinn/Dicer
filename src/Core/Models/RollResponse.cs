using System.Collections.Generic;

namespace Dicer.Models;

/// <summary>
/// Final result for a roll.
/// </summary>
/// <param name="Result">The result of the roll.</param>
/// <param name="Rolls">All the rolls for the response.</param>
public record RollResponse(int Result, IEnumerable<Roll> Rolls);