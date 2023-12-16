using BenchmarkDotNet.Attributes;

namespace Example04;

[Config(typeof(BenchmarkConfig))]
public class BenchmarkSample
{
    [Params(100, 500)]
    public int Size { get; set; }

    private IEnumerable<int> _items;

    [GlobalSetup]
    public void Setup()
    {
        _items = Enumerable.Range(0, Size)
            .Select(_ => Random.Shared.Next(1, 1000))
            .ToArray();
    }

    [Arguments(1, 200)]
    [Benchmark(Baseline = true)]
    public ICollection<int> ConsumeUsingTemporaryCollection(int min, int max) => FilteringUsingTemporaryCollection(min, max).ToList();

    [Arguments(1, 200)]
    [Benchmark]
    public ICollection<int> ConsumeUsingYieldOperator(int min, int max) => FilteringUsingYieldOperator(min, max).ToList();

    [Arguments(1, 200)]
    [Benchmark]
    public ICollection<int> ConsumeUsingLinqToObjects(int min, int max) => FilteringUsingLinqToObjects(min, max).ToList();

    private IEnumerable<int> FilteringUsingTemporaryCollection(int min, int max)
    {
        var results = new List<int>();

        foreach (var item in _items)
        {
            if (item >= min && item <= max)
            {
                results.Add(item);
            }
        }

        return results;
    }

    private IEnumerable<int> FilteringUsingYieldOperator(int min, int max)
    {
        foreach (var item in _items)
        {
            if (item >= min && item <= max)
            {
                yield return item;
            }
        }
    }

    private IEnumerable<int> FilteringUsingLinqToObjects(int min, int max)
    {
        return _items.Where(item => item >= min && item <= max);
    }
}