using System;
using Cake.Common;
using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Frosting;
using Cake.Core.Diagnostics;

public class Lifetime : FrostingLifetime<Context>
{
    public override void Setup(Context context)
    {
        context.Target = context.Argument("target", "Default");
        context.Configuration = context.Argument("configuration", "Release");
        context.LinkSources = context.Argument("linkSources", false);
        context.CoreOnly = context.Argument("CoreOnly", !context.IsRunningOnWindows());

        context.Artifacts = "./packaging/";
        context.CodeCoverage = "./coverage-results/";

        // Build system information.
        var buildSystem = context.BuildSystem();
        context.IsLocalBuild = buildSystem.IsLocalBuild;

        if (context.CoreOnly && !context.IsLocalBuild)
        {
            context.Warning("CoreOnly was specified on a non-local build. Artifacts may be versioned incorrectly!");
        }

        context.AppVeyor = buildSystem.AppVeyor.IsRunningOnAppVeyor;
        context.TravisCI = buildSystem.TravisCI.IsRunningOnTravisCI;
        context.IsTagged = IsBuildTagged(buildSystem);

        if (context.AppVeyor)
        {
            context.IsPullRequest = buildSystem.AppVeyor.Environment.PullRequest.IsPullRequest;
            context.IsOriginalRepo = StringComparer.OrdinalIgnoreCase.Equals("octokit/octokit.net", buildSystem.AppVeyor.Environment.Repository.Name);
            context.IsMasterBranch = StringComparer.OrdinalIgnoreCase.Equals("master", buildSystem.AppVeyor.Environment.Repository.Branch);
        }
        else if (context.TravisCI)
        {
            context.IsPullRequest = !string.IsNullOrEmpty(buildSystem.TravisCI.Environment.Repository.PullRequest);
            context.IsOriginalRepo = StringComparer.OrdinalIgnoreCase.Equals("octokit/octokit.net", buildSystem.TravisCI.Environment.Repository.Slug);
            context.IsMasterBranch = StringComparer.OrdinalIgnoreCase.Equals("master", buildSystem.TravisCI.Environment.Build.Branch);
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

        context.DotNetFormatToolPath = ToolInstaller.DotNetCoreToolInstall(context, "dotnet-format", "3.1.37601", "dotnet-format");
        context.GitVersionToolPath = ToolInstaller.DotNetCoreToolInstall(context, "GitVersion.Tool", "5.0.0", "dotnet-gitversion");

        // Calculate semantic version.
        context.Version = BuildVersion.Calculate(context);
        context.Version.Prefix = context.Argument<string>("version", context.Version.Prefix);
        context.Version.Suffix = context.Argument<string>("suffix", context.Version.Suffix);

        context.Information("Version:        {0}", context.Version.Prefix);
        context.Information("Version suffix: {0}", context.Version.Suffix);
        context.Information("Configuration:  {0}", context.Configuration);
        context.Information("LinkSources:    {0}", context.LinkSources);
        context.Information("CoreOnly:       {0}", context.CoreOnly);
        context.Information("Target:         {0}", context.Target);
        context.Information("AppVeyor:       {0}", context.AppVeyor);
        context.Information("TravisCI:       {0}", context.TravisCI);
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
}