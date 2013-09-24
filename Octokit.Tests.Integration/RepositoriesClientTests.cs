using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class RepositoriesClientTests
    {
        public class TheGetAsyncMethod
        {
            [IntegrationTest]
            public async Task ReturnsSpecifiedUser()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };

                Repository repository = await github.Repository.Get("haacked", "seegit");

                Assert.Equal("https://github.com/Haacked/SeeGit.git", repository.CloneUrl);
            }
        }

        public class TheGetAllForOrgMethod
        {
            [IntegrationTest]
            public async Task ReturnsAllRepositoriesForOrganization()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };

                var repositories = await github.Repository.GetAllForOrg("github");

                Assert.True(repositories.Count > 80);
            }
        }

        public class TheGetReadmeMethod
        {
            [IntegrationTest]
            public async Task ReturnsReadmeForOctokit()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };

                // TODO: Change this to request github/Octokit.net once we make this OSS.
                var readme = await github.Repository.GetReadme("haacked", "seegit");
                Assert.Equal("README.md", readme.Name);
                string readMeHtml = await readme.GetHtmlContent();
                Assert.Contains(@"<div id=""readme""", readMeHtml);
                Assert.Contains("<p><strong>WARNING: This is some haacky code.", readMeHtml);
            }
        }
    }
}
