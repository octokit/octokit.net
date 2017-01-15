using System.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using System.Reactive.Linq;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableRepositoryCommitsClientTests
    {
        public class TheGetAllMethod
        {
            readonly ObservableRepositoryCommitsClient _repositoryCommitsClient;

            public TheGetAllMethod()
            {
                var client = Helper.GetAuthenticatedClient();
                _repositoryCommitsClient = new ObservableRepositoryCommitsClient(client);
            }

            [IntegrationTest]
            public async Task CanGetCorrectCountOfCommitsWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var commits = await _repositoryCommitsClient.GetAll("shiftkey", "ReactiveGit", options).ToList();
                Assert.Equal(5, commits.Count);
            }

            [IntegrationTest]
            public async Task CanGetCorrectCountOfCommitsWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var commits = await _repositoryCommitsClient.GetAll("shiftkey", "ReactiveGit", options).ToList();
                Assert.Equal(5, commits.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctResultsBasedOnStart()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var firstCommit = await _repositoryCommitsClient.GetAll("shiftkey", "ReactiveGit", startOptions).ToList();
                var secondCommit = await _repositoryCommitsClient.GetAll("shiftkey", "ReactiveGit", skipStartOptions).ToList();

                Assert.NotEqual(firstCommit[0].Sha, secondCommit[0].Sha);
                Assert.NotEqual(firstCommit[1].Sha, secondCommit[1].Sha);
                Assert.NotEqual(firstCommit[2].Sha, secondCommit[2].Sha);
                Assert.NotEqual(firstCommit[3].Sha, secondCommit[3].Sha);
                Assert.NotEqual(firstCommit[4].Sha, secondCommit[4].Sha);
            }
        }
    }
}
