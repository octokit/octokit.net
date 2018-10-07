using System.Linq;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Frosting;

[Dependency(typeof(Build))]
public sealed class ConventionTests : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        foreach (var project in context.Projects.Where(x => x.ConventionTests))
        {
            context.Information("Executing Convention Tests Project {0}...", project.Name);
            context.DotNetCoreTest(project.Path.FullPath, context.GetTestSettings());
        }
    }
}