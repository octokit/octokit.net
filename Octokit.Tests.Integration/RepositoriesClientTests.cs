using System;
using System.Runtime.CompilerServices;
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
                Assert.False(createdRepository.Private);
                Assert.Equal(cloneUrl, createdRepository.CloneUrl);
                var repository = await github.Repository.Get(github.Credentials.Login, repoName);
                Assert.Equal(repoName, repository.Name);
                Assert.Null(repository.Description);
                Assert.False(repository.Private);
                Assert.True(repository.HasDownloads);
                Assert.True(repository.HasIssues);
                Assert.True(repository.HasWiki);
                Assert.Null(repository.Homepage);
            }

            [IntegrationTest]
            public async Task CreatesANewPrivateRepository()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };
                var repoName = AutomationSettings.MakeNameWithTimestamp("private-repo");

                var createdRepository = await github.Repository.Create(new NewRepository
                {
                    Name = repoName,
                    Private = true
                });

                Assert.True(createdRepository.Private);
                var repository = await github.Repository.Get(github.Credentials.Login, repoName);
                Assert.True(repository.Private);
            }

            [IntegrationTest]
            public async Task CreatesARepositoryWithoutDownloads()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };
                var repoName = AutomationSettings.MakeNameWithTimestamp("repo-without-downloads");

                var createdRepository = await github.Repository.Create(new NewRepository
                {
                    Name = repoName,
                    HasDownloads = false
                });

                Assert.False(createdRepository.HasDownloads);
                var repository = await github.Repository.Get(github.Credentials.Login, repoName);
                Assert.False(repository.HasDownloads);
            }

            [IntegrationTest]
            public async Task CreatesARepositoryWithoutIssues()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };
                var repoName = AutomationSettings.MakeNameWithTimestamp("repo-without-issues");

                var createdRepository = await github.Repository.Create(new NewRepository
                {
                    Name = repoName,
                    HasIssues = false
                });

                Assert.False(createdRepository.HasIssues);
                var repository = await github.Repository.Get(github.Credentials.Login, repoName);
                Assert.False(repository.HasIssues);
            }

            [IntegrationTest]
            public async Task CreatesARepositoryWithoutAWiki()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };
                var repoName = AutomationSettings.MakeNameWithTimestamp("repo-without-wiki");

                var createdRepository = await github.Repository.Create(new NewRepository
                {
                    Name = repoName,
                    HasWiki = false
                });

                Assert.False(createdRepository.HasWiki);
                var repository = await github.Repository.Get(github.Credentials.Login, repoName);
                Assert.False(repository.HasWiki);
            }

            [IntegrationTest]
            public async Task CreatesARepositoryWithADescription()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };
                var repoName = AutomationSettings.MakeNameWithTimestamp("repo-with-description");

                var createdRepository = await github.Repository.Create(new NewRepository
                {
                    Name = repoName,
                    Description = "theDescription"
                });

                Assert.Equal("theDescription", createdRepository.Description);
                var repository = await github.Repository.Get(github.Credentials.Login, repoName);
                Assert.Equal("theDescription", repository.Description);
            }

            [IntegrationTest]
            public async Task CreatesARepositoryWithAHomepage()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };
                var repoName = AutomationSettings.MakeNameWithTimestamp("repo-with-description");

                var createdRepository = await github.Repository.Create(new NewRepository
                {
                    Name = repoName,
                    Homepage = "http://aUrl.to/nowhere"
                });

                Assert.Equal("http://aUrl.to/nowhere", createdRepository.Homepage);
                var repository = await github.Repository.Get(github.Credentials.Login, repoName);
                Assert.Equal("http://aUrl.to/nowhere", repository.Homepage);
            }

            [IntegrationTest]
            public async Task CreatesARepositoryWithAutoInit()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };
                var repoName = AutomationSettings.MakeNameWithTimestamp("repo-with-autoinit");

                var createdRepository = await github.Repository.Create(new NewRepository
                {
                    Name = repoName,
                    AutoInit = true
                });

                // TODO: Once the contents API has been added, check the actual files in the created repo
                Assert.Equal(repoName, createdRepository.Name);
                var repository = await github.Repository.Get(github.Credentials.Login, repoName);
                Assert.Equal(repoName, repository.Name);
            }

            [IntegrationTest]
            public async Task CreatesARepositoryWithAGitignoreTemplate()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };
                var repoName = AutomationSettings.MakeNameWithTimestamp("repo-with-gitignore");

                var createdRepository = await github.Repository.Create(new NewRepository
                {
                    Name = repoName,
                    AutoInit = true,
                    GitignoreTemplate = "visualstudio"
                });

                // TODO: Once the contents API has been added, check the actual files in the created repo
                Assert.Equal(repoName, createdRepository.Name);
                var repository = await github.Repository.Get(github.Credentials.Login, repoName);
                Assert.Equal(repoName, repository.Name);
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
