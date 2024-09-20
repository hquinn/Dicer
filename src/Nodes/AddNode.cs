using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes;

/// <summary>
///     Node for adding two nodes together.
/// </summary>
internal record AddNode(DiceExpression First, DiceExpression Second) : DiceExpression
{
    internal override ExpressionResponse Evaluate(IDiceRoller roller, IRoundingStrategy diceRoundingStrategy)
    {
        var firstEval = First.Evaluate(roller, diceRoundingStrategy);
        var secondEval = Second.Evaluate(roller, diceRoundingStrategy);

        return ExpressionResponse.Plus(firstEval, secondEval);
    }

    public override string ToString()
    {
        return $"ADD({First},{Second})";
    }
}