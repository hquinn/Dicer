using Dicer.Models;
using Dicer.Rounding;
using System;
using System.Linq;

namespace Dicer.Rollers;

public class AverageRoller : IRoller
{
	public RollResponse Roll(NodeResponse numDice, NodeResponse dieSize, NodeResponse? keep, IRoundingStrategy roundingStrategy)
	{
		var numDiceResult = (int)roundingStrategy.Round(numDice.Result);
		var dieSizeResult = (int)roundingStrategy.Round(dieSize.Result);
		var keepResult = keep is null ? numDiceResult : (int)roundingStrategy.Round(Math.Abs(keep.Result));
		keepResult = Math.Min(keepResult, numDiceResult);
		var average = (int)roundingStrategy.Round((dieSizeResult + 1) / 2.0);

		return new(average * keepResult, Enumerable.Repeat(new Roll(average, dieSizeResult), keepResult));
	}
}