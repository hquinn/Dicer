namespace Dicer;

/// <summary>
///     Node for subtracting two <see cref="INode" /> together.
/// </summary>
internal record SubtractNode(BaseNode First, BaseNode Second) : BaseNode
{
	internal override NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
	{
		var firstEval = First.Evaluate(roller, roundingStrategy);
		var secondEval = Second.Evaluate(roller, roundingStrategy);

		return NodeResponse.Minus(firstEval, secondEval, roundingStrategy);
	}

	public override string ToString()
	{
		return $"SUBTRACT({First},{Second})";
	}
}