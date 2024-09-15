using System.Linq;
using Dicer.Tests.Factories;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using static Dicer.Tests.Helpers.EnumerableExtensions;
using static Dicer.Tests.Helpers.NodeTestsImplementations;

namespace Dicer.Tests;

public class DiceEvaluatorTests
{
    [Theory]
    [InlineData("1", 1)]
    [InlineData("2.51", 2.51)]
    [InlineData("3.14", 3.14)]
    [InlineData("1+1", 2)]
    [InlineData("1.1+1.41", 2.51)]
    [InlineData("2.01+1.13", 3.14)]
    [InlineData("2-1", 1)]
    [InlineData("2.41-1.1", 1.31)]
    [InlineData("2.61-1.1", 1.51)]
    [InlineData("2*2", 4)]
    [InlineData("1.2*1.1", 1.32)]
    [InlineData("2.1*2.2", 4.62)]
    [InlineData("2/2", 1)]
    [InlineData("5/2", 2.5)]
    [InlineData("2.5/1.2", 2.083)]
    [InlineData("-2", -2)]
    [InlineData("--2", 2)]
    [InlineData("--3.14", 3.14)]
    public void ShouldNotRoundExpressionWithoutDiceNotation_WhenRoundingStrategyIsNoRounding(
        string expression,
        double expected)
    {
        PerformNoDiceNotationTest(expression, expected, RoundingStrategy.NoRounding);
    }

    [Theory]
    [InlineData("1", 1)]
    [InlineData("2.51", 2)]
    [InlineData("3.14", 3)]
    [InlineData("1+1", 2)]
    [InlineData("1.1+1.41", 2)]
    [InlineData("2.01+1.13", 3)]
    [InlineData("2-1", 1)]
    [InlineData("2.41-1.1", 1)]
    [InlineData("2.61-1.1", 1)]
    [InlineData("2*2", 4)]
    [InlineData("1.2*1.1", 1)]
    [InlineData("2.1*2.2", 4)]
    [InlineData("2/2", 1)]
    [InlineData("5/2", 2)]
    [InlineData("2.5/1.2", 2)]
    [InlineData("-2", -2)]
    [InlineData("--2", 2)]
    [InlineData("--3.14", 3)]
    public void ShouldRoundExpressionWithoutDiceNotationToFloor_WhenRoundingStrategyIsRoundToFloor(
        string expression,
        double expected)
    {
        PerformNoDiceNotationTest(expression, expected, RoundingStrategy.RoundToFloor);
    }

    [Theory]
    [InlineData("1", 1)]
    [InlineData("2.51", 3)]
    [InlineData("3.14", 4)]
    [InlineData("1+1", 2)]
    [InlineData("1.1+1.41", 3)]
    [InlineData("2.01+1.13", 4)]
    [InlineData("2-1", 1)]
    [InlineData("2.41-1.1", 2)]
    [InlineData("2.61-1.1", 2)]
    [InlineData("2*2", 4)]
    [InlineData("1.2*1.1", 2)]
    [InlineData("2.1*2.2", 5)]
    [InlineData("2/2", 1)]
    [InlineData("5/2", 3)]
    [InlineData("2.5/1.2", 3)]
    [InlineData("-2", -2)]
    [InlineData("--2", 2)]
    [InlineData("--3.14", 4)]
    public void ShouldRoundExpressionWithoutDiceNotationToCeiling_WhenRoundingStrategyIsRoundToCeiling(
        string expression,
        double expected)
    {
        PerformNoDiceNotationTest(expression, expected, RoundingStrategy.RoundToCeiling);
    }

    [Theory]
    [InlineData("1", 1)]
    [InlineData("2.51", 3)]
    [InlineData("3.14", 3)]
    [InlineData("1+1", 2)]
    [InlineData("1.1+1.41", 3)]
    [InlineData("2.01+1.13", 3)]
    [InlineData("2-1", 1)]
    [InlineData("2.41-1.1", 1)]
    [InlineData("2.61-1.1", 2)]
    [InlineData("2*2", 4)]
    [InlineData("1.2*1.1", 1)]
    [InlineData("2.1*2.2", 5)]
    [InlineData("2/2", 1)]
    [InlineData("5/2", 2)]
    [InlineData("2.5/1.2", 2)]
    [InlineData("-2", -2)]
    [InlineData("--2", 2)]
    [InlineData("--3.14", 3)]
    public void ShouldRoundExpressionWithoutDiceNotationToNearest_WhenRoundingStrategyIsRoundToNearest(
        string expression,
        double expected)
    {
        PerformNoDiceNotationTest(expression, expected, RoundingStrategy.RoundToNearest);
    }

