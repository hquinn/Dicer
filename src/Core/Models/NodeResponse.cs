using Dicer.Helpers;
using Dicer.Rounding;
using System.Collections.Generic;

namespace Dicer.Models
{
	public record NodeResponse(double Result, IEnumerable<RollResponse>? RollResponses = null)
	{
		public static NodeResponse Plus(NodeResponse first, NodeResponse second, IRoundingStrategy roundingStrategy)
		{
			var calculation = roundingStrategy.Round(first.Result + second.Result);

			return CreateNodeResponse(calculation, first.RollResponses, second.RollResponses);
		}

		public static NodeResponse Minus(NodeResponse first, NodeResponse second, IRoundingStrategy roundingStrategy)
		{
			var calculation = roundingStrategy.Round(first.Result - second.Result);

			return CreateNodeResponse(calculation, first.RollResponses, second.RollResponses);
		}

		public static NodeResponse Times(NodeResponse first, NodeResponse second, IRoundingStrategy roundingStrategy)
		{
			var calculation = roundingStrategy.Round(first.Result * second.Result);

			return CreateNodeResponse(calculation, first.RollResponses, second.RollResponses);
		}

		public static NodeResponse Divide(NodeResponse first, NodeResponse second, IRoundingStrategy roundingStrategy)
		{
			var calculation = roundingStrategy.Round(first.Result / second.Result);

			return CreateNodeResponse(calculation, first.RollResponses, second.RollResponses);
		}

		public static NodeResponse Unary(NodeResponse node, IRoundingStrategy roundingStrategy)
		{
			var calculation = -roundingStrategy.Round(node.Result);

			return CreateNodeResponse(calculation, node.RollResponses);
		}

		private static NodeResponse CreateNodeResponse(double result, params IEnumerable<RollResponse>?[] rolls)
		{
			return new(result, RollHelpers.Merge(rolls));
		}
	}
}