using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;

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

                repository.CloneUrl.Should().Be("https://github.com/Haacked/SeeGit.git");
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

                IReadOnlyCollection<Repository> repositories = await github.Repository.GetAllForOrg("github");

                repositories.Count.Should().BeGreaterThan(80);
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
                Readme readme = await github.Repository.GetReadme("haacked", "seegit");
                readme.Name.Should().Be("README.md");
                string readMeHtml = await readme.GetHtmlContent();
                readMeHtml.Should().Contain(@"<div id=""readme""");
                readMeHtml.Should().Contain("<p><strong>WARNING: This is some haacky code.");
            }
        }
    }
}
