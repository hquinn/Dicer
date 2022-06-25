using Dicer.Rounding;
using System;

namespace Dicer.Models;

internal class NodeRollResponse
{
	public NodeRollResponse(NodeResponse? result, IRoundingStrategy roundingStrategy)
	{
		var value = result?.Result ?? 0;

		IsNegative = value < 0;
		Result = (int)roundingStrategy.Round(Math.Abs(value));
	}

	public int Result { get; }
	public bool IsNegative { get; }
}