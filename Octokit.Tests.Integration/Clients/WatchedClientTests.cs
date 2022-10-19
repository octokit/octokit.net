using System.Linq;
using System.Threading.Tasks;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class WatchedClientTests
    {
        public class TheGetAllForCurrentMethod
        {
            [IntegrationTest]
            public async Task CanRetrieveResults()
            {
                var github = Helper.GetAuthenticatedClient();

                var repositories = await github.Activity.Watching.GetAllForCurrent();

                Assert.NotEmpty(repositories);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfRepositoriesWithoutStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 3,
                    PageCount = 1
                };

                var repositories = await github.Activity.Watching.GetAllForCurrent(options);

                Assert.Equal(3, repositories.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfRepositoriesWithStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 3,
                    PageCount = 1,
                    StartPage = 2
                };

                var repositories = await github.Activity.Watching.GetAllForCurrent(options);

                Assert.Equal(3, repositories.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctPullRequestsBasedOnStartPage()
            {
                var github = Helper.GetAuthenticatedClient();

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var firstPage = await github.Activity.Watching.GetAllForCurrent(startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await github.Activity.Watching.GetAllForCurrent(skipStartOptions);

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            }
        }

        public class TheGetAllForUserMethod
        {
            [IntegrationTest]
            public async Task CanRetrieveResults()
            {
                var github = Helper.GetAuthenticatedClient();

                var repositories = await github.Activity.Watching.GetAllForUser(Helper.UserName);

                Assert.NotEmpty(repositories);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfRepositoriesWithoutStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 3,
                    PageCount = 1
                };

                var repositories = await github.Activity.Watching.GetAllForUser(Helper.UserName, options);

                Assert.Equal(3, repositories.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfRepositoriesWithStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 3,
                    PageCount = 1,
                    StartPage = 2
                };

                var repositories = await github.Activity.Watching.GetAllForUser(Helper.UserName, options);

                Assert.Equal(3, repositories.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctPullRequestsBasedOnStartPage()
            {
                var github = Helper.GetAuthenticatedClient();

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var firstPage = await github.Activity.Watching.GetAllForUser(Helper.UserName, startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await github.Activity.Watching.GetAllForUser(Helper.UserName, skipStartOptions);

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            }
        }

        public class TheGetAllWatchersMethod
        {
            [IntegrationTest]
            public async Task CanRetrieveResults()
            {
                var github = Helper.GetAuthenticatedClient();

                var repositories = await github.Activity.Watching.GetAllWatchers("octokit", "octokit.net");

                Assert.NotEmpty(repositories);
            }

            [IntegrationTest]
            public async Task CanRetrieveResultsWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var repositories = await github.Activity.Watching.GetAllWatchers(7528679);

                Assert.NotEmpty(repositories);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfRepositoriesWithoutStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 3,
                    PageCount = 1
                };

                var repositories = await github.Activity.Watching.GetAllWatchers("octokit", "octokit.net", options);

                Assert.Equal(3, repositories.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfRepositoriesWithoutStartWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 3,
                    PageCount = 1
                };

                var repositories = await github.Activity.Watching.GetAllWatchers(7528679, options);

                Assert.Equal(3, repositories.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfRepositoriesWithStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 3,
                    PageCount = 1,
                    StartPage = 2
                };

                var repositories = await github.Activity.Watching.GetAllWatchers("octokit", "octokit.net", options);

                Assert.Equal(3, repositories.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfRepositoriesWithStartWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 3,
                    PageCount = 1,
                    StartPage = 2
                };

                var repositories = await github.Activity.Watching.GetAllWatchers(7528679, options);

                Assert.Equal(3, repositories.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctPullRequestsBasedOnStartPage()
            {
                var github = Helper.GetAuthenticatedClient();

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var firstPage = await github.Activity.Watching.GetAllWatchers("octokit", "octokit.net", startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await github.Activity.Watching.GetAllWatchers("octokit", "octokit.net", skipStartOptions);

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctPullRequestsBasedOnStartPageWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var firstPage = await github.Activity.Watching.GetAllWatchers(7528679, startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await github.Activity.Watching.GetAllWatchers(7528679, skipStartOptions);

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            }
        }

        public class TheCheckWatchedMethod
        {
            readonly IWatchedClient _watchingClient;
            readonly RepositoryContext _context;

            public TheCheckWatchedMethod()
            {
                var github = Helper.GetAuthenticatedClient();

                _watchingClient = github.Activity.Watching;

                _context = github.CreateRepositoryContextWithAutoInit("public-repo").Result;
            }

            [IntegrationTest]
            public async Task CheckWatched()
            {
                var check = await _watchingClient.CheckWatched(_context.RepositoryOwner, _context.RepositoryName);

                Assert.True(check);
            }

            [IntegrationTest]
            public async Task CheckWatchedWithRepositoryId()
            {
                var check = await _watchingClient.CheckWatched(_context.Repository.Id);

                Assert.True(check);
            }
        }

        public class TheWatchRepoMethod
        {
            readonly IWatchedClient _watchingClient;

            public TheWatchRepoMethod()
            {
                var gitHubClient = Helper.GetAuthenticatedClient();

                _watchingClient = gitHubClient.Activity.Watching;
            }

            [IntegrationTest]
            public async Task WatchRepo()
            {
                var newSubscription = new NewSubscription
                {
                    Subscribed = true
                };

                await _watchingClient.UnwatchRepo("octocat", "hello-worId");

                var subscription = await _watchingClient.WatchRepo("octocat", "hello-worId", newSubscription);
                Assert.NotNull(subscription);

                var newWatchers = await _watchingClient.GetAllWatchers("octocat", "hello-worId");
                var @default = newWatchers.FirstOrDefault(user => user.Login == Helper.UserName);
                Assert.NotNull(@default);
            }

            [IntegrationTest]
            public async Task WatchRepoWithRepositoryId()
            {
                var newSubscription = new NewSubscription();

                await _watchingClient.UnwatchRepo(18221276);

                var subscription = await _watchingClient.WatchRepo(18221276, newSubscription);
                Assert.NotNull(subscription);

                var newWatchers = await _watchingClient.GetAllWatchers(18221276);
                var @default = newWatchers.FirstOrDefault(user => user.Login == Helper.UserName);
                Assert.NotNull(@default);
            }
        }

        public class TheUnwatchRepoMethod
        {
            readonly IWatchedClient _watchingClient;

            public TheUnwatchRepoMethod()
            {
                var gitHubClient = Helper.GetAuthenticatedClient();

                _watchingClient = gitHubClient.Activity.Watching;
            }

            [IntegrationTest]
            public async Task WatchRepo()
            {
                var newSubscription = new NewSubscription
                {
                    Subscribed = true
                };

                await _watchingClient.UnwatchRepo("octocat", "hello-worId");

                var subscription = await _watchingClient.WatchRepo("octocat", "hello-worId", newSubscription);
                Assert.NotNull(subscription);

                await _watchingClient.UnwatchRepo("octocat", "hello-worId");

                var newWatchers = await _watchingClient.GetAllWatchers("octocat", "hello-worId");
                var @default = newWatchers.FirstOrDefault(user => user.Login == Helper.UserName);
                Assert.Null(@default);
            }

            [IntegrationTest]
            public async Task WatchRepoWithRepositoryId()
            {
                var newSubscription = new NewSubscription();

                await _watchingClient.UnwatchRepo(18221276);

                var subscription = await _watchingClient.WatchRepo(18221276, newSubscription);
                Assert.NotNull(subscription);

                await _watchingClient.UnwatchRepo(18221276);

                var newWatchers = await _watchingClient.GetAllWatchers(18221276);
                var @default = newWatchers.FirstOrDefault(user => user.Login == Helper.UserName);
                Assert.Null(@default);
            }
        }
    }
}
