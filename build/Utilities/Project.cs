using Cake.Core.IO;

public class Project
{
    public string Name { get; set; }
    public FilePath Path { get; set; }
    public bool Publish { get; set; }
    public bool Tests { get; set; }
}