namespace Dicer;

/// <summary>
///     Node that represents the unary of an <see cref="INode" />.
/// </summary>
internal class UnaryNode : INode
{
	private readonly INode _node;

	public UnaryNode(INode node)
	{
		_node = node;
	}

	/// <inheritdoc />
	public NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
	{
		return NodeResponse.Unary(_node.Evaluate(roller, roundingStrategy), roundingStrategy);
	}

	public override string ToString()
	{
		return $"UNARY({_node})";
	}
}