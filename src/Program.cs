#pragma warning disable CA2201 // Do not raise reserved exception types
#pragma warning disable CA1303 // Do not pass literals as localized parameters

using System.Globalization;
using System.Runtime.InteropServices;
using DynamicProbes;
using Libstapsdt;

try
{
    var providerName = "myprovider";
    var probeName = "myprobe";

    using var provider = Provider.Init(providerName);

    //Libstapsdt.Libstapsdt.ProviderUseMemfd(ref provider, MemFDOption_t.MemfdEnabled);

    var probe = provider.AddProbe<Int64Arg, IntPtrAsUInt64Arg>(probeName);
    _ = provider.Load();

    Console.WriteLine("Ready! Trace me with:");
    Console.WriteLine($$"""   sudo bpftrace -p {{Environment.ProcessId}} -e 'usdt:*:myprovider:myprobe { printf("Fired values: %ld %s\n", arg0, str(arg1)); }'""");

    while (true)
    {
        var val = long.MinValue;
        var isoTimeString = DateTime.Now.ToString("O", CultureInfo.InvariantCulture);
        var isoTimeStringPtr = Marshal.StringToCoTaskMemUTF8(isoTimeString);
        try
        {
            probe.Active?.Fire(val, isoTimeStringPtr);
        }
        finally
        {
            Marshal.FreeCoTaskMem(isoTimeStringPtr);
        }
        Console.WriteLine("Probe fired! Probe is currently {0}", probe.IsEnabled ? "watched" : "not watched");
        Thread.Sleep(500);
    }
}
catch (DllNotFoundException)
{
    Console.WriteLine("ERROR: libstapsdt is not found. Please install it: https://github.com/linux-usdt/libstapsdt?tab=readme-ov-file#install");
}

/// <summary>
/// <see langword="nint"/> as an argument of type <see cref="ArgType_t.UInt64"/>.
/// </summary>
readonly record struct IntPtrAsUInt64Arg(nint Value) : IArgType, IFireArgLong
{
    public static ArgType ArgType => ArgType.UInt64;
    public static implicit operator IntPtrAsUInt64Arg(nint value) => new(value);
    long IFireArgLong.UncheckedValue => Value;
}
