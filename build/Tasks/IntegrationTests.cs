using System.Linq;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Test;
using Cake.Frosting;

[Dependency(typeof(Build))]
public sealed class IntegrationTests : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        foreach (var project in context.Projects.Where(x => x.IntegrationTests))
        {
            context.Information("Executing Integration Tests Project {0}...", project.Name);
            context.DotNetCoreTest(project.Path.FullPath, context.GetTestSettings());
        }
    }

    public override bool ShouldRun(Context context)
    {
        var environmentVariablesNames = new[] { "OCTOKIT_GITHUBUSERNAME", "OCTOKIT_GITHUBPASSWORD" };
        var areEnvironmentVariablesSet = environmentVariablesNames.All(x => !string.IsNullOrEmpty(context.Environment.GetEnvironmentVariable(x)));

        if (!areEnvironmentVariablesSet)
        {
            context.Warning($"The integration tests were skipped because the following environment variables are not set: {string.Join(", ", environmentVariablesNames)}.");
            context.Warning("Please configure these environment variables for a GitHub test account (DO NOT USE A \"REAL\" ACCOUNT).");
        }

        return areEnvironmentVariablesSet;
    }
}