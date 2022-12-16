using System;

namespace Dicer;

internal static class RoundingStrategyFactory
{
	internal static IRoundingStrategy Create(this RoundingStrategy roundingStrategy)
	{
		return roundingStrategy switch
		{
			RoundingStrategy.RoundToFloor => new RoundToFloor(),
			RoundingStrategy.RoundToCeiling => new RoundToCeiling(),
			RoundingStrategy.RoundToNearest => new RoundToNearest(),
			RoundingStrategy.NoRounding => new NoRounding(),
			_ => throw new ArgumentOutOfRangeException(nameof(roundingStrategy), roundingStrategy, null)
		};
	}
}