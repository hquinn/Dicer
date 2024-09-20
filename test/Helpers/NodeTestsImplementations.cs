using System.Collections.Generic;
using System.Linq;
using Dicer.Tests.Factories;
using FluentAssertions;
using static Dicer.Tests.Helpers.EnumerableExtensions;

namespace Dicer.Tests.Helpers;

public static class NodeTestsImplementations
{
    public static void PerformCombinableDiceNotationTest(string expression, double result)
    {
        const Roller roller = Roller.Random;
        var firstRollResponse = RollResponseFactory.Create(6, Roll(2, 3, 4), Roll(1));
        var secondRollResponse = RollResponseFactory.Create(10, Roll(5, 6), null);
        var expected = new ExpressionResponse(result, Combine(firstRollResponse, secondRollResponse));

        PerformDiceNotationTest(expression, roller, DiceRoundingStrategy.RoundToCeiling, expected);
    }

    public static void PerformNoDiceNotationTest(
        string expression,
        double expected,
        RoundingStrategy roundingStrategy)
    {
        // Arrange
        var sut = DiceEvaluatorFactory.Create();
        var diceExpression = DiceExpressionParser.Parse(expression);

        // Act
        var actual = sut.Evaluate(diceExpression, selectedRoundingStrategy: roundingStrategy);

        // Assert
        actual.Result.Should().BeApproximately(expected, 0.01F);
        actual.RollResponses.Should().BeEmpty();
    }

    public static void PerformBasicDiceNotationTest(
        string expression,
        Roller roller,
        IReadOnlyCollection<int> diceRolls,
        IReadOnlyCollection<int>? discarded = null,
        DiceRoundingStrategy diceRoundingStrategy = DiceRoundingStrategy.RoundToCeiling,
        int dieSize = 6)
    {
        var expected =
            new ExpressionResponse(diceRolls.Sum(), RollResponseFactory.Create(dieSize, diceRolls, discarded));
        PerformDiceNotationTest(expression, roller, diceRoundingStrategy, expected);
    }

    public static void PerformDiceNotationTest(
        string expression,
        Roller roller,
        DiceRoundingStrategy diceRoundingStrategy,
        ExpressionResponse expected)
    {
        // Arrange
        var sut = DiceEvaluatorFactory.Create();

        // Act
        var actual = sut.Evaluate(expression, roller, RoundingStrategy.NoRounding, diceRoundingStrategy);

        // Assert

        actual.Result.Should().BeApproximately(expected.Result, 0.01F, roller.ToString());
        actual.RollResponses.Should().BeEquivalentTo(expected.RollResponses, roller.ToString());
    }
}