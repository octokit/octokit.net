using System;
using Cake.Common;
using Cake.Frosting;

public sealed class FormatCode : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        int result  = context.StartProcess(context.DotNetFormatToolPath);
        if (result != 0)
        {
            throw new Exception($"Failed to execute {context.DotNetFormatToolPath} ({result})");
        }
    }

    public override bool ShouldRun(Context context)
    {
        return context.IsLocalBuild;
    }
}
