using IEStringLibrary;

namespace FileSystemProject.Containers;

public class WindowOption //window option container. You can see this shit in the upper of your window
{
    private string _name;
    
    
    public string Name
    {
        get { return " " + _name + " "; }
        set { _name = value; }
    }

    public delegate void SelectOption(object[] args);

    public SelectOption Option;

    public WindowOption(string name, SelectOption option)
    {
        Name = name;
        Option = option;
    }

    public WindowOption()
    {
        Name = "";
        Option = DefaultProtocol;
    }

    private void DefaultProtocol(object[] args) => args = new object[0];
}