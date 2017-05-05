using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Restore;
using Cake.Frosting;

[Dependency(typeof(Clean))]
public sealed class Restore : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.DotNetCoreRestore(".", new DotNetCoreRestoreSettings
        {
            Verbose = false
        });
    }
}