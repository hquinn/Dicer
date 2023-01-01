using System;

namespace Dicer;

internal static class DiceRoundingStrategyFactory
{
    internal static IRoundingStrategy Create(this DiceRoundingStrategy diceRoundingStrategy)
    {
        return diceRoundingStrategy switch
        {
            DiceRoundingStrategy.RoundToFloor => new RoundToFloor(),
            DiceRoundingStrategy.RoundToCeiling => new RoundToCeiling(),
            DiceRoundingStrategy.RoundToNearest => new RoundToNearest(),
        };
    }
}