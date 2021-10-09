using Dicer.Helpers;
using Dicer.Models;
using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes;

public class DiceNode : INode
{
	private readonly INode _numDice;
	private readonly INode _dieSize;
	private readonly INode? _keep;

	public DiceNode(INode numDice, INode dieSize, INode? keep = null)
	{
		_numDice = numDice;
		_dieSize = dieSize;
		_keep = keep;
	}

	public NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
	{
		var numDiceResponse = _numDice.Evaluate(roller, roundingStrategy);
		var dieSizeResponse = _dieSize.Evaluate(roller, roundingStrategy);
		var keepResponse = _keep?.Evaluate(roller, roundingStrategy);

		var rollResult = roller.Roll(numDiceResponse, dieSizeResponse, keepResponse, roundingStrategy);

		var mergedRolls = RollHelpers.Merge(numDiceResponse.RollResponses, new[] { rollResult }, dieSizeResponse.RollResponses);
		return new(rollResult.Result, mergedRolls);
	}

	public override string ToString()
	{
		string keep = _keep is not null ? $",{_keep}" : string.Empty;
		return $"DICE({_numDice},{_dieSize}{keep})";
	}
}