using System.Security.Cryptography;

namespace Dicer.Randomizer
{
	public class DefaultRandom : IRandom
	{
		public int RollDice(int dieSize)
		{
			return RandomNumberGenerator.GetInt32(1, dieSize + 1);
		}
	}
}
