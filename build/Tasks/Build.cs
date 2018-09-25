using Cake.Common;
using Cake.Common.IO;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Core;
using Cake.Frosting;

[Dependency(typeof(Restore))]
public class Build : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        var structuredLoggerPath = context.Directory(context.EnvironmentVariable("USERPROFILE"))
            .Path.CombineWithFilePath(".nuget\\packages\\msbuild.structuredlogger\\2.0.11\\lib\\netstandard2.0\\StructuredLogger.dll")
            .MakeAbsolute(context.Environment)
            .FullPath;

        var outputBinaryLogPath = context.Artifacts.CombineWithFilePath("output.binlog").FullPath;

        context.DotNetCoreBuild("./Octokit.sln", new DotNetCoreBuildSettings
        {
            Configuration = context.Configuration,
            ArgumentCustomization = args => args
                .Append("/logger:BinaryLogger,\"{0}\";\"{1}\"", structuredLoggerPath, outputBinaryLogPath)
                .Append("/p:Version={0}", context.Version.GetSemanticVersion())
                .Append("/p:SourceLinkCreate={0}", context.LinkSources.ToString().ToLower())
        });
    }
}