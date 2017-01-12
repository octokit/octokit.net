using Cake.Core.IO;
using Cake.Core.Tooling;

public class PdbGitSettings : ToolSettings
{
    public PdbGitSettings(FilePath pdbFile)
    {
        PdbFile = pdbFile;
    }
    public FilePath PdbFile { get; }
}
