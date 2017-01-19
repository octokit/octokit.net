using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class RepositoryPagesClientTests
{
    public class TheGetMethod
    {
        readonly IRepositoryPagesClient _repositoryPagesClient;
        const string owner = "octokit";
        const string name = "octokit.net";
        const long repositoryId = 7528679;

        public TheGetMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _repositoryPagesClient = github.Repository.Page;
        }

        [IntegrationTest(Skip = "These tests require repository admin rights - see https://github.com/octokit/octokit.net/issues/1263 for discussion")]
        public async Task ReturnsMetadata()
        {
            var data = await _repositoryPagesClient.Get(owner, name);
            Assert.Equal("https://api.github.com/repos/octokit/octokit.net/pages", data.Url);
        }

        [IntegrationTest(Skip = "These tests require repository admin rights - see https://github.com/octokit/octokit.net/issues/1263 for discussion")]
        public async Task ReturnsMetadataWithRepositoryId()
        {
            var data = await _repositoryPagesClient.Get(repositoryId);
            Assert.Equal("https://api.github.com/repos/octokit/octokit.net/pages", data.Url);
        }
    }
}