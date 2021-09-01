using System;

namespace Dicer.Rounding
{
	public class RoundToCeiling : IRoundingStrategy
	{
		public double Round(double number)
		{
			return Math.Ceiling(number);
		}
	}
}