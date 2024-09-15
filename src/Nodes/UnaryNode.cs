namespace Dicer.Nodes;

/// <summary>
///     Node that represents the unary of an <see cref="IDiceExpression" />.
/// </summary>
internal record UnaryNode(BaseNode Node) : BaseNode
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