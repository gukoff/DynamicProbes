using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ProbePtr = nint;
using SdtProviderPtr = nint;

#pragma warning disable CA5392 // Use DefaultDllImportSearchPaths attribute for P/Invokes
                               // ...but it has no effect on non-Windows platforms or the Mono runtime.

namespace Libstapsdt;

// Source: https://github.com/linux-usdt/libstapsdt/blob/0d53f987b0787362fd9c16a93cdad2c273d809fc/src/libstapsdt.h

public static partial class Libstapsdt
{
    const string LibstapsdtLibrary = "libstapsdt.so.0";

    // P/Invoke function declarations
    [LibraryImport(LibstapsdtLibrary, EntryPoint = "providerInit", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SdtProviderPtr ProviderInit(string name);

    [LibraryImport(LibstapsdtLibrary, EntryPoint = "providerUseMemfd")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int ProviderUseMemfd(SdtProviderPtr provider, MemfdOption option);

    // Overloads for providerAddProbe for different argument counts

    public static ProbePtr ProviderAddProbe(SdtProviderPtr provider, string name, ArgType arg1) =>
        ProviderAddProbe(provider, name, 1, arg1);

    public static ProbePtr ProviderAddProbe(SdtProviderPtr provider, string name, ArgType arg1, ArgType arg2) =>
        ProviderAddProbe(provider, name, 2, arg1, arg2);

    public static ProbePtr ProviderAddProbe(SdtProviderPtr provider, string name, ArgType arg1, ArgType arg2, ArgType arg3) =>
        ProviderAddProbe(provider, name, 3, arg1, arg2, arg3);

    public static ProbePtr ProviderAddProbe(SdtProviderPtr provider, string name, ArgType arg1, ArgType arg2, ArgType arg3, ArgType arg4) =>
        ProviderAddProbe(provider, name, 4, arg1, arg2, arg3, arg4);

    public static ProbePtr ProviderAddProbe(SdtProviderPtr provider, string name, ArgType arg1, ArgType arg2, ArgType arg3, ArgType arg4, ArgType arg5) =>
        ProviderAddProbe(provider, name, 5, arg1, arg2, arg3, arg4, arg5);

    public static ProbePtr ProviderAddProbe(SdtProviderPtr provider, string name, ArgType arg1, ArgType arg2, ArgType arg3, ArgType arg4, ArgType arg5, ArgType arg6) =>
        ProviderAddProbe(provider, name, 6, arg1, arg2, arg3, arg4, arg5, arg6);

    [LibraryImport(LibstapsdtLibrary, EntryPoint = "providerAddProbe", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ProbePtr ProviderAddProbe(SdtProviderPtr provider, string name, int argCount, ArgType arg1);

    [LibraryImport(LibstapsdtLibrary, EntryPoint = "providerAddProbe", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ProbePtr ProviderAddProbe(SdtProviderPtr provider, string name, int argCount, ArgType arg1, ArgType arg2);

    [LibraryImport(LibstapsdtLibrary, EntryPoint = "providerAddProbe", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ProbePtr ProviderAddProbe(SdtProviderPtr provider, string name, int argCount, ArgType arg1, ArgType arg2, ArgType arg3);

    [LibraryImport(LibstapsdtLibrary, EntryPoint = "providerAddProbe", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ProbePtr ProviderAddProbe(SdtProviderPtr provider, string name, int argCount, ArgType arg1, ArgType arg2, ArgType arg3, ArgType arg4);

    [LibraryImport(LibstapsdtLibrary, EntryPoint = "providerAddProbe", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ProbePtr ProviderAddProbe(SdtProviderPtr provider, string name, int argCount, ArgType arg1, ArgType arg2, ArgType arg3, ArgType arg4, ArgType arg5);

    [LibraryImport(LibstapsdtLibrary, EntryPoint = "providerAddProbe", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ProbePtr ProviderAddProbe(SdtProviderPtr provider, string name, int argCount, ArgType arg1, ArgType arg2, ArgType arg3, ArgType arg4, ArgType arg5, ArgType arg6);

    [LibraryImport(LibstapsdtLibrary, EntryPoint = "providerLoad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int ProviderLoad(SdtProviderPtr provider);  // return -1 on error, 0 on success

    [LibraryImport(LibstapsdtLibrary, EntryPoint = "providerUnload")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int ProviderUnload(SdtProviderPtr provider);  // return -1 on error, 0 on success

    [LibraryImport(LibstapsdtLibrary, EntryPoint = "providerDestroy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ProviderDestroy(SdtProviderPtr provider);

    // Overloads for probeFire for different argument counts

    [LibraryImport(LibstapsdtLibrary, EntryPoint = "probeFire")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ProbeFire(ProbePtr probe);

    [LibraryImport(LibstapsdtLibrary, EntryPoint = "probeFire")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ProbeFire(ProbePtr probe, long arg1);

    [LibraryImport(LibstapsdtLibrary, EntryPoint = "probeFire")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ProbeFire(ProbePtr probe, long arg1, long arg2);

    [LibraryImport(LibstapsdtLibrary, EntryPoint = "probeFire")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ProbeFire(ProbePtr probe, long arg1, long arg2, long arg3);

    [LibraryImport(LibstapsdtLibrary, EntryPoint = "probeFire")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ProbeFire(ProbePtr probe, long arg1, long arg2, long arg3, long arg4);

    [LibraryImport(LibstapsdtLibrary, EntryPoint = "probeFire")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ProbeFire(ProbePtr probe, long arg1, long arg2, long arg3, long arg4, long arg5);

    [LibraryImport(LibstapsdtLibrary, EntryPoint = "probeFire")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ProbeFire(ProbePtr probe, long arg1, long arg2, long arg3, long arg4, long arg5, long arg6);

    [LibraryImport(LibstapsdtLibrary, EntryPoint = "probeIsEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool ProbeIsEnabled(ProbePtr probe);  // return 1 if true, 0 if false
}

enum SdtError // SDTError_t
{
    NoError                 = -1, // noError
    ElfCreationError        = 0,  // elfCreationError
    TmpCreationError        = 1,  // tmpCreationError
    SharedLibraryOpenError  = 2,  // sharedLibraryOpenError
    SymbolLoadingError      = 3,  // symbolLoadingError
    SharedLibraryCloseError = 4   // sharedLibraryCloseError
}

enum ArgType // ArgType_t
{
    NoArg = 0,  // noarg
    UInt8 = 1,  // uint8
    Int8 = -1,  // int8
    UInt16 = 2, // uint16
    Int16 = -2, // int16
    UInt32 = 4, // uint32
    Int32 = -4, // int32
    UInt64 = 8, // uint64
    Int64 = -8  // int64
}

enum MemfdOption // MemFDOption_t
{
    Disabled = 0, // memfd_disabled
    Enabled = 1   // memfd_enabled
}

[StructLayout(LayoutKind.Sequential)]
struct SdtProvider // SDTProvider_t
{
    public IntPtr Name;     // char* name
    public IntPtr Probes;   // SDTProbeList_t *probes
    public SdtError ErrNo;  // SDTError_t errno
    public IntPtr Error;    // char* error
}
