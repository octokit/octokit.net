using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Helpers;
using Octokit.Tests.Integration;
using Xunit;

public class RepositoriesClientTests
{
    public class TheCreateMethodForUser
    {
        [IntegrationTest]
        public async Task CreatesANewPublicRepository()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            //var createdRepository = await github.Repository.Create(new NewRepository { Name = repoName });
            using (var createdRepository = github.CreateTestRepository(new NewRepository { Name = repoName }))
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
                Assert.NotNull(repository.DefaultBranch);
            }
        }

        [PaidAccountTest]
        public async Task CreatesANewPrivateRepository()
        {
            var github = Helper.GetAuthenticatedClient();

            var userDetails = await github.User.Current();
            if (userDetails.Plan.PrivateRepos == 0)
            {
                throw new Exception("Test cannot complete, account is on free plan");
            }

            var repoName = Helper.MakeNameWithTimestamp("private-repo");

                using (var createdRepository = github.CreateTestRepository(new NewRepository
                {
                    Name = repoName,
                    Private = true
                }))
                {

                    Assert.True(createdRepository.Private);
                    var repository = await github.Repository.Get(Helper.UserName, repoName);
                    Assert.True(repository.Private);
                }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryWithoutDownloads()
        {
            var github = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("repo-without-downloads");

            using (var createdRepository = github.CreateTestRepository(new NewRepository
            {
                Name = repoName,
                HasDownloads = false
            }))
            {

                Assert.False(createdRepository.HasDownloads);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.False(repository.HasDownloads);
            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryWithoutIssues()
        {
            var github = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("repo-without-issues");

            using (var createdRepository = github.CreateTestRepository(new NewRepository
            {
                Name = repoName,
                HasIssues = false
            }))
            {
                Assert.False(createdRepository.HasIssues);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.False(repository.HasIssues);
            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryWithoutAWiki()
        {
            var github = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("repo-without-wiki");

            using (var createdRepository = github.CreateTestRepository(new NewRepository
            {
                Name = repoName,
                HasWiki = false
            }))
            {
                Assert.False(createdRepository.HasWiki);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.False(repository.HasWiki);
            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryWithADescription()
        {
            var github = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("repo-with-description");

            using (var createdRepository = github.CreateTestRepository(new NewRepository
            {
                Name = repoName,
                Description = "theDescription"
            }))
            {
                Assert.Equal("theDescription", createdRepository.Description);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.Equal("theDescription", repository.Description);
            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryWithAHomepage()
        {
            var github = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("repo-with-homepage");

            using (var createdRepository = github.CreateTestRepository(new NewRepository
            {
                Name = repoName,
                Homepage = "http://aUrl.to/nowhere"
            })) { 
                Assert.Equal("http://aUrl.to/nowhere", createdRepository.Homepage);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.Equal("http://aUrl.to/nowhere", repository.Homepage);
            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryWithAutoInit()
        {
            var github = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("repo-with-autoinit");

            using (var createdRepository = github.CreateTestRepository(new NewRepository
            {
                Name = repoName,
                AutoInit = true
            }))
            {
                // TODO: Once the contents API has been added, check the actual files in the created repo
                Assert.Equal(repoName, createdRepository.Name);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.Equal(repoName, repository.Name);
            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryWithAGitignoreTemplate()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("repo-with-gitignore");

            using (var createdRepository = github.CreateTestRepository(new NewRepository
            {
                Name = repoName,
                AutoInit = true,
                GitignoreTemplate = "VisualStudio"
            }))
            {
                // TODO: Once the contents API has been added, check the actual files in the created repo
                Assert.Equal(repoName, createdRepository.Name);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.Equal(repoName, repository.Name);
            }
        }

        [IntegrationTest(Skip="this test is bollocks")]
        public async Task ThrowsRepositoryExistsExceptionForExistingRepository()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("existing-repo");
            var repository = new NewRepository { Name = repoName };
            using (var createdRepository = github.CreateTestRepository(repository))
            {
                var thrown = await AssertEx.Throws<RepositoryExistsException>(
                    async () => await github.Repository.Create(repository));
                Assert.NotNull(thrown);
                Assert.Equal(repoName, thrown.RepositoryName);
                Assert.Equal(Helper.Credentials.Login, thrown.Owner);
                Assert.False(thrown.OwnerIsOrganization);
            }
        }

        [PaidAccountTest]
        public async Task ThrowsPrivateRepositoryQuotaExceededExceptionWhenOverQuota()
        {
            var github = Helper.GetAuthenticatedClient();

            var userDetails = await github.User.Current();
            var freePrivateSlots = userDetails.Plan.PrivateRepos - userDetails.OwnedPrivateRepos;

            if (userDetails.Plan.PrivateRepos == 0)
            {
                throw new Exception("Test cannot complete, account is on free plan");
            }

            var createRepoTasks = 
                Enumerable.Range(0, (int)freePrivateSlots)
                .Select(x =>
                {
                    var repoName = Helper.MakeNameWithTimestamp("private-repo-" + x);
                    var repository = new NewRepository { Name = repoName, Private = true };
                    return github.Repository.Create(repository);
                });

            var createdRepositories = await Task.WhenAll(createRepoTasks);

            try
            {
                await Assert.ThrowsAsync<PrivateRepositoryQuotaExceededException>(
                    () => github.Repository.Create(new NewRepository { Name = "x-private", Private = true }));
            }
            finally
            {
                var deleteRepos = createdRepositories
                    .Select(repo => github.Repository.Delete(repo.Owner.Login, repo.Name));

                Task.WhenAll(deleteRepos).Wait();
            }
        }
    }

    public class TheCreateMethodForOrganization
    {
        [OrganizationTest]
        public async Task CreatesANewPublicRepository()
        {
            var github = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("public-org-repo");

            var createdRepository = await github.Repository.Create(Helper.Organization, new NewRepository { Name = repoName });

            try
            {
                var cloneUrl = string.Format("https://github.com/{0}/{1}.git", Helper.Organization, repoName);
                Assert.Equal(repoName, createdRepository.Name);
                Assert.False(createdRepository.Private);
                Assert.Equal(cloneUrl, createdRepository.CloneUrl);
                var repository = await github.Repository.Get(Helper.Organization, repoName);
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

    public class TheEditMethod : IDisposable
    {
        Repository _repository;

        [IntegrationTest]
        public async Task UpdatesName()
        {
            var github = Helper.GetAuthenticatedClient();
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
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository { Name = repoName, AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, Description = "Updated description" };

            _repository = await github.Repository.Edit(Helper.UserName, repoName, update);

            Assert.Equal("Updated description", _repository.Description);
        }

        [IntegrationTest]
        public async Task UpdatesHomepage()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository { Name = repoName, AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, Homepage = "http://aUrl.to/nowhere" };

            _repository = await github.Repository.Edit(Helper.UserName, repoName, update);

            Assert.Equal("http://aUrl.to/nowhere", _repository.Homepage);
        }

        [PaidAccountTest]
        public async Task UpdatesPrivate()
        {
            var github = Helper.GetAuthenticatedClient();

            var userDetails = await github.User.Current();
            if (userDetails.Plan.PrivateRepos == 0)
            {
                throw new Exception("Test cannot complete, account is on free plan");
            }

            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository { Name = repoName, AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, Private = true };

            _repository = await github.Repository.Edit(Helper.UserName, repoName, update);

            Assert.Equal(true, _repository.Private);
        }

        [IntegrationTest]
        public async Task UpdatesHasDownloads()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository { Name = repoName, AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, HasDownloads = false };

            _repository = await github.Repository.Edit(Helper.UserName, repoName, update);

            Assert.Equal(false, _repository.HasDownloads);
        }

        [IntegrationTest]
        public async Task UpdatesHasIssues()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository { Name = repoName, AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, HasIssues = false };

            _repository = await github.Repository.Edit(Helper.UserName, repoName, update);

            Assert.Equal(false, _repository.HasIssues);
        }

        [IntegrationTest]
        public async Task UpdatesHasWiki()
        {
            var github = Helper.GetAuthenticatedClient();
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
            var github = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("repo-to-delete");
            await github.Repository.Create(new NewRepository { Name = repoName });

            await github.Repository.Delete(Helper.UserName, repoName);
        }
    }

    public class TheGetMethod
    {
        [IntegrationTest]
        public async Task ReturnsSpecifiedRepository()
        {
            var github = Helper.GetAuthenticatedClient();

            var repository = await github.Repository.Get("haacked", "seegit");

            Assert.Equal("https://github.com/Haacked/SeeGit.git", repository.CloneUrl);
            Assert.False(repository.Private);
            Assert.False(repository.Fork);
        }

        [IntegrationTest]
        public async Task ReturnsForkedRepository()
        {
            var github = Helper.GetAuthenticatedClient();

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
            var github = Helper.GetAuthenticatedClient();

            var repositories = await github.Repository.GetAllForOrg("github");

            Assert.True(repositories.Count > 80);
        }
    }

    public class TheGetReadmeMethod
    {
        [IntegrationTest]
        public async Task ReturnsReadmeForSeeGit()
        {
            var github = Helper.GetAuthenticatedClient();

            var readme = await github.Repository.GetReadme("octokit", "octokit.net");
            Assert.Equal("README.md", readme.Name);
            string readMeHtml = await readme.GetHtmlContent();
            Assert.True(readMeHtml.StartsWith("<div class="));
            Assert.Contains(@"data-path=""README.md"" id=""file""", readMeHtml);
            Assert.Contains("Octokit - GitHub API Client Library for .NET", readMeHtml);
        }

        [IntegrationTest]
        public async Task ReturnsReadmeHtmlForSeeGit()
        {
            var github = Helper.GetAuthenticatedClient();

            var readmeHtml = await github.Repository.GetReadmeHtml("octokit", "octokit.net");
            Assert.True(readmeHtml.StartsWith("<div class="));
            Assert.Contains(@"data-path=""README.md"" id=""readme""", readmeHtml);
            Assert.Contains("Octokit - GitHub API Client Library for .NET", readmeHtml);
        }
    }

    public class TheGetAllContributorsMethod
    {
        [IntegrationTest]
        public async Task GetsContributors()
        {
            var github = Helper.GetAuthenticatedClient();

            var contributors = await github.Repository.GetAllContributors("octokit", "octokit.net");

            Assert.True(contributors.Any(c => c.Login == "pmacn"));
        }
    }

    public class TheGetAllLanguagesMethod
    {
        [IntegrationTest]
        public async Task GetsLanguages()
        {
            var github = Helper.GetAuthenticatedClient();

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
            var github = Helper.GetAuthenticatedClient();

            var tags = await github.Repository.GetAllTags("octokit", "octokit.net");

            Assert.True(tags.Any(t => t.Name == "v0.1.0"));
        }
    }

    public class TheGetBranchMethod
    {
        [IntegrationTest]
        public async Task GetsABranch()
        {
            var github = Helper.GetAuthenticatedClient();

            var branch = await github.Repository.GetBranch("octokit", "octokit.net", "master");

            Assert.NotNull(branch);
            Assert.Equal("master", branch.Name);
        }
    }
}
