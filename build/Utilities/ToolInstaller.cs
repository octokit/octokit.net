using System.IO;
using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.NuGet;
using Cake.Common.Tools.NuGet.Install;
using Cake.Core;

public static class ToolInstaller
{
    public static void Install(ICakeContext context, string package, string version)
    {
        var settings = new NuGetInstallSettings
        {
            Version = version,
            ExcludeVersion = true,
            OutputDirectory = "./tools"
        };

        if (!context.IsRunningOnWindows())
        {
            settings.ToolPath = "mono";
            settings.ArgumentCustomization = args => args.Prepend("./tools/nuget/NuGet.exe");
        }

        context.NuGetInstall(package, settings);
    }
}