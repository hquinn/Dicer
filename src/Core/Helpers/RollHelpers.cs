using Dicer.Models;
using System.Collections.Generic;
using System.Linq;

namespace Dicer.Helpers;

internal static class RollHelpers
{
	internal static IEnumerable<RollResponse> Merge(params IEnumerable<RollResponse>?[] responses)
	{
		return responses
			.Select(x => x ??= Enumerable.Empty<RollResponse>())
			.SelectMany(x => x);
	}
}