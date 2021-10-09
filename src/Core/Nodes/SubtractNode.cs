using Dicer.Models;
using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes;

/// <summary>
/// Node for subtracting two <see cref="INode"/> together.
/// </summary>
public class SubtractNode : INode
{
	private readonly INode _first;
	private readonly INode _second;

	public SubtractNode(INode first, INode second)
	{
		_first = first;
		_second = second;
	}

	/// <inheritdoc />
	public NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
	{
		return NodeResponse.Minus(_first.Evaluate(roller, roundingStrategy), _second.Evaluate(roller, roundingStrategy), roundingStrategy);
	}

	public override string ToString()
	{
		return $"SUBTRACT({_first},{_second})";
	}
}