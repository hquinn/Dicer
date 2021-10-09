using Dicer.Rounding;
using FluentAssertions;
using Xunit;

namespace Dicer.Tests.Rounding;

public class RoundToCeilingTests
{
	public class RoundTests
	{
		[Theory]
		[InlineData(1.1, 2.0)]
		[InlineData(1.5, 2.0)]
		[InlineData(1.8, 2.0)]
		public void ShouldRoundUp(double number, double expected)
		{
			// Arrange
			var sut = new RoundToCeiling();

			// Act
			var actual = sut.Round(number);

			// Assert
			actual.Should().Be(expected);
		}
	}
}