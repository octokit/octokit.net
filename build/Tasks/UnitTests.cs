using System.Linq;
using Cake.Frosting;
using Cake.Common.Diagnostics;
using Cake.Common.Tools;
using Cake.Common.Tools.DotNet;

[IsDependentOn(typeof(Build))]
public sealed class UnitTests : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        foreach (var project in context.Projects.Where(x => x.UnitTests))
        {
            context.Information("Executing Unit Tests Project {0}...", project.Name);
            context.DotNetTest(project.Path.FullPath, context.GetTestSettings());
        }
    }
}
