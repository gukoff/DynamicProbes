namespace MyBenchmarks;

using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Libstapsdt;

public class Benchmarks
{
    private SDTProvider_t provider;
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
        probe = Libstapsdt.providerAddProbe(ref provider, "myprobe1", 1, ArgType_t.Int64);
        probe = Libstapsdt.providerAddProbe(ref provider, "myprobe2", 2, ArgType_t.Int64, ArgType_t.Int64);
        probe = Libstapsdt.providerAddProbe(ref provider, "myprobe3", 3, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64);
        probe = Libstapsdt.providerAddProbe(ref provider, "myprobe4", 4, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64);
        probe = Libstapsdt.providerAddProbe(ref provider, "myprobe5", 5, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64);
        probe = Libstapsdt.providerAddProbe(ref provider, "myprobe6", 6, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64, ArgType_t.Int64);
        _ = Libstapsdt.providerLoad(ref provider);
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        _ = Libstapsdt.providerUnload(ref provider);
        Libstapsdt.providerDestroy(ref provider);
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
        var summary = BenchmarkRunner.Run<Benchmarks>();
    }
}
