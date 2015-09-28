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

                Assert.Equal(create.Config.Count, 4);

                Assert.True(create.Config.ContainsKey("url"));
                Assert.True(create.Config.ContainsKey("content_type"));
                Assert.True(create.Config.ContainsKey("secret"));
                Assert.True(create.Config.ContainsKey("insecure_ssl"));

                Assert.Equal(create.Config["url"], "http://test.com/example");
                Assert.Equal(create.Config["content_type"], WebHookContentType.Form.ToParameter());
                Assert.Equal(create.Config["secret"], "");
                Assert.Equal(create.Config["insecure_ssl"], "0");

                Assert.Equal(create.Url, "http://test.com/example");
                Assert.Equal(create.ContentType, WebHookContentType.Form);
                Assert.Empty(create.Secret);
                Assert.False(create.InsecureSsl);
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

                var create = new NewRepositoryWebHook("windowsazure", config, "http://test.com/example", WebHookContentType.Json, string.Empty, true);

                Assert.Equal(create.Config.Count, 7);

                Assert.True(create.Config.ContainsKey("url"));
                Assert.True(create.Config.ContainsKey("content_type"));
                Assert.True(create.Config.ContainsKey("secret"));
                Assert.True(create.Config.ContainsKey("insecure_ssl"));

                Assert.Equal(create.Config["url"], "http://test.com/example");
                Assert.Equal(create.Config["content_type"], WebHookContentType.Json.ToParameter());
                Assert.Equal(create.Config["secret"], "");
                Assert.Equal(create.Config["insecure_ssl"], "1");

                Assert.True(create.Config.ContainsKey("hostname"));
                Assert.Equal(create.Config["hostname"], config["hostname"]);
                Assert.True(create.Config.ContainsKey("username"));
                Assert.Equal(create.Config["username"], config["username"]);
                Assert.True(create.Config.ContainsKey("password"));
                Assert.Equal(create.Config["password"], config["password"]);

                Assert.Equal(create.Url, "http://test.com/example");
                Assert.Equal(create.ContentType, WebHookContentType.Json);
                Assert.Empty(create.Secret);
                Assert.True(create.InsecureSsl);
            }
        }
    }
}
