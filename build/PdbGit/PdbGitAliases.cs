using Cake.Core;
using Cake.Core.IO;

public static class PdbGitAliases
{
    public static void PdbGit(this ICakeContext context, FilePath pdbFile)
    {
        context.PdbGit(pdbFile, new PdbGitSettings());
    }

    public static void PdbGit(this ICakeContext context, FilePath pdbFile, PdbGitSettings settings)
    {
        var tool = new PdbGitTool(
            context.FileSystem,
            context.Environment,
            context.ProcessRunner,
            context.Tools);

        tool.Run(pdbFile, settings);
    }
}
