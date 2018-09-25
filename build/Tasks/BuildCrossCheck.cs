using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Execute;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

[Dependency(typeof(Build))]
public sealed class BuildCrossCheck : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        if (context.AppVeyor)
        {
            var userProfilePath = context.FileSystem.GetDirectory(context.EnvironmentVariable("USERPROFILE"))
                .Path;

            var msbuildLogDll = userProfilePath
                .CombineWithFilePath(
                    $".nuget\\packages\\bcc-msbuildlog\\0.0.2-alpha\\tools\\netcoreapp2.1\\BCC.MSBuildLog.dll")
                .MakeAbsolute(context.Environment)
                .FullPath;

            var checkRunJsonPath = context.Artifacts.CombineWithFilePath("checkrun.json").FullPath;

            context.Information("Running BCC-MSbuild");

            context.DotNetCoreExecute(msbuildLogDll, new ProcessArgumentBuilder()
                .AppendSwitchQuoted("-i", context.Artifacts.CombineWithFilePath("output.binlog").FullPath)
                .AppendSwitchQuoted("-o", checkRunJsonPath)
                .AppendSwitchQuoted("-c", context.Environment.WorkingDirectory.FullPath));

            var bccToken = context.EnvironmentVariable("BCC_TOKEN");

            if (!string.IsNullOrWhiteSpace(bccToken))
            {
                var submissionDll = userProfilePath
                    .CombineWithFilePath(
                        $".nuget\\packages\\bcc-submission\\0.0.2-alpha\\tools\\netcoreapp2.1\\BCC.Submission.dll")
                    .MakeAbsolute(context.Environment)
                    .FullPath;

                context.Information("Running BCC-Submission");

                context.DotNetCoreExecute(submissionDll, new ProcessArgumentBuilder()
                    .AppendSwitchQuoted("-i", checkRunJsonPath)
                    .AppendSwitchQuoted("-t", bccToken)
                    .AppendSwitchQuoted("-h", context.EnvironmentVariable("APPVEYOR_REPO_COMMIT")));
            }
        }
    }
}