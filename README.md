![.NET Core](https://github.com/aimenux/BenchmarkDotNetDemo/workflows/.NET%20Core/badge.svg)
# BenchmarkDotNetDemo
```
Benchmarking using BenchmarkDotNet library
```

In this demo, i m using 'BenchmarkDotNet' in order to benchmark some methods used in order to filter an input collection of integer.
'LinqToObjects' method is the best approach here since it consume less cpu & memory than the others methods.

```
|                          Method | NumberOfItems |      Mean |     Error |    StdDev |       Min |       Max | Ratio |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|-------------------------------- |-------------- |----------:|----------:|----------:|----------:|----------:|------:|-------:|-------:|------:|----------:|
| **ConsumeUsingTemporaryCollection** |          **1000** | **12.115 μs** | **0.0376 μs** | **0.0333 μs** | **12.039 μs** | **12.154 μs** |  **1.00** | **0.9155** |      **-** |     **-** |    **4376 B** |
|       ConsumeUsingYieldOperator |          1000 | 11.418 μs | 0.0948 μs | 0.0840 μs | 11.249 μs | 11.478 μs |  0.94 | 0.0153 |      - |     - |      80 B |
|       ConsumeUsingLinqToObjects |          1000 |  6.145 μs | 0.0258 μs | 0.0241 μs |  6.103 μs |  6.189 μs |  0.51 | 0.0076 |      - |     - |      48 B |
|                                 |               |           |           |           |           |           |       |        |        |       |           |
| **ConsumeUsingTemporaryCollection** |          **5000** | **63.814 μs** | **0.1978 μs** | **0.1850 μs** | **63.406 μs** | **64.073 μs** |  **1.00** | **6.9580** | **0.4883** |     **-** |   **33120 B** |
|       ConsumeUsingYieldOperator |          5000 | 59.783 μs | 0.4737 μs | 0.3956 μs | 59.116 μs | 60.382 μs |  0.94 |      - |      - |     - |      80 B |
|       ConsumeUsingLinqToObjects |          5000 | 38.973 μs | 0.1681 μs | 0.1573 μs | 38.786 μs | 39.351 μs |  0.61 |      - |      - |     - |      48 B |
