using System.Collections.Generic;
using System.Linq;

namespace Dicer.Tests.Helpers;

public static class EnumerableExtensions
{
    public static RollResponse[] AsArray(this RollResponse rollResponse)
    {
        return new[] { rollResponse };
    }

    public static IReadOnlyCollection<RollResponse> Combine(IReadOnlyCollection<RollResponse> first,
        IReadOnlyCollection<RollResponse> second)
    {
        return first.Concat(second).ToList().AsReadOnly();
    }

    public static IReadOnlyCollection<int> Repeat(this int number, int times)
    {
        return Enumerable.Repeat(number, times).ToList();
    }

    public static IReadOnlyCollection<int> Roll(params int[] rolls)
    {
        return rolls;
    }
}