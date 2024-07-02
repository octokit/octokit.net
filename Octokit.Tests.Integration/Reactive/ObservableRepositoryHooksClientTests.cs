using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;
using Octokit.Tests.Integration.Fixtures;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableRepositoryHooksClientTests
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

                var client = new ObservableRepositoryHooksClient(github);

                var hooks = await client.GetAll(_fixture.RepositoryOwner, _fixture.RepositoryName).ToList();

                Assert.Equal(_fixture.ExpectedHooks.Count, hooks.Count);

                var actualHook = hooks[0];
                AssertHook(_fixture.ExpectedHook, actualHook);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfHooksWithoutStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var client = new ObservableRepositoryHooksClient(github);

                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var hooks = await client.GetAll(_fixture.RepositoryOwner, _fixture.RepositoryName, options).ToList();

                Assert.Equal(_fixture.ExpectedHooks.Count, hooks.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfHooksWithStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var client = new ObservableRepositoryHooksClient(github);

                var options = new ApiOptions
                {
                    PageSize = 3,
                    PageCount = 1,
                    StartPage = 2
                };

                var hooks = await client.GetAll(_fixture.RepositoryOwner, _fixture.RepositoryName, options).ToList();

                Assert.Single(hooks);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctResultsBasedOnStartPage()
            {
                var github = Helper.GetAuthenticatedClient();

                var client = new ObservableRepositoryHooksClient(github);

                var startOptions = new ApiOptions
                {
                    PageSize = 2,
                    PageCount = 1
                };

                var firstPage = await client.GetAll(_fixture.RepositoryOwner, _fixture.RepositoryName, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 2,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await client.GetAll(_fixture.RepositoryOwner, _fixture.RepositoryName, skipStartOptions).ToList();

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
                Assert.NotEqual(firstPage[1].Id, secondPage[1].Id);
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
}