    [Fact]
    public void ShouldRollDiceForNormalDiceNotationExpression_WhenRollerIsSet()
    {
        const string expression = "3d6";

        using (new AssertionScope())
        {
            PerformBasicDiceNotationTest(expression, Roller.Max, 6.Repeat(3));
            PerformBasicDiceNotationTest(expression, Roller.Average, 4.Repeat(3));
            PerformBasicDiceNotationTest(expression, Roller.Min, 1.Repeat(3));
            PerformBasicDiceNotationTest(expression, Roller.Random, Roll(1, 2, 3));
        }
    }

    [Fact]
    public void ShouldRollNegativeDiceForNormalDiceNotationExpression_WhenDiceValuesAreNegative()
    {
        const string firstExpression = "-3d-6";
        const string secondExpression = "-3d6";
        const string thirdExpression = "3d-6";

        using (new AssertionScope())
        {
            PerformBasicDiceNotationTest(firstExpression, Roller.Max, 6.Repeat(3), dieSize: -6);
            PerformBasicDiceNotationTest(firstExpression, Roller.Average, 4.Repeat(3), dieSize: -6);
            PerformBasicDiceNotationTest(firstExpression, Roller.Min, 1.Repeat(3), dieSize: -6);
            PerformBasicDiceNotationTest(firstExpression, Roller.Random, Roll(1, 2, 3), dieSize: -6);

            PerformBasicDiceNotationTest(secondExpression, Roller.Max, (-6).Repeat(3), dieSize: 6);
            PerformBasicDiceNotationTest(secondExpression, Roller.Average, (-4).Repeat(3), dieSize: 6);
            PerformBasicDiceNotationTest(secondExpression, Roller.Min, (-1).Repeat(3), dieSize: 6);
            PerformBasicDiceNotationTest(secondExpression, Roller.Random, Roll(-1, -2, -3), dieSize: 6);

            PerformBasicDiceNotationTest(thirdExpression, Roller.Max, (-6).Repeat(3), dieSize: -6);
            PerformBasicDiceNotationTest(thirdExpression, Roller.Average, (-4).Repeat(3), dieSize: -6);
            PerformBasicDiceNotationTest(thirdExpression, Roller.Min, (-1).Repeat(3), dieSize: -6);
            PerformBasicDiceNotationTest(thirdExpression, Roller.Random, Roll(-1, -2, -3), dieSize: -6);
        }
    }

    [Fact]
    public void ShouldKeepHighestRolls_WhenExpressionHasAPositiveKeep()
    {
        const string expression = "4d6k3";

        using (new AssertionScope())
        {
            PerformBasicDiceNotationTest(expression, Roller.Max, 6.Repeat(3), Roll(6));
            PerformBasicDiceNotationTest(expression, Roller.Average, 4.Repeat(3), Roll(4));
            PerformBasicDiceNotationTest(expression, Roller.Min, 1.Repeat(3), Roll(1));
            PerformBasicDiceNotationTest(expression, Roller.Random, Roll(2, 3, 4), Roll(1));
        }
    }

    [Fact]
    public void ShouldKeepLowestRolls_WhenExpressionHasANegativeKeep()
    {
        const string expression = "4d6k-3";

        using (new AssertionScope())
        {
            PerformBasicDiceNotationTest(expression, Roller.Max, 6.Repeat(3), Roll(6));
            PerformBasicDiceNotationTest(expression, Roller.Average, 4.Repeat(3), Roll(4));
            PerformBasicDiceNotationTest(expression, Roller.Min, 1.Repeat(3), Roll(1));
            PerformBasicDiceNotationTest(expression, Roller.Random, Roll(1, 2, 3), Roll(4));
        }
    }

