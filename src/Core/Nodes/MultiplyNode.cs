﻿using Dicer.Models;
using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes
{
	public class MultiplyNode : INode
	{
		private readonly INode _first;
		private readonly INode _second;

		public MultiplyNode(INode first, INode second)
		{
			_first = first;
			_second = second;
		}

		public NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
		{
			return NodeResponse.Times(_first.Evaluate(roller, roundingStrategy), _second.Evaluate(roller, roundingStrategy), roundingStrategy);
		}
	}
}