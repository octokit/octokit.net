using System.Collections.Generic;
using Xunit;

namespace Octokit.Tests.Models
{
    public class NewOrganizationWebHookTests
    {
        public class TheCtor
        {
            [Fact]
            public void UsesDefaultValuesForDefaultConfig()
            {
                var create = new NewOrganizationWebHook("windowsazure", new Dictionary<string, string>(), "http://test.com/example");
                Assert.Equal("http://test.com/example", create.Url);
                Assert.Equal(OrgWebHookContentType.Form, create.ContentType);
                Assert.Empty(create.Secret);
                Assert.False(create.InsecureSsl);

                var request = create.ToRequest();
                Assert.Equal(4, request.Config.Count);

                Assert.True(request.Config.ContainsKey("url"));
                Assert.True(request.Config.ContainsKey("content_type"));
                Assert.True(request.Config.ContainsKey("secret"));
                Assert.True(request.Config.ContainsKey("insecure_ssl"));

                Assert.Equal("http://test.com/example", request.Config["url"]);
                Assert.Equal(request.Config["content_type"], OrgWebHookContentType.Form.ToParameter());
                Assert.Equal("", request.Config["secret"]);
                Assert.Equal("False", request.Config["insecure_ssl"]);
            }

            [Fact]
            public void CombinesUserSpecifiedContentTypeWithConfig()
            {
                var config = new Dictionary<string, string>
                {
                    {"hostname", "http://hostname.url"},
                    {"username", "username"},
                    {"password", "password"}
                };

                var create = new NewOrganizationWebHook("windowsazure", config, "http://test.com/example")
                {
                    ContentType = OrgWebHookContentType.Json,
                    Secret = string.Empty,
                    InsecureSsl = true
                };

                Assert.Equal("http://test.com/example", create.Url);
                Assert.Equal(OrgWebHookContentType.Json, create.ContentType);
                Assert.Empty(create.Secret);
                Assert.True(create.InsecureSsl);

                var request = create.ToRequest();

                Assert.Equal(7, request.Config.Count);

                Assert.True(request.Config.ContainsKey("url"));
                Assert.True(request.Config.ContainsKey("content_type"));
                Assert.True(request.Config.ContainsKey("secret"));
                Assert.True(request.Config.ContainsKey("insecure_ssl"));

                Assert.Equal("http://test.com/example", request.Config["url"]);
                Assert.Equal(request.Config["content_type"], OrgWebHookContentType.Json.ToParameter());
                Assert.Equal("", request.Config["secret"]);
                Assert.Equal("True", request.Config["insecure_ssl"]);

                Assert.True(request.Config.ContainsKey("hostname"));
                Assert.Equal(request.Config["hostname"], config["hostname"]);
                Assert.True(request.Config.ContainsKey("username"));
                Assert.Equal(request.Config["username"], config["username"]);
                Assert.True(request.Config.ContainsKey("password"));
                Assert.Equal(request.Config["password"], config["password"]);
            }

            [Fact]
            public void CanSetHookEvents()
            {
                var create = new NewOrganizationWebHook("web", new Dictionary<string, string>(), "http://test.com/example")
                {
                    Events = new List<string> { "*" }
                };

                var request = create.ToRequest();

                Assert.Contains("*", request.Events);
            }

            [Fact]
            public void EnsureCanCallToRequestMultipleTimes()
            {
                var create = new NewOrganizationWebHook("web", new Dictionary<string, string>(), "http://test.com/example")
                {
                    Events = new List<string> { "*" }
                };

                var request = create.ToRequest();
                var requestRepeated = create.ToRequest();

                Assert.Contains("*", request.Events);
                Assert.Contains("*", requestRepeated.Events);
            }

            [Fact]
            public void ShouldNotContainDuplicateConfigEntriesOnSubsequentRequests()
            {
                var create = new NewOrganizationWebHook("web", new Dictionary<string, string>(), "http://test.com/example");

                var request = create.ToRequest();
                var requestRepeated = create.ToRequest();

                Assert.Equal(4, request.Config.Count);
                Assert.Equal(4, requestRepeated.Config.Count);
            }

            [Fact]
            public void ShouldNotContainDuplicateConfigEntriesOnSubsequentRequestsWithCustomisedConfig()
            {
                var config = new Dictionary<string, string>
                {
                    {"url", "http://example.com/test"},
                    {"hostname", "http://hostname.url"},
                    {"username", "username"},
                    {"password", "password"}
                };

                var create = new NewOrganizationWebHook("web", config, "http://test.com/example");

                var request = create.ToRequest();
                var requestRepeated = create.ToRequest();

                //This is not 8, because `url` used in config, is already part of the base config
                Assert.Equal(7, request.Config.Count);
                Assert.Equal(7, requestRepeated.Config.Count);
            }

            [Fact]
            public void PropertiesShouldTakePrecedenceOverConfigPassedIn()
            {
                var config = new Dictionary<string, string>
                {
                    {"url", "http://originalurl.com/test"},
                };

                var create = new NewOrganizationWebHook("web", config, "http://test.com/example");

                var request = create.ToRequest();

                Assert.Equal("http://test.com/example", request.Config["url"]);

                var subsequentRequest = create.ToRequest();
                Assert.Equal("http://test.com/example", subsequentRequest.Config["url"]);
            }
        }
    }
}
