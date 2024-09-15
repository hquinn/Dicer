namespace Dicer.Rollers;

/// <summary>
///     Rolls dice using the average roll for each die.
/// </summary>
internal class AverageDiceRoller : BaseDiceRoller
{
    protected override int RollSingleDice(int dieSize, int minimum, IRoundingStrategy roundingStrategy)
    {
        var average = (int)roundingStrategy.Round((dieSize + 1) / 2.0);

        return average < minimum && minimum <= dieSize ? minimum : average;
    }
}