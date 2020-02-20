using System.Text.Json;

namespace Octokit.CodeGen
{
    public class PathProcessor
    {
        public static PathResult Process(JsonProperty jsonProperty)
        {
            return new PathResult()
            {
                Path = jsonProperty.Name
            };
        }
    }

    public class PathResult
    {
        public string Path { get; set; }
    }
}
