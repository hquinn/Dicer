using System;

namespace Dicer.Randomizer;

/// <summary>
///     Default implementation of <see cref="IRandom" />.
/// </summary>
internal class DefaultRandom : IRandom
{
    /// <inheritdoc />
    public int RollDice(int dieSize)
    {
        return Random.Shared.Next(1, dieSize + 1);
    }
}