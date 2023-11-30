using FileSystemProject.FileSystemStructures;
using IEStringLibrary;
using System.Text;

namespace FileSystemProject;

public static class FileSystem //goddamn main class in project
{
    public static IEString way = new IEString("FileSystemProject/disk.dsk");
    public static FSFile[] GetFiles(IEString way) {
        List<FSFile> directoryFiles = new List<FSFile>();
        IEString buf = new IEString("");
            
        IEString endSeparator = new IEString("<filedata>");
        IEString searchCode = new IEString("F");
            
        long streamPosition = 0;
        buf = ReadTillNewLine(streamPosition, out streamPosition);
        while (!buf.Equals(endSeparator))
        {
            //Console.WriteLine(buf);
            IEString[] splittedBuf = buf.Split(':');
            if (splittedBuf[0].Equals(searchCode) && splittedBuf[1].Equals(way))
            {
                directoryFiles.Add(new FSFile(splittedBuf[2], 
                    Convert.ToInt64(splittedBuf[3].ToString()), splittedBuf[1]));
            }
            buf = ReadTillNewLine(streamPosition, out streamPosition);
        }    
        
        return directoryFiles.ToArray();
    }
        
    public static FSDirectory[] GetDirectories(IEString way)
    {
        List<FSDirectory> directoryDirectories = new List<FSDirectory>();
        IEString buf = new IEString("");
            
        IEString endSeparator = new IEString("<filedata>");
        IEString searchCode = new IEString("D");
            
        long streamPosition = 0;
        buf = ReadTillNewLine(streamPosition, out streamPosition);
        while (!buf.Equals(endSeparator))
        {
            //Console.WriteLine(buf);
            IEString[] splittedBuf = buf.Split(':');
            if (splittedBuf[0].Equals(searchCode) && splittedBuf[1].Equals(way))
            {
                directoryDirectories.Add(new FSDirectory(splittedBuf[2], splittedBuf[1]));
            }
            buf = ReadTillNewLine(streamPosition, out streamPosition);
        }

        return directoryDirectories.ToArray();
    }    

    public static IEString ReadTillNewLine(long startPosition, out long nextLinePosition)
    {
        FileStream fs = new FileStream(way.ToString(), FileMode.Open, FileAccess.Read);
        List<char> chars = new List<char>();
        fs.Position = startPosition;
        char lastReadedChar = (char)fs.ReadByte();
        while (lastReadedChar != '\n')
        {
            chars.Add(lastReadedChar);
            lastReadedChar = (char)fs.ReadByte();
        }
        nextLinePosition = fs.Position;
        fs.Close();
        return new IEString(chars);
    }    
        
    public static int WriteOnDisk(int address, string data)
    {
        //replace to public shit, typo
        FileStream fs = new FileStream(way.ToString(), FileMode.Open, FileAccess.Read); //filestream for finding position at the start of the line
        Span<byte> span = new Span<byte>(new byte[512]); //buffer for reading files
        int numOfLines = 0; //current stream positions in lines
        int readbuf = 0; //count of read symbols in buffer
        bool isSepFouded = false; //did stream position entered file data zone
            
        long lineLength = ReadFromDisk(address).Length; //length of the data at the spec address

        bool manualCycleStop = false; //typo, change code
            
        long streamPositionFinal = 0; //stream position at the start of the line with address

        do
        {
            readbuf = fs.Read(span);
            Span<byte>.Enumerator erator = span.GetEnumerator();
            erator.MoveNext();
            for (int j = 0; j < readbuf && manualCycleStop == false; j++)
            {
                char cur = (char)erator.Current;
                if (!isSepFouded)
                {
                    if (cur == '<')
                    {
                        isSepFouded = true;
                        numOfLines--;
                    }
                }
                else
                {
                    if (cur == '\n') numOfLines++;
                    if (numOfLines == address)
                    {
                        streamPositionFinal = fs.Position - (readbuf - j);
                        manualCycleStop = true;
                    }
                }
                erator.MoveNext();
            }
        } 
        while (readbuf > 0 && !manualCycleStop);
        fs.Close();
        streamPositionFinal++;
            
        byte[] dataAsByte = new byte[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            dataAsByte[i] = (byte)data[i];
        }
            

        if (lineLength <= data.Length)
        {
            MoveData(data.Length-lineLength, streamPositionFinal);
            Console.WriteLine("move");
        }

        FileStream fswr = new FileStream(way.ToString(), FileMode.Open, FileAccess.ReadWrite); //filestream
        //for modification file data
        fswr.Position = streamPositionFinal;
        fswr.Write(dataAsByte, 0, dataAsByte.Length);
        if (data.Length < lineLength)
        {
            Console.WriteLine("");
            byte[] FuckIT = new byte[lineLength - data.Length];
            Array.Fill(FuckIT, (byte)' ');
            fswr.Write(FuckIT, 0, FuckIT.Length);
        }    

        fswr.Close();
        GC.Collect(); //no need (maybe)
        GC.Collect();
        GC.Collect();    

        return numOfLines;
    }

