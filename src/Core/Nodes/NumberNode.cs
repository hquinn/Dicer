﻿using Dicer.Models;
using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes;

public class NumberNode : INode
{
	private readonly double _number;

	public NumberNode(double number)
	{
		_number = number;
	}

	public NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
	{
		return new(roundingStrategy.Round(_number));
	}

	public override string ToString()
	{
		return $"{_number}";
	}
}