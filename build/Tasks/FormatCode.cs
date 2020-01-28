using System;
using Cake.Common;
using Cake.Core.IO;
using Cake.Frosting;

public sealed class FormatCode : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        var result  = context.StartProcess("dotnet", new ProcessSettings
        {
            Arguments = $"format"
        });

        if (result != 0)
        {
            throw new Exception($"Failed to execute dotnet format ({result})");
        }
    }

    public override bool ShouldRun(Context context)
    {
        return context.IsLocalBuild;
    }
}
