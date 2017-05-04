using Cake.Common.Tools.NuGet;
using Cake.Common.Tools.NuGet.Install;
using Cake.Core;

public static class ToolInstaller
{
    public static void Install(ICakeContext context, string package, string version)
    {
        context.NuGetInstall(package, new NuGetInstallSettings
        {
            Version = version,
            ExcludeVersion = true,
            OutputDirectory = "./tools"
        });
    }
}