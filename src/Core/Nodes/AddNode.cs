using Dicer.Models;
using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes;

/// <summary>
/// Node for adding two <see cref="INode"/> together.
/// </summary>
public class AddNode : INode
{
	private readonly INode _first;
	private readonly INode _second;

	public AddNode(INode first, INode second)
	{
		_first = first;
		_second = second;
	}

	/// <inheritdoc />
	public NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
	{
		return NodeResponse.Plus(_first.Evaluate(roller, roundingStrategy), _second.Evaluate(roller, roundingStrategy), roundingStrategy);
	}

	public override string ToString()
	{
		return $"ADD({_first},{_second})";
	}
}