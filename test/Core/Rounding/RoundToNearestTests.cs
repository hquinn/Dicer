using FluentAssertions;
using Xunit;

namespace Dicer.Tests.Rounding;

public class RoundToNearestTests
{
	public class RoundTests
	{
		[Theory]
		[InlineData(1.1, 1.0)]
		[InlineData(1.5, 2.0)]
		[InlineData(1.8, 2.0)]
		public void ShouldRoundToNearest(double number, double expected)
		{
			// Arrange
			var sut = new RoundToNearest();

			// Act
			var actual = sut.Round(number);

			// Assert
			actual.Should().Be(expected);
		}
	}
}