    [Fact]
    public void ShouldRollWithMinimumValues_WhenExpressionHasAMinimumBelowDieSize()
    {
        const string expression = "3d6m5";

        using (new AssertionScope())
        {
            PerformBasicDiceNotationTest(expression, Roller.Max, 6.Repeat(3));
            PerformBasicDiceNotationTest(expression, Roller.Average, 5.Repeat(3));
            PerformBasicDiceNotationTest(expression, Roller.Min, 5.Repeat(3));
            PerformBasicDiceNotationTest("3d6m2", Roller.Random, Roll(2, 2, 3));
        }
    }

    [Fact]
    public void ShouldNotRollWithMinimumValues_WhenExpressionHasAMinimumAboveDieSize()
    {
        const string expression = "3d6m7";

        using (new AssertionScope())
        {
            PerformBasicDiceNotationTest(expression, Roller.Max, 6.Repeat(3));
            PerformBasicDiceNotationTest(expression, Roller.Average, 4.Repeat(3));
            PerformBasicDiceNotationTest(expression, Roller.Min, 1.Repeat(3));
            PerformBasicDiceNotationTest(expression, Roller.Random, Roll(1, 2, 3));
        }
    }

    [Fact]
    public void ShouldKeepRollsWithMinimum_WhenExpressionHasAKeepAndMinimum()
    {
        const string expression = "4d6k3m5";

        using (new AssertionScope())
        {
            PerformBasicDiceNotationTest(expression, Roller.Max, 6.Repeat(3), Roll(6));
            PerformBasicDiceNotationTest(expression, Roller.Average, 5.Repeat(3), Roll(5));
            PerformBasicDiceNotationTest(expression, Roller.Min, 5.Repeat(3), Roll(5));
            PerformBasicDiceNotationTest("4d6k3m3", Roller.Random, Roll(3, 3, 4), Roll(3));
        }
    }

    [Fact]
    public void ShouldRoundDiceExpressionValues_WhenValuesAreDecimalNumbers()
    {
        const DiceRoundingStrategy ceiling = DiceRoundingStrategy.RoundToCeiling;
        const DiceRoundingStrategy floor = DiceRoundingStrategy.RoundToFloor;
        const DiceRoundingStrategy nearest = DiceRoundingStrategy.RoundToNearest;

        using (new AssertionScope())
        {
            PerformBasicDiceNotationTest("3.9d5.51k2.01", Roller.Average, 4.Repeat(3), Roll(4), ceiling);
            PerformBasicDiceNotationTest("4.1d6.5k3.99", Roller.Average, 3.Repeat(3), Roll(3), floor);
            PerformBasicDiceNotationTest("3.99d6.5k3.01", Roller.Average, 4.Repeat(3), Roll(4), nearest);
        }
    }

    [Fact]
    public void ShouldCombineRolls_WhenMultipleDiceExpressionsArePresent()
    {
        using (new AssertionScope())
        {
            PerformCombinableDiceNotationTest("4d6k3 + 2d10m3", 20);
            PerformCombinableDiceNotationTest("4d6k3 - 2d10m3", -2);
            PerformCombinableDiceNotationTest("4d6k3 * 2d10m3", 99);
            PerformCombinableDiceNotationTest("4d6k3 / 2d10m3", 0.81);
        }
    }

    [Fact]
    public void ShouldRepeatRolls_WhenUsingTheRepeatingNode()
    {
        // Arrange
        var diceExpression = DiceExpressionParser.Parse("3d6");
        var repeatingDiceExpression = DiceExpressionParser.Parse("3.01");
        var sut = DiceEvaluatorFactory.Create();

        // Act
        var actual = sut.Evaluate(diceExpression, repeatingDiceExpression, Roller.Max);

        // Assert
        var nodeResponse = new ExpressionResponse(18, RollResponseFactory.Create(6, 6.Repeat(3), null));
        var expected = new[] { nodeResponse, nodeResponse, nodeResponse };
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldNotHaveRollsOutsideOfDieSize_WhenUsingDefaultRandom()
    {
        // Arrange
        var sut = DiceEvaluatorFactory.CreateWithDefaults();
        var diceExpression = DiceExpressionParser.Parse("100d2");

        // Act
        var actual = sut.Evaluate(diceExpression);

        // Assert
        actual.RollResponses
            .SelectMany(x => x.Rolls.Select(y => y.Result))
            .Where(x => x is < 1 or > 2)
            .Should()
            .BeEmpty();
    }
}