using System;

namespace Dicer.Rounding
{
	public class RoundToNearest : IRoundingStrategy
	{
		public double Round(double number)
		{
			return Math.Round(number);
		}
	}
}