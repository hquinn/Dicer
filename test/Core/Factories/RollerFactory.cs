using Dicer.Models;
using Dicer.Rollers;
using Dicer.Rounding;
using NSubstitute;
using System.Linq;

namespace Dicer.Tests.Factories;

public static class RollerFactory
{
	public static IRoller CreateRoller(int numDice, int dieSize)
	{
		var mockRoller = Substitute.For<IRoller>();
		mockRoller
			.Roll(Arg.Any<NodeResponse>(), Arg.Any<NodeResponse>(), Arg.Any<NodeResponse?>(), Arg.Any<IRoundingStrategy>())
			.Returns(new RollResponse(numDice * dieSize, Enumerable.Repeat<Roll>(new(dieSize, dieSize), numDice)));

		return mockRoller;
	}

	public static IRoller CreateEmptyRoller()
	{
		return Substitute.For<IRoller>();
	}
}