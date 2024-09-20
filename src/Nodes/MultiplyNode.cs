using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes;

/// <summary>
///     Node for multiplying two nodes together.
/// </summary>
internal record MultiplyNode(DiceExpression First, DiceExpression Second) : DiceExpression
{
    internal override ExpressionResponse Evaluate(IDiceRoller roller, IRoundingStrategy diceRoundingStrategy)
    {
        var firstEval = First.Evaluate(roller, diceRoundingStrategy);
        var secondEval = Second.Evaluate(roller, diceRoundingStrategy);

        return ExpressionResponse.Times(firstEval, secondEval);
    }

    public override string ToString()
    {
        return $"MULTIPLY({First},{Second})";
    }
}