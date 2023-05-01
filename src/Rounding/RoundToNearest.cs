using System;

namespace Dicer;

/// <summary>
///     Will round the number to the nearest integer.
/// </summary>
internal class RoundToNearest : IRoundingStrategy
{
	/// <inheritdoc />
	public double Round(double number)
	{
		return Math.Round(number);
	}
}