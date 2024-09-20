using Dicer.Helpers;
using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes;

/// <summary>
///     Node for representing dice.
/// </summary>
internal record DiceNode(DiceExpression NumDice, DiceExpression DieSize, DiceExpression? Keep = null, DiceExpression? Minimum = null) : DiceExpression
{
    internal override ExpressionResponse Evaluate(IDiceRoller roller, IRoundingStrategy diceRoundingStrategy)
    {
        var numDiceResponse = NumDice.Evaluate(roller, diceRoundingStrategy);
        var dieSizeResponse = DieSize.Evaluate(roller, diceRoundingStrategy);
        var keepResponse = Keep?.Evaluate(roller, diceRoundingStrategy);
        var minimumResponse = Minimum?.Evaluate(roller, diceRoundingStrategy);

        var rollResult = roller.Roll(numDiceResponse, dieSizeResponse, keepResponse, minimumResponse,
            diceRoundingStrategy);

        var mergedRolls = RollHelpers.Merge(numDiceResponse.RollResponses, new[] { rollResult },
            dieSizeResponse.RollResponses);

        return new ExpressionResponse(rollResult.Result, mergedRolls);
    }

    public override string ToString()
    {
        var keep = Keep is not null ? $",{Keep}" : ",";
        var minimum = Minimum is not null ? $",{Minimum}" : ",";

        return $"DICE({NumDice},{DieSize}{keep}{minimum})";
    }
}