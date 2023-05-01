namespace Dicer;

/// <summary>
///     Node for adding two <see cref="INode" /> together.
/// </summary>
internal record AddNode(BaseNode First, BaseNode Second) : BaseNode
{
	internal override NodeResponse Evaluate(IRoller roller, IRoundingStrategy diceRoundingStrategy)
	{
		var firstEval = First.Evaluate(roller, diceRoundingStrategy);
		var secondEval = Second.Evaluate(roller, diceRoundingStrategy);
		
		return NodeResponse.Plus(firstEval, secondEval);
	}	

	public override string ToString()
	{
		return $"ADD({First},{Second})";
	}
}