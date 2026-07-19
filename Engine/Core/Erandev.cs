using ErandevEngine.Render;
using ErandevEngine.Scenes;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;

namespace ErandevEngine.Core;

public class Erandev
{
    private static readonly Lazy<Erandev> Lazy = new(() => new Erandev());
    public static Erandev Engine => Lazy.Value;
    public Window? Window { get; private set; }
    public SceneManager Scenes { get; } = new();
    public Camera? Camera { get; private set; }

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

        Camera = new Camera(size.X, size.Y);
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

    public void BGColor(Color4 color)
    {
        GL.ClearColor(color);
    }
}