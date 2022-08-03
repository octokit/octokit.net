using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Restore;
using Cake.Core;
using Cake.Frosting;

[IsDependentOn(typeof(Clean))]
public sealed class Restore : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.DotNetRestore(".", new DotNetCoreRestoreSettings
        {
            ArgumentCustomization = args => args
                .Append("/p:Version={0}", context.Version.GetSemanticVersion())
        });
    }
}
