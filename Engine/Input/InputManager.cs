using ErandevEngine.Core;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ErandevEngine.Input;

public static class InputManager
{
    public static bool IsKeyPressed(Keys key)
    {
        var window = Erandev.Engine.Window;
        if (window == null) return false;

        return window.IsKeyPressed(key);
    }

    public static bool IsKeyDown(Keys key)
    {
        var window = Erandev.Engine.Window;
        if (window == null) return false;

        return window.IsKeyDown(key);
    }
}