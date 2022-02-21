using Dicer.Models;
using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes;

/// <summary>
///     Node for multiplying two <see cref="INode" /> together.
/// </summary>
public class MultiplyNode : INode
{
	private readonly INode _first;
	private readonly INode _second;

	public MultiplyNode(INode first, INode second)
	{
		_first = first;
		_second = second;
	}

	/// <inheritdoc />
	public NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
	{
		return NodeResponse.Times(_first.Evaluate(roller, roundingStrategy), _second.Evaluate(roller, roundingStrategy),
			roundingStrategy);
	}

	public override string ToString()
	{
		return $"MULTIPLY({_first},{_second})";
	}
}