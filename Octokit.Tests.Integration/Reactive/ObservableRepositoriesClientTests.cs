using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class ObservableRepositoriesClientTests
    {
        public class TheGetMethod
        {
            [IntegrationTest]
            public async Task ReturnsSpecifiedRepository()
            {
                var github = Helper.GetAuthenticatedClient();

                var client = new ObservableRepositoriesClient(github);
                var observable = client.Get("haacked", "seegit");
                var repository = await observable;
                var repository2 = await observable;

                Assert.Equal("https://github.com/Haacked/SeeGit.git", repository.CloneUrl);
                Assert.False(repository.Private);
                Assert.False(repository.Fork);
                Assert.Equal("https://github.com/Haacked/SeeGit.git", repository2.CloneUrl);
                Assert.False(repository2.Private);
                Assert.False(repository2.Fork);
            }
        }

        public class TheGetAllPublicSinceMethod
        {
            [IntegrationTest]
            public async Task ReturnsAllPublicReposSinceLastSeen()
            {
                var github = Helper.GetAuthenticatedClient();

                var client = new ObservableRepositoriesClient(github);
                var request = new PublicRepositoryRequest
                {
                    Since = 32732250
                };
                var repositories = await client.GetAllPublic(request).ToArray();
                Assert.NotNull(repositories);
                Assert.True(repositories.Any());
                Assert.Equal(32732252, repositories[0].Id);
                Assert.False(repositories[0].Private);
                Assert.Equal("zad19", repositories[0].Name);
            }
        }
    }
}
