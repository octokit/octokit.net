using Cake.Common.IO;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Tool;
using Cake.Common.Tools.NuGet;
using Cake.Common.Tools.NuGet.Install;
using Cake.Core;
using Cake.Core.IO;

public static class ToolInstaller
{
    private static DirectoryPath ToolsPath { get; } = "./tools"; 
    public static void Install(ICakeContext context, string package, string version)
    {
        context.NuGetInstall(package, new NuGetInstallSettings
        {
            Version = version,
            ExcludeVersion = true,
            OutputDirectory = ToolsPath
        });
    }

    public static FilePath DotNetCoreToolInstall(
        this ICakeContext context,
        string package,
        string version,
        string toolName)
    {
        context.EnsureDirectoryExists(ToolsPath);

        var toolsPath = context.MakeAbsolute(ToolsPath);

        var toolInstallPath = toolsPath
                                .Combine(".store")
                                .Combine(package.ToLowerInvariant())
                                .Combine(version.ToLowerInvariant());

        var toolPath = toolsPath.CombineWithFilePath(
                        string.Concat(
                            toolName,
                            context.Environment.Platform.IsUnix()
                                ? string.Empty
                                : ".exe"
                            )
                        );
                    
        if (!context.DirectoryExists(toolInstallPath) && context.FileExists(toolPath))
        {
            context.DotNetCoreTool("tool", new DotNetCoreToolSettings
                {
                    ArgumentCustomization = args => args
                        .Append("uninstall")
                        .AppendSwitchQuoted("--tool-path", toolsPath.FullPath)
                        .AppendQuoted(package)
                });
        }

        if (!context.FileExists(toolPath))
        {
            context.DotNetCoreTool("tool", new DotNetCoreToolSettings
                {
                    ArgumentCustomization = args => args
                        .Append("install")
                        .AppendSwitchQuoted("--version", version)
                        .AppendSwitchQuoted("--tool-path", toolsPath.FullPath)
                        .AppendQuoted(package)
                });
        }

        if (!context.FileExists(toolPath))
        {
            throw new System.Exception($"Failed to install .NET Core tool {package} ({version}).");
        }

        return toolPath;
    }
}