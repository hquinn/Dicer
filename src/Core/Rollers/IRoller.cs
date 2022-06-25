using Dicer.Models;
using Dicer.Rounding;

namespace Dicer.Rollers;

/// <summary>
///     Interface for implementing different rolling strategies.
/// </summary>
public interface IRoller
{
	/// <summary>
	///     Rolls dice using the specified strategy.
	/// </summary>
	/// <param name="numDice">Number of dice to roll.</param>
	/// <param name="dieSize">The size of the dice (aka number of sides).</param>
	/// <param name="keep">
	///     How many dice to keep from highest to lowest. If value is null, then <paramref name="numDice" />
	///     will be used. If a negative number is specified, then the dice will be kept from lowest to highest.
	/// </param>
	/// <param name="roundingStrategy">The rounding strategy to use for rounding values.</param>
	/// <returns>The <see cref="RollResponse" /> of the dice rolls.</returns>
	RollResponse Roll(
		NodeResponse numDice,
		NodeResponse dieSize,
		NodeResponse? keep,
		NodeResponse? minimum,
		IRoundingStrategy roundingStrategy);
}