namespace VirtualFileSystemSharp.Containers;

public class Size //container to store info about size
{
    public int Width;
    public int Height;

    public Size()
    {
        Width = 0;
        Height = 0;
    }

    public Size(int width, int height)
    {
        Width = width;
        Height = height;
    }
}