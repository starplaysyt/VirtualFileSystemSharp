using IEStringLibrary;

namespace VirtualFileSystemSharp;

public static class Visual //for graphics purposes only
{
    public static IEString RetLine(char ch, int lenght)
    {
        IEString str = new IEString("");
        for (int i = 0; i < lenght; i++)
        {
            str += ch;
        }

        return str;
    }

    public static IEString RetFixed(IEString str, int lenght)
    {
        IEString retstr = new IEString("");
        for (int i = 0; i < lenght; i++)
        {
            retstr += i >= str.Length ? ' ' : str.ElementAt(i);
        }

        return retstr;
    }

    public static void WriteColor(IEString str) //for graphics purposes only
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(str.ToCharArray());
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
    }
}