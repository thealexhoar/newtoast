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

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct NTEntrypointSpecifier
    {

        [MarshalAs(UnmanagedType.LPStr)]
        public string NsName;

        [MarshalAs(UnmanagedType.LPStr)]
        public string ClassName;
        // public int i;
    }


    [AttributeUsage(AttributeTargets.Assembly)]
    public class NTEntrypointSpecifierAttribute : Attribute
    {
        public readonly string NsName, ClassName;

        public NTEntrypointSpecifierAttribute(string nsName, string className)
        {
            NsName = nsName;
            ClassName = className;
        }
    }
}