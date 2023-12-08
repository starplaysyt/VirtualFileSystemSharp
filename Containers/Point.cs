namespace VirtualFileSystemSharp.Containers;

public class Point //container to store info about position in console
{
    public int X;
    public int Y;

    public Point()
    {
        X = 0;
        Y = 0;
    }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}