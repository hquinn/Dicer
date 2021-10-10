using Dicer.Models;
using Dicer.Rounding;
using System;

namespace Dicer.Rollers;

internal class NodeRollResponse
{
	public NodeRollResponse(NodeResponse response, IRoundingStrategy roundingStrategy)
	{
		Response = response;
		IsNegative = response.Result < 0;
		Result = (int)roundingStrategy.Round(Math.Abs(response.Result));
	}

	public NodeResponse Response { get; }
	public int Result { get; }
	public bool IsNegative { get; }
};