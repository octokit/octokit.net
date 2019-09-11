using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Octokit.Migration
{
    class Program
    {
        static string routesDir = "C:\\Users\\shiftkey\\src\\routes";
        static string rootForApi = Path.Combine(routesDir, "openapi", "api.github.com");
        static string rootRoute = Path.Combine(rootForApi, "index.json");

        static void Main(string[] args)
        {
            var json = File.ReadAllText(rootRoute);

            var obj = JObject.Parse(json);

            var paths = obj["paths"];

            foreach (JProperty p in paths)
            {
                var path = p.Name;
                var details = p.Value;
                Console.WriteLine($"Found path {path}");
                foreach (JProperty d in details)
                {
                    var relativeFilePath = d.Value["$ref"].ToString();
                    var fullPath = Path.Combine(rootForApi, relativeFilePath);
                    var newPayload = File.ReadAllText(fullPath);
                    var inner = JToken.Parse(newPayload);
                    var description = inner["description"].ToString();
                    var parameters = inner["parameters"] as JArray;

                    var acceptParameters = parameters.Where(param => param["name"].ToString() == "accept");
                    var paginationParameters = parameters.Where(param => param["name"].ToString() == "page" || param["name"].ToString() == "per_page");
                    var otherParameters = parameters.Except(acceptParameters).Except(paginationParameters).ToArray();

                    Console.WriteLine($" - {d.Name}");

                    if (description.Length > 0)
                    {
                        Console.WriteLine($"Has description \"{description}\"");
                    }

                    if (otherParameters.Length > 0)
                    {
                        Console.WriteLine($"Has parameters: ");
                        foreach (var param in otherParameters)
                        {
                            Console.WriteLine($" -- {param["name"]}");
                        }
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
