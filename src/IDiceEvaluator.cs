using System.Collections.Generic;

namespace Dicer;

/// <summary>
///     Used to evaluate the dice expression.
///     Note: It's recommended to initialize this only once.
/// </summary>
public interface IDiceEvaluator
{
    /// <summary>
    ///     Calculates the dice expression.
    /// </summary>
    /// <param name="expression">The <see cref="IDiceExpression" /> to evaluate.</param>
    /// <param name="selectedRoller">The roller to use when rolling dice.</param>
    /// <param name="selectedRoundingStrategy">The rounding strategy to use when rounding the result.</param>
    /// <param name="selectedDiceRoundingStrategy">The rounding strategy to use when rounding dice values.</param>
    /// <returns>The <see cref="ExpressionResponse" /> of the resulting calculations.</returns>
    ExpressionResponse Evaluate(
        IDiceExpression expression,
        Roller selectedRoller = Roller.Random,
        RoundingStrategy selectedRoundingStrategy = RoundingStrategy.RoundToFloor,
        DiceRoundingStrategy selectedDiceRoundingStrategy = DiceRoundingStrategy.RoundToCeiling);

    /// <summary>
    ///     Calculates the dice expression.
    /// </summary>
    /// <param name="expression">The <see cref="IDiceExpression" /> to evaluate.</param>
    /// <param name="numberOfTimes">The number of times to evaluate this expression.</param>
    /// <param name="selectedRoller">The roller to use when rolling dice.</param>
    /// <param name="selectedRoundingStrategy">The rounding strategy to use when rounding the result.</param>
    /// <param name="selectedDiceRoundingStrategy">The rounding strategy to use when rounding dice values.</param>
    /// <returns>The <see cref="ExpressionResponse" /> of the resulting calculations.</returns>
    IReadOnlyCollection<ExpressionResponse> Evaluate(
        IDiceExpression expression,
        int numberOfTimes,
        Roller selectedRoller = Roller.Random,
        RoundingStrategy selectedRoundingStrategy = RoundingStrategy.RoundToFloor,
        DiceRoundingStrategy selectedDiceRoundingStrategy = DiceRoundingStrategy.RoundToCeiling);

    /// <summary>
    ///     Calculates the dice expression.
    /// </summary>
    /// <param name="expression">The <see cref="IDiceExpression" /> to evaluate.</param>
    /// <param name="numberOfTimesExpression">The number of times to evaluate this expression.</param>
    /// <param name="selectedRoller">The roller to use when rolling dice.</param>
    /// <param name="selectedRoundingStrategy">The rounding strategy to use when rounding the result.</param>
    /// <param name="selectedDiceRoundingStrategy">The rounding strategy to use when rounding dice values.</param>
    /// <returns>The <see cref="ExpressionResponse" /> of the resulting calculations.</returns>
    IReadOnlyCollection<ExpressionResponse> Evaluate(
        IDiceExpression expression,
        IDiceExpression numberOfTimesExpression,
        Roller selectedRoller = Roller.Random,
        RoundingStrategy selectedRoundingStrategy = RoundingStrategy.RoundToFloor,
        DiceRoundingStrategy selectedDiceRoundingStrategy = DiceRoundingStrategy.RoundToCeiling);
    
    /// <summary>
    ///     Calculates the dice expressionToParse.
    /// </summary>
    /// <param name="expressionToParse">The expressionToParse to parse and evaluate.</param>
    /// <param name="selectedRoller">The roller to use when rolling dice.</param>
    /// <param name="selectedRoundingStrategy">The rounding strategy to use when rounding the result.</param>
    /// <param name="selectedDiceRoundingStrategy">The rounding strategy to use when rounding dice values.</param>
    /// <returns>The <see cref="ExpressionResponse" /> of the resulting calculations.</returns>
    ExpressionResponse Evaluate(
        string expressionToParse,
        Roller selectedRoller = Roller.Random,
        RoundingStrategy selectedRoundingStrategy = RoundingStrategy.RoundToFloor,
        DiceRoundingStrategy selectedDiceRoundingStrategy = DiceRoundingStrategy.RoundToCeiling);

    /// <summary>
    ///     Calculates the dice expressionToParse.
    /// </summary>
    /// <param name="expressionToParse">The expressionToParse to parse and evaluate.</param>
    /// <param name="numberOfTimes">The number of times to evaluate this expressionToParse.</param>
    /// <param name="selectedRoller">The roller to use when rolling dice.</param>
    /// <param name="selectedRoundingStrategy">The rounding strategy to use when rounding the result.</param>
    /// <param name="selectedDiceRoundingStrategy">The rounding strategy to use when rounding dice values.</param>
    /// <returns>The <see cref="ExpressionResponse" /> of the resulting calculations.</returns>
    IReadOnlyCollection<ExpressionResponse> Evaluate(
        string expressionToParse,
        int numberOfTimes,
        Roller selectedRoller = Roller.Random,
        RoundingStrategy selectedRoundingStrategy = RoundingStrategy.RoundToFloor,
        DiceRoundingStrategy selectedDiceRoundingStrategy = DiceRoundingStrategy.RoundToCeiling);

    /// <summary>
    ///     Calculates the dice expressionToParse.
    /// </summary>
    /// <param name="expressionToParse">The expressionToParse to parse and evaluate.</param>
    /// <param name="numberOfTimesExpressionToParse">The number of times to evaluate this expressionToParse once parsed.</param>
    /// <param name="selectedRoller">The roller to use when rolling dice.</param>
    /// <param name="selectedRoundingStrategy">The rounding strategy to use when rounding the result.</param>
    /// <param name="selectedDiceRoundingStrategy">The rounding strategy to use when rounding dice values.</param>
    /// <returns>The <see cref="ExpressionResponse" /> of the resulting calculations.</returns>
    IReadOnlyCollection<ExpressionResponse> Evaluate(
        string expressionToParse,
        string numberOfTimesExpressionToParse,
        Roller selectedRoller = Roller.Random,
        RoundingStrategy selectedRoundingStrategy = RoundingStrategy.RoundToFloor,
        DiceRoundingStrategy selectedDiceRoundingStrategy = DiceRoundingStrategy.RoundToCeiling);
}