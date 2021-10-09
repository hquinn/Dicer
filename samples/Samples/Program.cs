using Dicer.Models;
using Dicer.Nodes;
using Dicer.Rollers;
using Dicer.Rounding;
using System;
using System.Linq;
using static Dicer.Parser.Parser;

BasicArithmetic();
DiceExample();
KeepLowestExample();
BasicParsingExample();
ComplexParsingExample();

static void BasicArithmetic()
{
	var node = new AddNode(new NumberNode(1), new MultiplyNode(new NumberNode(2), new NumberNode(3)));
	var response = node.Evaluate(new MinRoller(), new NoRounding());
	Console.WriteLine($"BasicArithmetic Result: {response.Result}");
}

static void DiceExample()
{
	var node = new AddNode(new DiceNode(new NumberNode(4), new NumberNode(6), new NumberNode(3)), new DiceNode(new NumberNode(2), new NumberNode(10)));
	var response = node.Evaluate(new RandomRoller(), new RoundToCeiling());
	Console.WriteLine($"DiceExample Result: {response.Result}");
	OutputRolls(response);
}

static void KeepLowestExample()
{
	var node = new DiceNode(new NumberNode(4), new NumberNode(6), new NumberNode(-3));
	var response = node.Evaluate(new RandomRoller(), new RoundToNearest());
	Console.WriteLine($"KeepLowestExample Result: {response.Result}");
	OutputRolls(response);
}

static void BasicParsingExample()
{
	var node = Parse("4D6K3 + 2d10");
	var response = node.Evaluate(new RandomRoller(), new RoundToCeiling());
	Console.WriteLine($"BasicParsingExample Result: {response.Result}");
	OutputRolls(response);
}

static void ComplexParsingExample()
{
	var node = Parse("4D(5/(1+2))K-3 +- (5*(1+2))d10");
	var response = node.Evaluate(new RandomRoller(), new RoundToCeiling());
	Console.WriteLine($"ComplexParsingExample Result: {response.Result}");
	OutputRolls(response);
}

static void OutputRolls(NodeResponse response)
{
	foreach (var (_, rolls) in response.RollResponses!)
	{
		Console.WriteLine($"Die Size: {rolls.First().DieSize}: {string.Join(", ", rolls.Select(x => x.Result.ToString()))}");
	}
}
