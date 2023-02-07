using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Core;
using Cake.Frosting;

[IsDependentOn(typeof(Restore))]
[IsDependentOn(typeof(FormatCode))]
public class Build : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.DotNetBuild("./Octokit.sln", new DotNetBuildSettings
        {
            Configuration = context.Configuration,
            ArgumentCustomization = args => args
                .Append("/p:Version={0}", context.Version.GetSemanticVersion()),
        });
    }
}
