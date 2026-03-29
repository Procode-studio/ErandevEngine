using ErandevEngine.Shaders;
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
            // Position          Texture coordinates
            0.0f,  0.5f, 0.0f,  0.5f, 1.0f,  // top right
            0.5f, -0.5f, 0.0f,  1.0f, 0.0f,  // bottom right
            -0.5f, -0.5f, 0.0f,  0.0f, 0.0f,  // bottom left
        };

        uint[] indices = { 0, 1, 2 };

        return (vertices, indices);
    }

    public static (float[] vertices, uint[] indices) Quad()
    {
        float[] vertices =
        {
            //Position          Texture coordinates
            0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
            0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };

        uint[] indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        return (vertices, indices);
    }
}