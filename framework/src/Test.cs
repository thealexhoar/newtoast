using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace NTF
{

    public delegate int FooFn();

    public delegate int BarFn(int x);

    public delegate IntPtr MemTestFn();

    public delegate void EnqueueFn(int x);
    public delegate void DumpQueueFn();

    public static class Test
    {
        static ConcurrentQueue<int> q = new();

        static Queue<int> q2 = new();
        static Mutex qm = new(); // Mutex is system-level and could theoretically sync w/ Rust

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

        public static void TestEnqueue(int x)
        {
            // q.Enqueue(x);
            lock (q2)
            {
                q2.Enqueue(x);
            }
        }


        public static void DumpQueue()
        {
            Console.WriteLine("Dumping test queue");

            // foreach (var i in q) Console.WriteLine($"  {i}");

            lock (q2)
            {
                foreach (var i in q2) Console.WriteLine($"  {i}");
            }
        }

    }
}