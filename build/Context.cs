using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNet.Test;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

public class Context : FrostingContext
{
    public string Target { get; set; }
    public new string Configuration { get; set; }
    public bool FormatCode { get; set; }
    public BuildVersion Version { get; set; }

    public DirectoryPath Artifacts { get; set; }
    public bool IsLocalBuild { get; set; }
    public bool IsPullRequest { get; set; }
    public bool IsOriginalRepo { get; set; }
    public bool IsMainBranch { get; set; }
    public bool ForcePublish { get; set; }
    public bool GitHubActions { get; set; }

    public Project[] Projects { get; set; }

    public FilePath GitVersionToolPath { get; set; }

    public DotNetTestSettings GetTestSettings()
    {
        var settings = new DotNetTestSettings
        {
            Configuration = Configuration,
            NoBuild = true
        };

        if (!this.IsRunningOnWindows())
        {
            var testFramework = "net6.0";

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
