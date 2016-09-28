using Cake.Core;
using Cake.Frosting;

public class BuildContext : FrostingContext
{
    public string Target { get; set; }
    public string Configuration { get; set; }
    public string Version { get; set; }
    public string Suffix { get; set; }

    public string MyGetSource { get; set; }
    public string MyGetApiKey { get; set; }
    
    public bool IsLocalBuild { get; set; }
    public bool IsPullRequest { get; set; }
    public bool IsOriginalRepo { get; set; }
    public bool IsTagged { get; set; }
    public bool IsMasterBranch { get; set; }
    public bool ForcePublish { get; set; }

    public bool AppVeyor { get; set; }
    public bool TravisCI { get; set; }
    public Project[] Projects { get; set; }

    public BuildContext(ICakeContext context)
        : base(context)
    {
    }
}