using System;
using System.Collections.Generic;
using Dicer.Randomizer;
using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer;

/// <summary>
///     <inheritdoc />
/// </summary>
public class DiceEvaluator : IDiceEvaluator
{
    private static readonly Lazy<DiceEvaluator> _instance = new(() => new DiceEvaluator());
    private readonly Dictionary<Roller, IDiceRoller> _rollers;
    private readonly IRoundingStrategy[] _roundingStrategies;

    public DiceEvaluator()
        : this(new DefaultRandom())
    {
    }

    public DiceEvaluator(IRandom random)
    {
        _rollers = new Dictionary<Roller, IDiceRoller>
        {
            [Roller.Random] = new RandomDiceRoller(random),
            [Roller.Min] = new MinDiceRoller(),
            [Roller.Max] = new MaxDiceRoller(),
            [Roller.Average] = new AverageDiceRoller()
        };

        _roundingStrategies = new IRoundingStrategy[]
        {
            new RoundToFloor(),
            new RoundToCeiling(),
            new RoundToNearest(),
            new NoRounding()
        };
    }

    public static DiceEvaluator Instance => _instance.Value;

    /// <summary>
    ///     <inheritdoc />
    /// </summary>
    public ExpressionResponse Evaluate(
        DiceExpression expression,
        Roller selectedRoller = Roller.Random,
        RoundingStrategy selectedRoundingStrategy = RoundingStrategy.RoundToFloor,
        DiceRoundingStrategy selectedDiceRoundingStrategy = DiceRoundingStrategy.RoundToCeiling)
    {
        var roller = _rollers[selectedRoller];
        var roundingStrategy = _roundingStrategies[(int)selectedRoundingStrategy];
        var diceRoundingStrategy = _roundingStrategies[(int)selectedDiceRoundingStrategy];

        return expression.Evaluate(roller, roundingStrategy, diceRoundingStrategy);
    }

    /// <summary>
    ///     <inheritdoc />
    /// </summary>
    public IReadOnlyCollection<ExpressionResponse> Evaluate(
        DiceExpression expression,
        int numberOfTimes,
        Roller selectedRoller = Roller.Random,
        RoundingStrategy selectedRoundingStrategy = RoundingStrategy.RoundToFloor,
        DiceRoundingStrategy selectedDiceRoundingStrategy = DiceRoundingStrategy.RoundToCeiling)
    {
        var results = new List<ExpressionResponse>();

        for (var i = 0; i < numberOfTimes; i++)
        {
            var result = Evaluate(
                expression,
                selectedRoller,
                selectedRoundingStrategy,
                selectedDiceRoundingStrategy);

            results.Add(result);
        }

        return results;
    }

    /// <summary>
    ///     <inheritdoc />
    /// </summary>
    public IReadOnlyCollection<ExpressionResponse> Evaluate(
        DiceExpression expression,
        DiceExpression numberOfTimesExpression,
        Roller selectedRoller = Roller.Random,
        RoundingStrategy selectedRoundingStrategy = RoundingStrategy.RoundToFloor,
        DiceRoundingStrategy selectedDiceRoundingStrategy = DiceRoundingStrategy.RoundToCeiling)
    {
        var numberOfTimes = (int)Evaluate(
            numberOfTimesExpression,
            selectedRoller,
            selectedRoundingStrategy,
            selectedDiceRoundingStrategy).Result;

        return Evaluate(
            expression,
            numberOfTimes,
            selectedRoller,
            selectedRoundingStrategy,
            selectedDiceRoundingStrategy);
    }

    /// <summary>
    ///     <inheritdoc />
    /// </summary>
    public ExpressionResponse Evaluate(
        string expressionToParse, 
        Roller selectedRoller = Roller.Random,
        RoundingStrategy selectedRoundingStrategy = RoundingStrategy.RoundToFloor,
        DiceRoundingStrategy selectedDiceRoundingStrategy = DiceRoundingStrategy.RoundToCeiling)
    {
        var expression = DiceExpressionParser.Parse(expressionToParse);
        
        return Evaluate(
            expression, 
            selectedRoller, 
            selectedRoundingStrategy, 
            selectedDiceRoundingStrategy);
    }

    /// <summary>
    ///     <inheritdoc />
    /// </summary>
    public IReadOnlyCollection<ExpressionResponse> Evaluate(
        string expressionToParse, 
        int numberOfTimes, 
        Roller selectedRoller = Roller.Random,
        RoundingStrategy selectedRoundingStrategy = RoundingStrategy.RoundToFloor,
        DiceRoundingStrategy selectedDiceRoundingStrategy = DiceRoundingStrategy.RoundToCeiling)
    {
        var expression = DiceExpressionParser.Parse(expressionToParse);
        
        return Evaluate(
            expression,
            numberOfTimes,
            selectedRoller,
            selectedRoundingStrategy,
            selectedDiceRoundingStrategy);
    }

    /// <summary>
    ///     <inheritdoc />
    /// </summary>
    public IReadOnlyCollection<ExpressionResponse> Evaluate(
        string expressionToParse, 
        string numberOfTimesExpressionToParse, 
        Roller selectedRoller = Roller.Random,
        RoundingStrategy selectedRoundingStrategy = RoundingStrategy.RoundToFloor,
        DiceRoundingStrategy selectedDiceRoundingStrategy = DiceRoundingStrategy.RoundToCeiling)
    {
        var expression = DiceExpressionParser.Parse(expressionToParse);
        var numberOfTimesExpression = DiceExpressionParser.Parse(numberOfTimesExpressionToParse);
        
        return Evaluate(
            expression,
            numberOfTimesExpression,
            selectedRoller,
            selectedRoundingStrategy,
            selectedDiceRoundingStrategy);
    }
}