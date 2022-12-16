using Dicer.Tests.Helpers;
using NSubstitute;
using System.Linq;

namespace Dicer.Tests.Factories;

public static class RandomFactory
{
	internal static IRandom CreateRandom(params int[] randomValues)
	{
		if (!randomValues.Any())
		{
			randomValues = new[] { 2 };
		}

		var queue = new InfiniteQueue(randomValues);
		var mockRandom = Substitute.For<IRandom>();
		mockRandom.RollDice(Arg.Any<int>()).Returns(x => queue.Dequeue());

		return mockRandom;
	}
}