using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class UserKeysClientTests
    {
        [IntegrationTest(Skip = "see https://github.com/octokit/octokit.net/issues/533 for the resolution to this failing test")]
        public async Task GetAll()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };

            var keys = await github.User.Keys.GetAll();
            Assert.NotEmpty(keys);

            var first = keys[0];
            Assert.NotNull(first.Id);
            Assert.NotNull(first.Key);
            Assert.NotNull(first.Title);
            Assert.NotNull(first.Url);
        }

        [IntegrationTest]
        public async Task GetAllForGivenUser()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };

            var keys = await github.User.Keys.GetAll("shiftkey");
            Assert.NotEmpty(keys);

            var first = keys[0];
            Assert.NotNull(first.Id);
            Assert.NotNull(first.Key);
            Assert.Null(first.Title);
            Assert.Null(first.Url);
        }
    }
}
