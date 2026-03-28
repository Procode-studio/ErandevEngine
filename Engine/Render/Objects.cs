using Engine.Shaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace ErandevEngine.Render;

public enum Primitive
{
    Triangle,
    Quad,
    Circle,
}

public static class Objects
{
    public static (float[] vertices, uint[] indices) Triangle()
    {
        float[] vertices =
        {
            -0.5f, -0.5f, 0f,
            0.5f, -0.5f, 0f,
            0f, 0.5f, 0f,
        };

        uint[] indices = { 0, 1, 2 };

        return (vertices, indices);
    }

    public static (float[] vertices, uint[] indices) Quad()
    {
        float[] vertices =
        {
            -0.5f, -0.5f, 0f,
            0.5f, -0.5f, 0f,
            0.5f, 0.5f, 0f,
            -0.5f, 0.5f, 0f,
        };

        uint[] indices =
        {
            0, 1, 2,
            2, 3, 0
        };

        return (vertices, indices);
    }
}