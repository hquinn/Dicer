using Dicer.Models;
using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes;

public class DivideNode : INode
{
	private readonly INode _first;
	private readonly INode _second;

	public DivideNode(INode first, INode second)
	{
		_first = first;
		_second = second;
	}

	public NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
	{
		return NodeResponse.Divide(_first.Evaluate(roller, roundingStrategy), _second.Evaluate(roller, roundingStrategy), roundingStrategy);
	}

	public override string ToString()
	{
		return $"DIVIDE({_first},{_second})";
	}
}