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

namespace Example05;

public class BenchmarkConfig : ManualConfig
{
    private const string OldVersion = "11.0.1";
    private const string NewVersion = "12.0.1";
    private const string PackageName = "AutoMapper";
    
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
        AddJob(Job.Default.WithRuntime(CoreRuntime.Core60).WithNuGet(PackageName, OldVersion).WithId(OldVersion));
        AddJob(Job.Default.WithRuntime(CoreRuntime.Core70).WithNuGet(PackageName, OldVersion).WithId(OldVersion));
        AddJob(Job.Default.WithRuntime(CoreRuntime.Core60).WithNuGet(PackageName, NewVersion).WithId(NewVersion));
        AddJob(Job.Default.WithRuntime(CoreRuntime.Core70).WithNuGet(PackageName, NewVersion).WithId(NewVersion));
    }
}