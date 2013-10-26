using System.Net.Http.Headers;
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
                var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
                {
                    Credentials = Helper.Credentials
                };
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
    }
}
