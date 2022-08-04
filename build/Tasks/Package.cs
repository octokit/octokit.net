using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Pack;
using Cake.Core;
using Cake.Frosting;

[IsDependentOn(typeof(UnitTests))]
[IsDependentOn(typeof(ConventionTests))]
[IsDependentOn(typeof(ValidateLINQPadSamples))]
public sealed class Package : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        foreach (var project in context.Projects)
        {
            if (project.Publish)
            {
                context.Information("Packing {0}...", project.Name);
                context.DotNetPack(project.Path.FullPath, new DotNetPackSettings()
                {
                    Configuration = context.Configuration,
                    NoBuild = true,
                    OutputDirectory = context.Artifacts,
                    ArgumentCustomization = args => args
                        .Append("/p:Version={0}", context.Version.GetSemanticVersion()),
                });
            }
        }
    }
}
