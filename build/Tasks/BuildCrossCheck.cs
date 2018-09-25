using Cake.BCC;
using Cake.Common;
using Cake.Frosting;

[Dependency(typeof(Build))]
public sealed class BuildCrossCheck : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        var binaryLogPath = context.Artifacts.CombineWithFilePath("output.binlog").FullPath;
        var checkRunDataPath = context.Artifacts.CombineWithFilePath("checkrun.json").FullPath;
        var cloneRoot = context.Environment.WorkingDirectory.FullPath;

        context.BCCMSBuildLog(new BCCMSBuildLogToolSettings()
        {
            InputPath = binaryLogPath,
            OutputPath = checkRunDataPath,
            ClonePath = cloneRoot
        });

        context.BCCSubmission(new BCCMSBuildLogToolSettings());
    }
}