using System;

namespace Dicer;

internal static class RollerFactory
{
	private static IRandom random = new DefaultRandom();

	internal static void SetRandom(IRandom random)
	{
		RollerFactory.random = random;
	}
	
	
	internal static IRoller Create(this Roller roller)
	{
		return roller switch
		{
			Roller.Random => new RandomRoller(random),
			Roller.Min => new MinRoller(),
			Roller.Average => new AverageRoller(),
			Roller.Max => new MaxRoller(),
		};
	}
}