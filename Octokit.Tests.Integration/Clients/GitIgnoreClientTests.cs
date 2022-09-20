using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class GitIgnoreClientTests
    {
        [IntegrationTest]
        public async Task ReturnsListOfGitIgnoreTemplates()
        {
            var github = Helper.GetAuthenticatedClient();

            var result = await github.GitIgnore.GetAllGitIgnoreTemplates();

            Assert.True(result.Count > 2);
        }
    }
}