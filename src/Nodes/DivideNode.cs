using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes;

/// <summary>
///     Node for dividing two nodes together.
/// </summary>
internal record DivideNode(DiceExpression First, DiceExpression Second) : DiceExpression
{
    internal override ExpressionResponse Evaluate(IDiceRoller roller, IRoundingStrategy diceRoundingStrategy)
    {
        var firstEval = First.Evaluate(roller, diceRoundingStrategy);
        var secondEval = Second.Evaluate(roller, diceRoundingStrategy);

        return ExpressionResponse.Divide(firstEval, secondEval);
    }

    public override string ToString()
    {
        return $"DIVIDE({First},{Second})";
    }
}