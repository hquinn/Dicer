using Dicer.Models;
using Dicer.Rounding;

namespace Dicer.Rollers;

public interface IRoller
{
	RollResponse Roll(NodeResponse numDice, NodeResponse dieSize, NodeResponse? keep, IRoundingStrategy roundingStrategy);
}