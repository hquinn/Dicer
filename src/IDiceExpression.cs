namespace Dicer;

/// <summary>
///     Represents an expression tree, used for calculating dice expressions.
/// </summary>
public interface IDiceExpression
{
    ExpressionResponse Evaluate(
        IDiceRoller roller,
        IRoundingStrategy resultRoundingStrategy,
        IRoundingStrategy diceRoundingStrategy);
}