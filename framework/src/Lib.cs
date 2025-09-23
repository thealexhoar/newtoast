using System;
using System.Runtime.InteropServices;


namespace NTF
{
    public static partial class Lib
    {

        [LibraryImport("newtoast_engine", EntryPoint = "test_internal_call")]
        public static partial int TestInternalCall(int x);

        [LibraryImport("newtoast_engine", EntryPoint = "test_internal_ptr_return_call")]
        public static partial IntPtr TestInternalPtrReturnCall();
    }
}