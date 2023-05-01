using System.Collections.Generic;
using System.Linq;

namespace Dicer;

/// <summary>
///     Represents the result from calculating a <see cref="INode" />, including the <paramref name="Result" />
///     and the <paramref name="RollResponses" />.
/// </summary>
/// <param name="Result">Final result of the calculation from the <see cref="INode" />.</param>
/// <param name="RollResponses">All rolls and results which occurred from the <see cref="INode" />.</param>
public record NodeResponse
{
	public double Result { get; }
	public IEnumerable<RollResponse> RollResponses { get; }
	
	public NodeResponse(double result, IEnumerable<RollResponse>? rollResponses = null)
	{
		Result = result;
		RollResponses = rollResponses ?? Enumerable.Empty<RollResponse>();
	}
	
	internal static NodeResponse Plus(NodeResponse first, NodeResponse second)
	{
		var calculation = first.Result + second.Result;

		return CreateNodeResponse(calculation, first.RollResponses, second.RollResponses);
	}

	internal static NodeResponse Minus(NodeResponse first, NodeResponse second)
	{
		var calculation = first.Result - second.Result;

		return CreateNodeResponse(calculation, first.RollResponses, second.RollResponses);
	}

	internal static NodeResponse Times(NodeResponse first, NodeResponse second)
	{
		var calculation = first.Result * second.Result;

		return CreateNodeResponse(calculation, first.RollResponses, second.RollResponses);
	}

	internal static NodeResponse Divide(NodeResponse first, NodeResponse second)
	{
		var calculation = first.Result / second.Result;

		return CreateNodeResponse(calculation, first.RollResponses, second.RollResponses);
	}

	internal static NodeResponse Unary(NodeResponse node)
	{
		var calculation = -node.Result;

		return CreateNodeResponse(calculation, node.RollResponses);
	}

	private static NodeResponse CreateNodeResponse(double result, params IEnumerable<RollResponse>[] rolls)
	{
		return new(result, RollHelpers.Merge(rolls));
	}
}