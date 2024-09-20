using Dicer.Rounding;

namespace Dicer.Rollers;

/// <summary>
///     Rolls dice using the min roll for each die.
/// </summary>
internal class MinDiceRoller : BaseDiceRoller
{
    protected override int RollSingleDice(int dieSize, int minimum, IRoundingStrategy roundingStrategy)
    {
        return minimum > 0 && minimum <= dieSize ? minimum : 1;
    }
}