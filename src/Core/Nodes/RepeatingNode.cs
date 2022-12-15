﻿using System.Collections.Generic;

namespace Dicer;

internal class RepeatingNode : IRepeatingNode
{
	private readonly INode _node;
	private readonly INode _repeat;

	public RepeatingNode(INode node, INode repeat)
	{
		_node = node;
		_repeat = repeat;
	}

	/// <inheritdoc />
	public IEnumerable<NodeResponse> Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
	{
		var repeatCount = (int)_repeat.Evaluate(roller, roundingStrategy).Result;

		for (var i = 0; i < repeatCount; i++)
		{
			yield return _node.Evaluate(roller, roundingStrategy);
		}
	}

	public override string ToString()
	{
		return $"REPEAT({_node},{_repeat})";
	}
}