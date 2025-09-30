using NTF;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;


[assembly:NTEntrypointSpecifier("NTF.Test")]

namespace NTF
{
    public class Test : NTEntrypoint
    {
        public void Initialize()
        {
            Console.WriteLine("Hello from Test.Load!");
        }

        public void Shutdown()
        {
            Console.WriteLine("Hello from Test.Unload!");
        }

        public void Update(double dt)
        {
            Console.WriteLine("Hello from Test.Update!");
        }

        public void Draw()
        {
            Console.WriteLine("Hello from Test.Draw!");
        }
    }
}