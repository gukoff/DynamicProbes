﻿// <auto-generated/>
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Interop.LibraryImportGenerator", "8.0.10.36612")]
        [global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
        public static partial nint ProviderInit(string name)
        {
            byte* __name_native = default;
            nint __retVal = default;
            // Setup - Perform required setup.
            scoped global::System.Runtime.InteropServices.Marshalling.Utf8StringMarshaller.ManagedToUnmanagedIn __name_native__marshaller = new();
            try
            {
                // Marshal - Convert managed data to native data.
                __name_native__marshaller.FromManaged(name, stackalloc byte[global::System.Runtime.InteropServices.Marshalling.Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize]);
                {
                    // PinnedMarshal - Convert managed data to native data that requires the managed data to be pinned.
                    __name_native = __name_native__marshaller.ToUnmanaged();
                    __retVal = __PInvoke(__name_native);
                }
            }
            finally
            {
                // CleanupCallerAllocated - Perform cleanup of caller allocated resources.
                __name_native__marshaller.Free();
            }

            return __retVal;
            // Local P/Invoke
            [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "providerInit", ExactSpelling = true)]
            [global::System.Runtime.InteropServices.UnmanagedCallConvAttribute(CallConvs = new global::System.Type[] { typeof(global::System.Runtime.CompilerServices.CallConvCdecl) })]
            static extern unsafe nint __PInvoke(byte* __name_native);
        }
    }
}
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "providerUseMemfd", ExactSpelling = true)]
        public static extern partial int ProviderUseMemfd(nint provider, global::LibstapsdtPinvokes.MemfdOption option);
    }
}
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Interop.LibraryImportGenerator", "8.0.10.36612")]
        [global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
        private static partial nint ProviderAddProbe(nint provider, string name, int argCount, global::LibstapsdtPinvokes.ArgType arg1)
        {
            byte* __name_native = default;
            nint __retVal = default;
            // Setup - Perform required setup.
            scoped global::System.Runtime.InteropServices.Marshalling.Utf8StringMarshaller.ManagedToUnmanagedIn __name_native__marshaller = new();
            try
            {
                // Marshal - Convert managed data to native data.
                __name_native__marshaller.FromManaged(name, stackalloc byte[global::System.Runtime.InteropServices.Marshalling.Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize]);
                {
                    // PinnedMarshal - Convert managed data to native data that requires the managed data to be pinned.
                    __name_native = __name_native__marshaller.ToUnmanaged();
                    __retVal = __PInvoke(provider, __name_native, argCount, arg1);
                }
            }
            finally
            {
                // CleanupCallerAllocated - Perform cleanup of caller allocated resources.
                __name_native__marshaller.Free();
            }

            return __retVal;
            // Local P/Invoke
            [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "providerAddProbe", ExactSpelling = true)]
            [global::System.Runtime.InteropServices.UnmanagedCallConvAttribute(CallConvs = new global::System.Type[] { typeof(global::System.Runtime.CompilerServices.CallConvCdecl) })]
            static extern unsafe nint __PInvoke(nint __provider_native, byte* __name_native, int __argCount_native, global::LibstapsdtPinvokes.ArgType __arg1_native);
        }
    }
}
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Interop.LibraryImportGenerator", "8.0.10.36612")]
        [global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
        private static partial nint ProviderAddProbe(nint provider, string name, int argCount, global::LibstapsdtPinvokes.ArgType arg1, global::LibstapsdtPinvokes.ArgType arg2)
        {
            byte* __name_native = default;
            nint __retVal = default;
            // Setup - Perform required setup.
            scoped global::System.Runtime.InteropServices.Marshalling.Utf8StringMarshaller.ManagedToUnmanagedIn __name_native__marshaller = new();
            try
            {
                // Marshal - Convert managed data to native data.
                __name_native__marshaller.FromManaged(name, stackalloc byte[global::System.Runtime.InteropServices.Marshalling.Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize]);
                {
                    // PinnedMarshal - Convert managed data to native data that requires the managed data to be pinned.
                    __name_native = __name_native__marshaller.ToUnmanaged();
                    __retVal = __PInvoke(provider, __name_native, argCount, arg1, arg2);
                }
            }
            finally
            {
                // CleanupCallerAllocated - Perform cleanup of caller allocated resources.
                __name_native__marshaller.Free();
            }

            return __retVal;
            // Local P/Invoke
            [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "providerAddProbe", ExactSpelling = true)]
            [global::System.Runtime.InteropServices.UnmanagedCallConvAttribute(CallConvs = new global::System.Type[] { typeof(global::System.Runtime.CompilerServices.CallConvCdecl) })]
            static extern unsafe nint __PInvoke(nint __provider_native, byte* __name_native, int __argCount_native, global::LibstapsdtPinvokes.ArgType __arg1_native, global::LibstapsdtPinvokes.ArgType __arg2_native);
        }
    }
}
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Interop.LibraryImportGenerator", "8.0.10.36612")]
        [global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
        private static partial nint ProviderAddProbe(nint provider, string name, int argCount, global::LibstapsdtPinvokes.ArgType arg1, global::LibstapsdtPinvokes.ArgType arg2, global::LibstapsdtPinvokes.ArgType arg3)
        {
            byte* __name_native = default;
            nint __retVal = default;
            // Setup - Perform required setup.
            scoped global::System.Runtime.InteropServices.Marshalling.Utf8StringMarshaller.ManagedToUnmanagedIn __name_native__marshaller = new();
            try
            {
                // Marshal - Convert managed data to native data.
                __name_native__marshaller.FromManaged(name, stackalloc byte[global::System.Runtime.InteropServices.Marshalling.Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize]);
                {
                    // PinnedMarshal - Convert managed data to native data that requires the managed data to be pinned.
                    __name_native = __name_native__marshaller.ToUnmanaged();
                    __retVal = __PInvoke(provider, __name_native, argCount, arg1, arg2, arg3);
                }
            }
            finally
            {
                // CleanupCallerAllocated - Perform cleanup of caller allocated resources.
                __name_native__marshaller.Free();
            }

            return __retVal;
            // Local P/Invoke
            [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "providerAddProbe", ExactSpelling = true)]
            [global::System.Runtime.InteropServices.UnmanagedCallConvAttribute(CallConvs = new global::System.Type[] { typeof(global::System.Runtime.CompilerServices.CallConvCdecl) })]
            static extern unsafe nint __PInvoke(nint __provider_native, byte* __name_native, int __argCount_native, global::LibstapsdtPinvokes.ArgType __arg1_native, global::LibstapsdtPinvokes.ArgType __arg2_native, global::LibstapsdtPinvokes.ArgType __arg3_native);
        }
    }
}
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Interop.LibraryImportGenerator", "8.0.10.36612")]
        [global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
        private static partial nint ProviderAddProbe(nint provider, string name, int argCount, global::LibstapsdtPinvokes.ArgType arg1, global::LibstapsdtPinvokes.ArgType arg2, global::LibstapsdtPinvokes.ArgType arg3, global::LibstapsdtPinvokes.ArgType arg4)
        {
            byte* __name_native = default;
            nint __retVal = default;
            // Setup - Perform required setup.
            scoped global::System.Runtime.InteropServices.Marshalling.Utf8StringMarshaller.ManagedToUnmanagedIn __name_native__marshaller = new();
            try
            {
                // Marshal - Convert managed data to native data.
                __name_native__marshaller.FromManaged(name, stackalloc byte[global::System.Runtime.InteropServices.Marshalling.Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize]);
                {
                    // PinnedMarshal - Convert managed data to native data that requires the managed data to be pinned.
                    __name_native = __name_native__marshaller.ToUnmanaged();
                    __retVal = __PInvoke(provider, __name_native, argCount, arg1, arg2, arg3, arg4);
                }
            }
            finally
            {
                // CleanupCallerAllocated - Perform cleanup of caller allocated resources.
                __name_native__marshaller.Free();
            }

            return __retVal;
            // Local P/Invoke
            [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "providerAddProbe", ExactSpelling = true)]
            [global::System.Runtime.InteropServices.UnmanagedCallConvAttribute(CallConvs = new global::System.Type[] { typeof(global::System.Runtime.CompilerServices.CallConvCdecl) })]
            static extern unsafe nint __PInvoke(nint __provider_native, byte* __name_native, int __argCount_native, global::LibstapsdtPinvokes.ArgType __arg1_native, global::LibstapsdtPinvokes.ArgType __arg2_native, global::LibstapsdtPinvokes.ArgType __arg3_native, global::LibstapsdtPinvokes.ArgType __arg4_native);
        }
    }
}
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Interop.LibraryImportGenerator", "8.0.10.36612")]
        [global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
        private static partial nint ProviderAddProbe(nint provider, string name, int argCount, global::LibstapsdtPinvokes.ArgType arg1, global::LibstapsdtPinvokes.ArgType arg2, global::LibstapsdtPinvokes.ArgType arg3, global::LibstapsdtPinvokes.ArgType arg4, global::LibstapsdtPinvokes.ArgType arg5)
        {
            byte* __name_native = default;
            nint __retVal = default;
            // Setup - Perform required setup.
            scoped global::System.Runtime.InteropServices.Marshalling.Utf8StringMarshaller.ManagedToUnmanagedIn __name_native__marshaller = new();
            try
            {
                // Marshal - Convert managed data to native data.
                __name_native__marshaller.FromManaged(name, stackalloc byte[global::System.Runtime.InteropServices.Marshalling.Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize]);
                {
                    // PinnedMarshal - Convert managed data to native data that requires the managed data to be pinned.
                    __name_native = __name_native__marshaller.ToUnmanaged();
                    __retVal = __PInvoke(provider, __name_native, argCount, arg1, arg2, arg3, arg4, arg5);
                }
            }
            finally
            {
                // CleanupCallerAllocated - Perform cleanup of caller allocated resources.
                __name_native__marshaller.Free();
            }

            return __retVal;
            // Local P/Invoke
            [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "providerAddProbe", ExactSpelling = true)]
            [global::System.Runtime.InteropServices.UnmanagedCallConvAttribute(CallConvs = new global::System.Type[] { typeof(global::System.Runtime.CompilerServices.CallConvCdecl) })]
            static extern unsafe nint __PInvoke(nint __provider_native, byte* __name_native, int __argCount_native, global::LibstapsdtPinvokes.ArgType __arg1_native, global::LibstapsdtPinvokes.ArgType __arg2_native, global::LibstapsdtPinvokes.ArgType __arg3_native, global::LibstapsdtPinvokes.ArgType __arg4_native, global::LibstapsdtPinvokes.ArgType __arg5_native);
        }
    }
}
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Interop.LibraryImportGenerator", "8.0.10.36612")]
        [global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
        private static partial nint ProviderAddProbe(nint provider, string name, int argCount, global::LibstapsdtPinvokes.ArgType arg1, global::LibstapsdtPinvokes.ArgType arg2, global::LibstapsdtPinvokes.ArgType arg3, global::LibstapsdtPinvokes.ArgType arg4, global::LibstapsdtPinvokes.ArgType arg5, global::LibstapsdtPinvokes.ArgType arg6)
        {
            byte* __name_native = default;
            nint __retVal = default;
            // Setup - Perform required setup.
            scoped global::System.Runtime.InteropServices.Marshalling.Utf8StringMarshaller.ManagedToUnmanagedIn __name_native__marshaller = new();
            try
            {
                // Marshal - Convert managed data to native data.
                __name_native__marshaller.FromManaged(name, stackalloc byte[global::System.Runtime.InteropServices.Marshalling.Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize]);
                {
                    // PinnedMarshal - Convert managed data to native data that requires the managed data to be pinned.
                    __name_native = __name_native__marshaller.ToUnmanaged();
                    __retVal = __PInvoke(provider, __name_native, argCount, arg1, arg2, arg3, arg4, arg5, arg6);
                }
            }
            finally
            {
                // CleanupCallerAllocated - Perform cleanup of caller allocated resources.
                __name_native__marshaller.Free();
            }

            return __retVal;
            // Local P/Invoke
            [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "providerAddProbe", ExactSpelling = true)]
            [global::System.Runtime.InteropServices.UnmanagedCallConvAttribute(CallConvs = new global::System.Type[] { typeof(global::System.Runtime.CompilerServices.CallConvCdecl) })]
            static extern unsafe nint __PInvoke(nint __provider_native, byte* __name_native, int __argCount_native, global::LibstapsdtPinvokes.ArgType __arg1_native, global::LibstapsdtPinvokes.ArgType __arg2_native, global::LibstapsdtPinvokes.ArgType __arg3_native, global::LibstapsdtPinvokes.ArgType __arg4_native, global::LibstapsdtPinvokes.ArgType __arg5_native, global::LibstapsdtPinvokes.ArgType __arg6_native);
        }
    }
}
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "providerLoad", ExactSpelling = true)]
        public static extern partial int ProviderLoad(nint provider);
    }
}
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "providerUnload", ExactSpelling = true)]
        public static extern partial int ProviderUnload(nint provider);
    }
}
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "providerDestroy", ExactSpelling = true)]
        public static extern partial void ProviderDestroy(nint provider);
    }
}
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "probeFire", ExactSpelling = true)]
        public static extern partial void ProbeFire(nint probe);
    }
}
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "probeFire", ExactSpelling = true)]
        public static extern partial void ProbeFire(nint probe, long arg1);
    }
}
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "probeFire", ExactSpelling = true)]
        public static extern partial void ProbeFire(nint probe, long arg1, long arg2);
    }
}
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "probeFire", ExactSpelling = true)]
        public static extern partial void ProbeFire(nint probe, long arg1, long arg2, long arg3);
    }
}
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "probeFire", ExactSpelling = true)]
        public static extern partial void ProbeFire(nint probe, long arg1, long arg2, long arg3, long arg4);
    }
}
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "probeFire", ExactSpelling = true)]
        public static extern partial void ProbeFire(nint probe, long arg1, long arg2, long arg3, long arg4, long arg5);
    }
}
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "probeFire", ExactSpelling = true)]
        public static extern partial void ProbeFire(nint probe, long arg1, long arg2, long arg3, long arg4, long arg5, long arg6);
    }
}
namespace LibstapsdtPinvokes
{
    public static unsafe partial class Libstapsdt
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Interop.LibraryImportGenerator", "8.0.10.36612")]
        [global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
        public static partial bool ProbeIsEnabled(nint probe)
        {
            bool __retVal;
            int __retVal_native;
            {
                __retVal_native = __PInvoke(probe);
            }

            // Unmarshal - Convert native data to managed data.
            __retVal = __retVal_native != 0;
            return __retVal;
            // Local P/Invoke
            [global::System.Runtime.InteropServices.DllImportAttribute("libstapsdt.so.0", EntryPoint = "probeIsEnabled", ExactSpelling = true)]
            [global::System.Runtime.InteropServices.UnmanagedCallConvAttribute(CallConvs = new global::System.Type[] { typeof(global::System.Runtime.CompilerServices.CallConvCdecl) })]
            static extern unsafe int __PInvoke(nint __probe_native);
        }
    }
}
