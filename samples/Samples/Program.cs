using Dicer.Models;
using Dicer.Nodes;
using Dicer.Rollers;
using Dicer.Rounding;
using System;
using System.Linq;
using Dicer.Formatter;
using static Dicer.Parser.Parser;

var formatter = new DefaultResponseFormatter();

BasicArithmetic(formatter);
DiceExample(formatter);
KeepLowestExample(formatter);
BasicParsingExample(formatter);
ComplexParsingExample(formatter);
BasicRepeatingNodeExample(formatter);

static void BasicArithmetic(IResponseFormatter formatter)
{
	var node = new AddNode(new NumberNode(1), new MultiplyNode(new NumberNode(2), new NumberNode(3)));
	var response = node.Evaluate(new MinRoller(), new NoRounding());
	Console.WriteLine(formatter.Format(response, "BasicArithmetic"));
}

static void DiceExample(IResponseFormatter formatter)
{
	var node = new AddNode(new DiceNode(new NumberNode(4), new NumberNode(6), new NumberNode(3)), new DiceNode(new NumberNode(2), new NumberNode(10)));
	var response = node.Evaluate(new RandomRoller(), new RoundToCeiling());
	Console.WriteLine(formatter.Format(response, "DiceExample"));
}

static void KeepLowestExample(IResponseFormatter formatter)
{
	var node = new DiceNode(new NumberNode(4), new NumberNode(6), new NumberNode(-3));
	var response = node.Evaluate(new RandomRoller(), new RoundToNearest());
	Console.WriteLine(formatter.Format(response, "KeepLowestExample"));
}

static void BasicParsingExample(IResponseFormatter formatter)
{
	var node = Parse("4D6K3 + 2d10");
	var response = node.Evaluate(new RandomRoller(), new RoundToCeiling());
	Console.WriteLine(formatter.Format(response, "BasicParsingExample"));
}

static void ComplexParsingExample(IResponseFormatter formatter)
{
	var node = Parse("4D(5/(1+2))K-3 +- (5*(1+2))d10");
	var response = node.Evaluate(new RandomRoller(), new RoundToCeiling());
	Console.WriteLine(formatter.Format(response, "ComplexParsingExample"));
}

static void BasicRepeatingNodeExample(IResponseFormatter formatter)
{
	var node = Parse("4D6K3", "6");
	var responses = node.Evaluate(new RandomRoller(), new RoundToCeiling());
	Console.WriteLine(formatter.Format(responses, "BasicRepeatingNodeExample"));
}
