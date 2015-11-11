using System.Collections.Generic;
using Xunit;

namespace Octokit.Tests.Models
{
    public class NewRepositoryWebHookTests
    {
        public class TheCtor
        {
            string ExpectedRepositoryWebHookConfigExceptionMessage =
                "Duplicate webhook config values found - these values: Url should not be passed in as part of the config values. Use the properties on the NewRepositoryWebHook class instead.";

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
            public void ShouldThrowRepositoryWebHookConfigExceptionWhenDuplicateKeysExists()
            {
                var config = new Dictionary<string, string>
                {
                    {"url", "http://example.com/test"},
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

                var ex = Assert.Throws<RepositoryWebHookConfigException>(() => create.ToRequest());
                Assert.Equal(ExpectedRepositoryWebHookConfigExceptionMessage, ex.Message);
            }
        }
    }
}
