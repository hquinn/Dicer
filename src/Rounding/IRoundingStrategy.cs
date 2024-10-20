﻿namespace Dicer.Rounding;

/// <summary>
///     Interface for implementing different rounding strategies.
/// </summary>
internal interface IRoundingStrategy
{
	/// <summary>
	///     Rounds the value using the specified strategy.
	/// </summary>
	/// <param name="number">The number to round.</param>
	/// <returns>A rounded number using the rounding strategy.</returns>
	double Round(double number);
}