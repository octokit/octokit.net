using System;
using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

[Dependency(typeof(Package))]
public class TestSourceLink : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        var nugetPackages = context.GetFiles($"./{context.Artifacts}/*.nupkg");

        foreach (var nugetPackage in nugetPackages)
        {
            context.Information("Testing sourcelink info in {0}", context.Environment.WorkingDirectory.GetRelativePath(nugetPackage));
            var exitCode = context.StartProcess("dotnet", new ProcessSettings
            {
                WorkingDirectory = "Octokit",
                Arguments = $"sourcelink test {nugetPackage.FullPath}"
            });

            if (exitCode != 0)
            {
                throw new Exception("Sourcelink test failed!");
            }
        }
    }

    public override bool ShouldRun(Context context)
    {
        return context.LinkSources;
    }
}