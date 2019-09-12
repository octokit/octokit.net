using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Octokit.Migration
{
    class Program
    {
        static Report report = new Report();

        static string routesDir = "C:\\Users\\shiftkey\\src\\routes";
        static string rootForApi = Path.Combine(routesDir, "openapi", "api.github.com");
        static string rootRoute = Path.Combine(rootForApi, "index.json");

        static string[] routePrefixes = { "/repos/" };

        static Dictionary<string, Type> resolutions = new Dictionary<string, Type>() {
            { "/repos/{owner}/{repo}", typeof(IRepositoriesClient) },
            { "/repos/{owner}/{repo}/branches", typeof(IRepositoryBranchesClient) },
            { "/repos/{owner}/{repo}/branches/{branch}", typeof(IRepositoryBranchesClient) },
            { "/repos/{owner}/{repo}/releases", typeof(IReleasesClient) }
        };

        static void Main(string[] args)
        {
            var json = File.ReadAllText(rootRoute);
            var obj = JObject.Parse(json);
            var paths = obj["paths"];

            foreach (JProperty path in paths)
            {
                ParseEndpoint(path);
            }

            report.OutputSummary();
        }

        static void ParseEndpoint(JProperty endpoint)
        {
            var route = endpoint.Name;
            if (!routePrefixes.Any(prefix => route.StartsWith(prefix)))
            {
                report.AddInconclusive(route);
                return;
            }

            Type resolvedType;
            if (!resolutions.TryGetValue(route, out resolvedType))
            {
                report.AddMissing(route);
                return;
            }

            var details = endpoint.Value;
            foreach (JProperty d in details)
            {
                var verb = d.Name;
                var path = d.Value["$ref"].ToString();
                FindMatchingMethod(route, verb, path, resolvedType);
            }
        }

        static void FindMatchingMethod(string route, string verb, string path, Type resolvedType)
        {
            var fullPath = Path.Combine(rootForApi, path);

            var newPayload = File.ReadAllText(fullPath);
            var inner = JToken.Parse(newPayload);
            var parameters = inner["parameters"] as JArray;

            var acceptParameters = parameters.Where(param => param["name"].ToString() == "accept");
            var paginationParameters = parameters.Where(param => param["name"].ToString() == "page" || param["name"].ToString() == "per_page");
            var otherParameters = parameters.Except(acceptParameters).Except(paginationParameters).ToArray();

            var expectedMethodName = MapToMethodName(path);

            if (string.IsNullOrWhiteSpace(expectedMethodName))
            {
                report.AddMissing(route, verb);
                return;
            }

            var method = resolvedType.GetMethods()
                .Where(m => !m.GetCustomAttributes(false).Contains(typeof(ObsoleteAttribute)))
                .Where(m => m.Name == expectedMethodName)
                .FirstOrDefault();
            if (method == null)
            {
                report.AddMissing(route, verb);
                return;

            }

            // TODO: a bunch of other validation

            if (otherParameters.Length > 0)
            {
                Console.WriteLine($"Has parameters: ");
                foreach (var param in otherParameters)
                {
                    var type = ParseType(param);
                    var required = IsRequired(param) ? "(required)" : "";
                    Console.WriteLine($" -- {param["name"]} ({type}) {required}");
                }
            }
        }

        static string MapToMethodName(string path)
        {
            var fileName = Path.GetFileName(path);
            if (Regex.IsMatch(fileName, "list.*\\.json"))
            {
                return "GetAll";
            }

            if (Regex.IsMatch(fileName, "get.*\\.json"))
            {
                return "Get";
            }

            if (Regex.IsMatch(fileName, "update.*\\.json"))
            {
                return "Update";
            }

            if (Regex.IsMatch(fileName, "delete.*\\.json"))
            {
                return "Delete";
            }

            return "";


        }

        static string ParseType(JToken param)
        {
            return param["schema"]["type"].ToString();
        }

        static bool IsRequired(JToken param)
        {
            if (param["required"] == null)
            {
                return false;
            }

            return param["required"].Value<bool>();
        }
    }
}
