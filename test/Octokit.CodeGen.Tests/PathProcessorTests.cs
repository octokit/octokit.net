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
        public async Task Process_WithSimplePath_ExtractsInformation()
        {
            var path = await LoadsSimplePath();

            var result = PathProcessor.Process(path);

            Assert.Equal("/marketplace_listing/accounts/{account_id}", result.Path);
            Assert.Single(result.Verbs);

            var get = result.Verbs.First();
            Assert.Equal(HttpMethod.Get, get.Method);
            Assert.Equal("application/vnd.github.v3+json", get.AcceptHeader);

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

            var content = response.Content;

            Assert.Single(content.Properties.Where(p => p.Name == "url" && p.Type == "string"));

            var objectPropType = content.Properties.Single(p => p.Name == "marketplace_pending_change" && p.Type == "object");
            var nestedObject = Assert.IsType<ObjectProperty>(objectPropType);

            Assert.Single(nestedObject.Properties.Where(p => p.Name == "unit_count" && p.Type == "string"));
            var nestedNestedObjectType = nestedObject.Properties.Single(p => p.Name == "plan" && p.Type == "object");

            var nestedNestedObject = Assert.IsType<ObjectProperty>(nestedNestedObjectType);

            Assert.Single(nestedNestedObject.Properties.Where(p => p.Name == "yearly_price_in_cents" && p.Type == "number"));
        }

        private static async Task<JsonDocument> LoadFixture(string filename)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var manifestResourceNames = assembly.GetManifestResourceNames();
            var stream = assembly.GetManifestResourceStream($"Octokit.CodeGen.Tests.fixtures.{filename}");
            return await JsonDocument.ParseAsync(stream);
        }

        private static async Task<JsonProperty> LoadsSimplePath()
        {
            var json = await LoadFixture("example-route.json");
            var paths = json.RootElement.GetProperty("paths");
            var properties = paths.EnumerateObject();
            var firstPath = properties.ElementAt(0);
            return firstPath;
        }
    }
}
