using ErandevEngine.Render;
using ErandevEngine.Input;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ErandevEngine.Core;

public class Window(GameWindowSettings gws, NativeWindowSettings nws) : GameWindow(gws, nws)
{
    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(Color4.Black);
        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GL.Enable(EnableCap.DepthTest);
        Erandev.Engine.Scenes.RenderSelected(args.Time);
        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);
        GL.Viewport(0, 0, e.Width, e.Height);
        Erandev.Engine.Camera?.Resize(e.Width, e.Height);
        Erandev.Engine.Scenes.ResizeSelected(e.Size);
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        if (InputManager.IsKeyPressed(Keys.Escape))
        {
            Erandev.Engine.Close();
        }
        Erandev.Engine.Scenes.UpdateSelected(args.Time);
    }

    protected override void OnUnload()
    {
        Erandev.Engine.Scenes.UnloadSelected();
    }
}