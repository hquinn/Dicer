namespace Dicer;

/// <summary>
///     Interface for implementing random number generators for rolling dice.
/// </summary>
public interface IRandom
{
    /// <summary>
    ///     Generates a random number in a format most suitable for dice rolls.
    /// </summary>
    /// <param name="dieSize">The size of the die to be rolled.</param>
    /// <returns>A random number as if a die was rolled.</returns>
    int RollDice(int dieSize);
}