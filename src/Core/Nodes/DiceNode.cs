namespace Dicer;

/// <summary>
///     Node for representing dice.
/// </summary>
internal record DiceNode(BaseNode NumDice, BaseNode DieSize, BaseNode? Keep = null, BaseNode? Minimum = null) : BaseNode
{
	internal override NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
	{
		var numDiceResponse = NumDice.Evaluate(roller, roundingStrategy);
		var dieSizeResponse = DieSize.Evaluate(roller, roundingStrategy);
		var keepResponse = Keep?.Evaluate(roller, roundingStrategy);
		var minimumResponse = Minimum?.Evaluate(roller, roundingStrategy);

		var rollResult = roller.Roll(numDiceResponse, dieSizeResponse, keepResponse, minimumResponse, roundingStrategy);

		var mergedRolls = RollHelpers.Merge(numDiceResponse.RollResponses, new[] { rollResult },
			dieSizeResponse.RollResponses);

		return new(rollResult.Result, mergedRolls);
	}

	public override string ToString()
	{
		var keep = Keep is not null ? $",{Keep}" : ",";
		var minimum = Minimum is not null ? $",{Minimum}" : ",";

		return $"DICE({NumDice},{DieSize}{keep}{minimum})";
	}
}