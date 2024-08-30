namespace Libstapsdt;

using System.Runtime.InteropServices;
using ProbePtr = System.IntPtr;

// Source: https://github.com/linux-usdt/libstapsdt/blob/0d53f987b0787362fd9c16a93cdad2c273d809fc/src/libstapsdt.h

public static class Libstapsdt
{
    private const string LibstapsdtLibrary = "libstapsdt.so.0";

    // P/Invoke function declarations

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern ref SDTProvider_t providerInit(string name);

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern int providerUseMemfd(ref SDTProvider_t provider, MemFDOption_t use_memfd);

    // Overloads for providerAddProbe for different argument counts

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern ProbePtr providerAddProbe(ref SDTProvider_t provider, string name, int argCount, ArgType_t arg1);

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern ProbePtr providerAddProbe(ref SDTProvider_t provider, string name, int argCount, ArgType_t arg1, ArgType_t arg2);

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern ProbePtr providerAddProbe(ref SDTProvider_t provider, string name, int argCount, ArgType_t arg1, ArgType_t arg2, ArgType_t arg3);

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern ProbePtr providerAddProbe(ref SDTProvider_t provider, string name, int argCount, ArgType_t arg1, ArgType_t arg2, ArgType_t arg3, ArgType_t arg4);

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern ProbePtr providerAddProbe(ref SDTProvider_t provider, string name, int argCount, ArgType_t arg1, ArgType_t arg2, ArgType_t arg3, ArgType_t arg4, ArgType_t arg5);

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern ProbePtr providerAddProbe(ref SDTProvider_t provider, string name, int argCount, ArgType_t arg1, ArgType_t arg2, ArgType_t arg3, ArgType_t arg4, ArgType_t arg5, ArgType_t arg6);

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern int providerLoad(ref SDTProvider_t provider);  // return -1 on error, 0 on success

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern int providerUnload(ref SDTProvider_t provider);  // return -1 on error, 0 on success

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern void providerDestroy(ref SDTProvider_t provider);

    // Overloads for probeFire for different argument counts

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern void probeFire(ProbePtr probe);

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern void probeFire(ProbePtr probe, long arg1);

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern void probeFire(ProbePtr probe, long arg1, long arg2);

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern void probeFire(ProbePtr probe, long arg1, long arg2, long arg3);

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern void probeFire(ProbePtr probe, long arg1, long arg2, long arg3, long arg4);

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern void probeFire(ProbePtr probe, long arg1, long arg2, long arg3, long arg4, long arg5);

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern void probeFire(ProbePtr probe, long arg1, long arg2, long arg3, long arg4, long arg5, long arg6);

    [DllImport(LibstapsdtLibrary, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool probeIsEnabled(ProbePtr probe);  // return 1 if true, 0 if false
}

public enum SDTError_t
{
    NoError = -1,
    ElfCreationError = 0,
    TmpCreationError = 1,
    SharedLibraryOpenError = 2,
    SymbolLoadingError = 3,
    SharedLibraryCloseError = 4
}

public enum ArgType_t
{
    NoArg = 0,
    UInt8 = 1,
    Int8 = -1,
    UInt16 = 2,
    Int16 = -2,
    UInt32 = 4,
    Int32 = -4,
    UInt64 = 8,
    Int64 = -8
}

public enum MemFDOption_t
{
    MemfdDisabled = 0,
    MemfdEnabled = 1
}

// Structs from the C header file

[StructLayout(LayoutKind.Sequential)]
public struct SDTProvider_t
{
    public IntPtr Name;  // char*
    public IntPtr Probes; // struct SDTProbeList_t*
    public SDTError_t Errno;
    public IntPtr Error; // char*

    // private members
    private IntPtr _Handle;
    private IntPtr _Filename;
    private int _Memfd;
    private MemFDOption_t _UseMemfd;
}
