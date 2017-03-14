using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Frosting;

[Dependency(typeof(UnitTests))]
[Dependency(typeof(ValidateLINQPadSamples))]
public sealed class Package : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        foreach (var project in context.Projects)
        {
            if (project.Publish)
            {
                context.Information("Packing {0}...", project.Name);
                context.DotNetCorePack(project.Path.FullPath, new DotNetCorePackSettings()
                {
                    Configuration = context.Configuration,
                    VersionSuffix = context.Version.Suffix,
                    NoBuild = true,
                    Verbose = false,
                    OutputDirectory = context.Artifacts
                });
            }
        }
    }
}