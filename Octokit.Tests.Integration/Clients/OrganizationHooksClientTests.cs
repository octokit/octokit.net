using Octokit.Tests.Integration.fixtures;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class OrganizationHooksClientTests
    {
        [Collection(OrganizationsHooksCollection.Name)]
        public class TheGetAllMethod
        {
            readonly OrganizationsHooksFixture _fixture;

            public TheGetAllMethod(OrganizationsHooksFixture fixture)
            {
                _fixture = fixture;
            }

            [IntegrationTest]
            public async Task ReturnsAllHooksFromOrganization()
            {
                var github = Helper.GetAuthenticatedClient();

                var hooks = await github.Organization.Hooks.GetAll( _fixture.OrganizationName);

                Assert.Single(hooks);
                var actualHook = hooks[0];

                AssertHook(_fixture.ExpectedHook, actualHook);
            }
        }

        [Collection(OrganizationsHooksCollection.Name)]
        public class TheGetMethod
        {
            readonly OrganizationsHooksFixture _fixture;

            public TheGetMethod(OrganizationsHooksFixture fixture)
            {
                _fixture = fixture;
            }

            [IntegrationTest]
            public async Task GetHookByCreatedId()
            {
                var github = Helper.GetAuthenticatedClient();

                var actualHook = await github.Organization.Hooks.Get(_fixture.OrganizationName, _fixture.ExpectedHook.Id);

                AssertHook(_fixture.ExpectedHook, actualHook);
            }
        }

        public class TheCreateMethod
        {
            readonly OrganizationsHooksFixture _fixture;

            public TheCreateMethod(OrganizationsHooksFixture fixture)
            {
                _fixture = fixture;
            }
            [IntegrationTest]
            public async Task CreateAWebHookForTestOrganization()
            {
                var github = Helper.GetAuthenticatedClient();
                var url = "http://test.com/example";
                var contentType = OrgWebHookContentType.Json;
                var secret = "53cr37";
                var config = new Dictionary<string, string>
                {
                    { "hostname", "http://hostname.url" },
                    { "username", "username" },
                    { "password", "password" }
                };
                var parameters = new NewOrganizationWebHook("windowsazure", config, url)
                {
                    Events = new[] { "push" },
                    Active = false,
                    ContentType = contentType,
                    Secret = secret
                };

                var hook = await github.Organization.Hooks.Create(_fixture.OrganizationName, parameters.ToRequest());

                var baseHookUrl = CreateExpectedBaseHookUrl(url, hook.Id);
                var webHookConfig = CreateExpectedConfigDictionary(config, url, contentType, secret);

                Assert.Equal("windowsazure", hook.Name);
                Assert.Equal(new[] { "push" }.ToList(), hook.Events.ToList());
                Assert.Equal(baseHookUrl, hook.Url);
                Assert.Equal(baseHookUrl + "/pings", hook.PingUrl);
                Assert.NotNull(hook.CreatedAt);
                Assert.NotNull(hook.UpdatedAt);
                Assert.Equal(webHookConfig.Keys, hook.Config.Keys);
                Assert.Equal(webHookConfig.Values, hook.Config.Values);
                Assert.Equal(false, hook.Active);
            }

            Dictionary<string, string> CreateExpectedConfigDictionary(Dictionary<string, string> config, string url, OrgWebHookContentType contentType, string secret)
            {
                return new Dictionary<string, string>
                {
                    { "url", url },
                    { "content_type", contentType.ToString().ToLowerInvariant() },
                    { "secret", secret },
                    { "insecure_ssl", "False" }
                }.Union(config).ToDictionary(k => k.Key, v => v.Value);
            }

            string CreateExpectedBaseHookUrl(string url, int id)
            {
                return url + "/hooks/" + id;
            }
        }

        [Collection(OrganizationsHooksCollection.Name)]
        public class TheEditMethod
        {
            readonly OrganizationsHooksFixture _fixture;

            public TheEditMethod(OrganizationsHooksFixture fixture)
            {
                _fixture = fixture;
            }

            [IntegrationTest]
            public async Task EditHookTest()
            {
                var github = Helper.GetAuthenticatedClient();

                var editOrganizationHook = new EditOrganizationHook
                {
                    Events = new[] { "pull_request" }
                };

                var actualHook = await github.Organization.Hooks.Edit( _fixture.OrganizationName, _fixture.ExpectedHook.Id, editOrganizationHook);

                var expectedConfig = new Dictionary<string, string> { { "content_type", "json" }, { "url", "http://test.com/example" } };
                Assert.Equal(new[] { "commit_comment", "pull_request" }.ToList(), actualHook.Events.ToList());
                Assert.Equal(expectedConfig.Keys, actualHook.Config.Keys);
                Assert.Equal(expectedConfig.Values, actualHook.Config.Values);
            }

            //[IntegrationTest]
            //public async Task EditHookWithNewInformation()
            //{
            //    var github = Helper.GetAuthenticatedClient();

            //    var editOrganizationHook = new EditOrganizationHook(new Dictionary<string, string> { { "project", "GEZDGORQFY2TCNZRGY2TSMBVGUYDK" } })
            //    {
            //        AddEvents = new[] { "pull_request" }
            //    };

            //    var actualHook = await github.Organization.Hooks.Edit(_fixture.OrganizationOwner, _fixture.OrganizationName, _fixture.ExpectedHook.Id, editOrganizationHook);

            //    var expectedConfig = new Dictionary<string, string> { { "project", "GEZDGORQFY2TCNZRGY2TSMBVGUYDK" } };
            //    Assert.Equal(new[] { "commit_comment", "pull_request" }.ToList(), actualHook.Events.ToList());
            //    Assert.Equal(expectedConfig.Keys, actualHook.Config.Keys);
            //    Assert.Equal(expectedConfig.Values, actualHook.Config.Values);
            //}
        }

        [Collection(OrganizationsHooksCollection.Name)]
        public class ThePingMethod
        {
            readonly OrganizationsHooksFixture _fixture;

            public ThePingMethod(OrganizationsHooksFixture fixture)
            {
                _fixture = fixture;
            }

            [IntegrationTest]
            public async Task PingACreatedHook()
            {
                var github = Helper.GetAuthenticatedClient();

                await github.Organization.Hooks.Ping( _fixture.OrganizationName, _fixture.ExpectedHook.Id);
            }
        }

        [Collection(OrganizationsHooksCollection.Name)]
        public class TheDeleteMethod
        {
            readonly OrganizationsHooksFixture _fixture;

            public TheDeleteMethod(OrganizationsHooksFixture fixture)
            {
                _fixture = fixture;
            }

            [IntegrationTest]
            public async Task DeleteCreatedWebHook()
            {
                var github = Helper.GetAuthenticatedClient();

                await github.Organization.Hooks.Delete(_fixture.OrganizationName, _fixture.ExpectedHook.Id);
                var hooks = await github.Organization.Hooks.GetAll( _fixture.OrganizationName);

                Assert.Empty(hooks);
            }
        }

        static void AssertHook(OrganizationHook expectedHook, OrganizationHook actualHook)
        {
            Assert.Equal(expectedHook.Id, actualHook.Id);
            Assert.Equal(expectedHook.Active, actualHook.Active);
            Assert.Equal(expectedHook.Config, actualHook.Config);
            Assert.Equal(expectedHook.CreatedAt, actualHook.CreatedAt);
            Assert.Equal(expectedHook.Name, actualHook.Name);
            Assert.Equal(expectedHook.PingUrl, actualHook.PingUrl);
            Assert.Equal(expectedHook.TestUrl, actualHook.TestUrl);
            Assert.Equal(expectedHook.UpdatedAt, actualHook.UpdatedAt);
            Assert.Equal(expectedHook.Url, actualHook.Url);
        }
    }
}
