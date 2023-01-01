namespace Dicer;

/// <summary>
///     Represents a node in an expression tree, used for calculating mathematical expressions.
/// </summary>
public interface INode
{
	/// <summary>
	///     Calculates the node.
	/// </summary>
	/// <param name="selectedRoller">The roller to use when rolling dice.</param>
	/// <param name="selectedRoundingStrategy">The rounding strategy to use when rounding the result.</param>
	/// <param name="selectedDiceRoundingStrategy">The rounding strategy to use when rounding dice values.</param>
	/// <returns>The <see cref="NodeResponse" /> of the resulting calculations.</returns>
	NodeResponse Evaluate(
		Roller selectedRoller = Roller.Random, 
		RoundingStrategy selectedRoundingStrategy = RoundingStrategy.RoundToFloor, 
		DiceRoundingStrategy selectedDiceRoundingStrategy = DiceRoundingStrategy.RoundToCeiling);
}