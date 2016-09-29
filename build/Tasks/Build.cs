using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Core;
using Cake.Frosting;

[Dependency(typeof(Restore))]
[TaskName("Dotnet-Build")]
public class Build : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        foreach(var project in context.Projects)
        {
            context.Information("Building {0}...", project.Name);
            context.DotNetCoreBuild(
                project.Path.FullPath,
                new DotNetCoreBuildSettings
                {
                    Configuration = context.Configuration,
                    VersionSuffix = context.Suffix
                });
        }
    }
}