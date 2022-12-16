using Dicer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Dicer.Parser;

BasicParsingExample();
ComplexParsingExample();
BasicRepeatingNodeExample();

static void BasicParsingExample()
{
	var response = Parse("4D6K3 + 2d10m2").Evaluate();
	Console.WriteLine(Format(response, "BasicParsingExample"));
}

static void ComplexParsingExample()
{
	var response = Parse("4D(5/(1+2))K-3 +- (5*(1+2))d10m(1d2k1)").Evaluate();
	Console.WriteLine(Format(response, "ComplexParsingExample"));
}

static void BasicRepeatingNodeExample()
{
	var responses = Parse("4D6K3", "6").Evaluate();
	Console.WriteLine(FormatEnumerable(responses, "BasicRepeatingNodeExample"));
}

static string Format(NodeResponse response, params string[] args)
{
	var builder = new StringBuilder();
	builder.AppendLine($"{GetHeader(args)}: {response.Result}");
	OutputRolls(response, builder);

	return builder.ToString();
}

static string FormatEnumerable(IEnumerable<NodeResponse> responses, params string[] args)
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

static void OutputRolls(NodeResponse response, StringBuilder builder)
{
	foreach (var (_, rolls) in response.RollResponses!)
	{
		var rollArray = rolls as Roll[] ?? rolls.ToArray();

		if (rollArray.Any())
		{
			builder.AppendLine(
				$"Die Size({rollArray.First().DieSize}): [{string.Join(", ", rollArray.Select(x => x.Result))}]");
		}
	}
}