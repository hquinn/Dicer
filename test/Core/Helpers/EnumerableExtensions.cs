using System.Collections.Generic;
using System.Linq;

namespace Dicer.Tests.Helpers;

public static class EnumerableExtensions
{
    public static IEnumerable<RollResponse> AsEnumerable(this RollResponse rollResponse)
    {
        return new[] { rollResponse };
    }

    public static IEnumerable<int> Repeat(this int number, int times)
    {
        return Enumerable.Repeat(number, times);
    }

    public static IEnumerable<int> Roll(params int[] rolls)
    {
        return rolls;
    }
}