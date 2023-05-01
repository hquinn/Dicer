namespace Dicer;

/// <summary>
///     Node for dividing two <see cref="INode" /> together.
/// </summary>
internal record DivideNode(BaseNode First, BaseNode Second) : BaseNode
{
	internal override NodeResponse Evaluate(IRoller roller, IRoundingStrategy diceRoundingStrategy)
	{
		var firstEval = First.Evaluate(roller, diceRoundingStrategy);
		var secondEval = Second.Evaluate(roller, diceRoundingStrategy);

		return NodeResponse.Divide(firstEval, secondEval);
	}

	public override string ToString()
	{
		return $"DIVIDE({First},{Second})";
	}
}