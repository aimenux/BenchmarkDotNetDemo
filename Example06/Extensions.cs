using BenchmarkDotNet.Jobs;

namespace Example06;

public static class Extensions
{
    public static Job WithNuGets(this Job job, params NuGetReference[] nuGetReferences)
    {
        ArgumentNullException.ThrowIfNull(nuGetReferences);
        return job.WithNuGet(new NuGetReferenceList(nuGetReferences));
    }
}