using ErandevEngine.Shaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace ErandevEngine.Render;

public enum Primitive
{
    Triangle,
    Quad,
    Cube,
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
    public static (float[] vertices, uint[] indices) Cube()
    {
        float[] vertices = {
            // FRONT
            -0.5f,-0.5f, 0.5f, 0,0,
            0.5f,-0.5f, 0.5f, 1,0,
            0.5f, 0.5f, 0.5f, 1,1,
            -0.5f, 0.5f, 0.5f, 0,1,

            // BACK
            -0.5f,-0.5f,-0.5f, 0,0,
            0.5f,-0.5f,-0.5f, 1,0,
            0.5f, 0.5f,-0.5f, 1,1,
            -0.5f, 0.5f,-0.5f, 0,1,

            // LEFT
            -0.5f,-0.5f,-0.5f, 0,0,
            -0.5f,-0.5f, 0.5f, 1,0,
            -0.5f, 0.5f, 0.5f, 1,1,
            -0.5f, 0.5f,-0.5f, 0,1,

            // RIGHT
            0.5f,-0.5f,-0.5f, 0,0,
            0.5f,-0.5f, 0.5f, 1,0,
            0.5f, 0.5f, 0.5f, 1,1,
            0.5f, 0.5f,-0.5f, 0,1,

            // BOTTOM
            -0.5f,-0.5f,-0.5f, 0,0,
            0.5f,-0.5f,-0.5f, 1,0,
            0.5f,-0.5f, 0.5f, 1,1,
            -0.5f,-0.5f, 0.5f, 0,1,

            // TOP
            -0.5f, 0.5f,-0.5f, 0,0,
            0.5f, 0.5f,-0.5f, 1,0,
            0.5f, 0.5f, 0.5f, 1,1,
            -0.5f, 0.5f, 0.5f, 0,1,
        };

        uint[] indices = {
            0,1,2, 0,2,3,       // front
            4,5,6, 4,6,7,       // back
            8,9,10, 8,10,11,    // left
            12,13,14, 12,14,15, // right
            16,17,18, 16,18,19, // bottom
            20,21,22, 20,22,23  // top
        };

        return (vertices, indices);
    }
}