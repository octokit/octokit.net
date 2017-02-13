using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableGistCommentsClientTests
    {
        public class TheGetAllForGistMethod
        {
            readonly ObservableGistCommentsClient _gistCommentsClient;
            const string gistId = "7783a2c14a15a2e3c93b";

            public TheGetAllForGistMethod()
            {
                var github = Helper.GetAuthenticatedClient();

                _gistCommentsClient = new ObservableGistCommentsClient(github);
            }

            [IntegrationTest]
            public async Task ReturnsGistComments()
            {
                var comments = await _gistCommentsClient.GetAllForGist(gistId).ToList();

                Assert.NotEmpty(comments);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfGistCommentsWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var comments = await _gistCommentsClient.GetAllForGist(gistId, options).ToList();

                Assert.Equal(5, comments.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfGistCommentsWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 4,
                    PageCount = 1,
                    StartPage = 2
                };

                var comments = await _gistCommentsClient.GetAllForGist(gistId, options).ToList();

                Assert.Equal(4, comments.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctResultsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 4,
                    PageCount = 1
                };

                var firstCommentsPage = await _gistCommentsClient.GetAllForGist(gistId, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 4,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondCommentsPage = await _gistCommentsClient.GetAllForGist(gistId, skipStartOptions).ToList();

                Assert.NotEqual(firstCommentsPage[0].Id, secondCommentsPage[0].Id);
                Assert.NotEqual(firstCommentsPage[1].Id, secondCommentsPage[1].Id);
                Assert.NotEqual(firstCommentsPage[2].Id, secondCommentsPage[2].Id);
                Assert.NotEqual(firstCommentsPage[3].Id, secondCommentsPage[3].Id);
            }
        }
    }
}
