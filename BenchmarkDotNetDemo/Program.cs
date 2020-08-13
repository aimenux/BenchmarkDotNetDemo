using BenchmarkDotNet.Running;

namespace BenchmarkDotNetDemo
{
    public static class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<CollectionFilteringBenchmarks>();
        }
    }
}
