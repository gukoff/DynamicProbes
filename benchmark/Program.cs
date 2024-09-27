using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using LibstapsdtPinvokes;

namespace Benchmark;

[JsonExporterAttribute.Full]
[JsonExporterAttribute.FullCompressed]
[SimpleJob(RuntimeMoniker.NativeAot80)]
public class UnobservedProbeFireBenchmarks
{
    nint provider;
    nint probe1;
    nint probe2;
    nint probe3;
    nint probe4;
    nint probe5;
    nint probe6;

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

    const long Arg1 = 1234567890123456789;
    const long Arg2 = 2234567890123456789;
    const long Arg3 = 3234567890123456789;
    const long Arg4 = 4234567890123456789;
    const long Arg5 = 5234567890123456789;
    const long Arg6 = 6234567890123456789;


    [Benchmark]
    public void ArgCount1() => Libstapsdt.ProbeFire(this.probe1, Arg1);

    [Benchmark]
    public void ArgCount2() => Libstapsdt.ProbeFire(this.probe2, Arg1, Arg2);

    [Benchmark]
    public void ArgCount3() => Libstapsdt.ProbeFire(this.probe3, Arg1, Arg2, Arg3);

    [Benchmark]
    public void ArgCount4() => Libstapsdt.ProbeFire(this.probe4, Arg1, Arg2, Arg3, Arg4);

    [Benchmark]
    public void ArgCount5() => Libstapsdt.ProbeFire(this.probe5, Arg1, Arg2, Arg3, Arg4, Arg5);

    [Benchmark]
    public void ArgCount6() => Libstapsdt.ProbeFire(this.probe6, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6);
}

public static class Program
{
    public static void Main()
    {
        _ = BenchmarkRunner.Run<UnobservedProbeFireBenchmarks>();
    }
}
