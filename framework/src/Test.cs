using NTF;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;


[assembly:NTEntrypointSpecifier("NTF", "Test")]

namespace NTF
{

    public delegate int FooFn();

    public delegate int BarFn(int x);

    public delegate IntPtr MemTestFn();

    public delegate void EnqueueFn(int x);
    public delegate void DumpQueueFn();
    public delegate bool FindEntrypointFn(ref NTEntrypointSpecifier entrypoint);

    public class Test
    {
        static ConcurrentQueue<int> q = new();

        static Queue<int> q2 = new();
        static Mutex qm = new(); // Mutex is system-level and could theoretically sync w/ Rust

        public static int Foo()
        {
            Console.WriteLine("Hello from Test.Foo!");


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


        public static bool FindEntrypoint(ref NTEntrypointSpecifier entrypoint)
        {
            Console.WriteLine("Finding entrypoint via FindEntrypoint()");
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            // var output = new NTEntrypointSpecifier();
            var outputSet = false;
            foreach (var asm in assemblies)
            {
                var allAttrs = asm.GetCustomAttributes(typeof(NTEntrypointSpecifierAttribute), false);
                var attrs = (NTEntrypointSpecifierAttribute[])allAttrs;
                if (attrs.Length > 0)
                {
                    entrypoint.NsName = attrs[0].NsName;
                    entrypoint.ClassName = attrs[0].ClassName;
                    return true;
                    // output.i = 12;
                }
            }
            return false;
        }

    }
}