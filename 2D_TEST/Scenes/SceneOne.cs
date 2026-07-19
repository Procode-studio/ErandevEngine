using ErandevEngine.Core;
using ErandevEngine.Render;
using ErandevEngine.Input;
using ErandevEngine.Scenes;
using ErandevEngine.Audio;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System.Drawing;

namespace test2d.Scenes;

public class SceneOne(string name) : Scene(name)
{
    Erandev engine = Erandev.Engine;
    private SystemRender? player;
    private AudioManager am = new AudioManager();
    protected override void OnLoad()
    {
        player = SystemRender.Create(
            Primitive.Quad,
            "Assets/PlayerTop.png"
        );

        player.Position = (0, 0, 0);
        player.Scale = (0.4f, 0.4f, 0);
        player.Rotation = (0, 0, 0);
    }

    protected override void OnRender(double elapsedSeconds)
    {
        engine.BGColor(Color.DarkGreen);
        player?.Render();
    }

    protected override void OnUnload()
    {
        player?.Unload();
    }

    protected override void OnUpdate(double elapsedSeconds)
    {
        if (InputManager.IsKeyPressed(Keys.D1))
        {
            Console.WriteLine($"это сцена {Name}");
        }
        else if (InputManager.IsKeyPressed(Keys.D2))
        {
            Erandev.Engine.Scenes.Select("2");
        }

        float moveSpeed = 0.5f * (float)elapsedSeconds;
        if (InputManager.IsKeyDown(Keys.W))
        {
            player?.Position += (0, moveSpeed, 0);
            player?.Rotation = (0, 0, 0);
        }
        if (InputManager.IsKeyDown(Keys.S))
        {
            player?.Position -= (0, moveSpeed, 0);
            player?.Rotation = (0, 0, -180);
        }
        if (InputManager.IsKeyDown(Keys.A))
        {
            player?.Position -= (moveSpeed, 0, 0);
            player?.Rotation = (0, 0, 90);
        }
        if (InputManager.IsKeyDown(Keys.D))
        {
            player?.Position += (moveSpeed, 0, 0);
            player?.Rotation = (0, 0, -90);
        }

        if (InputManager.IsKeyPressed(Keys.Space))
        {
            am.PlaySound("Assets/click.mp3"); //работает только mp3!
        }
    }

    protected override void OnWindowResize(Vector2i size)
    {
    }
}