using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.CodeGen.Tests
{
    public class PathProcessorTests
    {
        [Fact]
        public async Task Process_ForPathWithOneVerb_ExtractsInformation()
        {
            var path = await LoadPathWithGet();

            var result = PathProcessor.Process(path);

            Assert.Equal("/marketplace_listing/accounts/{account_id}", result.Path);
            Assert.Single(result.Verbs);

            var get = result.Verbs.First();
            Assert.Equal(HttpMethod.Get, get.Method);
            Assert.Equal("application/vnd.github.v3+json", get.AcceptHeader);
            Assert.Equal("Check if a GitHub account is associated with any Marketplace listing", get.Summary);
            Assert.Equal("Some description goes here", get.Description);

            Assert.Single(get.Parameters);
            var parameter = get.Parameters.First();

            Assert.Equal("account_id", parameter.Name);
            Assert.Equal("path", parameter.In);
            Assert.Equal("integer", parameter.Type);
            Assert.True(parameter.Required);

            Assert.Single(get.Responses);
            var response = get.Responses.First();

            Assert.Equal("200", response.StatusCode);
            Assert.Equal("application/json", response.ContentType);
            Assert.Equal("object", response.Content.Type);

            var content = Assert.IsType<ObjectResponseContent>(response.Content);

            Assert.Single(content.Properties.Where(p => p.Name == "url" && p.Type == "string"));

            var objectPropType = content.Properties.Single(p => p.Name == "marketplace_pending_change" && p.Type == "object");
            var nestedObject = Assert.IsType<ObjectProperty>(objectPropType);

            Assert.Single(nestedObject.Properties.Where(p => p.Name == "unit_count" && p.Type == "string"));
            var nestedNestedObjectType = nestedObject.Properties.Single(p => p.Name == "plan" && p.Type == "object");

            var nestedNestedObject = Assert.IsType<ObjectProperty>(nestedNestedObjectType);

            Assert.Single(nestedNestedObject.Properties.Where(p => p.Name == "yearly_price_in_cents" && p.Type == "number"));
        }

        [Fact]
        public async Task Process_ForPathWithTwoVerbs_ExtractsInformation()
        {
            var path = await LoadPathWithGetAndPost();

            var result = PathProcessor.Process(path);

            Assert.Equal("/repos/{owner}/{repo}/commits/{commit_sha}/comments", result.Path);
            Assert.Equal(2, result.Verbs.Count);

            var get = result.Verbs.First();
            Assert.Equal(HttpMethod.Get, get.Method);
            Assert.Equal("application/vnd.github.v3+json", get.AcceptHeader);
            Assert.Equal("List comments for a single commit", get.Summary);
            Assert.Equal("Use the `:commit_sha` to specify the commit that will have its comments listed.", get.Description);

            // required parameters
            Assert.Single(get.Parameters.Where(p => p.Name == "owner" && p.In == "path" && p.Type == "string" && p.Required));
            Assert.Single(get.Parameters.Where(p => p.Name == "repo" && p.In == "path" && p.Type == "string" && p.Required));
            Assert.Single(get.Parameters.Where(p => p.Name == "commit_sha" && p.In == "path" && p.Type == "string" && p.Required));

            // optional parameters
            Assert.Single(get.Parameters.Where(p => p.Name == "per_page" && p.In == "query" && p.Type == "integer" && !p.Required));
            Assert.Single(get.Parameters.Where(p => p.Name == "page" && p.In == "query" && p.Type == "integer" && !p.Required));

            Assert.Single(get.Responses);
            var response = get.Responses.First();

            Assert.Equal("200", response.StatusCode);
            Assert.Equal("application/json", response.ContentType);
            Assert.Equal("array", response.Content.Type);
        }

        private static async Task<JsonDocument> LoadFixture(string filename)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var manifestResourceNames = assembly.GetManifestResourceNames();
            var stream = assembly.GetManifestResourceStream($"Octokit.CodeGen.Tests.fixtures.{filename}");
            return await JsonDocument.ParseAsync(stream);
        }

        private static async Task<JsonProperty> LoadPathWithGet()
        {
            var json = await LoadFixture("example-get-route.json");
            var paths = json.RootElement.GetProperty("paths");
            var properties = paths.EnumerateObject();
            var firstPath = properties.ElementAt(0);
            return firstPath;
        }

        private static async Task<JsonProperty> LoadPathWithGetAndPost()
        {
            var json = await LoadFixture("example-get-and-post-route.json");
            var paths = json.RootElement.GetProperty("paths");
            var properties = paths.EnumerateObject();
            var firstPath = properties.ElementAt(0);
            return firstPath;
        }
    }
}
