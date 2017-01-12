using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;
using System.Linq;

[Dependency(typeof(Build))]
public class LinkSources : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var matchingDirectories = context
            .Projects
            .Where(x => !x.IsTests)
            .Select(x => x.Name)
            .ToArray();

        var pdbFiles = context.Globber
            .Match("**/*.pdb", x => x.Path.Segments.Intersect(matchingDirectories).Any())
            .Cast<FilePath>()
            .ToArray();

        foreach (var pdbFile in pdbFiles)
        {
            context.PdbGitLinkSources(pdbFile);
        }

        context.Information($"Successfully linked sources in {pdbFiles.Length} pdb files");
    }

    public override bool ShouldRun(BuildContext context)
    {
        return !context.Environment.Platform.IsUnix();
    }
}
