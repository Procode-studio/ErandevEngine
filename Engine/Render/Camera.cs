using OpenTK.Mathematics;

namespace ErandevEngine.Render;

public class Camera
{
    public Matrix4 Projection { get; private set; }
    public Matrix4 View { get; private set; }

    public Camera(int width, int height)
    {
        Resize(width, height);
        View = Matrix4.Identity;
    }
    public void Resize(int width, int height)
    {
        float aspect = width / (float)height;
        Projection = Matrix4.CreateOrthographicOffCenter(-aspect, aspect, -1, 1, -1, 1);
    }

}
