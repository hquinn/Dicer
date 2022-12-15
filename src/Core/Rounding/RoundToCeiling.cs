using System;

namespace Dicer;

/// <summary>
///     Always rounds the number to the next highest integer.
/// </summary>
public class RoundToCeiling : IRoundingStrategy
{
	/// <inheritdoc />
	public double Round(double number)
	{
		return Math.Ceiling(number);
	}
}