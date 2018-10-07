using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.GitVersion;
using Cake.Core;

public static class GitVersionRunner
{
    public static GitVersion Run(ICakeContext context, GitVersionOutput outputType)
    {
        if (context.IsRunningOnWindows())
        {
            return context.GitVersion(new GitVersionSettings
            {
                OutputType = outputType
            });
        }
        else
        {
            // On non windows platform, point the GitVersion task at our wrapper script that uses mono to run GitVersion.exe
            context.Information("Overriding GitVersion ToolPath to /bin/sh ./tools/gitversion_wrapper.sh");
            return context.GitVersion(new GitVersionSettings
            {
                OutputType = outputType,
                ToolPath = "/bin/sh",
                ArgumentCustomization = args => args.Prepend("./tools/gitversion_wrapper.sh")
            });
        }
    }
}