using FluentAssertions;
using Xunit;

namespace Dicer.Tests.Rounding;

public class RoundToFloorTests
{
	public class RoundTests
	{
		[Theory]
		[InlineData(1.1, 1.0)]
		[InlineData(1.5, 1.0)]
		[InlineData(1.8, 1.0)]
		public void ShouldRoundDown(double number, double expected)
		{
			// Arrange
			var sut = new RoundToFloor();

			// Act
			var actual = sut.Round(number);

			// Assert
			actual.Should().Be(expected);
		}
	}
}