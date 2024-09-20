using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes;

/// <summary>
///     Node that represents the unary of an <see cref="DiceExpression" />.
/// </summary>
internal record UnaryNode(DiceExpression Node) : DiceExpression
{
    internal override ExpressionResponse Evaluate(IDiceRoller roller, IRoundingStrategy diceRoundingStrategy)
    {
        return ExpressionResponse.Unary(Node.Evaluate(roller, diceRoundingStrategy));
    }

    public override string ToString()
    {
        return $"UNARY({Node})";
    }
}