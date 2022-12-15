using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Parsing.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class ParsingBenchmarks
{
	private const string ComplexExpression =
		"4D(2*(((3-1)+(0.5*(1+1)))*1))k(10-((5-3)-1)-5) * (----2+-6d20*(5D9+(4D100-(+-3D5-(44d60k(2*2))+4*2)/2)/2)--2)";

	[Benchmark]
	public void BenchmarkComplexParsing()
	{
		Parse(ComplexExpression);
	}
}