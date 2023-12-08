using IEStringLibrary;

namespace VirtualFileSystemSharp.FileSystemStructures;

public class FSFile
{
    public IEString Name { get; set; }
    public IEString Path { get; set; }
    
    public long Adress;

    public FSFile(IEString name, long adress, IEString path)
    {
        Name = name;
        Adress = adress;
        Path = path;
    }

    public FSFile()
    {
        Name = new IEString("");
        Adress = 0;
        Path = new IEString("");
    }
}