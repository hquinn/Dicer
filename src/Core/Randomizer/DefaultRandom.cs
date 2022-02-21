using System.Security.Cryptography;

namespace Dicer.Randomizer;

/// <summary>
///     Default implementation of <see cref="IRandom" />, which uses <see cref="RandomNumberGenerator" /> under the hood.
/// </summary>
public class DefaultRandom : IRandom
{
	/// <inheritdoc />
	public int RollDice(int dieSize)
	{
		return RandomNumberGenerator.GetInt32(1, dieSize + 1);
	}
}