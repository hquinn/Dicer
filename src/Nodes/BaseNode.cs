namespace Dicer;

internal abstract record BaseNode : INode
{
	public NodeResponse Evaluate(
		Roller selectedRoller, 
		RoundingStrategy selectedRoundingStrategy, 
		DiceRoundingStrategy selectedDiceRoundingStrategy)
	{
		var roller = selectedRoller.Create();
		var roundingStrategy = selectedRoundingStrategy.Create();
		var diceRoundingStrategy = selectedDiceRoundingStrategy.Create();
		
		var result = Evaluate(roller, diceRoundingStrategy);

		return new NodeResponse(roundingStrategy.Round(result.Result), result.RollResponses);
	}

	internal abstract NodeResponse Evaluate(IRoller roller, IRoundingStrategy diceRoundingStrategy);
}