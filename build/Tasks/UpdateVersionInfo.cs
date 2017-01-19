using System.Text;
using Cake.Common.Diagnostics;
using Cake.Frosting;

public class UpdateVersionInfo : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var newVersion = context.Version + "-*";
        context.Information("Updating version in project files to {0} ...", newVersion);

        foreach (var project in context.Projects)
        {
            var content = System.IO.File.ReadAllText(project.Path.FullPath, Encoding.UTF8);
            var node = Newtonsoft.Json.Linq.JObject.Parse(content);
            if (node["version"] != null && node["version"].ToString() != newVersion)
            {
                node["version"].Replace(newVersion);
                System.IO.File.WriteAllText(project.Path.FullPath, node.ToString(), Encoding.UTF8);
            };
        }
    }
}