namespace Dicer;

/// <summary>
///     Node for multiplying two <see cref="INode" /> together.
/// </summary>
internal record MultiplyNode(BaseNode First, BaseNode Second) : BaseNode
{
	internal override NodeResponse Evaluate(IRoller roller, IRoundingStrategy diceRoundingStrategy)
	{
		var firstEval = First.Evaluate(roller, diceRoundingStrategy);
		var secondEval = Second.Evaluate(roller, diceRoundingStrategy);

		return NodeResponse.Times(firstEval, secondEval);
	}

	public override string ToString()
	{
		return $"MULTIPLY({First},{Second})";
	}
}