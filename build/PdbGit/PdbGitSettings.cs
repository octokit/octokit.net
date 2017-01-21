using Cake.Core.IO;
using Cake.Core.Tooling;

public class PdbGitSettings : ToolSettings
{
    public PdbGitMethod? Method { get; set; }
    public string RepositoryUrl { get; set; }
    public string CommitSha { get; set; }
    public DirectoryPath BaseDirectory { get; set; }
    public bool SkipVerify { get; set; }
}
