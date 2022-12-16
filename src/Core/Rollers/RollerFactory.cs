using System;

namespace Dicer;

internal static class RollerFactory
{
	internal static IRoller Create(this Roller roller)
	{
		return roller switch
		{
			Roller.Random => new RandomRoller(),
			Roller.Min => new MinRoller(),
			Roller.Average => new AverageRoller(),
			Roller.Max => new MaxRoller(),
			_ => throw new ArgumentOutOfRangeException(nameof(roller), roller, null)
		};
	}
}