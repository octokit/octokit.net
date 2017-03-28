using Cake.Common;
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
        string version = null;
        string semVersion = null;

        var settings = new GitVersionSettings();
        if (!context.IsRunningOnWindows())
        {
            // On non windows, use our wrapper that uses mono to run GitVersion.exe
            settings.ToolPath = "./tools/gitversion_wrapper.sh";
        }

        context.Information("Calculating semantic version...");
        if (!context.IsLocalBuild)
        {
            // Run to set the version properties inside the CI server
            settings.OutputType = GitVersionOutput.BuildServer;
            context.GitVersion(settings);
        }

        // Run in interactive mode to get the properties for the rest of the script
        settings.OutputType = GitVersionOutput.Json;
        GitVersion assertedversions = context.GitVersion(settings);

        version = assertedversions.MajorMinorPatch;
        semVersion = assertedversions.LegacySemVerPadded;

        if (string.IsNullOrWhiteSpace(version))
        {
            throw new CakeException("Could not calculate version of build.");
        }

        return new BuildVersion(version, semVersion.Substring(version.Length).TrimStart('-'));
    }
}