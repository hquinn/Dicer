using System;

namespace Dicer.Rounding;

/// <summary>
///     Always rounds the number to the next lowest integer.
/// </summary>
internal class RoundToFloor : IRoundingStrategy
{
    /// <inheritdoc />
    public double Round(double number)
    {
        return Math.Floor(number);
    }
}