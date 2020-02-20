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
