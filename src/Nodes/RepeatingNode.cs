using System.Collections.Generic;

namespace Dicer;

internal record RepeatingNode(INode Node, INode Repeat) : IRepeatingNode
{
	public IEnumerable<NodeResponse> Evaluate(
		Roller roller, 
		RoundingStrategy roundingStrategy,
		DiceRoundingStrategy diceRoundingStrategy)
	{
		var repeatCount = (int)Repeat.Evaluate(roller, roundingStrategy, diceRoundingStrategy).Result;

		for (var i = 0; i < repeatCount; i++)
		{
			yield return Node.Evaluate(roller, roundingStrategy, diceRoundingStrategy);
		}
	}

	public override string ToString()
	{
		return $"REPEAT({Node},{Repeat})";
	}
}