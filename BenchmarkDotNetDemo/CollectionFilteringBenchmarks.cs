using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace BenchmarkDotNetDemo
{
    [MemoryDiagnoser]
    [MinColumn, MaxColumn]
    public class CollectionFilteringBenchmarks
    {
        private readonly Consumer _consumer = new Consumer();

        [Params(1000, 5000)]
        public int NumberOfItems { get; set; }

        public IEnumerable<int> Items { get; private set; }

        [GlobalSetup]
        public void Setup()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            Items = Enumerable.Range(0, NumberOfItems)
                .Select(_ => random.Next(1, 1000))
                .ToArray();
        }

        [Benchmark(Baseline = true)]
        public void ConsumeUsingTemporaryCollection()
        {
            FilteringUsingTemporaryCollection().Consume(_consumer);
        }

        [Benchmark]
        public void ConsumeUsingYieldOperator()
        {
            FilteringUsingYieldOperator().Consume(_consumer);
        }

        [Benchmark]
        public void ConsumeUsingLinqToObjects()
        {
            FilteringUsingLinqToObjects().Consume(_consumer);
        }

        private IEnumerable<int> FilteringUsingTemporaryCollection()
        {
            var results = new List<int>();

            foreach (var item in Items)
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
            foreach (var item in Items)
            {
                if (item % 2 == 0)
                {
                    yield return item;
                }
            }
        }

        private IEnumerable<int> FilteringUsingLinqToObjects()
        {
            return Items.Where(item => item % 2 == 0);
        }
    }
}