using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;
using Octokit.Tests.Helpers;

public class RepositoriesClientTests
{
    public class TheCreateMethodForUser : IDisposable
    {
        [IntegrationTest]
        public async Task CreatesANewPublicRepository()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            var createdRepository = await github.Repository.Create(new NewRepository { Name = repoName });
                
            try
            {
                var cloneUrl = string.Format("https://github.com/{0}/{1}.git", Helper.UserName, repoName);
                Assert.Equal(repoName, createdRepository.Name);
                Assert.False(createdRepository.Private);
                Assert.Equal(cloneUrl, createdRepository.CloneUrl);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.Equal(repoName, repository.Name);
                Assert.Null(repository.Description);
                Assert.False(repository.Private);
                Assert.True(repository.HasDownloads);
                Assert.True(repository.HasIssues);
                Assert.True(repository.HasWiki);
                Assert.Null(repository.Homepage);
            }
            finally
            {
                Helper.DeleteRepo(createdRepository);
            }
        }

        [IntegrationTest]
        public async Task CreatesANewPrivateRepository()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var repoName = Helper.MakeNameWithTimestamp("private-repo");

            var createdRepository = await github.Repository.Create(new NewRepository
            {
                Name = repoName,
                Private = true
            });

            try
            {
                Assert.True(createdRepository.Private);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.True(repository.Private);
            }
            finally
            {
                Helper.DeleteRepo(createdRepository);
            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryWithoutDownloads()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var repoName = Helper.MakeNameWithTimestamp("repo-without-downloads");

            var createdRepository = await github.Repository.Create(new NewRepository
            {
                Name = repoName,
                HasDownloads = false
            });

            try
            {
                Assert.False(createdRepository.HasDownloads);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.False(repository.HasDownloads);
            }
            finally
            {
                Helper.DeleteRepo(createdRepository);
            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryWithoutIssues()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var repoName = Helper.MakeNameWithTimestamp("repo-without-issues");

            var createdRepository = await github.Repository.Create(new NewRepository
            {
                Name = repoName,
                HasIssues = false
            });

            try
            {
                Assert.False(createdRepository.HasIssues);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.False(repository.HasIssues);
            }
            finally
            {
                Helper.DeleteRepo(createdRepository);
            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryWithoutAWiki()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var repoName = Helper.MakeNameWithTimestamp("repo-without-wiki");

            var createdRepository = await github.Repository.Create(new NewRepository
            {
                Name = repoName,
                HasWiki = false
            });

            try
            {
                Assert.False(createdRepository.HasWiki);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.False(repository.HasWiki);
            }
            finally
            {
                Helper.DeleteRepo(createdRepository);
            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryWithADescription()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var repoName = Helper.MakeNameWithTimestamp("repo-with-description");

            var createdRepository = await github.Repository.Create(new NewRepository
            {
                Name = repoName,
                Description = "theDescription"
            });

            try
            {
                Assert.Equal("theDescription", createdRepository.Description);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.Equal("theDescription", repository.Description);
            }
            finally
            {
                Helper.DeleteRepo(createdRepository);
            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryWithAHomepage()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var repoName = Helper.MakeNameWithTimestamp("repo-with-homepage");

            var createdRepository = await github.Repository.Create(new NewRepository
            {
                Name = repoName,
                Homepage = "http://aUrl.to/nowhere"
            });

            try
            {
                Assert.Equal("http://aUrl.to/nowhere", createdRepository.Homepage);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.Equal("http://aUrl.to/nowhere", repository.Homepage);
            }
            finally
            {
                Helper.DeleteRepo(createdRepository);
            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryWithAutoInit()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var repoName = Helper.MakeNameWithTimestamp("repo-with-autoinit");

            var createdRepository = await github.Repository.Create(new NewRepository
            {
                Name = repoName,
                AutoInit = true
            });

            try
            {
                // TODO: Once the contents API has been added, check the actual files in the created repo
                Assert.Equal(repoName, createdRepository.Name);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.Equal(repoName, repository.Name);
            }
            finally
            {
                Helper.DeleteRepo(createdRepository);
            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryWithAGitignoreTemplate()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var repoName = Helper.MakeNameWithTimestamp("repo-with-gitignore");

            var createdRepository = await github.Repository.Create(new NewRepository
            {
                Name = repoName,
                AutoInit = true,
                GitignoreTemplate = "VisualStudio"
            });

            try
            {
                // TODO: Once the contents API has been added, check the actual files in the created repo
                Assert.Equal(repoName, createdRepository.Name);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.Equal(repoName, repository.Name);
            }
            finally
            {
                Helper.DeleteRepo(createdRepository);
            }
        }

        [IntegrationTest]
        public async Task ThrowsRepositoryExistsExceptionForExistingRepository()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var repoName = Helper.MakeNameWithTimestamp("existing-repo");
            var repository = new NewRepository { Name = repoName };
            var createdRepository = await github.Repository.Create(repository);

            try
            {
                var thrown = await AssertEx.Throws<RepositoryExistsException>(
                    async () => await github.Repository.Create(repository));
                Assert.NotNull(thrown);
                Assert.Equal(repoName, thrown.RepositoryName);
                Assert.Equal(Helper.Credentials.Login, thrown.Owner);
                Assert.False(thrown.OwnerIsOrganization);
            }
            finally
            {
                Helper.DeleteRepo(createdRepository);
            }
        }

        [IntegrationTest]
        public async Task ThrowsPrivateRepositoryQuotaExceededExceptionWhenOverQuota()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };

            for (int i = 0; i < 5; i++)
            {
                var repoName = Helper.MakeNameWithTimestamp("private-repo" + i);
                var repository = new NewRepository { Name = repoName, Private = true };
                await github.Repository.Create(repository);
            }

            var thrown = await AssertEx.Throws<PrivateRepositoryQuotaExceededException>(
                async () => await github.Repository.Create(new NewRepository { Name = "x-private", Private = true }));
            Assert.NotNull(thrown);
        }

        // Clean up the repos.
        public void Dispose()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var repositories = github.Repository.GetAllForCurrent().Result;

            foreach (var repository in repositories)
            {
                try
                {
                    github.Repository.Delete(repository.Owner.Login, repository.Name).Wait();
                }
                catch (Exception) { }
            }
        }
    }

    public class TheCreateMethodForOrganization
    {
        [IntegrationTest]
        public async Task CreatesANewPublicRepository()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var repoName = Helper.MakeNameWithTimestamp("public-org-repo");
            var orgLogin = Helper.UserName + "-org";

            // TODO: Create the org as part of the test
            var createdRepository = await github.Repository.Create(orgLogin, new NewRepository { Name = repoName });

            try
            {
                var cloneUrl = string.Format("https://github.com/{0}/{1}.git", orgLogin, repoName);
                Assert.Equal(repoName, createdRepository.Name);
                Assert.False(createdRepository.Private);
                Assert.Equal(cloneUrl, createdRepository.CloneUrl);
                var repository = await github.Repository.Get(orgLogin, repoName);
                Assert.Equal(repoName, repository.Name);
                Assert.Null(repository.Description);
                Assert.False(repository.Private);
                Assert.True(repository.HasDownloads);
                Assert.True(repository.HasIssues);
                Assert.True(repository.HasWiki);
                Assert.Null(repository.Homepage);
            }
            finally
            {
                Helper.DeleteRepo(createdRepository);
            }
        }

        // TODO: Add a test for the team_id param once an overload that takes an oranization is added
    }

