using System.Collections.Generic;
using System.Linq;

namespace Dicer.Tests.Factories;

public static class RollResponseFactory
{
	public static RollResponse CreateResponse(int dieSize, IEnumerable<int> rolls)
	{
		return RollResponse.CreateResponse(CreateRolls(rolls, dieSize), CreateRolls(Enumerable.Empty<int>(), dieSize));
	}

	public static RollResponse CreateResponse(int dieSize, IEnumerable<int> rolls, IEnumerable<int> discarded)
	{
		return RollResponse.CreateResponse(CreateRolls(rolls, dieSize), CreateRolls(discarded, dieSize));
	}

	private static IEnumerable<Roll> CreateRolls(IEnumerable<int> rolls, int dieSize)
	{
		return rolls.Select(x => new Roll(x, dieSize));
	}
}