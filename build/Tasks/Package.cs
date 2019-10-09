using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Core;
using Cake.Frosting;

[Dependency(typeof(UnitTests))]
[Dependency(typeof(ConventionTests))]
[Dependency(typeof(CodeCoverage))]
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
                    NoBuild = true,
                    OutputDirectory = context.Artifacts,
                    ArgumentCustomization = args => args
                        .Append("/p:Version={0}", context.Version.GetSemanticVersion())
                        .Append("/p:CoreOnly={0}", context.CoreOnly),
                });
            }
        }
    }
}