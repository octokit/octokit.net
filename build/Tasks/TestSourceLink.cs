using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

[Dependency(typeof(Build))]
public class TestSourceLink : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        var linkedAssemblies = new List<FilePath>();
        linkedAssemblies.AddRange(context.GetFiles($"Octokit/bin/{context.Configuration}/*/Octokit.dll"));
        linkedAssemblies.AddRange(context.GetFiles($"Octokit.Reactive/bin/{context.Configuration}/*/Octokit.Reactive.dll"));

        foreach (var assembly in linkedAssemblies)
        {
            context.Information("Testing sourcelink info in {0}", context.Environment.WorkingDirectory.GetRelativePath(assembly));
            var exitCode = context.StartProcess("dotnet", new ProcessSettings
            {
                WorkingDirectory = "Octokit",
                Arguments = $"sourcelink test {assembly.FullPath}"
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