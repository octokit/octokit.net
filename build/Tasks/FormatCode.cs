using System;
using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

public sealed class FormatCode : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        var exitCode = context.StartProcess("dotnet", new ProcessSettings
        {
            Arguments = $"format"
        });

        if (exitCode != 0)
        {
            throw new Exception($"Failed to format code - got exit code '{exitCode}'");
        }
    }

    public override bool ShouldRun(Context context)
    {
        return context.FormatCode;
    }
}
