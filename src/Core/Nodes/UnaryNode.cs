using Dicer.Models;
using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes;

public class UnaryNode : INode
{
	private readonly INode _node;

	public UnaryNode(INode node)
	{
		_node = node;
	}

	public NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
	{
		return NodeResponse.Unary(_node.Evaluate(roller, roundingStrategy), roundingStrategy);
	}

	public override string ToString()
	{
		return $"UNARY({_node})";
	}
}