    private static IGitHubClient CreateGitHubClient()
    {
        return new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };
    }

    public class TheEditMethod : IDisposable
    {
        Repository _repository;

        [IntegrationTest]
        public async Task UpdatesName()
        {
            var github = CreateGitHubClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository { Name = repoName, AutoInit = true });
            var updatedName = Helper.MakeNameWithTimestamp("updated-repo");
            var update = new RepositoryUpdate { Name = updatedName };

            _repository = await github.Repository.Edit(Helper.UserName, repoName, update);

            Assert.Equal(update.Name, _repository.Name);
        }

        [IntegrationTest]
        public async Task UpdatesDescription()
        {
            var github = CreateGitHubClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository { Name = repoName, AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, Description = "Updated description" };

            _repository = await github.Repository.Edit(Helper.UserName, repoName, update);

            Assert.Equal("Updated description", _repository.Description);
        }

        [IntegrationTest]
        public async Task UpdatesHomepage()
        {
            var github = CreateGitHubClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository { Name = repoName, AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, Homepage = "http://aUrl.to/nowhere" };

            _repository = await github.Repository.Edit(Helper.UserName, repoName, update);

            Assert.Equal("http://aUrl.to/nowhere", _repository.Homepage);
        }

        [IntegrationTest]
        public async Task UpdatesPrivate()
        {
            var github = CreateGitHubClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository { Name = repoName, AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, Private = true };

            _repository = await github.Repository.Edit(Helper.UserName, repoName, update);

            Assert.Equal(true, _repository.Private);
        }

        [IntegrationTest]
        public async Task UpdatesHasDownloads()
        {
            var github = CreateGitHubClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository { Name = repoName, AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, HasDownloads = false };

            _repository = await github.Repository.Edit(Helper.UserName, repoName, update);

            Assert.Equal(false, _repository.HasDownloads);
        }

        [IntegrationTest]
        public async Task UpdatesHasIssues()
        {
            var github = CreateGitHubClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository { Name = repoName, AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, HasIssues = false };

            _repository = await github.Repository.Edit(Helper.UserName, repoName, update);

            Assert.Equal(false, _repository.HasIssues);
        }

        [IntegrationTest]
        public async Task UpdatesHasWiki()
        {
            var github = CreateGitHubClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository { Name = repoName, AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, HasWiki = false };

            _repository = await github.Repository.Edit(Helper.UserName, repoName, update);

            Assert.Equal(false, _repository.HasWiki);
        }

        public void Dispose()
        {
            Helper.DeleteRepo(_repository);
        }
    }

    public class TheDeleteMethod
    {
        [IntegrationTest]
        public async Task DeletesRepository()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var repoName = Helper.MakeNameWithTimestamp("repo-to-delete");
            await github.Repository.Create(new NewRepository { Name = repoName });

            Assert.DoesNotThrow(async () => { await github.Repository.Delete(Helper.UserName, repoName); });
        }
    }

    public class TheGetMethod
    {
        [IntegrationTest]
        public async Task ReturnsSpecifiedRepository()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };

            var repository = await github.Repository.Get("haacked", "seegit");

            Assert.Equal("https://github.com/Haacked/SeeGit.git", repository.CloneUrl);
            Assert.False(repository.Private);
            Assert.False(repository.Fork);
        }

        [IntegrationTest]
        public async Task ReturnsForkedRepository()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
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
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };

            var repositories = await github.Repository.GetAllForOrg("github");

            Assert.True(repositories.Count > 80);
        }
    }

    public class TheGetReadmeMethod
    {
        [IntegrationTest]
        public async Task ReturnsReadmeForSeeGit()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };

            // TODO: Change this to request github/Octokit.net once we make this OSS.
            var readme = await github.Repository.GetReadme("haacked", "seegit");
            Assert.Equal("README.md", readme.Name);
            string readMeHtml = await readme.GetHtmlContent();
            Assert.Contains(@"<div id=""readme""", readMeHtml);
            Assert.Contains("<p><strong>WARNING: This is some haacky code.", readMeHtml);
        }

        [IntegrationTest]
        public async Task ReturnsReadmeHtmlForSeeGit()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };

            // TODO: Change this to request github/Octokit.net once we make this OSS.
            var readmeHtml = await github.Repository.GetReadmeHtml("haacked", "seegit");
            Assert.True(readmeHtml.StartsWith("<div "));
            Assert.Contains("<p><strong>WARNING: This is some haacky code.", readmeHtml);
        }
    }

    public class TheGetAllContributorsMethod
    {
        [IntegrationTest]
        public async Task GetsContributors()
        {
            var github = CreateGitHubClient();

            var contributors = await github.Repository.GetAllContributors("octokit", "octokit.net");

            Assert.True(contributors.Any(c => c.Login == "pmacn"));
        }
    }

    public class TheGetAllLanguagesMethod
    {
        [IntegrationTest]
        public async Task GetsLanguages()
        {
            var github = CreateGitHubClient();

            var languages = await github.Repository.GetAllLanguages("octokit", "octokit.net");

            Assert.NotEmpty(languages);
            Assert.True(languages.Any(l => l.Name == "C#"));
        }
    }

    public class TheGetAllTagsMethod
    {
        [IntegrationTest]
        public async Task GetsTags()
        {
            var github = CreateGitHubClient();

            var tags = await github.Repository.GetAllTags("octokit", "octokit.net");

            Assert.True(tags.Any(t => t.Name == "v0.1.0"));
        }
    }

    public class TheGetBranchMethod
    {
        [IntegrationTest]
        public async Task GetsABranch()
        {
            var github = CreateGitHubClient();

            var branch = await github.Repository.GetBranch("octokit", "octokit.net", "master");

            Assert.NotNull(branch);
            Assert.Equal("master", branch.Name);
        }
    }
}
