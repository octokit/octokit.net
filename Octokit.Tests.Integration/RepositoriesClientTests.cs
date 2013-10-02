using System;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class RepositoriesClientTests
    {
        public class TheCreateMethod
        {
            [IntegrationTest]
            public async Task CreatesANewPublicRepository()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };
                var repoName = AutomationSettings.MakeNameWithTimestamp("public-repo");

                var createdRepository = await github.Repository.Create(new NewRepository { Name = repoName });

                var cloneUrl = string.Format("https://github.com/{0}/{1}.git", github.Credentials.Login, repoName);
                Assert.Equal(repoName, createdRepository.Name);
                Assert.Equal(cloneUrl, createdRepository.CloneUrl);
                Assert.False(createdRepository.Private);
                var repository = await github.Repository.Get(github.Credentials.Login, repoName);
                Assert.Equal(repoName, repository.Name);
                Assert.False(repository.Private);
            }
        }

        public class TheGetAsyncMethod
        {
            [IntegrationTest]
            public async Task ReturnsSpecifiedRepository()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };

                var repository = await github.Repository.Get("haacked", "seegit");

                Assert.Equal("https://github.com/Haacked/SeeGit.git", repository.CloneUrl);
                Assert.False(repository.Private);
                Assert.False(repository.Fork);
            }

            [IntegrationTest]
            public async Task ReturnsNeverPushedRepository()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };

                var repository = await github.Repository.Get("Test-Octowin", "PrivateTestRepository");

                Assert.Equal("https://github.com/Test-Octowin/PrivateTestRepository.git", repository.CloneUrl);
                Assert.True(repository.Private);
                Assert.False(repository.Fork);
                Assert.Equal(3709146, repository.Id);
                Assert.Null(repository.PushedAt);
            }

            [IntegrationTest]
            public async Task ReturnsForkedRepository()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };

                var repository = await github.Repository.Get("haacked", "libgit2sharp");

                Assert.Equal("https://github.com/Haacked/libgit2sharp.git", repository.CloneUrl);
                Assert.True(repository.Fork);
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
