using Dicer.Models;
using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes
{
	public class AddNode : INode
	{
		private readonly INode _first;
		private readonly INode _second;

		public AddNode(INode first, INode second)
		{
			_first = first;
			_second = second;
		}

		public NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
		{
			return NodeResponse.Plus(_first.Evaluate(roller, roundingStrategy), _second.Evaluate(roller, roundingStrategy), roundingStrategy);
		}
	}
}