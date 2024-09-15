namespace Dicer.Nodes;

/// <summary>
///     Node for subtracting two nodes together.
/// </summary>
internal record SubtractNode(BaseNode First, BaseNode Second) : BaseNode
{
    internal override ExpressionResponse Evaluate(IDiceRoller roller, IRoundingStrategy diceRoundingStrategy)
    {
        var firstEval = First.Evaluate(roller, diceRoundingStrategy);
        var secondEval = Second.Evaluate(roller, diceRoundingStrategy);

        return ExpressionResponse.Minus(firstEval, secondEval);
    }

    public override string ToString()
    {
        return $"SUBTRACT({First},{Second})";
    }
}