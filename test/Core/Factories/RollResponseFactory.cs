using System.Linq;
using Dicer.Models;

namespace Dicer.Tests.Factories;

public static class RollResponseFactory
{
	public static RollResponse CreateResponse(int result, int dieSize, params int[] rolls)
	{
		return new RollResponse(result, rolls.Select(x => new Roll(x, dieSize)));
	}
}