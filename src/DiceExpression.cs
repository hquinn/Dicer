using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer;

public abstract record DiceExpression
{
    internal ExpressionResponse Evaluate(
        IDiceRoller roller,
        IRoundingStrategy resultRoundingStrategy,
        IRoundingStrategy diceRoundingStrategy)
    {
        var result = Evaluate(roller, diceRoundingStrategy);

        return new ExpressionResponse(resultRoundingStrategy.Round(result.Result), result.RollResponses);
    }

    internal abstract ExpressionResponse Evaluate(IDiceRoller roller, IRoundingStrategy diceRoundingStrategy);
}