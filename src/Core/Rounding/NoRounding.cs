namespace Dicer.Rounding
{
	public class NoRounding : IRoundingStrategy
	{
		public double Round(double number)
		{
			return number;
		}
	}
}
