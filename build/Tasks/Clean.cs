using Cake.Common.IO;
using Cake.Frosting;

public class Clean : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (context.DirectoryExists(context.OutputDir))
        {
            context.DeleteDirectory(context.OutputDir, recursive: true);
        }
        context.CreateDirectory(context.OutputDir);
    }
}