using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using ErandevEngine.Shaders;
using ErandevEngine.Core;

namespace ErandevEngine.Render;

public class SystemRender
{
    public Vector3 Position { get; set; } = Vector3.Zero;
    public Vector3 Scale { get; set; } = Vector3.One;
    public Vector3 Rotation { get; set; } = Vector3.Zero;
    private Matrix4 model = Matrix4.Identity;
    public int VBO { get; private set; }
    public int VAO { get; private set; }
    public int EBO { get; private set; }
    public int ColorBuffer { get; private set; }
    public Shader? shader { get; private set; }
    public int TextureHandle { get; private set; }
    private int _modelLocation;
    private int IndexCount;
    private static string appPath = AppContext.BaseDirectory;
    static string vertshaderpath = "Shaders/Default/shader.vert";
    static string fragshaderpath = "Shaders/Default/shader.frag";

    public SystemRender NewObject(float[] vertices, uint[] indices, string texturePath, string? vertexPath = null, string? fragmentPath = null)
    {
        int tex = 0;
        if (!string.IsNullOrEmpty(texturePath))
        {
            tex = Texture.LoadTexture(texturePath);
        }

        string[] vertexPaths = { appPath, vertshaderpath };
        string[] fragmentPaths = { appPath, fragshaderpath };
        vertexPath ??= Path.Combine(vertexPaths);
        fragmentPath ??= Path.Combine(fragmentPaths);

        Load(vertices, indices, vertexPath, fragmentPath, tex);
        return this;
    }

    public static SystemRender Create(Primitive type, string texturePath, string? vertexPath = null, string? fragmentPath = null)
    {
        (float[] v, uint[] i) data = type switch
        {
            Primitive.Triangle => Objects.Triangle(),
            Primitive.Quad => Objects.Quad(),
            Primitive.Cube => Objects.Cube(),
            _ => throw new ArgumentException("Ошибка при создании обьекта")
        };

        int tex = 0;
        if (!string.IsNullOrEmpty(texturePath))
        {
            tex = Texture.LoadTexture(texturePath);
        }

        string[] vertexPaths = { appPath, vertshaderpath };
        string[] fragmentPaths = { appPath, fragshaderpath };
        vertexPath ??= Path.Combine(vertexPaths);
        fragmentPath ??= Path.Combine(fragmentPaths);

        var render = new SystemRender();
        render.Load(data.v, data.i, vertexPath, fragmentPath, tex);
        return render;
    }

    public void Load(float[] vertices, uint[] indices, string vertexPath, string fragmentPath, int textureHandle)
    {
        IndexCount = indices.Length;
        TextureHandle = textureHandle;
        shader = new Shader(vertexPath, fragmentPath);
        _modelLocation = GL.GetUniformLocation(shader.Handle, "model");

        VAO = GL.GenVertexArray();
        GL.BindVertexArray(VAO);

        VBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        EBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, IndexCount * sizeof(uint), indices, BufferUsageHint.DynamicDraw);

        int texCoordLocation = GL.GetAttribLocation(shader.Handle, "aTexCoord");
        GL.EnableVertexAttribArray(2);
        GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

        GL.BindVertexArray(0);
    }

    public void Render()
    {
        Matrix4 scale = Matrix4.CreateScale(Scale);
        Matrix4 rotation = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(Rotation.X)) *
                           Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Rotation.Y)) *
                           Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation.Z));
        Matrix4 translation = Matrix4.CreateTranslation(Position);
        int projLoc = GL.GetUniformLocation(shader!.Handle, "projection");
        int viewLoc = GL.GetUniformLocation(shader!.Handle, "view");

        model = scale * rotation * translation;

        shader?.Use();
        GL.UniformMatrix4(_modelLocation, false, ref model);

        Matrix4 projection = Erandev.Engine.Camera!.Projection;
        Matrix4 view = Erandev.Engine.Camera!.View;

        GL.UniformMatrix4(projLoc, false, ref projection);
        GL.UniformMatrix4(viewLoc, false, ref view);

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
