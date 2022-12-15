namespace Dicer;

/// <summary>
///     Interface for implementing random number generators for rolling dice.
/// </summary>
internal interface IRandom
{
	/// <summary>
	///     Generates a random number in a format most suitable for dice rolls.
	/// </summary>
	/// <param name="dieSize"></param>
	/// <returns>A random number as if a die was rolled.</returns>
	int RollDice(int dieSize);
}