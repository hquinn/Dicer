using Dicer.Models;
using Dicer.Rounding;
using System;
using System.Linq;

namespace Dicer.Rollers;

/// <summary>
/// Rolls dice using the min roll for each die.
/// </summary>
public class MinRoller : IRoller
{
	/// <inheritdoc />
	public RollResponse Roll(NodeResponse numDice, NodeResponse dieSize, NodeResponse? keep, IRoundingStrategy roundingStrategy)
	{
		var numDiceResult = (int)roundingStrategy.Round(numDice.Result);
		var dieSizeResult = (int)roundingStrategy.Round(dieSize.Result);
		var keepResult = keep is null ? numDiceResult : (int)roundingStrategy.Round(Math.Abs(keep.Result));
		keepResult = Math.Min(keepResult, numDiceResult);

		return new(keepResult, Enumerable.Repeat(new Roll(1, dieSizeResult), keepResult));
	}
}