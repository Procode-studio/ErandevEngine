using Engine.Shaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using ErandevEngine.Render;

namespace ErandevEngine.Render;

public class SystemRender
{
    public Vector3 Position { get; set; } = Vector3.Zero;
    public Vector3 Scale { get; set; } = Vector3.One;
    public float Rotation { get; set; } = 0f;
    private Matrix4 model = Matrix4.Identity;
    private Matrix4 _projection = Matrix4.Identity;
    private Matrix4 _view = Matrix4.Identity;
    public int VBO { get; private set; }
    public int VAO { get; private set; }
    public int EBO { get; private set; }
    public int ColorBuffer { get; private set; }
    public Shader shader { get; private set; }
    private int _modelLocation;
    private int IndexCount;

    public SystemRender NewObject(float[] vertices, uint[] indices, float[] arrayColor, string vertexPath, string fragmentPath)
    {
        Load(vertices, indices, arrayColor, vertexPath, fragmentPath);
        return this;
    }

    public static SystemRender Create(Primitive type, Color4 nameColor, string vertexPath, string fragmentPath)
    {
        vertexPath ??= "../Engine/Shaders/Default/shader.vert";
        fragmentPath ??= "../Engine/Shaders/Default/shader.frag";
        (float[] v, uint[] i) data = type switch
        {
            Primitive.Triangle => Objects.Triangle(),
            Primitive.Quad => Objects.Quad(),
            _ => throw new ArgumentException("Ошибка при создании обьекта")
        };
        float[] arrayColor = { nameColor.R, nameColor.G, nameColor.B, nameColor.A };

        var render = new SystemRender();
        render.Load(data.v, data.i, arrayColor, vertexPath, fragmentPath);
        return render;
    }


    public void Load(float[] vertices, uint[] indices, float[] color, string vertexPath, string fragmentPath)
    {
        IndexCount = indices.Length;
        shader = new Shader(vertexPath, fragmentPath);
        _modelLocation = GL.GetUniformLocation(shader.Handle, "model");
        _projection = Matrix4.CreateOrthographicOffCenter(-1, 1, -1, 1, -1, 1);

        VAO = GL.GenVertexArray();
        GL.BindVertexArray(VAO);

        VBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        EBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, IndexCount * sizeof(uint), indices, BufferUsageHint.DynamicDraw);

        ColorBuffer = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, ColorBuffer);
        GL.BufferData(BufferTarget.ArrayBuffer, color.Length * sizeof(float), color, BufferUsageHint.DynamicDraw);
        GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
        GL.EnableVertexAttribArray(1);

        GL.BindVertexArray(0);
    }

    public void Render()
    {
        Matrix4 scale = Matrix4.CreateScale(Scale);
        Matrix4 rotation = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation));
        Matrix4 translation = Matrix4.CreateTranslation(Position);
        int projLoc = GL.GetUniformLocation(shader.Handle, "projection");
        int viewLoc = GL.GetUniformLocation(shader.Handle, "view");

        model = scale * rotation * translation;

        shader.Use();
        GL.UniformMatrix4(_modelLocation, false, ref model);
        GL.UniformMatrix4(projLoc, false, ref _projection);
        GL.UniformMatrix4(viewLoc, false, ref _view);

        GL.BindVertexArray(VAO);
        GL.DrawElements(PrimitiveType.Triangles, IndexCount, DrawElementsType.UnsignedInt, 0);
        GL.BindVertexArray(0);
    }

    public void Unload()
    {
        GL.DeleteBuffer(VBO);
        GL.DeleteBuffer(EBO);
        GL.DeleteVertexArray(VAO);
        GL.DeleteBuffer(ColorBuffer);
        shader?.Dispose();
    }
}
