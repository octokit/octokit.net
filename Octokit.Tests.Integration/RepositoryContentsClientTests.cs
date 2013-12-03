using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class RepositoryContentsClientTests
    {
        [IntegrationTest]
        public async Task GetContentForASpecificFolder()
        {
            var client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var fixture = client.Repository.Content;

            var result = await fixture.GetContents("octokit", "octokit.net", "Octokit");

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [IntegrationTest]
        public async Task GetContentForASpecificFolderUsesExtensionMethods()
        {
            var client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var fixture = client.Repository.Content;

            var result = await fixture.GetContents("octokit", "octokit.net", "Octokit");

            Assert.NotEmpty(result.Files());
            Assert.NotEmpty(result.Directories());
        }

        [IntegrationTest]
        public async Task GetContentForRootReturnsList()
        {
            var client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var fixture = client.Repository.Content;

            var result = await fixture.GetContents("octokit", "octokit.net");

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [IntegrationTest]
        public async Task GetContentForReadmeReturnsAnItem()
        {
            var client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var fixture = client.Repository.Content;

            var result = await fixture.GetFile("octokit", "octokit.net", "README.md");

            Assert.NotNull(result);
            Assert.True(result.Size > 0);
        }
    }
}