        public static IEString ReadFromDisk(int adress)
        {
            FileStream fs = new FileStream("FileSystemProject/disk.dsk", FileMode.Open, FileAccess.Read);
            Span<byte> span = new Span<byte>(new byte[512]);
            int numoflines = 0;
            int readedbuf; //count of read symbols in buffer
            bool isSepFouded = false; //did stream position entered file data zone
            bool doContinueReadingAfterRBCycleComplete = false; //RB - reading buffer
            StringBuilder retstr = new StringBuilder("");
            do
            {
                readedbuf = fs.Read(span); 
                Span<byte>.Enumerator erator = span.GetEnumerator();
                erator.MoveNext();
                for (int j = 0; j < readedbuf; j++)
                {
                    char cur = (char)erator.Current;
                    if (!isSepFouded)
                    {
                        if (cur == '<')
                        {
                            isSepFouded = true;
                            numoflines--;
                        }
                    }
                    else
                    {
                        if (cur == '\n') numoflines++;
                        if (numoflines == adress || doContinueReadingAfterRBCycleComplete)
                        {
                            erator.MoveNext(); j++;
                            //Console.WriteLine("FoundAdress [numolines = " + numoflines + ", retstr = " + retstr.ToString());
                            //Console.ReadKey();
                            while ((char)erator.Current != '\n' && j < readedbuf)
                            {
                                retstr.Append((char)erator.Current);
                                erator.MoveNext();
                                j++;
                            }

                            if (!(j < readedbuf))
                            {
                                doContinueReadingAfterRBCycleComplete = true;
                            }
                            else
                            {
                                fs.Close();
                                return new IEString(retstr);
                            }
                        }
                    }
                    erator.MoveNext();
                }
            } 
            while (readedbuf > 0);
            fs.Close();
            return new IEString(retstr);
        }

    public static void MoveData(long count, long localpos)
    {
        FileStream fs = new FileStream("FileSystemProject/disk.dsk", FileMode.Open, FileAccess.ReadWrite);
        long prevPosition = localpos; //previous stream position in file
        byte[] buf1 = new byte[count]; //first buffer
        byte[] buf2 = new byte[count]; //second buffer
        int bufLength; //count of read symbols in buffer
        fs.Position = prevPosition;
        bufLength = fs.Read(buf1, 0, buf1.Length);
        fs.Position = prevPosition;
        fs.Position += count;
        buf1.CopyTo(buf2, 0);
        while (bufLength > 0)
        {
            prevPosition = fs.Position;
            bufLength = fs.Read(buf1, 0, buf1.Length);
            fs.Position = prevPosition;
            fs.Write(buf2, 0, buf2.Length); 
            buf1.CopyTo(buf2, 0); 
            //everything seems to be working, but there can be on issue here, size in the end of the file
        }
        fs.Close();
    }
}
