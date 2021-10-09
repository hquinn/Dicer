using Dicer.Models;
using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes;

public class SubtractNode : INode
{
	private readonly INode _first;
	private readonly INode _second;

	public SubtractNode(INode first, INode second)
	{
		_first = first;
		_second = second;
	}

	public NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
	{
		return NodeResponse.Minus(_first.Evaluate(roller, roundingStrategy), _second.Evaluate(roller, roundingStrategy), roundingStrategy);
	}

	public override string ToString()
	{
		return $"SUBTRACT({_first},{_second})";
	}
}