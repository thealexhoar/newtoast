using System.Runtime.InteropServices;

namespace NTF;

[StructLayout(LayoutKind.Sequential)]
public struct InitConfig
{
    public nint WindowTitle;
    public int WindowWidth;
    public int WindowHeight;
    public bool VSync;

    public bool Windowed;
}


public class InitConfigBuilder
{
    private InitConfig config;

    public InitConfigBuilder()
    {
        config = new InitConfig
        {
            WindowTitle = Marshal.StringToHGlobalUni("NewToast Application"),
            WindowWidth = 320,
            WindowHeight = 240,
            VSync = true,
            Windowed = true
        };
    }

    public InitConfigBuilder WithTitle(string title)
    {
        if (config.WindowTitle != nint.Zero)
            Marshal.FreeHGlobal(config.WindowTitle);
        config.WindowTitle = Marshal.StringToHGlobalUni(title);
        return this;
    }

    public InitConfigBuilder WithSize(int width, int height)
    {
        config.WindowWidth = width;
        config.WindowHeight = height;
        return this;
    }

    public InitConfigBuilder WithVSync(bool vsync)
    {
        config.VSync = vsync;
        return this;
    }

    public InitConfigBuilder WithWindowed(bool windowed)
    {
        config.Windowed = windowed;
        return this;
    }

    public InitConfig Build()
    {
        return config;
    }
}