using System.Linq;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNet;
using Cake.Frosting;

[IsDependentOn(typeof(Build))]
public sealed class ConventionTests : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        foreach (var project in context.Projects.Where(x => x.ConventionTests))
        {
            context.Information("Executing Convention Tests Project {0}...", project.Name);
            context.DotNetTest(project.Path.FullPath, context.GetTestSettings());
        }
    }
}
