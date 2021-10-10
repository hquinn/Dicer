using Dicer.Helpers;
using Dicer.Models;
using Dicer.Randomizer;
using Dicer.Rounding;
using System.Collections.Generic;

namespace Dicer.Rollers;

/// <summary>
/// Rolls dice using a random roll for each die.
/// </summary>
public class RandomRoller : IRoller
{
	private readonly IRandom _random;

	public RandomRoller()
	{
		_random = new DefaultRandom();
	}

	public RandomRoller(IRandom random)
	{
		_random = random;
	}

	/// <inheritdoc />
	public RollResponse Roll(NodeResponse numDice, NodeResponse dieSize, NodeResponse? keep, IRoundingStrategy roundingStrategy)
	{
		var numDiceResult = new NodeRollResponse(numDice, roundingStrategy);
		var dieSizeResult = new NodeRollResponse(dieSize, roundingStrategy);

		var rolls = RollDice(numDiceResult, dieSizeResult)
			.PickDiceToKeep(keep, roundingStrategy);

		return RollResponse.CreateResponse(rolls);
	}

	private IEnumerable<Roll> RollDice(NodeRollResponse numDiceResult, NodeRollResponse dieSizeResult)
	{
		for (var i = 1; i <= numDiceResult.Result; i++)
		{
			var random = _random.RollDice(dieSizeResult.Result);

			if (dieSizeResult.IsNegative)
			{
				random = -random;
			}

			var numDiceMultiplier = numDiceResult.IsNegative ? -1 : 1;
			var dieSizeMultiplier = dieSizeResult.IsNegative ? -1 : 1;

			yield return new(random * numDiceMultiplier, dieSizeResult.Result * dieSizeMultiplier);
		}
	}
}