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
            WebClient downloadClient = new WebClient();
            var jsonFile = downloadClient.DownloadString("https://raw.githubusercontent.com/github/rest-api-description/main/descriptions/api.github.com/api.github.com.json");

            var apiSchemaObject = new SimpleJsonSerializer().Deserialize<OpenApiSchema>(jsonFile);
            var knownPaths = apiSchemaObject.Paths.Keys.ToList();

            var allRouteProps = typeof(ApiRoutes).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            var allRoutes = allRouteProps.Select(x => "/" + x.GetValue(this));

            var matches = allRoutes.Where(x => knownPaths.Contains(x)).ToList();
            var failures = allRoutes.Where(x => !knownPaths.Contains(x)).ToList();

            _outputHelper.WriteLine($"Failures: {failures.Count}");

            foreach (var item in failures)
            {
                _outputHelper.WriteLine(item);
            }

            _outputHelper.WriteLine($"Matches: {matches.Count}");

            foreach (var item in matches)
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
