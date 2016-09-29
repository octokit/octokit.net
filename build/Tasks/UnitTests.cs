using System.Linq;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Test;
using Cake.Frosting;

[Dependency(typeof(Build))]
public class UnitTests : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        foreach (var project in context.Projects.Where(x => x.Tests))
        {
            context.Information("Executing Test Project {0}...", project.Name);
            context.DotNetCoreTest(
                project.Path.FullPath,
                new DotNetCoreTestSettings
                {
                    Configuration = context.Configuration,
                    NoBuild = true,
                    Verbose = false
                });
        }
    }
}