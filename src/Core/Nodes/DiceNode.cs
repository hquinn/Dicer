namespace Dicer;

/// <summary>
///     Node for representing dice.
/// </summary>
public class DiceNode : INode
{
	private readonly INode _dieSize;
	private readonly INode? _keep;
	private readonly INode? _minimum;
	private readonly INode _numDice;

	public DiceNode(INode numDice, INode dieSize, INode? keep = null, INode? minimum = null)
	{
		_numDice = numDice;
		_dieSize = dieSize;
		_keep = keep;
		_minimum = minimum;
	}

	/// <inheritdoc />
	public NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
	{
		var numDiceResponse = _numDice.Evaluate(roller, roundingStrategy);
		var dieSizeResponse = _dieSize.Evaluate(roller, roundingStrategy);
		var keepResponse = _keep?.Evaluate(roller, roundingStrategy);
		var minimumResponse = _minimum?.Evaluate(roller, roundingStrategy);

		var rollResult = roller.Roll(numDiceResponse, dieSizeResponse, keepResponse, minimumResponse, roundingStrategy);

		var mergedRolls = RollHelpers.Merge(numDiceResponse.RollResponses, new[] { rollResult },
			dieSizeResponse.RollResponses);

		return new(rollResult.Result, mergedRolls);
	}

	public override string ToString()
	{
		var keep = _keep is not null ? $",{_keep}" : ",";
		var minimum = _minimum is not null ? $",{_minimum}" : ",";

		return $"DICE({_numDice},{_dieSize}{keep}{minimum})";
	}
}