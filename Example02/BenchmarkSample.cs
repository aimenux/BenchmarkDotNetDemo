using BenchmarkDotNet.Attributes;

namespace Example02;

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

    [Benchmark(Baseline = true)]
    public ICollection<int> ConsumeUsingTemporaryCollection() => FilteringUsingTemporaryCollection().ToList();

    [Benchmark]
    public ICollection<int> ConsumeUsingYieldOperator() => FilteringUsingYieldOperator().ToList();

    [Benchmark]
    public ICollection<int> ConsumeUsingLinqToObjects() => FilteringUsingLinqToObjects().ToList();

    private IEnumerable<int> FilteringUsingTemporaryCollection()
    {
        var results = new List<int>();

        foreach (var item in _items)
        {
            if (item % 2 == 0)
            {
                results.Add(item);
            }
        }

        return results;
    }

    private IEnumerable<int> FilteringUsingYieldOperator()
    {
        foreach (var item in _items)
        {
            if (item % 2 == 0)
            {
                yield return item;
            }
        }
    }

    private IEnumerable<int> FilteringUsingLinqToObjects()
    {
        return _items.Where(item => item % 2 == 0);
    }
}