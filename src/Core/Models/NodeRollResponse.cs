using Dicer.Rounding;
using System;

namespace Dicer.Models;

internal class NodeRollResponse
{
	public NodeRollResponse(double result, IRoundingStrategy roundingStrategy)
	{
		IsNegative = result < 0;
		Result = (int)roundingStrategy.Round(Math.Abs(result));
	}

	public int Result { get; }
	public bool IsNegative { get; }
};