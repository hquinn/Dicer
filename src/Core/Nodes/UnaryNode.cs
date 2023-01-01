namespace Dicer;

/// <summary>
///     Node that represents the unary of an <see cref="INode" />.
/// </summary>
internal record UnaryNode(BaseNode Node) : BaseNode
{
	internal override NodeResponse Evaluate(IRoller roller, IRoundingStrategy diceRoundingStrategy)
	{
		return NodeResponse.Unary(Node.Evaluate(roller, diceRoundingStrategy));
	}

	public override string ToString()
	{
		return $"UNARY({Node})";
	}
}