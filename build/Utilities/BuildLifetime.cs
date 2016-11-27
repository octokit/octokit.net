using System;
using System.Text;
using Cake.Common;
using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Restore;
using Cake.Common.Tools.GitVersion;
using Cake.Core;
using Cake.Frosting;

public class BuildLifetime : FrostingLifetime<BuildContext>
{
    public override void Setup(BuildContext context)
    {
        context.Target = context.Argument<string>("target", "Default");
        context.Configuration = context.Argument<string>("configuration", "Release");

        context.OutputDir = "./packaging/";

        // Build system information.
        var buildSystem = context.BuildSystem();
        context.IsLocalBuild = buildSystem.IsLocalBuild;

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
            new Project { Name = "Octokit.Next", Path = "./Octokit.Next/project.json", Publish = true },
            new Project { Name = "Octokit.Next.Tests", Path = "./Octokit.Next.Tests/project.json", Tests = true }
        };

        // Install tools
        context.Information("Installing tools...");
        context.DotNetCoreRestore("./build/tools.project.json", new DotNetCoreRestoreSettings
        {
            PackagesDirectory = "./tools",
            Verbosity = DotNetCoreRestoreVerbosity.Error,
            Sources = new[]
            {
                "https://api.nuget.org/v3/index.json"
            }
        });

        // Calculate semantic version.
        var info = GetVersion(context);
        context.Version = context.Argument<string>("version", info.Version);
        context.Suffix = context.Argument<string>("suffix", info.Suffix);

        context.Information("Version: {0}", context.Version);
        context.Information("Version suffix: {0}", context.Suffix);
        context.Information("Configuration: {0}", context.Configuration);
        context.Information("Target: {0}", context.Target);
        context.Information("AppVeyor: {0}", context.AppVeyor);
        context.Information("TravisCI: {0}", context.TravisCI);
    }

    private static BuildVersion GetVersion(BuildContext context)
    {
        string version = null;
        string semVersion = null;

        if (context.IsRunningOnWindows())
        {
            context.Information("Calculating semantic version...");
            if (!context.IsLocalBuild)
            {
                context.GitVersion(new GitVersionSettings
                {
                    OutputType = GitVersionOutput.BuildServer
                });
            }

            GitVersion assertedVersions = context.GitVersion(new GitVersionSettings
            {
                OutputType = GitVersionOutput.Json
            });
            version = assertedVersions.MajorMinorPatch;
            semVersion = assertedVersions.LegacySemVerPadded;
        }

        if (string.IsNullOrEmpty(version) || string.IsNullOrEmpty(semVersion))
        {
            context.Information("Fetching verson from first project.json...");
            foreach (var project in context.Projects)
            {
                var content = System.IO.File.ReadAllText(project.Path.FullPath, Encoding.UTF8);
                var node = Newtonsoft.Json.Linq.JObject.Parse(content);
                if (node["version"] != null)
                {
                    version = node["version"].ToString().Replace("-*", "");
                }
            }
            semVersion = version;
        }

        if (string.IsNullOrWhiteSpace(version))
        {
            throw new CakeException("Could not calculate version of build.");
        }

        return new BuildVersion(version, semVersion.Substring(version.Length).TrimStart('-'));
    }

    private static bool IsBuildTagged(BuildSystem buildSystem)
    {
        return buildSystem.AppVeyor.Environment.Repository.Tag.IsTag
            && !string.IsNullOrWhiteSpace(buildSystem.AppVeyor.Environment.Repository.Tag.Name);
    }

    private static string GetEnvironmentValueOrArgument(BuildContext context, string environmentVariable, string argumentName)
    {
        var arg = context.EnvironmentVariable(environmentVariable);
        if (string.IsNullOrWhiteSpace(arg))
        {
            arg = context.Argument<string>(argumentName, null);
        }
        return arg;
    }
}