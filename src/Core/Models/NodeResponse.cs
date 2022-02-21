using Dicer.Helpers;
using Dicer.Rounding;
using System.Collections.Generic;

namespace Dicer.Models;

/// <summary>
///     Represents the result from calculating a <see cref="Dicer.Nodes.INode" />, including the <paramref name="Result" />
///     and the <paramref name="RollResponses" />.
/// </summary>
/// <param name="Result">Final result of the calculation from the <see cref="Dicer.Nodes.INode" />.</param>
/// <param name="RollResponses">All rolls and results which occurred from the <see cref="Dicer.Nodes.INode" />.</param>
public record NodeResponse(double Result, IEnumerable<RollResponse>? RollResponses = null)
{
	internal static NodeResponse Plus(NodeResponse first, NodeResponse second, IRoundingStrategy roundingStrategy)
	{
		var calculation = roundingStrategy.Round(first.Result + second.Result);

		return CreateNodeResponse(calculation, first.RollResponses, second.RollResponses);
	}

	internal static NodeResponse Minus(NodeResponse first, NodeResponse second, IRoundingStrategy roundingStrategy)
	{
		var calculation = roundingStrategy.Round(first.Result - second.Result);

		return CreateNodeResponse(calculation, first.RollResponses, second.RollResponses);
	}

	internal static NodeResponse Times(NodeResponse first, NodeResponse second, IRoundingStrategy roundingStrategy)
	{
		var calculation = roundingStrategy.Round(first.Result * second.Result);

		return CreateNodeResponse(calculation, first.RollResponses, second.RollResponses);
	}

	internal static NodeResponse Divide(NodeResponse first, NodeResponse second, IRoundingStrategy roundingStrategy)
	{
		var calculation = roundingStrategy.Round(first.Result / second.Result);

		return CreateNodeResponse(calculation, first.RollResponses, second.RollResponses);
	}

	internal static NodeResponse Unary(NodeResponse node, IRoundingStrategy roundingStrategy)
	{
		var calculation = -roundingStrategy.Round(node.Result);

		return CreateNodeResponse(calculation, node.RollResponses);
	}

	private static NodeResponse CreateNodeResponse(double result, params IEnumerable<RollResponse>?[] rolls)
	{
		return new(result, RollHelpers.Merge(rolls));
	}
}