using System;
using System.Runtime.InteropServices;

namespace NTF
{

    public delegate int FooFn();

    public delegate int BarFn(int x);

    public delegate IntPtr MemTestFn();

    public static class Test
    {

        public static int Foo()
        {
            return 12345;
        }

        public static int Bar(int x)
        {
            return Lib.TestInternalCall(x);
        }

        public static int Baz(IntPtr args, int sizeBytes)
        {
            return sizeBytes;
        }

        public static IntPtr MemTest()
        {
            return Lib.TestInternalPtrReturnCall();
        }

        // TOOD test multithreading from rust :)
    }
}