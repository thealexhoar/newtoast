using System;
using System.Runtime.InteropServices;


namespace NTF
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RuntimeSingletons
    {
        public static RuntimeSingletons instance;

        public nint Foo;
        // public nint Render;
    }

    public static partial class Lib
    {

        const string LIB_NEWTOAST_CORE = "newtoast_core";

        // [LibraryImport(LIB_NEWTOAST_CORE, EntryPoint = "test_internal_call")]
        // public static partial int TestInternalCall(int x);

        [LibraryImport(LIB_NEWTOAST_CORE, EntryPoint = "test_internal_ptr_return_call")]
        public static partial IntPtr TestInternalPtrReturnCall();

        [LibraryImport(LIB_NEWTOAST_CORE, EntryPoint = "test_modify_ref")]
        public static partial void TestModifyRef(nint foo);
    }


}