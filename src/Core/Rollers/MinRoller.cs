using Dicer.Helpers;
using Dicer.Models;
using Dicer.Rounding;
using System.Collections.Generic;

namespace Dicer.Rollers;

/// <summary>
/// Rolls dice using the min roll for each die.
/// </summary>
public class MinRoller : IRoller
{
	/// <inheritdoc />
	public RollResponse Roll(NodeResponse numDice, NodeResponse dieSize, NodeResponse? keep, IRoundingStrategy roundingStrategy)
	{
		var numDiceResult = new NodeRollResponse(numDice, roundingStrategy);
		var dieSizeResult = new NodeRollResponse(dieSize, roundingStrategy);

		var rolls = RollDice(numDiceResult, dieSizeResult)
			.PickDiceToKeep(keep, roundingStrategy);

		return RollResponse.CreateResponse(rolls);
	}

	private static IEnumerable<Roll> RollDice(NodeRollResponse numDiceResult, NodeRollResponse dieSizeResult)
	{
		var min = 1;

		if (dieSizeResult.IsNegative)
		{
			min = -min;
		}

		var numDiceMultiplier = numDiceResult.IsNegative ? -1 : 1;
		var dieSizeMultiplier = dieSizeResult.IsNegative ? -1 : 1;

		for (var i = 1; i <= numDiceResult.Result; i++)
		{
			yield return new(min * numDiceMultiplier, dieSizeResult.Result * dieSizeMultiplier);
		}
	}
}