namespace Dicer.Nodes;

/// <summary>
///     Node for dividing two nodes together.
/// </summary>
internal record DivideNode(BaseNode First, BaseNode Second) : BaseNode
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