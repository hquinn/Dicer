namespace Dicer.Models;

/// <summary>
///     Represents a single roll result.
/// </summary>
/// <param name="Result">Result of a roll.</param>
/// <param name="DieSize">Size of a die (aka how many sides the die has).</param>
public record Roll(int Result, int DieSize);