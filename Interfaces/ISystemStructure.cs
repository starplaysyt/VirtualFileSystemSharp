using System.Text;

namespace FileSystemProject.Interfaces;

public interface ISystemStructure
{
    public string Path { get; }
    public StringBuilder Data { get; set; }
}