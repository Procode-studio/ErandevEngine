using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace ErandevEngine.Render;

public static class Texture
{
    public static int LoadTexture(string path)
    {
        int Handle = GL.GenTexture();

        GL.BindTexture(TextureTarget.Texture2D, Handle);
        StbImage.stbi_set_flip_vertically_on_load(1);
        ImageResult image = ImageResult.FromStream(File.OpenRead(path), ColorComponents.RedGreenBlueAlpha);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

        return Handle;
    }
}