using System.Collections.Generic;

namespace Dicer;

internal readonly record struct RepeatingNode(INode Node, INode Repeat) : IRepeatingNode
{
	public IEnumerable<NodeResponse> Evaluate(Roller roller, RoundingStrategy roundingStrategy)
	{
		var repeatCount = (int)Repeat.Evaluate(roller, roundingStrategy).Result;

		for (var i = 0; i < repeatCount; i++)
		{
			yield return Node.Evaluate(roller, roundingStrategy);
		}
	}

	public override string ToString()
	{
		return $"REPEAT({Node},{Repeat})";
	}
}