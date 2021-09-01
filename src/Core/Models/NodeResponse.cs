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

			return CreateNodeResponse(calculation, first, second);
		}

		public static NodeResponse Minus(NodeResponse first, NodeResponse second, IRoundingStrategy roundingStrategy)
		{
			var calculation = roundingStrategy.Round(first.Result - second.Result);

			return CreateNodeResponse(calculation, first, second);
		}

		public static NodeResponse Times(NodeResponse first, NodeResponse second, IRoundingStrategy roundingStrategy)
		{
			var calculation = roundingStrategy.Round(first.Result * second.Result);

			return CreateNodeResponse(calculation, first, second);
		}

		public static NodeResponse Divide(NodeResponse first, NodeResponse second, IRoundingStrategy roundingStrategy)
		{
			var calculation = roundingStrategy.Round(first.Result / second.Result);

			return CreateNodeResponse(calculation, first, second);
		}

		private static NodeResponse CreateNodeResponse(double result, NodeResponse first, NodeResponse second)
		{
			return new(result, RollHelpers.Merge(first.RollResponses, second.RollResponses));
		}
	}
}