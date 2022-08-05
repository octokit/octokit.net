using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Restore;
using Cake.Core;
using Cake.Frosting;

[IsDependentOn(typeof(Clean))]
public sealed class Restore : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.DotNetRestore(".", new DotNetRestoreSettings
        {
            ArgumentCustomization = args => args
                .Append("/p:Version={0}", context.Version.GetSemanticVersion())
        });
    }
}
