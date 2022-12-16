namespace Dicer;

/// <summary>
///     Node for multiplying two <see cref="INode" /> together.
/// </summary>
internal record MultiplyNode(BaseNode First, BaseNode Second) : BaseNode
{
	internal override NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
	{
		var firstEval = First.Evaluate(roller, roundingStrategy);
		var secondEval = Second.Evaluate(roller, roundingStrategy);

		return NodeResponse.Times(firstEval, secondEval, roundingStrategy);
	}

	public override string ToString()
	{
		return $"MULTIPLY({First},{Second})";
	}
}