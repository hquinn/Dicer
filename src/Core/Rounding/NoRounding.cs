namespace Dicer;

/// <summary>
/// Performs no rounding.
/// </summary>
public class NoRounding : IRoundingStrategy
{
	/// <inheritdoc />
	public double Round(double number)
	{
		return number;
	}
}