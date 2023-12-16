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

namespace Example06;

public class BenchmarkConfig : ManualConfig
{
    private const string OldVersion = "2.10.1";
    private const string NewVersion = "2.14.1";
    private const string PackageName1 = "Humanizer.Core";
    private const string PackageName2 = "Humanizer.Core.fr";
    
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
        AddJob(Job.Default.WithRuntime(CoreRuntime.Core60).WithNuGets(new NuGetReference(PackageName1, OldVersion), new NuGetReference(PackageName2, OldVersion)).WithId(OldVersion));
        AddJob(Job.Default.WithRuntime(CoreRuntime.Core70).WithNuGets(new NuGetReference(PackageName1, OldVersion), new NuGetReference(PackageName2, OldVersion)).WithId(OldVersion));
        AddJob(Job.Default.WithRuntime(CoreRuntime.Core60).WithNuGets(new NuGetReference(PackageName1, NewVersion), new NuGetReference(PackageName2, NewVersion)).WithId(NewVersion));
        AddJob(Job.Default.WithRuntime(CoreRuntime.Core70).WithNuGets(new NuGetReference(PackageName1, NewVersion), new NuGetReference(PackageName2, NewVersion)).WithId(NewVersion));
    }
}