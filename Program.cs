using VirtualFileSystemSharp;

namespace VirtualFileSystemSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("Howdy"); I'm hiding!
            //Show();
            WindowExplorer win = new WindowExplorer();
            win.Show();
            // FSFile[] files = FileSystem.GetFiles(new IEString("/"));
            // for (int i = 0; i < files.Length; i++)
            // {
            //     Console.WriteLine(files[i].Name);
            // }
            //
            // FSDirectory[] directories = FileSystem.GetDirectories(new IEString("/"));
            // for (int i = 0; i < directories.Length; i++)
            // {
            //     Console.WriteLine(directories[i].Name);
            // }
        }

        public static void Show()
        {
            Console.WriteLine(Conf.CHBR_RIGHTDOWN + Visual.RetLine(Conf.CHBR_HORIBORD, 60) + Conf.CHBR_LEFTDOWN);
            Console.WriteLine(Conf.CHBR_VERTBORD + 
                              " File " + 
                              Conf.CHBR_VERTBORD + 
                              " Edit " + 
                              Conf.CHBR_VERTBORD + 
                              " View " +
                              Conf.CHBR_VERTBORD +
                              Visual.RetLine(Conf.CHFL_VOIDFILL, 39) +
                              Conf.CHBR_VERTBORD);
            Console.WriteLine(Conf.CHBR_VERTBORD + 
                              " File " + 
                              Conf.CHBR_VERTBORD + 
                              " Edit " + 
                              Conf.CHBR_VERTBORD + 
                              " View " +
                              Conf.CHBR_VERTBORD +
                              Visual.RetLine(Conf.CHFL_VOIDFILL, 39) +
                              Conf.CHBR_VERTBORD);
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine(Conf.CHBR_VERTBORD + Visual.RetLine(Conf.CHFL_SEMIFILL, 60) + Conf.CHBR_VERTBORD);
            }
            Console.WriteLine(Conf.CHBR_RIGHTUP + Visual.RetLine(Conf.CHBR_HORIBORD, 60) + Conf.CHBR_LEFTUP);

            //Console.WriteLine(Directory.GetCurrentDirectory());
        }
        
        
                /*FileStream fs = new("FileSystemProject/disk.dsk", FileMode.Open, FileAccess.Write); here i am!
                StreamWriter strwr = new StreamWriter(fs);
                strwr.WriteLine("[disk/manager/swap1.txt] " + Console.ReadLine());
                strwr.WriteLine("[disk/manager/swap2.txt] " + Console.ReadLine());
                strwr.WriteLine("[disk/manager/swap3.txt] " + Console.ReadLine());
                strwr.WriteLine("[disk/manager/swap4.txt] " + Console.ReadLine());
                strwr.WriteLine("[disk/manager/swap5.txt] " + Console.ReadLine());
                strwr.Close();
                fs.Close();
                FileStream fsrd = new("FileSystemProject/disk.dsk", FileMode.Open, FileAccess.Read);
                StreamReader strrd = new StreamReader(fsrd);
                List<string> stringlist = new List<string>();
                    while (!strrd.EndOfStream)
                {
                    string str = strrd.ReadLine();
                    if (str.Contains("disk/manager"))
                    {
                        stringlist.Add(str);
                    }
                }

            for (int i = 0; i < stringlist.Count; i++)
            {
                Console.WriteLine(stringlist[i]);
            }*/

    }
}