using Dicer.Rounding;

namespace Dicer.Rollers;

/// <summary>
///     Rolls dice using the average roll for each die.
/// </summary>
public class AverageRoller : BaseRoller
{
	protected override int RollSingleDice(int dieSize, IRoundingStrategy roundingStrategy)
	{
		return (int)roundingStrategy.Round((dieSize + 1) / 2.0);
	}
}