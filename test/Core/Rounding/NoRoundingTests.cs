using Dicer.Rounding;
using FluentAssertions;
using Xunit;

namespace Dicer.Tests.Rounding
{
	public class NoRoundingTests
	{
		public class RoundTests
		{
			[Fact]
			public void ShouldNotRoundNumber()
			{
				// Arrange
				var number = 1.5;
				var sut = new NoRounding();

				// Act
				var actual = sut.Round(number);

				// Assert
				actual.Should().Be(number);
			}
		}
	}
}