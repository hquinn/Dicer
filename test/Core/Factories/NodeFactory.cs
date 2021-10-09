using Dicer.Models;
using Dicer.Nodes;
using Dicer.Rollers;
using Dicer.Rounding;
using NSubstitute;

namespace Dicer.Tests.Factories;

public static class NodeFactory
{
	public static AddNode CreateAddNode(double first, double second)
	{
		var firstRolls = new[] { new RollResponse((int)first, new[] { new Roll((int)first, (int)first) }) };
		var mockFirst = Substitute.For<INode>();
		mockFirst.Evaluate(Arg.Any<IRoller>(), Arg.Any<IRoundingStrategy>()).Returns(new NodeResponse(first, firstRolls));

		var secondRolls = new[] { new RollResponse((int)second, new[] { new Roll((int)second, (int)second) }) };
		var mockSecond = Substitute.For<INode>();
		mockSecond.Evaluate(Arg.Any<IRoller>(), Arg.Any<IRoundingStrategy>()).Returns(new NodeResponse(second, secondRolls));

		return new(mockFirst, mockSecond);
	}

	public static SubtractNode CreateSubtractNode(double first, double second)
	{
		var firstRolls = new[] { new RollResponse((int)first, new[] { new Roll((int)first, (int)first) }) };
		var mockFirst = Substitute.For<INode>();
		mockFirst.Evaluate(Arg.Any<IRoller>(), Arg.Any<IRoundingStrategy>()).Returns(new NodeResponse(first, firstRolls));

		var secondRolls = new[] { new RollResponse((int)second, new[] { new Roll((int)second, (int)second) }) };
		var mockSecond = Substitute.For<INode>();
		mockSecond.Evaluate(Arg.Any<IRoller>(), Arg.Any<IRoundingStrategy>()).Returns(new NodeResponse(second, secondRolls));

		return new(mockFirst, mockSecond);
	}

	public static MultiplyNode CreateMultiplyNode(double first, double second)
	{
		var firstRolls = new[] { new RollResponse((int)first, new[] { new Roll((int)first, (int)first) }) };
		var mockFirst = Substitute.For<INode>();
		mockFirst.Evaluate(Arg.Any<IRoller>(), Arg.Any<IRoundingStrategy>()).Returns(new NodeResponse(first, firstRolls));

		var secondRolls = new[] { new RollResponse((int)second, new[] { new Roll((int)second, (int)second) }) };
		var mockSecond = Substitute.For<INode>();
		mockSecond.Evaluate(Arg.Any<IRoller>(), Arg.Any<IRoundingStrategy>()).Returns(new NodeResponse(second, secondRolls));

		return new(mockFirst, mockSecond);
	}

	public static DivideNode CreateDivideNode(double first, double second)
	{
		var firstRolls = new[] { new RollResponse((int)first, new[] { new Roll((int)first, (int)first) }) };
		var mockFirst = Substitute.For<INode>();
		mockFirst.Evaluate(Arg.Any<IRoller>(), Arg.Any<IRoundingStrategy>()).Returns(new NodeResponse(first, firstRolls));

		var secondRolls = new[] { new RollResponse((int)second, new[] { new Roll((int)second, (int)second) }) };
		var mockSecond = Substitute.For<INode>();
		mockSecond.Evaluate(Arg.Any<IRoller>(), Arg.Any<IRoundingStrategy>()).Returns(new NodeResponse(second, secondRolls));

		return new(mockFirst, mockSecond);
	}

	public static DiceNode CreateDiceNode(int numDice, int dieSize)
	{
		var mockNumDice = Substitute.For<INode>();
		mockNumDice.Evaluate(Arg.Any<IRoller>(), Arg.Any<IRoundingStrategy>()).Returns(new NodeResponse(numDice));

		var mockDieSize = Substitute.For<INode>();
		mockDieSize.Evaluate(Arg.Any<IRoller>(), Arg.Any<IRoundingStrategy>()).Returns(new NodeResponse(dieSize));

		return new(mockNumDice, mockDieSize);
	}

	public static DiceNode CreateDiceNodeWithRolls(int numDice, int dieSize)
	{
		var mockNumDice = Substitute.For<INode>();
		mockNumDice.Evaluate(Arg.Any<IRoller>(), Arg.Any<IRoundingStrategy>()).Returns(new NodeResponse(numDice, new[] { new RollResponse(3, new[] { new Roll(3, 6) }) }));

		var mockDieSize = Substitute.For<INode>();
		mockDieSize.Evaluate(Arg.Any<IRoller>(), Arg.Any<IRoundingStrategy>()).Returns(new NodeResponse(dieSize, new[] { new RollResponse(6, new[] { new Roll(6, 6) }) }));

		return new(mockNumDice, mockDieSize);
	}

	public static UnaryNode CreateUnaryNode(double node)
	{
		var nodeRolls = new[] { new RollResponse((int)node, new[] { new Roll((int)node, (int)node) }) };
		var mockNode = Substitute.For<INode>();
		mockNode.Evaluate(Arg.Any<IRoller>(), Arg.Any<IRoundingStrategy>()).Returns(new NodeResponse(node, nodeRolls));

		return new(mockNode);
	}
}