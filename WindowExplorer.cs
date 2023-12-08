using System.Diagnostics;
using IEStringLibrary;
using VirtualFileSystemSharp.Containers;
using VirtualFileSystemSharp.FileSystemStructures;

namespace VirtualFileSystemSharp;

public class WindowExplorer
{
    public Point Location;
    public Size size;
    private List<WindowOption> options = new();

    public IEString currentDirectory;
    public FSDirectory[] dirsInDirectory;
    public FSFile[] filesInDirectory;

    public int selectIndex = 0;

    public WindowExplorer()
    {
        Location = new Point(0, 0);
        size = new Size(10, 30);
        options.Add(new WindowOption("FileSystemProject", HeaderFileOption));
        options.Add(new WindowOption("File", HeaderFileOption));
        options.Add(new WindowOption("Edit", EditFileOption));
        options.Add(new WindowOption("View", ViewFileOption));
        currentDirectory = new IEString("/");
        dirsInDirectory = FileSystem.GetDirectories(currentDirectory);
        filesInDirectory = FileSystem.GetFiles(currentDirectory);

        ConsoleKey key = ConsoleKey.A;
        while (true)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow :
                    if (selectIndex > 0) selectIndex--;
                    break;
                case ConsoleKey.DownArrow :
                    if (selectIndex < dirsInDirectory.Length + filesInDirectory.Length - 1) selectIndex++;
                    break;
                case ConsoleKey.RightArrow :
                    break; 
                case ConsoleKey.LeftArrow :
                    break;
                case ConsoleKey.Escape : return; 
                    break;
                case ConsoleKey.Enter : InDirectory(dirsInDirectory[selectIndex]);
                    break;
                case ConsoleKey.Backspace : FromDirectory(currentDirectory);
                    break;
            }
            Show();
            Console.WriteLine(Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024 + "Kb");
            key = Console.ReadKey().Key;
        }
    }

    private void InDirectory(FSDirectory dir)
    {
        if (dir.Path.Equals(new IEString('/'))) currentDirectory = dir.Path + dir.Name + '/';
        else currentDirectory = dir.Path + dir.Name + '/';
        dirsInDirectory = FileSystem.GetDirectories(currentDirectory);
        filesInDirectory = FileSystem.GetFiles(currentDirectory);
        selectIndex = 0;
        Console.Title = ("FileSystemProject - " + currentDirectory).ToString();
    }

    private void FromDirectory(IEString dir) //IN IEStringLib operators ошибка блять, лист как и любой класс ССЫЛОЧНЫЙ - оперировать нужно новыми экземплярами!!
    {                                       //пофикшено блять в IEStringLibrary 1.0.3
        IEString ret = new IEString("");
        IEString sepline = new IEString("/");
        IEString[] splitted = dir.Split('/');
        for (int i = 0; i < splitted.Length-2; i++)
        {
            ret.Append(sepline, splitted[i]);
        }
        ret.Append(sepline);

        currentDirectory = ret;
        dirsInDirectory = FileSystem.GetDirectories(currentDirectory);
        filesInDirectory = FileSystem.GetFiles(currentDirectory);
        selectIndex = 0;

        Console.Title = ("FileSystemProject - " + currentDirectory).ToString();
    }

    
    private void HeaderFileOption(object[] args) => Console.WriteLine("Shit happened");
    private void EditFileOption(object[] args) => Console.WriteLine("Edit happened");
    private void ViewFileOption(object[] args) => Console.WriteLine("View happened");

    public void Show() //just guess what this method does
    {
        //Console.Clear();
        Console.SetCursorPosition(Location.Y, Location.X);

        Console.Write(Conf.CHBR_RIGHTDOWN); //generate line upwards 
        for (int j = 0; j < options.Count; j++)
        {
            Console.Write(Visual.RetLine(Conf.CHBR_HORIBORD , options[j].Name.Length) + Conf.CHMD_HORIDOWN);
        }
        Console.Write(Visual.RetLine(Conf.CHBR_HORIBORD, 39) + Conf.CHBR_LEFTDOWN + "\n");
        
        Console.Write(Conf.CHBR_VERTBORD);
        for (int j = 0; j < options.Count; j++) //generate option line
        {
            Console.Write(options[j].Name);
            Console.Write(Conf.CHBR_VERTBORD);
        }
        Console.Write(Visual.RetLine(Conf.CHFL_VOIDFILL, 39) + Conf.CHBR_VERTBORD + "\n");

        Console.Write(Conf.CHMD_VERTRIGHT); //generate line under option
        for (int j = 0; j < options.Count; j++)
        {
            Console.Write(Visual.RetLine(Conf.CHBR_HORIBORD , options[j].Name.Length) + Conf.CHMD_HORIUP);
        }
        Console.Write(Visual.RetLine(Conf.CHBR_HORIBORD, 39) + Conf.CHMD_VERTLEFT + "\n");
        Console.SetCursorPosition(30, 2); Console.Write(Conf.CHMD_HORIDOWN);
        Console.SetCursorPosition(0,3);

        
        for (int j = 0; j < size.Height; j++) //generate body
        {
            Console.Write(Conf.CHBR_VERTBORD);
            Console.Write(' ');
            //Console.WriteLine("test");
            if (dirsInDirectory.Length > j)
            {
                if (selectIndex == j) Visual.WriteColor(Visual.RetFixed(dirsInDirectory[j].Name, 38));
                else Console.Write(Visual.RetFixed(dirsInDirectory[j].Name, 38).ToCharArray());
            }
            else if (filesInDirectory.Length > j - dirsInDirectory.Length)
            {
                if (selectIndex == j) Visual.WriteColor(Visual.RetFixed(filesInDirectory[j-dirsInDirectory.Length].Name, 38));
                else Console.Write(Visual.RetFixed(filesInDirectory[j-dirsInDirectory.Length].Name, 38).ToCharArray());
            }
            else Console.Write(Visual.RetLine(Conf.CHFL_VOIDFILL, 38).ToCharArray());
            Console.Write(' '); Console.Write(Conf.CHBR_VERTBORD); Console.Write(' ');
            Console.Write(Visual.RetLine(Conf.CHFL_VOIDFILL, 38).ToCharArray());
            Console.Write(Conf.CHBR_VERTBORD);

            Console.WriteLine();
                //Console.WriteLine(Conf.CHBR_VERTBORD + Visual.RetLine(Conf.CHFL_VOIDFILL, 80) + Conf.CHBR_VERTBORD);
        }
        
        Console.WriteLine(Conf.CHBR_RIGHTUP + Visual.RetLine(Conf.CHBR_HORIBORD, 80) + Conf.CHBR_LEFTUP); //generate bottom
    }

    private enum SelectionType
    {
        Header, ElementSelection
    }
}