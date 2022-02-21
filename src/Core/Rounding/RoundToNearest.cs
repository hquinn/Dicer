using System;

namespace Dicer.Rounding;

/// <summary>
///     Will round the number to the nearest integer.
/// </summary>
public class RoundToNearest : IRoundingStrategy
{
	/// <inheritdoc />
	public double Round(double number)
	{
		return Math.Round(number);
	}
}