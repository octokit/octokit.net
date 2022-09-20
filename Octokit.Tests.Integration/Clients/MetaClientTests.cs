using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class MetaClientTests
    {
        public class TheGetMetadataMethod
        {
            [IntegrationTest]
            public async Task CanRetrieveMetadata()
            {
                var github = Helper.GetAnonymousClient();

                var result = await github.Meta.GetMetadata();

                Assert.True(result.VerifiablePasswordAuthentication);
                Assert.NotEmpty(result.GitHubServicesSha);
                Assert.True(result.Hooks.Count > 0);
                Assert.True(result.Git.Count > 0);
                Assert.True(result.Pages.Count > 0);
                Assert.True(result.Importer.Count > 0);
            }
        }
    }
}