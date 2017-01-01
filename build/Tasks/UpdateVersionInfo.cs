using System.Text;
using Cake.Frosting;

[TaskName("Update-Version-Information")]
public class UpdateVersionInfo : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        foreach (var project in context.Projects)
        {
            var content = System.IO.File.ReadAllText(project.Path.FullPath, Encoding.UTF8);
            var node = Newtonsoft.Json.Linq.JObject.Parse(content);
            if (node["version"] != null && node["version"].ToString() != (context.Version + "-*"))
            {
                node["version"].Replace(string.Concat(context.Version, "-*"));
                System.IO.File.WriteAllText(project.Path.FullPath, node.ToString(), Encoding.UTF8);
            };
        }
    }
}