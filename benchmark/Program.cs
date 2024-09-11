using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using LibstapsdtPinvokes;

namespace Benchmark;

[JsonExporterAttribute.Full]
[JsonExporterAttribute.FullCompressed]
[SimpleJob(RuntimeMoniker.NativeAot80)]
public class NotObservedProbeFireBenchmarks
{
    nint provider;
    nint probe1;
    nint probe2;
    nint probe3;
    nint probe4;
    nint probe5;
    nint probe6;
    readonly long arg1 = 1234567890123456789;
    readonly long arg2 = 2234567890123456789;
    readonly long arg3 = 3234567890123456789;
    readonly long arg4 = 4234567890123456789;
    readonly long arg5 = 5234567890123456789;
    readonly long arg6 = 6234567890123456789;

    [GlobalSetup]
    public void GlobalSetup()
    {
        this.provider = Libstapsdt.ProviderInit("myprovider");
        this.probe1 = Libstapsdt.ProviderAddProbe(this.provider, "myprobe1", ArgType.Int64);
        this.probe2 = Libstapsdt.ProviderAddProbe(this.provider, "myprobe2", ArgType.Int64, ArgType.Int64);
        this.probe3 = Libstapsdt.ProviderAddProbe(this.provider, "myprobe3", ArgType.Int64, ArgType.Int64, ArgType.Int64);
        this.probe4 = Libstapsdt.ProviderAddProbe(this.provider, "myprobe4", ArgType.Int64, ArgType.Int64, ArgType.Int64, ArgType.Int64);
        this.probe5 = Libstapsdt.ProviderAddProbe(this.provider, "myprobe5", ArgType.Int64, ArgType.Int64, ArgType.Int64, ArgType.Int64, ArgType.Int64);
        this.probe6 = Libstapsdt.ProviderAddProbe(this.provider, "myprobe6", ArgType.Int64, ArgType.Int64, ArgType.Int64, ArgType.Int64, ArgType.Int64, ArgType.Int64);
        _ = Libstapsdt.ProviderLoad(this.provider);
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        _ = Libstapsdt.ProviderUnload(this.provider);
        Libstapsdt.ProviderDestroy(this.provider);
    }

    [Benchmark]
    public void ArgCount1() => Libstapsdt.ProbeFire(this.probe1, this.arg1);

    [Benchmark]
    public void ArgCount2() => Libstapsdt.ProbeFire(this.probe2, this.arg1, this.arg2);

    [Benchmark]
    public void ArgCount3() => Libstapsdt.ProbeFire(this.probe3, this.arg1, this.arg2, this.arg3);

    [Benchmark]
    public void ArgCount4() => Libstapsdt.ProbeFire(this.probe4, this.arg1, this.arg2, this.arg3, this.arg4);

    [Benchmark]
    public void ArgCount5() => Libstapsdt.ProbeFire(this.probe5, this.arg1, this.arg2, this.arg3, this.arg4, this.arg5);

    [Benchmark]
    public void ArgCount6() => Libstapsdt.ProbeFire(this.probe6, this.arg1, this.arg2, this.arg3, this.arg4, this.arg5, this.arg6);
}

public static class Program
{
    public static void Main()
    {
        _ = BenchmarkRunner.Run<NotObservedProbeFireBenchmarks>();
    }
}
