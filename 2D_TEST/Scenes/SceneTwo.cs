using ErandevEngine.Core;
using ErandevEngine.Render;
using ErandevEngine.Input;
using ErandevEngine.Scenes;
using ErandevEngine.Audio;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System.Drawing;
using System.Reflection.Metadata;

namespace test2d.Scenes;

public class SceneTwo(string name) : Scene(name)
{
    Erandev engine = Erandev.Engine;
    private SystemRender? player;
    private SystemRender? floor;
    private AudioManager am = new AudioManager();
    protected override void OnLoad()
    {
        player = SystemRender.Create(
            Primitive.Quad,
            "Assets/playerPlat.png"
        );
        player.Position = (0, 0, 0);
        player.Scale = (0.5f, 1f, 0);
        player.Rotation = (0, 0, 0);

        floor = SystemRender.Create(
            Primitive.Quad,
            "Assets/black.png"
        );
        floor.Position = (0, -0.8f, 0);
        floor.Scale = (5f, 0.5f, 0);
    }

    protected override void OnRender(double elapsedSeconds)
    {
        engine.BGColor(Color.White);
        player?.Render();
        floor?.Render();
    }

    protected override void OnUnload()
    {
        player?.Unload();
        floor?.Unload();
    }

    protected override void OnUpdate(double elapsedSeconds)
    {
        float velocityY = 0f;
        float gravity = -25f;
        bool isGrounded = false;

        if (player?.Position.Y <= -0.1f)
        {
            player?.Position = (player.Position.X, -0.1f, player.Position.Z);
            velocityY = 0;
            isGrounded = true;
        }

        float moveSpeed = 0.5f * (float)elapsedSeconds;
        if (InputManager.IsKeyPressed(Keys.W) && isGrounded)
        {
            velocityY = 20;
            isGrounded = false;
        }
        velocityY += gravity * (float)elapsedSeconds;
        player?.Position += (0, velocityY * (float)elapsedSeconds, 0);

        if (InputManager.IsKeyDown(Keys.A))
        {
            player?.Position -= (moveSpeed, 0, 0);
        }
        if (InputManager.IsKeyDown(Keys.D))
        {
            player?.Position += (moveSpeed, 0, 0);
        }

        if (InputManager.IsKeyPressed(Keys.Space))
        {
            am.PlaySound("Assets/click.mp3"); //работает только mp3!
        }

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