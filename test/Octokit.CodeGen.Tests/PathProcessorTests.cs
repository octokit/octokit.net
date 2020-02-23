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

            var parameter = Assert.Single(get.Parameters);

            Assert.Equal("account_id", parameter.Name);
            Assert.Equal("path", parameter.In);
            Assert.Equal("integer", parameter.Type);
            Assert.True(parameter.Required);

            var response = Assert.Single(get.Responses);

            Assert.Equal("200", response.StatusCode);
            Assert.Equal("application/json", response.ContentType);
            Assert.Equal("object", response.Content.Type);

            var content = Assert.IsType<ObjectContent>(response.Content);

            Assert.Single(content.Properties.Where(p => p.Name == "url" && p.Type == "string"));

            var nestedObject = Assert.Single(content.Properties.Where(p => p.Name == "marketplace_pending_change" && p.Type == "object").OfType<ObjectProperty>());

            Assert.Single(nestedObject.Properties.Where(p => p.Name == "unit_count" && p.Type == "string"));

            var nestedNestedObject = Assert.Single(nestedObject.Properties.Where(p => p.Name == "plan" && p.Type == "object").OfType<ObjectProperty>());

            Assert.Single(nestedNestedObject.Properties.Where(p => p.Name == "yearly_price_in_cents" && p.Type == "number"));
        }

        [Fact]
        public async Task Process_ForPathWithTwoVerbs_ExtractsInformationForGet()
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

            // no request parameters needed
            Assert.Null(get.RequestBody);

            var response = Assert.Single(get.Responses);

            Assert.Equal("200", response.StatusCode);
            Assert.Equal("application/json", response.ContentType);
            Assert.Equal("array", response.Content.Type);

            var content = Assert.IsType<ArrayContent>(response.Content);

            Assert.Single(content.ItemProperties.Where(p => p.Name == "html_url" && p.Type == "string"));

            var objectPropType = content.ItemProperties.Single(p => p.Name == "user" && p.Type == "object");
            var nestedObject = Assert.IsType<ObjectProperty>(objectPropType);

            Assert.Single(nestedObject.Properties.Where(p => p.Name == "login" && p.Type == "string"));
        }

        [Fact]
        public async Task Process_ForPathWithTwoVerbs_ExtractsInformationForPost()
        {
            var path = await LoadPathWithGetAndPost();

            var result = PathProcessor.Process(path);

            Assert.Equal("/repos/{owner}/{repo}/commits/{commit_sha}/comments", result.Path);
            Assert.Equal(2, result.Verbs.Count);

            var post = result.Verbs.Last();
            Assert.Equal(HttpMethod.Post, post.Method);
            Assert.Equal("application/vnd.github.v3+json", post.AcceptHeader);
            Assert.Equal("Create a commit comment", post.Summary);
            Assert.Equal("Create a comment for a commit using its `:commit_sha`.\n\nThis endpoint triggers [notifications](https://help.github.com/articles/about-notifications/). Creating content too quickly using this endpoint may result in abuse rate limiting. See \"[Abuse rate limits](https://developer.github.com/v3/#abuse-rate-limits)\" and \"[Dealing with abuse rate limits](https://developer.github.com/v3/guides/best-practices-for-integrators/#dealing-with-abuse-rate-limits)\" for details.", post.Description);

            // required parameters
            Assert.Single(post.Parameters.Where(p => p.Name == "owner" && p.In == "path" && p.Type == "string" && p.Required));
            Assert.Single(post.Parameters.Where(p => p.Name == "repo" && p.In == "path" && p.Type == "string" && p.Required));
            Assert.Single(post.Parameters.Where(p => p.Name == "commit_sha" && p.In == "path" && p.Type == "string" && p.Required));

            Assert.NotNull(post.RequestBody);

            Assert.Equal("application/json", post.RequestBody.ContentType);
            Assert.Equal("object", post.RequestBody.Content.Type);

            var requestContent = Assert.IsType<RequestObjectContent>(post.RequestBody.Content);

            Assert.Single(requestContent.Properties.Where(p => p.Name == "body" && p.Type == "string" && p.Required));
            Assert.Single(requestContent.Properties.Where(p => p.Name == "path" && p.Type == "string" && !p.Required));
            Assert.Single(requestContent.Properties.Where(p => p.Name == "position" && p.Type == "integer" && !p.Required));

            // TODO: this parameter is deprecated in the schema - we should not make it available to callers
            Assert.Single(requestContent.Properties.Where(p => p.Name == "line" && p.Type == "integer"));

            // TODO: we need to surface which parameters are required here, and
            //       repurposing the response parser means we don't have that
            //       type immediately available

            Assert.Single(post.Responses);
            var response = post.Responses.First();

            Assert.Equal("201", response.StatusCode);
            Assert.Equal("application/json", response.ContentType);
            Assert.Equal("object", response.Content.Type);

            var responseContent = Assert.IsType<ObjectContent>(response.Content);

            Assert.Single(responseContent.Properties.Where(p => p.Name == "html_url" && p.Type == "string"));

            var nestedObject = Assert.Single(responseContent.Properties.Where(p => p.Name == "user" && p.Type == "object").OfType<ObjectProperty>());

            Assert.Single(nestedObject.Properties.Where(p => p.Name == "login" && p.Type == "string"));
        }

        [Fact]
        public async Task Process_ForPathWithThreeVerb_ExtractsInformationForGet()
        {
            var path = await LoadPathWithGetPutAndDelete();

            var result = PathProcessor.Process(path);

            Assert.Equal("/user/following/{username}", result.Path);
            Assert.Equal(3, result.Verbs.Count);

            var get = result.Verbs.First(v => v.Method == HttpMethod.Get);

            Assert.Single(get.Parameters);
            Assert.Single(get.Parameters.Where(p => p.Name == "username" && p.Type == "string" && p.In == "path" && p.Required));

            Assert.Equal(2, get.Responses.Count);
        }

        [Fact]
        public async Task Process_ForPathWithThreeVerb_ExtractsInformationForPut()
        {
            var path = await LoadPathWithGetPutAndDelete();

            var result = PathProcessor.Process(path);

            var put = result.Verbs.First(v => v.Method == HttpMethod.Put);

            Assert.Equal("application/vnd.github.v3+json", put.AcceptHeader);

            Assert.Single(put.Parameters);
            Assert.Single(put.Parameters.Where(p => p.Name == "username" && p.Type == "string" && p.In == "path" && p.Required));

            Assert.Single(put.Responses);
        }

        [Fact]
        public async Task Process_ForPathWithThreeVerb_ExtractsInformationForDelete()
        {
            var path = await LoadPathWithGetPutAndDelete();

            var result = PathProcessor.Process(path);

            var delete = result.Verbs.First(v => v.Method == HttpMethod.Delete);

            Assert.Single(delete.Parameters);
            Assert.Single(delete.Parameters.Where(p => p.Name == "username" && p.Type == "string" && p.In == "path" && p.Required));

            Assert.Single(delete.Responses);
        }

        [Fact]
        public async Task Process_ForUserReposPath_IncludesEnumValues()
        {
            var path = await LoadUserReposEndpoint();

            var result = PathProcessor.Process(path);

            var get = Assert.Single(result.Verbs.Where(v => v.Method == HttpMethod.Get));

            // TODO: support parsing enum parameter details from source

            var visibility = Assert.Single(get.Parameters.Where(p => p.Name == "visibility" && p.Type == "string" && p.In == "query"));

            // Assert.Contains("all", visibility.Values);
            // Assert.Contains("public", visibility.Values);
            // Assert.Contains("private", visibility.Values);

            // Assert.Equal("all", visibility.Default);

            var type = Assert.Single(get.Parameters.Where(p => p.Name == "type" && p.Type == "string" && p.In == "query"));

            // Assert.Contains("all", type.Values);
            // Assert.Contains("owner", type.Values);
            // Assert.Contains("public", type.Values);
            // Assert.Contains("private", type.Values);
            // Assert.Contains("member", type.Values);

            // Assert.Equal("all", type.Default);

            var sort = Assert.Single(get.Parameters.Where(p => p.Name == "sort" && p.Type == "string" && p.In == "query"));

            // Assert.Contains("created", type.Values);
            // Assert.Contains("updated", type.Values);
            // Assert.Contains("pushed", type.Values);
            // Assert.Contains("full_name", type.Values);

            // Assert.Equal("full_name", type.Default);

            var direction = Assert.Single(get.Parameters.Where(p => p.Name == "direction" && p.Type == "string" && p.In == "query"));

            // Assert.Contains("created", type.Values);
            // Assert.Contains("updated", type.Values);
            // Assert.Contains("pushed", type.Values);
            // Assert.Contains("full_name", type.Values);

            // Assert.Equal("full_name", type.Default);

            Assert.Single(get.Parameters.Where(p => p.Name == "per_page" && p.Type == "integer" && p.In == "query"));

            Assert.Single(get.Parameters.Where(p => p.Name == "page" && p.Type == "integer" && p.In == "query"));
        }

        // [Fact]
        // public async Task Process_ForUserReposPath_IncludesOptionalParametersOnPost()
        // {
        //     var path = await LoadUserReposEndpoint();

        //     var result = PathProcessor.Process(path);

        //     var post = Assert.Single(result.Verbs.Where(v => v.Method == HttpMethod.Post));

        //     Assert.Single(post.Parameters);
        //     Assert.Single(post.Parameters.Where(p => p.Name == "username" && p.Type == "string" && p.In == "path" && p.Required));

        //     Assert.Single(post.Responses);
        // }

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

        private static async Task<JsonProperty> LoadPathWithGetPutAndDelete()
        {
            var json = await LoadFixture("example-get-put-delete-route.json");
            var paths = json.RootElement.GetProperty("paths");
            var properties = paths.EnumerateObject();
            var firstPath = properties.ElementAt(0);
            return firstPath;
        }

        private static async Task<JsonProperty> LoadUserReposEndpoint()
        {
            var json = await LoadFixture("user-repos.json");
            var paths = json.RootElement.GetProperty("paths");
            var properties = paths.EnumerateObject();
            var firstPath = properties.ElementAt(0);
            return firstPath;
        }
    }
}
