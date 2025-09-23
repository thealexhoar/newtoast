using System;
using System.Runtime.InteropServices;


namespace NTF
{

    public static partial class Lib
    {
        const string LIB_NEWTOAST_CORE = "newtoast_core";

        [LibraryImport(LIB_NEWTOAST_CORE, EntryPoint = "test_internal_call")]
        public static partial int TestInternalCall(int x);

        [LibraryImport(LIB_NEWTOAST_CORE, EntryPoint = "test_internal_ptr_return_call")]
        public static partial IntPtr TestInternalPtrReturnCall();
    }
}