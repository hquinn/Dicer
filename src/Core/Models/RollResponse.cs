using System.Collections.Generic;
using System.Linq;

namespace Dicer;

/// <summary>
///     Final result for a roll.
/// </summary>
/// <param name="Result">The result of the roll.</param>
/// <param name="Rolls">All the rolls for the response that was kept.</param>
/// <param name="Discarded">All the rolls for the response that was discarded.</param>
public record RollResponse(int Result, IEnumerable<Roll> Rolls, IEnumerable<Roll> Discarded)
{
	internal static RollResponse CreateResponse(IEnumerable<Roll> rolls, IEnumerable<Roll> discarded)
	{
		return new(rolls.Sum(x => x.Result), rolls, discarded);
	}
}