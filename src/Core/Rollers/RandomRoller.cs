using Dicer.Randomizer;
using Dicer.Rounding;

namespace Dicer.Rollers;

/// <summary>
///     Rolls dice using a random roll for each die.
/// </summary>
public class RandomRoller : BaseRoller
{
	private readonly IRandom _random;

	public RandomRoller()
	{
		_random = new DefaultRandom();
	}

	public RandomRoller(IRandom random)
	{
		_random = random;
	}

	protected override int RollSingleDice(int dieSize, int minimum, IRoundingStrategy roundingStrategy)
	{
		var roll = _random.RollDice(dieSize);

		return roll < minimum && minimum <= dieSize ? minimum : roll;
	}
}