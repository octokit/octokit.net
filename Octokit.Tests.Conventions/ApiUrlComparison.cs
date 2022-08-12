using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace Octokit.Tests.Conventions
{
    public class ApiUrlComparison
    {
        private readonly ITestOutputHelper _outputHelper;

        public ApiUrlComparison(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void WorkOut_HowFarWeAreFromOpenApi()
        {
            var file = File.ReadAllText(new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.ToString() + @"\Octokit\Helpers\ApiUrls.cs");

            var regex = new Regex("return \"[a-zA-Z\\/{ }0-9]+\"");
            var matches = regex.Matches(file);

            Assert.True(matches.Count > 0);

            var sdkRoutes = new List<string>();

            foreach (Match match in matches)
            {
                var matchValue = match.Value;
                matchValue = matchValue.Replace("return \"", "").Replace("\"", "").Trim();
                sdkRoutes.Add($"/{matchValue}");
            }

            Assert.Equal(matches.Count, sdkRoutes.Count);

            WebClient downloadClient = new WebClient();
            var jsonFile = downloadClient.DownloadString("https://raw.githubusercontent.com/github/rest-api-description/main/descriptions/api.github.com/api.github.com.json");

            var apiSchemaObject = new SimpleJsonSerializer().Deserialize<OpenApiSchema>(jsonFile);
            var knownPaths = apiSchemaObject.Paths.Keys.ToList();

            Assert.True(knownPaths.Any());

            var openApiRoutes = new List<string>();

            foreach (var path in knownPaths)
            {
                var localPath = path;
                var pathMatches = new Regex("{[a-zA-Z0-9_]+}").Matches(path);
                for (int i = 0; i < pathMatches.Count; i++)
                {
                    localPath = localPath.Replace(pathMatches[i].Value, "{" + i.ToString() + "}");
                }

                openApiRoutes.Add(localPath);
            }

            var distinctSdkRoutes = sdkRoutes.Distinct().ToList();
            var distinctOpenApiRoutes = openApiRoutes.Distinct().ToList();

            var routeMatches = distinctOpenApiRoutes.Where(x => distinctSdkRoutes.Contains(x)).ToList();

            _outputHelper.WriteLine($"Number of SDK routes: {distinctSdkRoutes.Count}");
            _outputHelper.WriteLine($"Number of OpenApi routes: {distinctOpenApiRoutes.Count}");

            _outputHelper.WriteLine($"Matches: {routeMatches.Count}");
            foreach(var item in routeMatches)
            {
                _outputHelper.WriteLine(item);
            }
        }

        private class ApiUrlComparisonResult
        {
            public ApiUrlComparisonResult(string name, Uri uri)
            {
                Name = name;
                Uri = uri;
                InvokedSuccessfully = true;
            }

            public ApiUrlComparisonResult(string name, string failureMessage)
            {
                Name = name;
                InvokedSuccessfully = false;
                FailureMessage = failureMessage;
            }

            public string Name { get; set; } = string.Empty;

            public Uri Uri { get; set; }

            public string Url => "/" + Uri.ToString();

            public bool InvokedSuccessfully { get; set; }

            public string FailureMessage { get; set; } = string.Empty;
        }
    }

    public class OpenApiSchema
    {
        public Dictionary<string, object> Paths { get; set; }
        public Dictionary<string, Dictionary<string, Parameter>> Components { get; set; }
    }

    public class Component
    {
        public Dictionary<string, Dictionary<string,Parameter>> Parameters { get; set; }
    }

    public class Parameter
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string In { get; set; }

        public Schema Schema { get; set; }
    }

    public class Schema
    {
        public string Type { get; set; }

        public object Default { get; set; }
    }
}
