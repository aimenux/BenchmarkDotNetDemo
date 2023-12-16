using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using Example02;

var benchmarkConfig = ManualConfig
    .Create(DefaultConfig.Instance)
    .AddDiagnoser(MemoryDiagnoser.Default)
    .AddDiagnoser(ExceptionDiagnoser.Default)
    .WithOptions(ConfigOptions.KeepBenchmarkFiles)
    .AddValidator(ExecutionValidator.FailOnError)
    .AddValidator(JitOptimizationsValidator.FailOnError)
    .AddLogicalGroupRules(BenchmarkLogicalGroupRule.ByParams)
    .AddColumn(StatisticColumn.Min, StatisticColumn.Max, RankColumn.Arabic)
    .WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest))
    .WithSummaryStyle(SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend))
    .AddJob(Job.Default.WithRuntime(CoreRuntime.Core60).WithId(nameof(RuntimeMoniker.Net60)))
    .AddJob(Job.Default.WithRuntime(CoreRuntime.Core60).WithId(nameof(RuntimeMoniker.Net70)));

BenchmarkRunner.Run<BenchmarkSample>(benchmarkConfig);