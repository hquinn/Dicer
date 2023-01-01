using System.Collections.Generic;
using System.Linq;
using Dicer.Tests.Helpers;

namespace Dicer.Tests.Factories;

public static class RollResponseFactory
{
	public static RollResponse CreateResponse(int dieSize, IEnumerable<int> rolls)
	{
		return RollResponse.CreateResponse(CreateRolls(rolls, dieSize), CreateRolls(Enumerable.Empty<int>(), dieSize));
	}
	
	public static RollResponse CreateResponse(
		int dieSize, 
		IEnumerable<int> rolls, 
		IEnumerable<int> discarded)
	{
		return RollResponse.CreateResponse(CreateRolls(rolls, dieSize), CreateRolls(discarded, dieSize));
	}
	
	public static IEnumerable<RollResponse> Create(
		int dieSize, 
		IEnumerable<int> rollValues, 
		IEnumerable<int>? discardedValues)
	{
		var rolls = CreateRolls(rollValues, dieSize);
		var discarded = discardedValues is {} ? CreateRolls(discardedValues, dieSize) : Enumerable.Empty<Roll>();
		
		return RollResponse.CreateResponse(rolls, discarded).AsEnumerable();
	}

	private static IEnumerable<Roll> CreateRolls(IEnumerable<int> rolls, int dieSize)
	{
		return rolls.Select(x => new Roll(x, dieSize));
	}
}