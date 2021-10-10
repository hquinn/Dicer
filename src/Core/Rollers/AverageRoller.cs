using Dicer.Helpers;
using Dicer.Models;
using Dicer.Rounding;
using System.Collections.Generic;

namespace Dicer.Rollers;

/// <summary>
/// Rolls dice using the average roll for each die.
/// </summary>
public class AverageRoller : IRoller
{
	/// <inheritdoc />
	public RollResponse Roll(NodeResponse numDice, NodeResponse dieSize, NodeResponse? keep, IRoundingStrategy roundingStrategy)
	{
		var numDiceResult = new NodeRollResponse(numDice, roundingStrategy);
		var dieSizeResult = new NodeRollResponse(dieSize, roundingStrategy);
		var rolls = RollDice(numDiceResult, dieSizeResult, roundingStrategy)
			.PickDiceToKeep(keep, roundingStrategy);

		return RollResponse.CreateResponse(rolls);
	}

	private static IEnumerable<Roll> RollDice(NodeRollResponse numDiceResult, NodeRollResponse dieSizeResult, IRoundingStrategy roundingStrategy)
	{
		var average = (int)roundingStrategy.Round((dieSizeResult.Result + 1) / 2.0);

		if (dieSizeResult.IsNegative)
		{
			average = -average;
		}

		var numDiceMultiplier = numDiceResult.IsNegative ? -1 : 1;
		var dieSizeMultiplier = dieSizeResult.IsNegative ? -1 : 1;
		for (var i = 1; i <= numDiceResult.Result; i++)
		{
			yield return new(average * numDiceMultiplier, dieSizeResult.Result * dieSizeMultiplier);
		}
	}
}