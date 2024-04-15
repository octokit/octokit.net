using Octokit.Tests.Integration.Fixtures;
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

                var hooks = await github.Organization.Hook.GetAll(_fixture.org);

                Assert.Equal(_fixture.ExpectedHooks.Count, hooks.Count);

                var actualHook = hooks[0];
                AssertHook(_fixture.ExpectedHook, actualHook);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfHooksWithoutStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var hooks = await github.Organization.Hook.GetAll(_fixture.org, options);

                Assert.Equal(_fixture.ExpectedHooks.Count, hooks.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfHooksWithStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 3,
                    PageCount = 1,
                    StartPage = 2
                };

                var hooks = await github.Organization.Hook.GetAll(_fixture.org, options);

                Assert.Single(hooks);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctResultsBasedOnStartPage()
            {
                var github = Helper.GetAuthenticatedClient();

                var startOptions = new ApiOptions
                {
                    PageSize = 2,
                    PageCount = 1
                };

                var firstPage = await github.Organization.Hook.GetAll(_fixture.org, startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 2,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await github.Organization.Hook.GetAll(_fixture.org, skipStartOptions);

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
                Assert.NotEqual(firstPage[1].Id, secondPage[1].Id);
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

                var actualHook = await github.Organization.Hook.Get(_fixture.org, _fixture.ExpectedHook.Id);

                AssertHook(_fixture.ExpectedHook, actualHook);
            }
        }

        [Collection(OrganizationsHooksCollection.Name)]
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
                //var secret = "53cr37";
                var config = new Dictionary<string, string>
                {
                    { "url", "http://hostname.url" },
                    { "content_type", "json" }
                };
                var parameters = new NewOrganizationHook("web", config)
                {
                    Events = new[] { "push" },
                    Active = false
                };

                var hook = await github.Organization.Hook.Create(_fixture.org, parameters.ToRequest());

                var baseHookUrl = CreateExpectedBaseHookUrl(_fixture.org, hook.Id);
                var webHookConfig = CreateExpectedConfigDictionary(config, url, contentType);

                Assert.Equal("web", hook.Name);
                Assert.Equal(new[] { "push" }.ToList(), hook.Events.ToList());
                Assert.Equal(baseHookUrl, hook.Url);
                Assert.Equal(baseHookUrl + "/pings", hook.PingUrl);
                Assert.NotEqual(default, hook.CreatedAt);
                Assert.NotEqual(default, hook.UpdatedAt);
                Assert.Equal(webHookConfig.Keys, hook.Config.Keys);
                Assert.False(hook.Active);
            }

            Dictionary<string, string> CreateExpectedConfigDictionary(Dictionary<string, string> config, string url, OrgWebHookContentType contentType)
            {
                return new Dictionary<string, string>
                {

                }.Union(config).ToDictionary(k => k.Key, v => v.Value);
            }

            string CreateExpectedBaseHookUrl(string org, int id)
            {
                return "https://api.github.com/orgs/" + org + "/hooks/" + id;
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

                var actualHook = await github.Organization.Hook.Edit(_fixture.org, _fixture.ExpectedHook.Id, editOrganizationHook);

                var expectedConfig = new Dictionary<string, string> { { "content_type", "json" }, { "url", "http://test.com/example" } };
                Assert.Equal(new[] { "commit_comment", "pull_request" }.ToList(), actualHook.Events.ToList());
                Assert.Equal(expectedConfig.Keys, actualHook.Config.Keys);
                Assert.Equal(expectedConfig.Values, actualHook.Config.Values);
            }
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

                await github.Organization.Hook.Ping(_fixture.org, _fixture.ExpectedHook.Id);
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

                await github.Organization.Hook.Delete(_fixture.org, _fixture.ExpectedHook.Id);
                var hooks = await github.Organization.Hook.GetAll(_fixture.org);

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
