using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableRepositoryPagesClientTests
    {
        public class TheGetAllMethod
        {
            readonly ObservableRepositoryPagesClient _repositoryPagesClient;
            const string owner = "octokit";
            const string name = "octokit.net";

            public TheGetAllMethod()
            {
                var github = Helper.GetAuthenticatedClient();

                _repositoryPagesClient = new ObservableRepositoryPagesClient(github);
            }

            [IntegrationTest]
            public async Task ReturnsRepositoryPages()
            {
                var pages = await _repositoryPagesClient.GetAll(owner, name).ToList();

                Assert.NotEmpty(pages);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfPagesWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var pages = await _repositoryPagesClient.GetAll(owner, name, options).ToList();
                Assert.Equal(5, pages.Count);
            }

            [IntegrationTest]
            public async Task ReturnCorrectCountOfPagesWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var pages = await _repositoryPagesClient.GetAll(owner, name, options).ToList();
                Assert.Equal(5, pages.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctResultsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var firstPage = await _repositoryPagesClient.GetAll(owner, name, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await _repositoryPagesClient.GetAll(owner, name, skipStartOptions).ToList();

                Assert.NotEqual(firstPage[0].Url, secondPage[0].Url);
                Assert.NotEqual(firstPage[1].Url, secondPage[1].Url);
                Assert.NotEqual(firstPage[2].Url, secondPage[2].Url);
                Assert.NotEqual(firstPage[3].Url, secondPage[3].Url);
                Assert.NotEqual(firstPage[4].Url, secondPage[4].Url);
            }
        }
    }
}
