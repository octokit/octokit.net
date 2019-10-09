using Cake.Common.Tools.DotNetCore;
using Cake.Frosting;

public sealed class FormatCode : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.DotNetCoreTool("format");
    }

    public override bool ShouldRun(Context context)
    {
        return context.IsLocalBuild;
    }
}
