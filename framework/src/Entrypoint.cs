

using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NTF
{
    public delegate NTEntrypointSpecifier FindEntrypointFn();
    public delegate void FreeHstrFn(nint hstr);
    public delegate void InitializeFn(RuntimeSingletons singletons);
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

    [StructLayout(LayoutKind.Sequential)]
    public struct InitConfig
    {
        public nint WindowTitle;
        public int WindowWidth;
        public int WindowHeight;
        public bool VSync;
        public bool Windowed;
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
            AssemblyName = (string)GetType().Assembly.GetName().Name;
        }
    }

    public static class NTEntrypointInternal
    {
        static NTEntrypoint? entrypointInstance;

        static void Initialize(RuntimeSingletons singletons)
        {
            RuntimeSingletons.instance = singletons;

            Lib.TestModifyRef(singletons.Foo);

            entrypointInstance = ConstructEntrypointInstance();
            if (entrypointInstance == null) {
                throw new Exception("Failed to construct entrypoint instance");
            }
            entrypointInstance.Initialize();
        }

        static void Shutdown()
        {
            entrypointInstance?.Shutdown();
        }

        static void Update(double dt)
        {
            entrypointInstance?.Update(dt);
        }

        static void Draw()
        {
            entrypointInstance?.Draw();
        }

        static void BindRenderServer(nint renderServer)
        {
            // TODO
        }

        static void UnbindRenderServer()
        {
            // TODO
        }

        public static void FreeHstr(nint hstr)
        {
            if (hstr != nint.Zero)
            {
                Console.WriteLine($"C#: Freeing hstr {hstr}");
                Marshal.FreeHGlobal(hstr);
            }
        }

        static NTEntrypointSpecifier IdentifyEntrypointForEngine()
        {
            var entrypoint = new NTEntrypointSpecifier {
                IsSet = false
            };
            var maybeEntrypoint = IdentifyEntrypoint();
            if (maybeEntrypoint.HasValue)
            {
                var (assemblyName, className) = maybeEntrypoint.Value;
                entrypoint.AssemblyName = Marshal.StringToHGlobalAnsi(assemblyName);
                entrypoint.ClassName = Marshal.StringToHGlobalAnsi(className);
                entrypoint.IsSet = true;
            }
            return entrypoint;
        }


        static (string, string)? IdentifyEntrypoint()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asm in assemblies)
            {
                var allAttrs = asm.GetCustomAttributes(typeof(NTEntrypointSpecifierAttribute), false);
                var attrs = (NTEntrypointSpecifierAttribute[])allAttrs;
                if (attrs.Length > 0)
                {
                    // Console.WriteLine($"Writing Types in assembly \"{asm.FullName}\":");
                    // foreach (var type in asm.GetTypes())
                    // {
                    //     Console.WriteLine($"  {type.FullName}");
                    // }
                    return (attrs[0].AssemblyName, attrs[0].ClassName);
                }
            }


            return null;
        }

        static NTEntrypoint? ConstructEntrypointInstance() {
            var maybeEntrypoint = IdentifyEntrypoint();
            if (maybeEntrypoint.HasValue)
            {
                var (assemblyName, className) = maybeEntrypoint.Value;
                return Activator.CreateInstance(assemblyName, className)?.Unwrap() as NTEntrypoint;
            }
            return null;
        }
    }

    public interface NTEntrypoint
    {
        void Initialize();
        void Shutdown();
        void Update(double dt);
        void Draw();
    }
}