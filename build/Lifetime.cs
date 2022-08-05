using System;
using Cake.Common;
using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Frosting;
using Cake.Core.Diagnostics;
using Cake.Core;

public class Lifetime : FrostingLifetime<Context>
{
    public override void Setup(Context context)
    {
        context.Target = context.Argument("target", "Default");
        context.Configuration = context.Argument("configuration", "Release");
        context.LinkSources = context.Argument("linkSources", false);
        context.FormatCode = context.Argument("formatCode", false);

        context.Artifacts = "./packaging/";

        // Build system information.
        var buildSystem = context.BuildSystem();
        context.IsLocalBuild = buildSystem.IsLocalBuild;

        context.GitHubActions = buildSystem.GitHubActions.IsRunningOnGitHubActions;
        context.AppVeyor = buildSystem.AppVeyor.IsRunningOnAppVeyor;
        context.IsTagged = IsBuildTagged(buildSystem);

        if (context.AppVeyor)
        {
            context.IsPullRequest = buildSystem.AppVeyor.Environment.PullRequest.IsPullRequest;
            context.IsOriginalRepo = StringComparer.OrdinalIgnoreCase.Equals("octokit/octokit.net", buildSystem.AppVeyor.Environment.Repository.Name);
            context.IsMainBranch = StringComparer.OrdinalIgnoreCase.Equals("main", buildSystem.AppVeyor.Environment.Repository.Branch);
        }
        else if (context.GitHubActions)
        {
            context.IsPullRequest = buildSystem.GitHubActions.Environment.PullRequest.IsPullRequest;
            context.IsOriginalRepo = StringComparer.OrdinalIgnoreCase.Equals("octokit/octokit.net", buildSystem.GitHubActions.Environment.Workflow.Repository);
            context.IsMainBranch = StringComparer.OrdinalIgnoreCase.Equals("main", buildSystem.GitHubActions.Environment.Workflow.Ref);
        }

        // Force publish?
        context.ForcePublish = context.Argument<bool>("forcepublish", false);

        // Setup projects.
        context.Projects = new Project[]
        {
            new Project { Name = "Octokit", Path = "./Octokit/Octokit.csproj", Publish = true },
            new Project { Name = "Octokit.Reactive", Path = "./Octokit.Reactive/Octokit.Reactive.csproj", Publish = true },
            new Project { Name = "Octokit.Tests", Path = "./Octokit.Tests/Octokit.Tests.csproj", UnitTests = true },
            new Project { Name = "Octokit.Tests.Conventions", Path = "./Octokit.Tests.Conventions/Octokit.Tests.Conventions.csproj", ConventionTests = true },
            new Project { Name = "Octokit.Tests.Integration", Path = "./Octokit.Tests.Integration/Octokit.Tests.Integration.csproj", IntegrationTests = true }
        };

        context.GitVersionToolPath = ToolInstaller.DotNetToolInstall(context, "GitVersion.Tool", "5.6.5", "dotnet-gitversion");
        ToolInstaller.DotNetToolInstall(context, "coverlet.console", "1.7.2", "coverlet");

        // Calculate semantic version.
        context.Version = BuildVersion.Calculate(context);
        context.Version.Prefix = context.Argument<string>("version", context.Version.Prefix);
        context.Version.Suffix = context.Argument<string>("suffix", context.Version.Suffix);

        context.Information("Version:        {0}", context.Version.Prefix);
        context.Information("Version suffix: {0}", context.Version.Suffix);
        context.Information("Configuration:  {0}", context.Configuration);
        context.Information("LinkSources:    {0}", context.LinkSources);
        context.Information("Target:         {0}", context.Target);
        context.Information("AppVeyor:       {0}", context.AppVeyor);
        context.Information("GitHub Actions: {0}", context.GitHubActions);
    }

    private static bool IsBuildTagged(BuildSystem buildSystem)
    {
        return buildSystem.AppVeyor.Environment.Repository.Tag.IsTag
            && !string.IsNullOrWhiteSpace(buildSystem.AppVeyor.Environment.Repository.Tag.Name);
    }

    private static string GetEnvironmentValueOrArgument(Context context, string environmentVariable, string argumentName)
    {
        var arg = context.EnvironmentVariable(environmentVariable);
        if (string.IsNullOrWhiteSpace(arg))
        {
            arg = context.Argument<string>(argumentName, null);
        }
        return arg;
    }

    public override void Teardown(Context context, ITeardownContext info)
    {
    }
}
