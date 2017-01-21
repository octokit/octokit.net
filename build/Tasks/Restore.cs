using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Restore;
using Cake.Frosting;

[Dependency(typeof(Clean))]
[Dependency(typeof(UpdateVersionInfo))]
public class Restore : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.DotNetCoreRestore(
            ".",
            new DotNetCoreRestoreSettings
            {
                Verbose = false
            });
    }
}