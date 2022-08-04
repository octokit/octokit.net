using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public void WorkOk()
        {
            ApiUrls.AllPublicRepositories(1);

            WebClient downloadClient = new WebClient();
            var jsonFile = downloadClient.DownloadString("https://raw.githubusercontent.com/github/rest-api-description/main/descriptions/api.github.com/api.github.com.json");

            var apiSchemaObject = new SimpleJsonSerializer().Deserialize<OpenApiSchema>(jsonFile);
            var knownPaths = apiSchemaObject.Paths.Keys.ToList();

            List<string> convertedPaths = new List<string>();

            foreach (var knownPath in knownPaths)
            {
                var splits = knownPath.Split('{');

                if (splits.Length == 1)
                {
                    convertedPaths.Add(knownPath);
                    continue;
                }

                var localPath = knownPath;

                foreach (var item in splits.Where(x => x.Contains("}")))
                {
                    var cleanItem = item.Replace("{", "").Split('}')[0].Replace("}", "");

                    var matchedType = apiSchemaObject.Components["parameters"].FirstOrDefault(x => x.Value.Name == cleanItem);
                    if (matchedType.Key != null && matchedType.Value.Schema.Type == "string")
                    {
                        localPath = localPath.Replace("{" + cleanItem + "}", "abcdef");
                    }
                    else if (matchedType.Key != null && matchedType.Value.Schema.Type == "integer")
                    {
                        localPath = localPath.Replace("{" + cleanItem + "}", "12345");
                    }
                }

                convertedPaths.Add(localPath);
            }

            //var apiUrls = ApiUrls._allUrls.Values.Select(x => "/" + x).ToList();
            //var allMatches = apiUrls.Where(x => knownPaths.Contains(x)).ToList();
            //var allFailures = apiUrls.Where(x => !knownPaths.Contains(x)).ToList();

            //_outputHelper.WriteLine($"Failures - {allFailures.Count}");
            
            //foreach (var item in allFailures)
            //{
            //    _outputHelper.WriteLine(item);
            //}

            //_outputHelper.WriteLine($"Matches - {allMatches.Count}");

            //foreach(var item in allMatches)
            //{
            //    _outputHelper.WriteLine(item);
            //}
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
