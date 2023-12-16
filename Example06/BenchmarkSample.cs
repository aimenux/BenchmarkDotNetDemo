using BenchmarkDotNet.Attributes;
using Humanizer;

namespace Example06;

[Config(typeof(BenchmarkConfig))]
public class BenchmarkSample
{
    [Benchmark]
    public string HumanizeDateTime() => DateTime.Now.Humanize();
}