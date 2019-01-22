using Cake.Common.Diagnostics;
using Cake.Common.Tools.GitVersion;
using Cake.Core;

public class BuildVersion
{
    public string Prefix { get; set; }
    public string Suffix { get; set; }

    public BuildVersion(string version, string suffix)
    {
        Prefix = version;
        Suffix = suffix;

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
        context.Information("Calculating semantic version...");
        if (context.CoreOnly)
        {
            context.Information("Skipping GitVersion query for local build");
            return new BuildVersion("0.0.0", "dev");
        }

        if (!context.IsLocalBuild)
        {
            // Run to set the version properties inside the CI server
            GitVersionRunner.Run(context, GitVersionOutput.BuildServer);
        }

        // Run in interactive mode to get the properties for the rest of the script
        var assertedversions = GitVersionRunner.Run(context, GitVersionOutput.Json);
        
        var version = assertedversions.MajorMinorPatch;
        var semVersion = assertedversions.LegacySemVerPadded;

        if (string.IsNullOrWhiteSpace(version))
        {
            throw new CakeException("Could not calculate version of build.");
        }

        return new BuildVersion(version, semVersion.Substring(version.Length).TrimStart('-'));
    }
}