using System;
using Dicer.Rounding;
using NSubstitute;
using NSubstitute.Core;

namespace Dicer.Tests.Factories
{
	public static class RoundingStrategyFactory
	{
		public static IRoundingStrategy CreateRoundingStrategy()
		{
			var mockRoundingStrategy = Substitute.For<IRoundingStrategy>();

			mockRoundingStrategy.Round(default).ReturnsForAnyArgs(x => (int)Math.Ceiling(GetNumber(x)));

			return mockRoundingStrategy;
		}

		private static double GetNumber(CallInfo callInfo)
		{
			return (double)callInfo[0];
		}
	}
}
