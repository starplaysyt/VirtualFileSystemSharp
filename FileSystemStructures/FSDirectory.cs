using IEStringLibrary;

namespace VirtualFileSystemSharp.FileSystemStructures;

public class FSDirectory
{
    public IEString Name { get; set; }
    public IEString Path { get; set; }

    public FSDirectory(IEString name, IEString path)
    {
        Name = name;
        Path = path;
    }

    public FSDirectory()
    {
        Name = new IEString("");
        Path = new IEString("");
    }
    
}