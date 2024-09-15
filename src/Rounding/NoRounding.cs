namespace Dicer.Rounding;

/// <summary>
///     Performs no rounding.
/// </summary>
internal class NoRounding : IRoundingStrategy
{
    /// <inheritdoc />
    public double Round(double number)
    {
        return number;
    }
}