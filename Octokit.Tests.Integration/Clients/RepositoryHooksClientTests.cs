using System.Linq;
using System.Threading.Tasks;
using Octokit.Tests.Integration.fixtures;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class RepositoryHooksClientTests
    {
        [Collection("Repositories Hooks Collection")]
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

        [Collection("Repositories Hooks Collection")]
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

                var parameters = new NewRepositoryHook("tenxer", new { content_type = "json", url = "http://test.com/example" })
                {
                    Events = new[] { "delete" },
                    Active = false
                };
                var hook = await github.Repository.Hooks.Create(Helper.Credentials.Login, repository.Name, parameters);

                var baseHookUrl = CreateExpectedBaseHookUrl(repository.Url, hook.Id);
                Assert.Equal("tenxer", hook.Name);
                Assert.Equal(new[] { "delete" }.ToList(), hook.Events.ToList());
                Assert.Equal(baseHookUrl, hook.Url);
                Assert.Equal(baseHookUrl + "/test", hook.TestUrl);
                Assert.Equal(baseHookUrl + "/pings", hook.PingUrl);
                Assert.NotNull(hook.CreatedAt);
                Assert.NotNull(hook.UpdatedAt);

                // TODO: KristianHald - It seems that even though I provide 'false' to active, the response states that it is active. Reported to github
                //Assert.Equal(false, hook.Active);
            }

            string CreateExpectedBaseHookUrl(string url, int id)
            {
                return url + "/hooks/" + id;
            }
        }

        [Collection("Repositories Hooks Collection")]
        public class TheEditMethod
        {
            readonly RepositoriesHooksFixture _fixture;

            public TheEditMethod(RepositoriesHooksFixture fixture)
            {
                _fixture = fixture;
            }

            [IntegrationTest]
            public async Task EditHookWithNewInformation()
            {
                var github = Helper.GetAuthenticatedClient();

                var editRepositoryHook = new EditRepositoryHook
                {
                    AddEvents = new[] { "pull_request" }
                };
                var actualHook = await github.Repository.Hooks.Edit(_fixture.RepositoryOwner, _fixture.RepositoryName, _fixture.ExpectedHook.Id, editRepositoryHook);

                Assert.Equal(new[] { "delete", "pull_request" }.ToList(), actualHook.Events.ToList());
            }
        }

        [Collection("Repositories Hooks Collection")]
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

        [Collection("Repositories Hooks Collection")]
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

        [Collection("Repositories Hooks Collection")]
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
