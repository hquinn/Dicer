namespace Dicer;

/// <summary>
///     Node for adding two <see cref="INode" /> together.
/// </summary>
internal record AddNode(BaseNode First, BaseNode Second) : BaseNode
{
	internal override NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
	{
		return NodeResponse.Plus(First.Evaluate(roller, roundingStrategy), Second.Evaluate(roller, roundingStrategy), roundingStrategy);
	}	

	public override string ToString()
	{
		return $"ADD({First},{Second})";
	}
}