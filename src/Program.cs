#pragma warning disable CA2201 // Do not raise reserved exception types
#pragma warning disable CA1303 // Do not pass literals as localized parameters

using System.Globalization;
using System.Runtime.InteropServices;
using Libstapsdt;


try
{
    var providerName = "myprovider";
    var probeName = "myprobe";

    var provider = Libstapsdt.Libstapsdt.ProviderInit(providerName);

    //Libstapsdt.Libstapsdt.ProviderUseMemfd(ref provider, MemFDOption_t.MemfdEnabled);

    var probe = Libstapsdt.Libstapsdt.ProviderAddProbe(provider, probeName, ArgType.Int64, ArgType.UInt64);
    if (probe == IntPtr.Zero)
        throw new Exception("Could not initialize the probe");

    var res = Libstapsdt.Libstapsdt.ProviderLoad(provider);
    if (res != 0)
        throw new Exception("Could not load provider");

    Console.WriteLine("Ready! Trace me with:");
    Console.WriteLine($$"""   sudo bpftrace -p {{Environment.ProcessId}} -e 'usdt:*:myprovider:myprobe { printf("Fired values: %ld %s\n", arg0, str(arg1)); }'""");

    while (true)
    {
        var val = long.MinValue;
        var isoTimeString = DateTime.Now.ToString("O", CultureInfo.InvariantCulture);
        var isoTimeStringPtr = Marshal.StringToCoTaskMemUTF8(isoTimeString);
        try
        {
            Libstapsdt.Libstapsdt.ProbeFire(probe, val, isoTimeStringPtr);
        }
        finally
        {
            Marshal.FreeCoTaskMem(isoTimeStringPtr);
        }
        Console.WriteLine("Probe fired! Probe is currently {0}", Libstapsdt.Libstapsdt.ProbeIsEnabled(probe) ? "watched" : "not watched");
        Thread.Sleep(500);
    }
}
catch (DllNotFoundException)
{
    Console.WriteLine("ERROR: libstapsdt is not found. Please install it: https://github.com/linux-usdt/libstapsdt?tab=readme-ov-file#install");
}
