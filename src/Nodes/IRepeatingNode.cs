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
	/// <param name="roller">The roller to use when rolling dice.</param>
	/// <param name="roundingStrategy">The rounding strategy to use when rounding the result.</param>
	/// <param name="diceRoundingStrategy">The rounding strategy to use when rounding dice values.</param>
	/// <returns>The <see cref="NodeResponse" /> of the resulting calculations.</returns>
	IEnumerable<NodeResponse> Evaluate(
		Roller roller = Roller.Random, 
		RoundingStrategy roundingStrategy = RoundingStrategy.RoundToFloor, 
		DiceRoundingStrategy diceRoundingStrategy = DiceRoundingStrategy.RoundToCeiling);
}