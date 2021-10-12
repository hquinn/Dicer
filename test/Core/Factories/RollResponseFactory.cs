using Dicer.Models;
using System.Linq;

namespace Dicer.Tests.Factories;

public static class RollResponseFactory
{
	public static RollResponse CreateResponse(int result, int dieSize, params int[] rolls)
	{
		return new(result, rolls.Select(x => new Roll(x, dieSize)));
	}
}