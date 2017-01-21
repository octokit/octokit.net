using Cake.Common.Build;
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

        var settings = new PdbGitSettings();
        if (context.AppVeyor && !context.IsOriginalRepo)
        {
            var appVeyorInformation = context.BuildSystem().AppVeyor.Environment;

            settings.RepositoryUrl = $"https://raw.githubusercontent.com/{appVeyorInformation.Repository.Name}";
            settings.CommitSha = appVeyorInformation.Repository.Commit.Id;

            context.Information($"The build doesn't run against the original repository");
            context.Information($"Using '{settings.RepositoryUrl}' as the repository URL and {settings.CommitSha} as the commit SHA");
        }

        foreach (var pdbFile in pdbFiles)
        {
            context.PdbGit(pdbFile);
        }

        context.Information($"Successfully linked sources in {pdbFiles.Length} pdb files");
    }

    public override bool ShouldRun(BuildContext context)
    {
        return !context.Environment.Platform.IsUnix();
    }
}
