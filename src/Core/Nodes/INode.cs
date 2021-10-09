using Dicer.Models;
using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes;

public interface INode
{
	NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy);
}