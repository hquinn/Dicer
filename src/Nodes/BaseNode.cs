namespace Dicer.Nodes;

internal abstract record BaseNode : IDiceExpression
{
    public ExpressionResponse Evaluate(
        IDiceRoller roller,
        IRoundingStrategy resultRoundingStrategy,
        IRoundingStrategy diceRoundingStrategy)
    {
        var result = Evaluate(roller, diceRoundingStrategy);

        return new ExpressionResponse(resultRoundingStrategy.Round(result.Result), result.RollResponses);
    }

    internal abstract ExpressionResponse Evaluate(IDiceRoller roller, IRoundingStrategy diceRoundingStrategy);
}