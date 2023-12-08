using System.Text;

namespace VirtualFileSystemSharp.Interfaces;

public interface ISystemStructure
{
    public string Path { get; }
    public StringBuilder Data { get; set; }
}