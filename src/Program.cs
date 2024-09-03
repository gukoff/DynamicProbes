using System.Globalization;
using System.Runtime.InteropServices;
using Libstapsdt;


try {
    var providerName = "myprovider";
    var probeName = "myprobe";

    var provider = Libstapsdt.Libstapsdt.providerInit(providerName);

    //Libstapsdt.Libstapsdt.providerUseMemfd(ref provider, MemFDOption_t.MemfdEnabled);

    var probe = Libstapsdt.Libstapsdt.providerAddProbe(provider, probeName, ArgType_t.Int64, ArgType_t.UInt64);
    if (probe == IntPtr.Zero) {
        throw new Exception("Could not initialize the probe");
    }
    var res = Libstapsdt.Libstapsdt.providerLoad(provider);
    if (res != 0) {
        throw new Exception("Could not load provider");
    }

    Console.WriteLine($"Ready! Trace me with:");
    Console.WriteLine($$"""   sudo bpftrace -p {{Environment.ProcessId}} -e 'usdt:*:myprovider:myprobe { printf("Fired values: %ld %s\n", arg0, str(arg1)); }'""");

    while (true) {
        var val = ulong.MinValue;
        var isoTimeString = DateTime.Now.ToString("O", CultureInfo.InvariantCulture);
        var isoTimeStringPtr = Marshal.StringToHGlobalAnsi(isoTimeString);
        try
        {
            Libstapsdt.Libstapsdt.probeFire(probe, long.MinValue, isoTimeStringPtr);
        }
        finally
        {
            Marshal.FreeHGlobal(isoTimeStringPtr);
        }
        Console.WriteLine("Probe fired! Probe is currently {0}", Libstapsdt.Libstapsdt.probeIsEnabled(probe) ? "watched" : "not watched");
        Thread.Sleep(500);
    }
}
catch (DllNotFoundException) {
    Console.WriteLine("ERROR: libstapsdt is not found. Please install it: https://github.com/linux-usdt/libstapsdt?tab=readme-ov-file#install");
}