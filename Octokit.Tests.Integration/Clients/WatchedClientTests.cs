using System.Threading.Tasks;
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
        }
    }
}
