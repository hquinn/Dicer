using Dicer.Nodes;
using Dicer.Tests.Factories;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Dicer.Tests.Nodes;

public class RepeatingNodeTests
{
	[Theory]
	[InlineData(1, 1, 2)]
	[InlineData(1, 2, 3)]
	public void ShouldReturnNodeEvaluationNumberOfTimesEqualToRepeat(double first, double second, double expected)
	{
		// Arrange
		var roller = RollerFactory.CreateEmptyRoller();
		var roundingStrategy = RoundingStrategyFactory.CreateRoundingStrategy();
		var node = NodeFactory.CreateAddNode(first, second);
		var repeat = new NumberNode(3);
		var sut = new RepeatingNode(node, repeat);

		// Act
		var evaluation = sut.Evaluate(roller, roundingStrategy);

		// Assert
		evaluation.Should().HaveCount(3);
		evaluation.Select(x => x.Result).Should().BeEquivalentTo(Enumerable.Repeat(expected, 3));
	}
}