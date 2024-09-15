using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dicer;
using static Dicer.DiceExpressionParser;

IDiceEvaluator diceEvaluator = new DiceEvaluator();

BasicParsingExample(diceEvaluator);
ComplexParsingExample(diceEvaluator);
BasicRepeatingExample(diceEvaluator);

static void BasicParsingExample(IDiceEvaluator diceEvaluator)
{
    var expression = Parse("4D6K3 + 2d10m2");
    var response = diceEvaluator.Evaluate(expression);
    Console.WriteLine(Format(response, "BasicParsingExample"));
}

static void ComplexParsingExample(IDiceEvaluator diceEvaluator)
{
    var expression = Parse("4D(5/(1+2))K-3 +- (5*(1+2))d10m(1d2k1)");
    var response = diceEvaluator.Evaluate(expression);
    Console.WriteLine(Format(response, "ComplexParsingExample"));
}

static void BasicRepeatingExample(IDiceEvaluator diceEvaluator)
{
    var expression = Parse("4D6K3");
    var responses = diceEvaluator.Evaluate(expression, 6);
    Console.WriteLine(FormatEnumerable(responses, "BasicRepeatingExample"));
}

static string Format(ExpressionResponse response, params string[] args)
{
    var builder = new StringBuilder();
    builder.AppendLine($"{GetHeader(args)}: {response.Result}");
    OutputRolls(response, builder);

    return builder.ToString();
}

static string FormatEnumerable(IEnumerable<ExpressionResponse> responses, params string[] args)
{
    var builder = new StringBuilder();
    var count = 1;

    foreach (var response in responses)
    {
        builder.AppendLine(Format(response, args.Append(count.ToString()).ToArray()));
        count++;
    }

    return builder.ToString();
}

static string GetHeader(string[] args)
{
    return args.Any() ? string.Join(" ", args) : "Result";
}

static void OutputRolls(ExpressionResponse response, StringBuilder builder)
{
    foreach (var (_, rolls, discarded) in response.RollResponses)
    {
        var rollArray = rolls as Roll[] ?? rolls.ToArray();
        var discardedArray = discarded as Roll[] ?? discarded.ToArray();

        if (rollArray.Any())
        {
            builder.Append(
                $"Die Size({rollArray.First().DieSize}): [{string.Join(", ", rollArray.Select(x => x.Result))}]");

            if (discardedArray.Any())
                builder.AppendLine(
                    $"/[{string.Join(", ", discardedArray.Select(x => x.Result))}]");

            builder.AppendLine();
        }
    }
}