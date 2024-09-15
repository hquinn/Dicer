namespace Dicer.Rollers;

/// <summary>
///     Rolls dice using the max roll for each die.
/// </summary>
internal class MaxDiceRoller : BaseDiceRoller
{
    protected override int RollSingleDice(int dieSize, int minimum, IRoundingStrategy roundingStrategy)
    {
        return dieSize;
    }
}