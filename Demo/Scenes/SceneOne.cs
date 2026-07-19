using ErandevEngine.Core;
using ErandevEngine.Render;
using ErandevEngine.Input;
using ErandevEngine.Scenes;
using ErandevEngine.Audio;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System.Drawing;

namespace Demo.Scenes;

public class SceneOne(string name) : Scene(name)
{
    Erandev engine = Erandev.Engine;
    private SystemRender? rectangle;
    private AudioManager am = new AudioManager();
    protected override void OnLoad()
    {
        rectangle = SystemRender.Create(
            Primitive.Cube,
            "Assets/Texture1.jpg"
        );

        rectangle.Position = new Vector3(0, 0, 0);
        rectangle.Scale = new Vector3(1, 1, 1);
        rectangle.Rotation = new Vector3(0, 0, 0);
    }

    protected override void OnRender(double elapsedSeconds)
    {
        engine.BGColor(Color.Aquamarine);
        rectangle?.Render();
    }

    protected override void OnUnload()
    {
        rectangle?.Unload();
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
        if (InputManager.IsKeyDown(Keys.W)) rectangle?.Position += new Vector3(0, moveSpeed, 0);
        if (InputManager.IsKeyDown(Keys.S)) rectangle?.Position -= new Vector3(0, moveSpeed, 0);
        if (InputManager.IsKeyDown(Keys.A)) rectangle?.Position -= new Vector3(moveSpeed, 0, 0);
        if (InputManager.IsKeyDown(Keys.D)) rectangle?.Position += new Vector3(moveSpeed, 0, 0);
        if (InputManager.IsKeyDown(Keys.Q)) rectangle?.Rotation += new Vector3(0, 0, 90f * (float)elapsedSeconds);
        if (InputManager.IsKeyDown(Keys.E)) rectangle?.Rotation -= new Vector3(0, 0, 90f * (float)elapsedSeconds);
        if (InputManager.IsKeyDown(Keys.Up)) rectangle?.Rotation -= new Vector3(90f * (float)elapsedSeconds, 0, 0);
        if (InputManager.IsKeyDown(Keys.Down)) rectangle?.Rotation += new Vector3(90f * (float)elapsedSeconds, 0, 0);
        if (InputManager.IsKeyDown(Keys.Left)) rectangle?.Rotation -= new Vector3(0, 90f * (float)elapsedSeconds, 0);
        if (InputManager.IsKeyDown(Keys.Right)) rectangle?.Rotation += new Vector3(0, 90f * (float)elapsedSeconds, 0);

        if (InputManager.IsKeyPressed(Keys.Space))
        {
            am.PlaySound("Assets/click.mp3"); //работает только mp3!
        }
    }

    protected override void OnWindowResize(Vector2i size)
    {
    }
}