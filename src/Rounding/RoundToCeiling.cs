using System;

namespace Dicer.Rounding;

/// <summary>
///     Always rounds the number to the next highest integer.
/// </summary>
internal class RoundToCeiling : IRoundingStrategy
{
    /// <inheritdoc />
    public double Round(double number)
    {
        return Math.Ceiling(number);
    }
}