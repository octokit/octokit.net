using System.Collections.Generic;
using Xunit;

namespace Octokit.Tests.Models
{
    public class NewRepositoryWebHookTests
    {
        public class TheCtor
        {
            [Fact]
            public void UsesDefaultValuesForDefaultConfig()
            {
                var create = new NewRepositoryWebHook("windowsazure", new Dictionary<string, string>(), "http://test.com/example");
                Assert.Equal(create.Url, "http://test.com/example");
                Assert.Equal(create.ContentType, WebHookContentType.Form);
                Assert.Empty(create.Secret);
                Assert.False(create.InsecureSsl);

                var request = create.ToRequest();
                Assert.Equal(request.Config.Count, 4);

                Assert.True(request.Config.ContainsKey("url"));
                Assert.True(request.Config.ContainsKey("content_type"));
                Assert.True(request.Config.ContainsKey("secret"));
                Assert.True(request.Config.ContainsKey("insecure_ssl"));

                Assert.Equal(request.Config["url"], "http://test.com/example");
                Assert.Equal(request.Config["content_type"], WebHookContentType.Form.ToParameter());
                Assert.Equal(request.Config["secret"], "");
                Assert.Equal(request.Config["insecure_ssl"], "False");
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

                var create = new NewRepositoryWebHook("windowsazure", config, "http://test.com/example")
                {
                    ContentType = WebHookContentType.Json,
                    Secret = string.Empty,
                    InsecureSsl = true
                };

                Assert.Equal(create.Url, "http://test.com/example");
                Assert.Equal(create.ContentType, WebHookContentType.Json);
                Assert.Empty(create.Secret);
                Assert.True(create.InsecureSsl);

                var request = create.ToRequest();

                Assert.Equal(request.Config.Count, 7);

                Assert.True(request.Config.ContainsKey("url"));
                Assert.True(request.Config.ContainsKey("content_type"));
                Assert.True(request.Config.ContainsKey("secret"));
                Assert.True(request.Config.ContainsKey("insecure_ssl"));

                Assert.Equal(request.Config["url"], "http://test.com/example");
                Assert.Equal(request.Config["content_type"], WebHookContentType.Json.ToParameter());
                Assert.Equal(request.Config["secret"], "");
                Assert.Equal(request.Config["insecure_ssl"], true.ToString());

                Assert.True(request.Config.ContainsKey("hostname"));
                Assert.Equal(request.Config["hostname"], config["hostname"]);
                Assert.True(request.Config.ContainsKey("username"));
                Assert.Equal(request.Config["username"], config["username"]);
                Assert.True(request.Config.ContainsKey("password"));
                Assert.Equal(request.Config["password"], config["password"]);
            }

            [Fact]
            public void CanSetHookAsActive()
            {
                var config = new Dictionary<string, string>
                {
                    {"hostname", "http://hostname.url"},
                    {"username", "username"},
                    {"password", "password"}
                };

                var create = new NewRepositoryWebHook("web", config, "http://test.com/example")
                {
                    Active = true
                };

                var request = create.ToRequest();

                Assert.True(request.Active);
            }

            [Fact]
            public void CanSetHookEvents()
            {
                var create = new NewRepositoryWebHook("web", new Dictionary<string, string>(), "http://test.com/example")
                {
                    Events = new List<string> { "*" }
                };

                var request = create.ToRequest();

                Assert.Contains("*", request.Events);
            }

            [Fact]
            public void EnsureCanCallToRequestMultipleTimes()
            {
                var create = new NewRepositoryWebHook("web", new Dictionary<string, string>(), "http://test.com/example")
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
                var create = new NewRepositoryWebHook("web", new Dictionary<string, string>(), "http://test.com/example");

                var request = create.ToRequest();
                var requestRepeated = create.ToRequest();

                Assert.Equal(request.Config.Count, 4);
                Assert.Equal(requestRepeated.Config.Count, 4);
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

                var create = new NewRepositoryWebHook("web", config, "http://test.com/example");

                var request = create.ToRequest();
                var requestRepeated = create.ToRequest();

                //This is not 8, because `url` used in config, is already part of the base config
                Assert.Equal(request.Config.Count, 7);
                Assert.Equal(requestRepeated.Config.Count, 7);
            }

            [Fact]
            public void PropertiesShouldTakePrecedenceOverConfigPassedIn()
            {
                var config = new Dictionary<string, string>
                {
                    {"url", "http://originalurl.com/test"},
                };

                var create = new NewRepositoryWebHook("web", config, "http://test.com/example");

                var request = create.ToRequest();

                Assert.Equal(request.Config["url"], "http://test.com/example");

                var subsequentRequest = create.ToRequest();
                Assert.Equal(subsequentRequest.Config["url"], "http://test.com/example");
            }
        }
    }
}
