namespace Dicer;

/// <summary>
///     Node for subtracting two <see cref="INode" /> together.
/// </summary>
internal record SubtractNode(BaseNode First, BaseNode Second) : BaseNode
{
	internal override NodeResponse Evaluate(IRoller roller, IRoundingStrategy diceRoundingStrategy)
	{
		var firstEval = First.Evaluate(roller, diceRoundingStrategy);
		var secondEval = Second.Evaluate(roller, diceRoundingStrategy);

		return NodeResponse.Minus(firstEval, secondEval);
	}

	public override string ToString()
	{
		return $"SUBTRACT({First},{Second})";
	}
}