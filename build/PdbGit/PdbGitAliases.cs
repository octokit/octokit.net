using Cake.Core;
using Cake.Core.IO;

public static class PdbGitAliases
{
    public static void PdbGitLinkSources(this ICakeContext context, FilePath pdbFile)
    {
        var tool = new PdbGitTool(
            context.FileSystem,
            context.Environment,
            context.ProcessRunner,
            context.Tools);

        var settings = new PdbGitSettings(pdbFile);

        tool.LinkSources(settings);
    }
}
