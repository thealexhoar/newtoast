using NTF;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;


[assembly:NTEntrypointSpecifier("NTF.Test")]

namespace NTF
{

    public class Test : NTEntryPoint
    {
        public static void Initialize()
        {
            Console.WriteLine("Hello from Test.Load!");
        }

        public static void Shutdown()
        {
            Console.WriteLine("Hello from Test.Unload!");
        }

        public static void Update(double dt)
        {
            Console.WriteLine("Hello from Test.Update!");
        }

        public static void Draw()
        {
            Console.WriteLine("Hello from Test.Draw!");
        }
    }
}