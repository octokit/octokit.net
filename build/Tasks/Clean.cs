using Cake.Common.IO;
using Cake.Frosting;

public sealed class Clean : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        var directories = context.GetDirectories("./**/bin", x => !x.Path.FullPath.Contains("/build/"))
            + context.GetDirectories("./**/obj", x => !x.Path.FullPath.Contains("/build/"))
            + context.Artifacts;

        foreach (var directory in directories)
        {
            context.CleanDirectory(directory);
        }
    }
}