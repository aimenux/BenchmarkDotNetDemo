using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Validators;

namespace Example04;

public class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        AddColumn(RankColumn.Arabic);
        AddColumn(StatisticColumn.Min);
        AddColumn(StatisticColumn.Max);
        AddLogger(ConsoleLogger.Default);
        AddExporter(MarkdownExporter.Default);
        AddDiagnoser(MemoryDiagnoser.Default);
        AddDiagnoser(ExceptionDiagnoser.Default);
        AddValidator(ExecutionValidator.FailOnError);
        AddValidator(JitOptimizationsValidator.FailOnError);
        AddLogicalGroupRules(BenchmarkLogicalGroupRule.ByParams);
        WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest));
        WithSummaryStyle(SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend));
        AddJob(Job.Default.WithRuntime(CoreRuntime.Core60).WithId(nameof(RuntimeMoniker.Net60)));
        AddJob(Job.Default.WithRuntime(CoreRuntime.Core70).WithId(nameof(RuntimeMoniker.Net70)));
    }
}