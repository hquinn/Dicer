using Dicer.Rounding;

namespace Dicer.Rollers;

/// <summary>
/// Rolls dice using the max roll for each die.
/// </summary>
public class MaxRoller : BaseRoller
{
	protected override int RollSingleDice(int dieSize, IRoundingStrategy roundingStrategy)
	{
		return dieSize;
	}
}