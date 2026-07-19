using System.Drawing;
using ErandevEngine.Core;
using ErandevEngine.Render;
using ErandevEngine.Input;
using ErandevEngine.Scenes;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Demo.Scenes;

public class SceneTwo(string name) : Scene(name)
{
    Erandev engine = Erandev.Engine;
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
    SystemRender obj = new SystemRender();

    protected override void OnLoad()
    {
        obj.NewObject(
            vertices,
            indices,
            "Assets/Texture2.jpg",
            "Shaders/shader.vert",
            "Shaders/shader.frag"
        );

        obj.Position = new Vector3(0, 0, 0);
        obj.Scale = new Vector3(1, 1, 1);
        obj.Rotation = new Vector3(0, 0, 0);
    }

    protected override void OnRender(double elapsedSeconds)
    {
        engine.BGColor(Color4.DarkRed);
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