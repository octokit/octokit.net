using Octokit.Tests.Integration.fixtures;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class RepositoryHooksClientTests
    {
        [Collection(RepositoriesHooksCollection.Name)]
        public class TheGetAllMethod
        {
            readonly RepositoriesHooksFixture _fixture;

            public TheGetAllMethod(RepositoriesHooksFixture fixture)
            {
                _fixture = fixture;
            }

            [IntegrationTest]
            public async Task ReturnsAllHooksFromRepository()
            {
                var github = Helper.GetAuthenticatedClient();

                var hooks = await github.Repository.Hooks.GetAll(_fixture.RepositoryOwner, _fixture.RepositoryName);

                Assert.Single(hooks);
                var actualHook = hooks[0];

                AssertHook(_fixture.ExpectedHook, actualHook);
            }
        }

        [Collection(RepositoriesHooksCollection.Name)]
        public class TheGetMethod
        {
            readonly RepositoriesHooksFixture _fixture;

            public TheGetMethod(RepositoriesHooksFixture fixture)
            {
                _fixture = fixture;
            }

            [IntegrationTest]
            public async Task GetHookByCreatedId()
            {
                var github = Helper.GetAuthenticatedClient();

                var actualHook = await github.Repository.Hooks.Get(_fixture.RepositoryOwner, _fixture.RepositoryName, _fixture.ExpectedHook.Id);

                AssertHook(_fixture.ExpectedHook, actualHook);
            }
        }

        public class TheCreateMethod
        {
            [IntegrationTest]
            public async Task CreateAWebHookForTestRepository()
            {
                var github = Helper.GetAuthenticatedClient();

                var repoName = Helper.MakeNameWithTimestamp("create-hooks-test");
                var repository = await github.Repository.Create(new NewRepository(repoName) { AutoInit = true });

                var config = new Dictionary<string, string>
                {
                    { "content_type", "json" },
                    { "url", "http://test.com/example" },
                    { "hostname", "http://hostname.url" },
                    { "username", "username" },
                    { "password", "password" }
                };
                var parameters = new NewRepositoryHook("windowsazure", config)
                {
                    Events = new[] { "push" },
                    Active = false
                };

                var hook = await github.Repository.Hooks.Create(Helper.Credentials.Login, repository.Name, parameters);

                var baseHookUrl = CreateExpectedBaseHookUrl(repository.Url, hook.Id);
                Assert.Equal("windowsazure", hook.Name);
                Assert.Equal(new[] { "push" }.ToList(), hook.Events.ToList());
                Assert.Equal(baseHookUrl, hook.Url);
                Assert.Equal(baseHookUrl + "/test", hook.TestUrl);
                Assert.Equal(baseHookUrl + "/pings", hook.PingUrl);
                Assert.NotNull(hook.CreatedAt);
                Assert.NotNull(hook.UpdatedAt);
                Assert.Equal(config.Keys, hook.Config.Keys);
                Assert.Equal(config.Values, hook.Config.Values);
                Assert.Equal(false, hook.Active);
            }

            string CreateExpectedBaseHookUrl(string url, int id)
            {
                return url + "/hooks/" + id;
            }
        }

        [Collection(RepositoriesHooksCollection.Name)]
        public class TheEditMethod
        {
            readonly RepositoriesHooksFixture _fixture;

            public TheEditMethod(RepositoriesHooksFixture fixture)
            {
                _fixture = fixture;
            }

            [IntegrationTest]
            public async Task EditHookWithNoNewConfigRetainsTheOldConfig()
            {
                var github = Helper.GetAuthenticatedClient();

                var editRepositoryHook = new EditRepositoryHook
                {
                    AddEvents = new[] { "pull_request" }
                };

                var actualHook = await github.Repository.Hooks.Edit(_fixture.RepositoryOwner, _fixture.RepositoryName, _fixture.ExpectedHook.Id, editRepositoryHook);

                var expectedConfig = new Dictionary<string, string> { { "content_type", "json" }, { "url", "http://test.com/example" } };
                Assert.Equal(new[] { "commit_comment", "pull_request" }.ToList(), actualHook.Events.ToList());
                Assert.Equal(expectedConfig.Keys, actualHook.Config.Keys);
                Assert.Equal(expectedConfig.Values, actualHook.Config.Values);
            }

            [IntegrationTest]
            public async Task EditHookWithNewInformation()
            {
                var github = Helper.GetAuthenticatedClient();

                var editRepositoryHook = new EditRepositoryHook(new Dictionary<string, string> { { "project", "GEZDGORQFY2TCNZRGY2TSMBVGUYDK" } })
                {
                    AddEvents = new[] { "pull_request" }
                };

                var actualHook = await github.Repository.Hooks.Edit(_fixture.RepositoryOwner, _fixture.RepositoryName, _fixture.ExpectedHook.Id, editRepositoryHook);

                var expectedConfig = new Dictionary<string, string> { { "project", "GEZDGORQFY2TCNZRGY2TSMBVGUYDK" } };
                Assert.Equal(new[] { "commit_comment", "pull_request" }.ToList(), actualHook.Events.ToList());
                Assert.Equal(expectedConfig.Keys, actualHook.Config.Keys);
                Assert.Equal(expectedConfig.Values, actualHook.Config.Values);
            }
        }

        [Collection(RepositoriesHooksCollection.Name)]
        public class TheTestMethod
        {
            readonly RepositoriesHooksFixture _fixture;

            public TheTestMethod(RepositoriesHooksFixture fixture)
            {
                _fixture = fixture;
            }

            [IntegrationTest]
            public async Task TestACreatedHook()
            {
                var github = Helper.GetAuthenticatedClient();

                await github.Repository.Hooks.Test(_fixture.RepositoryOwner, _fixture.RepositoryName, _fixture.ExpectedHook.Id);
            }
        }

        [Collection(RepositoriesHooksCollection.Name)]
        public class ThePingMethod
        {
            readonly RepositoriesHooksFixture _fixture;

            public ThePingMethod(RepositoriesHooksFixture fixture)
            {
                _fixture = fixture;
            }

            [IntegrationTest]
            public async Task PingACreatedHook()
            {
                var github = Helper.GetAuthenticatedClient();

                await github.Repository.Hooks.Ping(_fixture.RepositoryOwner, _fixture.RepositoryName, _fixture.ExpectedHook.Id);
            }
        }

        [Collection(RepositoriesHooksCollection.Name)]
        public class TheDeleteMethod
        {
            readonly RepositoriesHooksFixture _fixture;

            public TheDeleteMethod(RepositoriesHooksFixture fixture)
            {
                _fixture = fixture;
            }

            [IntegrationTest]
            public async Task DeleteCreatedWebHook()
            {
                var github = Helper.GetAuthenticatedClient();

                await github.Repository.Hooks.Delete(_fixture.RepositoryOwner, _fixture.RepositoryName, _fixture.ExpectedHook.Id);
                var hooks = await github.Repository.Hooks.GetAll(_fixture.RepositoryOwner, _fixture.RepositoryName);

                Assert.Empty(hooks);
            }
        }

        static void AssertHook(RepositoryHook expectedHook, RepositoryHook actualHook)
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
