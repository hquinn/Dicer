using System;
using NSubstitute;

namespace Dicer.Tests.Factories;

public static class NodeFactory
{
	internal static AddNode CreateAddNode(double first, double second)
	{
		return new(Create(first), Create(second));
	}

   internal static SubtractNode CreateSubtractNode(double first, double second)
	{
		return new(Create(first), Create(second));
	}

   internal static MultiplyNode CreateMultiplyNode(double first, double second)
	{
		return new(Create(first), Create(second));
	}

   internal static DivideNode CreateDivideNode(double first, double second)
	{
		return new(Create(first), Create(second));
	}

   internal static DiceNode CreateDiceNode(int numDice, int dieSize)
	{
		return new(CreateWithoutRolls(numDice), CreateWithoutRolls(dieSize));
	}

   internal static DiceNode CreateDiceNodeWithRolls(int numDice, int dieSize)
	{
		var mockNumDice = CreateWithRolls(numDice, new[] { new RollResponse(3, new[] { new Roll(3, 6) }, Array.Empty<Roll>()) });
		var mockDieSize = CreateWithRolls(dieSize, new[] { new RollResponse(6, new[] { new Roll(6, 6) }, Array.Empty<Roll>()) });

		return new(mockNumDice, mockDieSize);
	}

   internal static UnaryNode CreateUnaryNode(double node)
	{
		return new(Create(node));
	}

   private static BaseNode Create(double number)
   {
	   var rolls = new[] { new RollResponse((int)number, new[] { new Roll((int)number, (int)number) }, Array.Empty<Roll>()) };
	   var mockNumber = Substitute.For<BaseNode>();
	   mockNumber.Evaluate(Arg.Any<IRoller>(), Arg.Any<IRoundingStrategy>()).Returns(new NodeResponse(number, rolls));

	   return mockNumber;
	}

   private static BaseNode CreateWithoutRolls(int numDice)
   {
	   var mockDice = Substitute.For<BaseNode>();
	   mockDice.Evaluate(Arg.Any<IRoller>(), Arg.Any<IRoundingStrategy>()).Returns(new NodeResponse(numDice));
	   return mockDice;
	}

   private static BaseNode CreateWithRolls(int numDice, RollResponse[] rollResponses)
   {
	   var mockNumDice = Substitute.For<BaseNode>();
	   mockNumDice.Evaluate(Arg.Any<IRoller>(), Arg.Any<IRoundingStrategy>()).Returns(new NodeResponse(numDice, rollResponses));
	   return mockNumDice;
   }
}