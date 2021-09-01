using System.Collections.Generic;

namespace Dicer.Models
{
	public record RollResponse(int Result, IEnumerable<Roll> Rolls);
}