using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableGistClientTests
    {
        public class TheGetAllMethod
        {
            readonly ObservableGistsClient _gistsClient;          

            public TheGetAllMethod()
            {
                var github = Helper.GetAuthenticatedClient();

                _gistsClient = new ObservableGistsClient(github);
            }

            [IntegrationTest]
            public async Task ReturnsGists()
            {
                var gists = await _gistsClient.GetAll().ToList();

                Assert.NotEmpty(gists);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfGistsWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var gists = await _gistsClient.GetAll(options).ToList();

                Assert.Equal(5, gists.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfGistsWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 4,
                    PageCount = 1,
                    StartPage = 2
                };

                var gists = await _gistsClient.GetAll(options).ToList();

                Assert.Equal(4, gists.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctResultsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 4,
                    PageCount = 1
                };

                var firstGistsPage = await _gistsClient.GetAll(startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 4,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondGistsPage = await _gistsClient.GetAll(skipStartOptions).ToList();

                Assert.NotEqual(firstGistsPage[0].Id, secondGistsPage[0].Id);
                Assert.NotEqual(firstGistsPage[1].Id, secondGistsPage[1].Id);
                Assert.NotEqual(firstGistsPage[2].Id, secondGistsPage[2].Id);
                Assert.NotEqual(firstGistsPage[3].Id, secondGistsPage[3].Id);                
            }
        }
    }
}
