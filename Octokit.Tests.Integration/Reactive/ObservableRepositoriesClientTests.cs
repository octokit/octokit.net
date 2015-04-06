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
            [IntegrationTest(Skip = "This will take a very long time to return, so will skip it for now.")]
            public async Task ReturnsAllPublicReposSinceLastSeen()
            {
                var github = Helper.GetAuthenticatedClient();

                var client = new ObservableRepositoriesClient(github);
                var request = new PublicRepositoryRequest(32732250);
                var repositories = await client.GetAllPublic(request).ToArray();
                Assert.NotEmpty(repositories);
                Assert.Equal(32732252, repositories[0].Id);
                Assert.False(repositories[0].Private);
                Assert.Equal("zad19", repositories[0].Name);
            }
        }
    }
}
