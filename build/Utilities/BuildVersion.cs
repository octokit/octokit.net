using Cake.Common.Diagnostics;
using Cake.Common.Tools.GitVersion;
using Cake.Core;

public class BuildVersion
{
    public string Prefix { get; set; }
    public string Suffix { get; set; }
    public string FullSemVer { get; set; }

    public BuildVersion(string version, string suffix, string fullSemVer)
    {
        Prefix = version;
        Suffix = suffix;
        FullSemVer = fullSemVer;

        if (string.IsNullOrWhiteSpace(Suffix))
        {
            Suffix = null;
        }
    }

    public string GetSemanticVersion()
    {
        if (!string.IsNullOrWhiteSpace(Suffix))
        {
            return string.Concat(Prefix, "-", Suffix);
        }
        return Prefix;
    }

    public static BuildVersion Calculate(Context context)
    {
        string version = null;
        string semVersion = null;
        string fullSemVer = null;

        context.Information("Calculating semantic version...");

        if (!context.IsLocalBuild)
        {
            // Run to set the version properties inside the CI server
            GitVersionRunner.Run(context, GitVersionOutput.BuildServer);
        }

        // Run in interactive mode to get the properties for the rest of the script
        var assertedversions = GitVersionRunner.Run(context, GitVersionOutput.Json);
        
        version = assertedversions.MajorMinorPatch;
        semVersion = assertedversions.LegacySemVerPadded;
        fullSemVer = assertedversions.FullSemVer;

        if (string.IsNullOrWhiteSpace(version))
        {
            throw new CakeException("Could not calculate version of build.");
        }

        return new BuildVersion(version, semVersion.Substring(version.Length).TrimStart('-'), fullSemVer);
    }
}