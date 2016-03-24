using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableReleaseClientTests
    {
        public class TheGetAllMethod
        {
            readonly ObservableReleasesClient _releaseClient;
            const string owner = "octokit";
            const string name = "octokit.net";

            public TheGetAllMethod()
            {
                var github = Helper.GetAuthenticatedClient();

                _releaseClient = new ObservableReleasesClient(github);
            }

            [IntegrationTest]
            public async Task ReturnsReleases()
            {
                var releases = await _releaseClient.GetAll(owner, name).ToList();

                Assert.NotEmpty(releases);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfReleasesWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var releases = await _releaseClient.GetAll(owner, name, options).ToList();

                Assert.Equal(5, releases.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfReleasesWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var releases = await _releaseClient.GetAll(owner, name, options).ToList();

                Assert.Equal(5, releases.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctResultsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var firstPage = await _releaseClient.GetAll(owner, name, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await _releaseClient.GetAll(owner, name, skipStartOptions).ToList();

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
                Assert.NotEqual(firstPage[1].Id, secondPage[1].Id);
                Assert.NotEqual(firstPage[2].Id, secondPage[2].Id);
                Assert.NotEqual(firstPage[3].Id, secondPage[3].Id);
                Assert.NotEqual(firstPage[4].Id, secondPage[4].Id);
            }
        }
    }
}
