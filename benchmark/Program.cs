namespace Benchmark;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Libstapsdt;

[JsonExporterAttribute.Full]
[JsonExporterAttribute.FullCompressed]
[SimpleJob(RuntimeMoniker.NativeAot80)]
public class NotObservedProbeFireBenchmarks
{
    private nint provider;
    private nint probe;
    private readonly long arg1 = 1234567890123456789;
    private readonly long arg2 = 2234567890123456789;
    private readonly long arg3 = 3234567890123456789;
    private readonly long arg4 = 4234567890123456789;
    private readonly long arg5 = 5234567890123456789;
    private readonly long arg6 = 6234567890123456789;

    [GlobalSetup]
    public void GlobalSetup()
    {
        provider = Libstapsdt.providerInit("myprovider");
        probe = Libstapsdt.providerAddProbe(provider, "myprobe1", 1, ArgType_t.Int64);
        probe = Libstapsdt.providerAddProbe(provider, "myprobe2", 2, ArgType_t.Int64, ArgType_t.Int64);
        probe = Libstapsdt.providerAddProbe(provider, "myprobe3", 3, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64);
        probe = Libstapsdt.providerAddProbe(provider, "myprobe4", 4, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64);
        probe = Libstapsdt.providerAddProbe(provider, "myprobe5", 5, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64);
        probe = Libstapsdt.providerAddProbe(provider, "myprobe6", 6, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64);
        _ = Libstapsdt.providerLoad(provider);
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        _ = Libstapsdt.providerUnload(provider);
        Libstapsdt.providerDestroy(provider);
    }

    [Benchmark]
    public void ArgCount1() => Libstapsdt.probeFire(probe, arg1);

    [Benchmark]
    public void ArgCount2() => Libstapsdt.probeFire(probe, arg1, arg2);

    [Benchmark]
    public void ArgCount3() => Libstapsdt.probeFire(probe, arg1, arg2, arg3);

    [Benchmark]
    public void ArgCount4() => Libstapsdt.probeFire(probe, arg1, arg2, arg3, arg4);

    [Benchmark]
    public void ArgCount5() => Libstapsdt.probeFire(probe, arg1, arg2, arg3, arg4, arg5);

    [Benchmark]
    public void ArgCount6() => Libstapsdt.probeFire(probe, arg1, arg2, arg3, arg4, arg5, arg6);
}

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<NotObservedProbeFireBenchmarks>();
    }
}
