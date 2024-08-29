using System.Runtime.CompilerServices;

namespace Libstapsdt;

using System.Runtime.InteropServices;
using ProbePtr = nint;
using SdtProviderPtr = nint;

// Source: https://github.com/linux-usdt/libstapsdt/blob/0d53f987b0787362fd9c16a93cdad2c273d809fc/src/libstapsdt.h

public static partial class Libstapsdt
{
    private const string LibstapsdtLibrary = "libstapsdt.so.0";

    // P/Invoke function declarations
    [LibraryImport(LibstapsdtLibrary, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SdtProviderPtr providerInit(string name);

    [LibraryImport(LibstapsdtLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int providerUseMemfd(SdtProviderPtr provider, MemFDOption_t use_memfd);

    // Overloads for providerAddProbe for different argument counts

    [LibraryImport(LibstapsdtLibrary, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ProbePtr providerAddProbe(SdtProviderPtr provider, string name, int argCount, ArgType_t arg1);

    [LibraryImport(LibstapsdtLibrary, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ProbePtr providerAddProbe(SdtProviderPtr provider, string name, int argCount, ArgType_t arg1, ArgType_t arg2);

    [LibraryImport(LibstapsdtLibrary, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ProbePtr providerAddProbe(SdtProviderPtr provider, string name, int argCount, ArgType_t arg1, ArgType_t arg2, ArgType_t arg3);

    [LibraryImport(LibstapsdtLibrary, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ProbePtr providerAddProbe(SdtProviderPtr provider, string name, int argCount, ArgType_t arg1, ArgType_t arg2, ArgType_t arg3, ArgType_t arg4);

    [LibraryImport(LibstapsdtLibrary, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ProbePtr providerAddProbe(SdtProviderPtr provider, string name, int argCount, ArgType_t arg1, ArgType_t arg2, ArgType_t arg3, ArgType_t arg4, ArgType_t arg5);

    [LibraryImport(LibstapsdtLibrary, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ProbePtr providerAddProbe(SdtProviderPtr provider, string name, int argCount, ArgType_t arg1, ArgType_t arg2, ArgType_t arg3, ArgType_t arg4, ArgType_t arg5, ArgType_t arg6);

    [LibraryImport(LibstapsdtLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int providerLoad(SdtProviderPtr provider);  // return -1 on error, 0 on success

    [LibraryImport(LibstapsdtLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int providerUnload(SdtProviderPtr provider);  // return -1 on error, 0 on success

    [LibraryImport(LibstapsdtLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void providerDestroy(SdtProviderPtr provider);

    // Overloads for probeFire for different argument counts

    [LibraryImport(LibstapsdtLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void probeFire(ProbePtr probe);

    [LibraryImport(LibstapsdtLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void probeFire(ProbePtr probe, long arg1);

    [LibraryImport(LibstapsdtLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void probeFire(ProbePtr probe, long arg1, long arg2);

    [LibraryImport(LibstapsdtLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void probeFire(ProbePtr probe, long arg1, long arg2, long arg3);

    [LibraryImport(LibstapsdtLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void probeFire(ProbePtr probe, long arg1, long arg2, long arg3, long arg4);

    [LibraryImport(LibstapsdtLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void probeFire(ProbePtr probe, long arg1, long arg2, long arg3, long arg4, long arg5);

    [LibraryImport(LibstapsdtLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void probeFire(ProbePtr probe, long arg1, long arg2, long arg3, long arg4, long arg5, long arg6);

    [LibraryImport(LibstapsdtLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool probeIsEnabled(ProbePtr probe);  // return 1 if true, 0 if false
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
