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

            var bccToken = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsImtpZCI6bnVsbCwidHlwIjoiSldUIn0.eyJhdWQiOiIuQXBpIiwianRpIjoiODk4MjVjYzItMDg2Yy00NzMzLTk1Y2UtMmIwN2IwOGZkYWU1IiwiaWF0IjoxNTM3OTAxMTI2LCJ1cm46YmNjOnJlcG9zaXRvcnlJZCI6NzUyODY3OSwidXJuOmJjYzpyZXBvc2l0b3J5TmFtZSI6Im9jdG9raXQubmV0IiwidXJuOmJjYzpyZXBvc2l0b3J5T3duZXIiOiJvY3Rva2l0IiwidXJuOmJjYzpyZXBvc2l0b3J5T3duZXJJZCI6MCwic3ViIjoiNDE3NTcxIn0.t-9f1oOXVqOQNZutOF0gfa7flhVc9j_szoswFB16vKg";

            var submissionDll = userProfilePath
                .CombineWithFilePath(
                    $".nuget\\packages\\bcc-submission\\0.0.2-alpha\\tools\\netcoreapp2.1\\BCC.Submission.dll")
                .MakeAbsolute(context.Environment)
                .FullPath;

            var repoCommit = context.EnvironmentVariable("APPVEYOR_REPO_COMMIT");

            context.Information($"Running BCC-Submission repoCommit:{repoCommit}");

            context.DotNetCoreExecute(submissionDll, new ProcessArgumentBuilder()
                .AppendSwitchQuoted("-i", checkRunJsonPath)
                .AppendSwitchQuoted("-t", bccToken)
                .AppendSwitchQuoted("-h", repoCommit));
        }
    }
}