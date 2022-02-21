using Dicer.Helpers;
using Dicer.Models;
using Dicer.Rounding;
using System.Collections.Generic;

namespace Dicer.Rollers;

/// <summary>
///     Base class for rolling dice
/// </summary>
public abstract class BaseRoller : IRoller
{
	/// <inheritdoc />
	public RollResponse Roll(
		NodeResponse numDice,
		NodeResponse dieSize,
		NodeResponse? keep,
		IRoundingStrategy roundingStrategy)
	{
		var numDiceResult = new NodeRollResponse(numDice.Result, roundingStrategy);
		var dieSizeResult = new NodeRollResponse(dieSize.Result, roundingStrategy);

		var rolls = RollDice(numDiceResult, dieSizeResult, roundingStrategy)
			.PickDiceToKeep(keep, roundingStrategy);

		return RollResponse.CreateResponse(rolls);
	}

	protected abstract int RollSingleDice(int dieSize, IRoundingStrategy roundingStrategy);

	private IEnumerable<Roll> RollDice(
		NodeRollResponse numDiceResult,
		NodeRollResponse dieSizeResult,
		IRoundingStrategy roundingStrategy)
	{
		var numDiceMultiplier = numDiceResult.IsNegative ? -1 : 1;
		var dieSizeMultiplier = dieSizeResult.IsNegative ? -1 : 1;

		for (var i = 1; i <= numDiceResult.Result; i++)
		{
			var roll = RollSingleDice(dieSizeResult.Result, roundingStrategy);

			if (dieSizeResult.IsNegative)
			{
				roll = -roll;
			}

			yield return new(roll * numDiceMultiplier, dieSizeResult.Result * dieSizeMultiplier);
		}
	}
}