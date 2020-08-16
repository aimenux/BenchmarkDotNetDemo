using System.Net.Http;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace BenchmarkDotNetDemo
{
    [MemoryDiagnoser]
    [MinColumn, MaxColumn]
    public class TaskBenchmarks
    {
        [Params(1000, 5000)]
        public int Size { get; set; }

        public string Url { get; } = "https://www.google.com/";

        public HttpClient HttpClient { get; private set; }

        [GlobalSetup]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            HttpClient = httpClientFactory.CreateClient();
        }

        [Benchmark]
        public Task<string> ContinueWithTask()
        {
            return GetContentStringV1Async(Url);
        }

        [Benchmark]
        public Task<string> AsyncAwaitTask()
        {
            return GetContentStringV2Async(Url);
        }

        public async Task<string> GetContentStringV1Async(string url)
        {
            var request = await HttpClient.GetAsync(url);
            var response = await request.Content.ReadAsStringAsync();
            return response;
        }

        public Task<string> GetContentStringV2Async(string url) 
        { 
            var request = HttpClient.GetAsync(url); 
            var response = request
                .ContinueWith(message => message.Result.Content.ReadAsStringAsync()); 
            return response.Unwrap(); 
        }
    }
}