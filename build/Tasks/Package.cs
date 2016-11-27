using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Frosting;

[Dependency(typeof(UnitTests))]
public class Package : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        foreach (var project in context.Projects)
        {
            if (project.Publish)
            {
                context.Information("Packing {0}...", project.Name);
                context.DotNetCorePack(project.Path.FullPath, new DotNetCorePackSettings()
                {
                    OutputDirectory = context.OutputDir,
                    Configuration = context.Configuration,
                    VersionSuffix = context.Suffix,
                    NoBuild = true,
                    Verbose = false
                });
            }
        }
    }
}