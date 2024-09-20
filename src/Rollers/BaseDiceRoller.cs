using System.Collections.Generic;
using System.Linq;
using Dicer.Helpers;
using Dicer.Models;
using Dicer.Rounding;

namespace Dicer.Rollers;

/// <summary>
///     Base class for rolling dice
/// </summary>
internal abstract class BaseDiceRoller : IDiceRoller
{
    /// <inheritdoc />
    public RollResponse Roll(
        ExpressionResponse numDice,
        ExpressionResponse dieSize,
        ExpressionResponse? keep,
        ExpressionResponse? minimum,
        IRoundingStrategy roundingStrategy)
    {
        var numDiceResult = new ExpressionRollResponse(numDice, roundingStrategy);
        var dieSizeResult = new ExpressionRollResponse(dieSize, roundingStrategy);
        var minimumResult = new ExpressionRollResponse(minimum, roundingStrategy);

        var rolls = RollDice(numDiceResult, dieSizeResult, minimumResult, roundingStrategy)
            .ToArray();

        var rollsToKeep = rolls
            .PickDiceToKeep(keep, roundingStrategy);

        var discarded = rolls.PickDiceToDiscard(keep, roundingStrategy);

        return RollResponse.CreateResponse(rollsToKeep, discarded);
    }

    protected abstract int RollSingleDice(int dieSize, int minimumValue, IRoundingStrategy roundingStrategy);

    private IReadOnlyCollection<Roll> RollDice(
        ExpressionRollResponse numDiceResult,
        ExpressionRollResponse dieSizeResult,
        ExpressionRollResponse minimumResult,
        IRoundingStrategy roundingStrategy)
    {
        var numDiceMultiplier = numDiceResult.IsNegative ? -1 : 1;
        var dieSizeMultiplier = dieSizeResult.IsNegative ? -1 : 1;

        var rolls = new Roll[numDiceResult.Result];
        for (var i = 0; i < numDiceResult.Result; i++)
        {
            var roll = RollSingleDice(dieSizeResult.Result, minimumResult.Result, roundingStrategy);

            if (dieSizeResult.IsNegative) roll = -roll;

            rolls[i] = new Roll(roll * numDiceMultiplier, dieSizeResult.Result * dieSizeMultiplier);
        }

        return rolls;
    }
}