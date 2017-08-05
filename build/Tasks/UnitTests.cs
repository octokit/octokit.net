using System.Linq;
using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Test;
using Cake.Frosting;

[Dependency(typeof(Build))]
public sealed class UnitTests : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        var testSettings = new DotNetCoreTestSettings
        {
            Configuration = context.Configuration,
            NoBuild = true,
            Verbose = false
        };

        if (!context.IsRunningOnWindows())
        {
            var testFramework = "netcoreapp1.0";

            context.Information($"Running tests against {testFramework} only as we're not on Windows.");
            testSettings.Framework = testFramework;
        }

        foreach (var project in context.Projects.Where(x => x.UnitTests))
        {
            context.Information("Executing Unit Tests Project {0}...", project.Name);
            context.DotNetCoreTest(project.Path.FullPath, testSettings);
        }
    }
}