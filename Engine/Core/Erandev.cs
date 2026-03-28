using ErandevEngine.Input;
using ErandevEngine.Scenes;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace ErandevEngine.Core;

public class Erandev
{
    private static readonly Lazy<Erandev> Lazy = new(() => new Erandev());
    public static Erandev Engine => Lazy.Value;
    internal Window? Window;
    public SceneManager Scenes { get; } = new();
    private Erandev() { }

    public void CreateWindow(Vector2i size, string title = "ErandevEngine", double updateFrequency = 60)
    {
        GameWindowSettings gws = new() { UpdateFrequency = updateFrequency };

        NativeWindowSettings nws = new()
        {
            Title = title,
            ClientSize = size,
            Vsync = VSyncMode.On,
            WindowBorder = WindowBorder.Resizable,
            IsEventDriven = false,
            API = ContextAPI.OpenGL,
            APIVersion = new Version(4, 6),
            Profile = ContextProfile.Core,
            NumberOfSamples = 8
        };

        Window = new Window(gws, nws);
    }

    public void ToggleFullscreen()
    {
        if (Window == null) return;

        Window.WindowState = Window.IsFullscreen
        ? WindowState.Normal
        : WindowState.Fullscreen;
    }

    public void Run()
    {
        Window?.Run();
    }

    public void Close()
    {
        Window?.Close();
    }
}