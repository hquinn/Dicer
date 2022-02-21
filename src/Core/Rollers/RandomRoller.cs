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

	protected override int RollSingleDice(int dieSize, IRoundingStrategy roundingStrategy)
	{
		return _random.RollDice(dieSize);
	}
}