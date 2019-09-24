using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore.Test;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

public class Context : FrostingContext
{
    public string Target { get; set; }
    public string Configuration { get; set; }
    public bool LinkSources { get; set; }
    public BuildVersion Version { get; set; }

    public DirectoryPath Artifacts { get; set; }
    public DirectoryPath CodeCoverage { get; set; }

    public bool IsLocalBuild { get; set; }
    public bool IsPullRequest { get; set; }
    public bool IsOriginalRepo { get; set; }
    public bool IsTagged { get; set; }
    public bool IsMasterBranch { get; set; }
    public bool ForcePublish { get; set; }

    public bool AppVeyor { get; set; }
    public bool TravisCI { get; set; }

    public bool CoreOnly { get; set; }

    public Project[] Projects { get; set; }

    public FilePath DotNetFormatToolPath { get; set; }
    public FilePath GitVersionToolPath { get; set; }

    public DotNetCoreTestSettings GetTestSettings()
    {
        var settings = new DotNetCoreTestSettings
        {
            Configuration = Configuration,
            NoBuild = true
        };

        if (!this.IsRunningOnWindows())
        {
            var testFramework = "netcoreapp2.0";

            this.Information($"Running tests against {testFramework} only as we're not on Windows.");
            settings.Framework = testFramework;
        }

        return settings;
    }

    public Context(ICakeContext context)
        : base(context)
    {
    }
}