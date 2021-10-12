using Dicer.Rounding;

namespace Dicer.Rollers;

/// <summary>
/// Rolls dice using the min roll for each die.
/// </summary>
public class MinRoller : BaseRoller
{
	protected override int RollSingleDice(int dieSize, IRoundingStrategy roundingStrategy)
	{
		return 1;
	}
}