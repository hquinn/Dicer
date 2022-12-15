﻿namespace Dicer;

/// <summary>
///     Rolls dice using the max roll for each die.
/// </summary>
internal class MaxRoller : BaseRoller
{
	protected override int RollSingleDice(int dieSize, int minimum, IRoundingStrategy roundingStrategy)
	{
		return dieSize;
	}
}