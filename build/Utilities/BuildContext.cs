using System;
using Cake.Core;
using Cake.Frosting;

public class BuildContext : FrostingContext
{
    public string Target { get; set; }
    public string Configuration { get; set; }
    public string Version { get; set; }
    public string Suffix { get; set; }

    public string OutputDir { get; set; }

    public string MyGetSource { get; set; }
    public string MyGetApiKey { get; set; }
    
    public bool IsLocalBuild { get; set; }
    public bool IsPullRequest { get; set; }
    public string RepositoryName { get; set; }
    public bool IsOriginalRepo => !IsPullRequest && RepositoryName.Equals(Constants.OriginalRepositoryName, StringComparison.OrdinalIgnoreCase);
    public bool IsTagged { get; set; }
    public bool IsMasterBranch { get; set; }
    public bool IsReleaseBranch { get; set; }
    public bool ForcePublish { get; set; }

    public bool AppVeyor { get; set; }
    public bool TravisCI { get; set; }
    public Project[] Projects { get; set; }

    public BuildContext(ICakeContext context)
        : base(context)
    {
    }
}