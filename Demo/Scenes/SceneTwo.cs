using System.Drawing;
using ErandevEngine.Core;
using ErandevEngine.Render;
using ErandevEngine.Input;
using ErandevEngine.Scenes;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Demo.Scenes;

public class SceneTwo(string name) : Scene(name)
{
    float[] vertices =
    {
        0.0f, 0.5f, 0.0f, 0.5f, 1.0f,
        0.5f, -0.5f, 0.0f, 1.0f, 0.0f,
        -0.5f, -0.5f, 0.0f, 0.0f, 0.0f,
    };

    uint[] indices =
    {
        0, 1, 2
    };

    float[] color =
    {
        1.0f, 0.0f, 0.0f, 1.0f,
        0.0f, 1.0f, 0.0f, 1.0f,
        0.0f, 0.0f, 1.0f, 1.0f
    };
    SystemRender obj = new SystemRender();

    protected override void OnLoad()
    {
        obj.NewObject(
            vertices,
            indices,
            "Shaders/shader.vert",
            "Shaders/shader.frag",
            "Assets/Texture2.jpg"
        );

        obj.Position = new Vector3(0, 0, 0);
        obj.Scale = new Vector3(1, 1, 1);
        obj.Rotation = 0;
    }

    protected override void OnRender(double elapsedSeconds)
    {
        GL.ClearColor(Color4.Crimson);
        obj?.Render();
    }

    protected override void OnUnload()
    {
        obj?.Unload();
    }

    protected override void OnUpdate(double elapsedSeconds)
    {
        if (InputManager.IsKeyPressed(Keys.D1))
        {
            Console.WriteLine($"это сцена {Name}");
        }
        else if (InputManager.IsKeyPressed(Keys.D2))
        {
            Erandev.Engine.Scenes.Select("1");
        }
    }

    protected override void OnWindowResize(Vector2i size)
    {
    }
}