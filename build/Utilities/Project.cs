using Cake.Core.IO;

public class Project
{
    public string Name { get; set; }
    public FilePath Path { get; set; }
    public bool Publish { get; set; }
    public bool UnitTests { get; set; }
    public bool ConventionTests { get; set; }
    public bool IntegrationTests { get; set; }
    public bool IsTests => UnitTests || ConventionTests || IntegrationTests;
}