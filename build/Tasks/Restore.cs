using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Restore;
using Cake.Core;
using Cake.Frosting;

[Dependency(typeof(Clean))]
public sealed class Restore : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.DotNetCoreRestore(".", new DotNetCoreRestoreSettings
        {
            ArgumentCustomization = args => args
                .Append("/p:Version={0}", context.Version.GetSemanticVersion())
        });
    }
}