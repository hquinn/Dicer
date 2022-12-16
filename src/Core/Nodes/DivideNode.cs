namespace Dicer;

/// <summary>
///     Node for dividing two <see cref="INode" /> together.
/// </summary>
internal record DivideNode(BaseNode First, BaseNode Second) : BaseNode
{
	internal override NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
	{
		var firstEval = First.Evaluate(roller, roundingStrategy);
		var secondEval = Second.Evaluate(roller, roundingStrategy);

		return NodeResponse.Divide(firstEval, secondEval, roundingStrategy);
	}

	public override string ToString()
	{
		return $"DIVIDE({First},{Second})";
	}
}