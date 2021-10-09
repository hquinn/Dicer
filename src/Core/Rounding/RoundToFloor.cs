using System;

namespace Dicer.Rounding;

public class RoundToFloor : IRoundingStrategy
{
	public double Round(double number)
	{
		return Math.Floor(number);
	}
}