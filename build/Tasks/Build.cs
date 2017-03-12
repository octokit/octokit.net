using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Core;
using Cake.Frosting;

[Dependency(typeof(Restore))]
public class Build : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.DotNetCoreBuild("./Octokit.sln", new DotNetCoreBuildSettings
        {
            Configuration = context.Configuration,
            ArgumentCustomization = args => args.Append("/p:Version={0}", context.Version.GetSemanticVersion())
        });
    }
}