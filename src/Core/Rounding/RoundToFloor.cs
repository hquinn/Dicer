using System;

namespace Dicer;

/// <summary>
///     Always rounds the number to the next lowest integer.
/// </summary>
public class RoundToFloor : IRoundingStrategy
{
	/// <inheritdoc />
	public double Round(double number)
	{
		return Math.Floor(number);
	}
}