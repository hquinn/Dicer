using System.Collections.Generic;

namespace Dicer;

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
		NodeResponse? minimum,
		IRoundingStrategy roundingStrategy)
	{
		var numDiceResult = new NodeRollResponse(numDice, roundingStrategy);
		var dieSizeResult = new NodeRollResponse(dieSize, roundingStrategy);
		var minimumResult = new NodeRollResponse(minimum, roundingStrategy);

		var rolls = RollDice(numDiceResult, dieSizeResult, minimumResult, roundingStrategy)
			.PickDiceToKeep(keep, roundingStrategy);

		return RollResponse.CreateResponse(rolls);
	}

	protected abstract int RollSingleDice(int dieSize, int minimumValue, IRoundingStrategy roundingStrategy);

	private IEnumerable<Roll> RollDice(
		NodeRollResponse numDiceResult,
		NodeRollResponse dieSizeResult,
		NodeRollResponse minimumResult,
		IRoundingStrategy roundingStrategy)
	{
		var numDiceMultiplier = numDiceResult.IsNegative ? -1 : 1;
		var dieSizeMultiplier = dieSizeResult.IsNegative ? -1 : 1;

		for (var i = 1; i <= numDiceResult.Result; i++)
		{
			var roll = RollSingleDice(dieSizeResult.Result, minimumResult.Result, roundingStrategy);

			if (dieSizeResult.IsNegative)
			{
				roll = -roll;
			}

			yield return new(roll * numDiceMultiplier, dieSizeResult.Result * dieSizeMultiplier);
		}
	}
}