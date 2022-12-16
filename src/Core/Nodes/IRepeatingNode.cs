using System.Collections.Generic;

namespace Dicer;

/// <summary>
///     Represents a node in an expression tree, used for calculating mathematical expressions.
///     This node is able to repeat the calculation for a list of <see cref="NodeResponse" />.
/// </summary>
public interface IRepeatingNode
{
	/// <summary>
	///     Calculates the node multiple times.
	/// </summary>
	/// <param name="roller">The <see cref="IRoller" /> to use when rolling dice.</param>
	/// <param name="roundingStrategy">The <see cref="IRoundingStrategy" /> to use when rounding values.</param>
	/// <returns>The <see cref="NodeResponse" /> of the resulting calculations.</returns>
	IEnumerable<NodeResponse> Evaluate(Roller roller = Roller.Random, RoundingStrategy roundingStrategy = RoundingStrategy.RoundToFloor);
}