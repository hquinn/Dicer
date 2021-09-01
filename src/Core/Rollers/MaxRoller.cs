using Dicer.Models;
using Dicer.Rounding;
using System;
using System.Linq;

namespace Dicer.Rollers
{
	public class MaxRoller : IRoller
	{
		public RollResponse Roll(NodeResponse numDice, NodeResponse dieSize, NodeResponse? keep, IRoundingStrategy roundingStrategy)
		{
			var numDiceResult = (int)roundingStrategy.Round(numDice.Result);
			var dieSizeResult = (int)roundingStrategy.Round(dieSize.Result);
			var keepResult = keep is null ? numDiceResult : (int)roundingStrategy.Round(Math.Abs(keep.Result));
			keepResult = Math.Min(keepResult, numDiceResult);

			return new(dieSizeResult * keepResult, Enumerable.Repeat(new Roll(dieSizeResult, dieSizeResult), keepResult));
		}
	}
}