

using System.Runtime.CompilerServices;

namespace NTF
{

    public interface NTEntryPoint
    {
        static abstract void Load();
        static abstract void Update();
        static abstract void Draw();
    }
}