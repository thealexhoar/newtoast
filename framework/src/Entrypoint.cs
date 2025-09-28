

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NTF
{
    public delegate NTEntrypointSpecifier FindEntrypointFn();

    public delegate void FreeHstrFn(nint hstr);

    public delegate void InitializeFn();
    public delegate void ShutdownFn();
    public delegate void UpdateFn(double dt);
    public delegate void DrawFn();


    [StructLayout(LayoutKind.Sequential)]
    public struct NTEntrypointSpecifier
    {
        public nint AssemblyName;
        public nint ClassName;
        public bool IsSet;
    }

    // public static class NTEntryPoint
    // {
    //     public static void Load() {}

    //     public static void Unload() { }

    //     public static void Update(double dt) {}

    //     public static void Draw() {}
    // }


    [AttributeUsage(AttributeTargets.Assembly)]
    public class NTEntrypointSpecifierAttribute : Attribute
    {
        public readonly string ClassName;
        public readonly string AssemblyName;

        public NTEntrypointSpecifierAttribute(string className)
        {
            ClassName = className;
            AssemblyName =  (string)GetType().Assembly.GetName().Name;
        }
    }

    public interface NTEntryPoint
    {
        static abstract void Initialize();
        static abstract void Shutdown();
        static abstract void Update(double dt);
        static abstract void Draw();


        public static void FreeHstr(nint hstr)
        {
            if (hstr != nint.Zero)
            {
                Console.WriteLine($"C#: Freeing hstr {hstr}");
                Marshal.FreeHGlobal(hstr);
            }
        }

        public static NTEntrypointSpecifier IdentifyEntrypoint()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var entrypoint = new NTEntrypointSpecifier();
            foreach (var asm in assemblies)
            {
                var allAttrs = asm.GetCustomAttributes(typeof(NTEntrypointSpecifierAttribute), false);
                var attrs = (NTEntrypointSpecifierAttribute[])allAttrs;
                if (attrs.Length > 0)
                {
                    entrypoint.AssemblyName = Marshal.StringToHGlobalUni(attrs[0].AssemblyName);
                    entrypoint.ClassName = Marshal.StringToHGlobalUni(attrs[0].ClassName);// HACK these need to be cleaned up
                    entrypoint.IsSet = true;
                    Console.WriteLine($"Found entrypoint in Assembly named:\n   {attrs[0].AssemblyName}");
                    break;
                }
            }
            return entrypoint;
        }
    }
}