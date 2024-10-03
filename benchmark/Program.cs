using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using DynamicProbes;

namespace Benchmark;

[JsonExporterAttribute.Full]
[JsonExporterAttribute.FullCompressed]
[SimpleJob(RuntimeMoniker.NativeAot80)]
public class UnobservedProbeFireBenchmarks
{
    ILoadedProvider? provider;
    Probe<Int64Arg> probe1;
    Probe<Int64Arg, Int64Arg> probe2;
    Probe<Int64Arg, Int64Arg, Int64Arg> probe3;
    Probe<Int64Arg, Int64Arg, Int64Arg, Int64Arg> probe4;
    Probe<Int64Arg, Int64Arg, Int64Arg, Int64Arg, Int64Arg> probe5;
    Probe<Int64Arg, Int64Arg, Int64Arg, Int64Arg, Int64Arg, Int64Arg> probe6;

    [GlobalSetup]
    public void GlobalSetup()
    {
        var provider = Provider.Init("myprovider");
        try
        {
            this.probe1 = provider.AddProbe<Int64Arg>("myprobe1");
            this.probe2 = provider.AddProbe<Int64Arg, Int64Arg>("myprobe2");
            this.probe3 = provider.AddProbe<Int64Arg, Int64Arg, Int64Arg>("myprobe3");
            this.probe4 = provider.AddProbe<Int64Arg, Int64Arg, Int64Arg, Int64Arg>("myprobe4");
            this.probe5 = provider.AddProbe<Int64Arg, Int64Arg, Int64Arg, Int64Arg, Int64Arg>("myprobe5");
            this.probe6 = provider.AddProbe<Int64Arg, Int64Arg, Int64Arg, Int64Arg, Int64Arg, Int64Arg>("myprobe6");
            this.provider = provider.Load();
        }
        catch
        {
            provider.Dispose();
            throw;
        }
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        this.provider?.Dispose();
    }

    const long Arg1 = 1234567890123456789;
    const long Arg2 = 2234567890123456789;
    const long Arg3 = 3234567890123456789;
    const long Arg4 = 4234567890123456789;
    const long Arg5 = 5234567890123456789;
    const long Arg6 = 6234567890123456789;


    [Benchmark]
    public void ArgCount1() => this.probe1.Fire(Arg1);

    [Benchmark]
    public void ArgCount2() => this.probe2.Fire(Arg1, Arg2);

    [Benchmark]
    public void ArgCount3() => this.probe3.Fire(Arg1, Arg2, Arg3);

    [Benchmark]
    public void ArgCount4() => this.probe4.Fire(Arg1, Arg2, Arg3, Arg4);

    [Benchmark]
    public void ArgCount5() => this.probe5.Fire(Arg1, Arg2, Arg3, Arg4, Arg5);

    [Benchmark]
    public void ArgCount6() => this.probe6.Fire(Arg1, Arg2, Arg3, Arg4, Arg5, Arg6);
}

public static class Program
{
    public static void Main()
    {
        _ = BenchmarkRunner.Run<UnobservedProbeFireBenchmarks>();
    }
}
