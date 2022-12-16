namespace Dicer;

/// <summary>
///     Node that represents the unary of an <see cref="INode" />.
/// </summary>
internal record UnaryNode(BaseNode Node) : BaseNode
{
	internal override NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
	{
		return NodeResponse.Unary(Node.Evaluate(roller, roundingStrategy), roundingStrategy);
	}

	public override string ToString()
	{
		return $"UNARY({Node})";
	}
}