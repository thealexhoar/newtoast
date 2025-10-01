using NTF;
using System;
using GlmSharp;


[assembly:NTEntrypointSpecifier("NTF.Test")]

namespace NTF;

public class Test : NTEntrypoint
{
    public override InitConfig GetConfig()
    {
        return new InitConfigBuilder()
            .WithTitle("NewToast Game!")
            .WithSize(960, 720)
            .WithVSync(true)
            .WithWindowed(true)
            .Build();
    }

    public override void Initialize()
    {
        vec2 a = new vec2(1, 2);
        Console.WriteLine($"Hello from Test.Load! {a}");
    }

    public override void Shutdown()
    {
        Console.WriteLine("Hello from Test.Unload!");
    }

    public override  void Update(double dt)
    {
        // Console.WriteLine("Hello from Test.Update!");
    }

    public override void Draw()
    {
        // Console.WriteLine("Hello from Test.Draw!");
    }
}