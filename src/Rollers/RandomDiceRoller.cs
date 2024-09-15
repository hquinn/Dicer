namespace Dicer.Rollers;

/// <summary>
///     Rolls dice using a random roll for each die.
/// </summary>
internal class RandomDiceRoller : BaseDiceRoller
{
    private readonly IRandom _random;

    public RandomDiceRoller(IRandom random)
    {
        _random = random;
    }

    protected override int RollSingleDice(int dieSize, int minimum, IRoundingStrategy roundingStrategy)
    {
        var roll = _random.RollDice(dieSize);

        return roll < minimum && minimum <= dieSize ? minimum : roll;
    }
}