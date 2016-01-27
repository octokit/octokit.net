using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
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

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var createdRepository = context.Repository;

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

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName) { Private = true }))
            {
                var createdRepository = context.Repository;

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

            var newRepository = new NewRepository(repoName) { HasDownloads = false };

            using (var context = await github.CreateRepositoryContext(newRepository))
            {
                var createdRepository = context.Repository;

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

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName) { HasIssues = false }))
            {
                var createdRepository = context.Repository;

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

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName) { HasWiki = false }))
            {
                var createdRepository = context.Repository;

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

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName) { Description = "theDescription" }))
            {
                var createdRepository = context.Repository;

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

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName) { Homepage = "http://aUrl.to/nowhere" }))
            {
                var createdRepository = context.Repository;

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

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName) { AutoInit = true }))
            {
                var createdRepository = context.Repository;

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

            var newRepository = new NewRepository(repoName)
            {
                AutoInit = true,
                GitignoreTemplate = "VisualStudio"
            };

            using (var context = await github.CreateRepositoryContext(newRepository))
            {
                var createdRepository = context.Repository;

                // TODO: Once the contents API has been added, check the actual files in the created repo
                Assert.Equal(repoName, createdRepository.Name);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.Equal(repoName, repository.Name);
            }
        }


        [IntegrationTest]
        public async Task ThrowsInvalidGitIgnoreExceptionForInvalidTemplateNames()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("repo-with-gitignore");

            var newRepository = new NewRepository(repoName)
            {
                AutoInit = true,
                GitignoreTemplate = "visualstudio"
            };

            var thrown = await Assert.ThrowsAsync<InvalidGitIgnoreTemplateException>(
                    () => github.CreateRepositoryContext(newRepository));

            Assert.NotNull(thrown);
        }

        [IntegrationTest]
        public async Task ThrowsRepositoryExistsExceptionForExistingRepository()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("existing-repo");
            var repository = new NewRepository(repoName);

            using (var context = await github.CreateRepositoryContext(repository))
            {
                var createdRepository = context.Repository;

                var message = string.Format(CultureInfo.InvariantCulture, "There is already a repository named '{0}' for the current account.", repoName);

                var thrown = await Assert.ThrowsAsync<RepositoryExistsException>(
                    () => github.Repository.Create(repository));

                Assert.NotNull(thrown);
                Assert.Equal(repoName, thrown.RepositoryName);
                Assert.Equal(message, thrown.Message);
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
                    var repository = new NewRepository(repoName) { Private = true };
                    return github.Repository.Create(repository);
                });

            var createdRepositories = await Task.WhenAll(createRepoTasks);

            try
            {
                await Assert.ThrowsAsync<PrivateRepositoryQuotaExceededException>(
                    () => github.Repository.Create(new NewRepository("x-private") { Private = true }));
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

            using (var context = await github.CreateRepositoryContext(Helper.Organization, new NewRepository(repoName)))
            {
                var createdRepository = context.Repository;

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
        }

        [OrganizationTest]
        public async Task ThrowsRepositoryExistsExceptionForExistingRepository()
        {
            var github = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("existing-org-repo");

            var repository = new NewRepository(repoName);

            using (var context = await github.CreateRepositoryContext(Helper.Organization, repository))
            {
                var createdRepository = context.Repository;

                var repositoryUrl = string.Format(CultureInfo.InvariantCulture, "https://github.com/{0}/{1}", Helper.Organization, repository.Name);
                var message = string.Format(CultureInfo.InvariantCulture, "There is already a repository named '{0}' in the organization '{1}'.", repository.Name, Helper.Organization);

                var thrown = await Assert.ThrowsAsync<RepositoryExistsException>(
                    () => github.Repository.Create(Helper.Organization, repository));

                Assert.NotNull(thrown);
                Assert.Equal(repoName, thrown.RepositoryName);
                Assert.Equal(message, thrown.Message);
                Assert.True(thrown.OwnerIsOrganization);
                Assert.Equal(Helper.Organization, thrown.Organization);
                Assert.Equal(repositoryUrl, thrown.ExistingRepositoryWebUrl.ToString());
            }
        }

        // TODO: Add a test for the team_id param once an overload that takes an organization is added
    }

    public class TheEditMethod : IDisposable
    {
        Repository _repository;

        [IntegrationTest]
        public async Task UpdatesName()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository(repoName) { AutoInit = true });
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
            _repository = await github.Repository.Create(new NewRepository(repoName) { AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, Description = "Updated description" };

            _repository = await github.Repository.Edit(Helper.UserName, repoName, update);

            Assert.Equal("Updated description", _repository.Description);
        }

        [IntegrationTest]
        public async Task UpdatesHomepage()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository(repoName) { AutoInit = true });
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
            _repository = await github.Repository.Create(new NewRepository(repoName) { AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, Private = true };

            _repository = await github.Repository.Edit(Helper.UserName, repoName, update);

            Assert.Equal(true, _repository.Private);
        }

        [IntegrationTest]
        public async Task UpdatesHasDownloads()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository(repoName) { AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, HasDownloads = false };

            _repository = await github.Repository.Edit(Helper.UserName, repoName, update);

            Assert.Equal(false, _repository.HasDownloads);
        }

        [IntegrationTest]
        public async Task UpdatesHasIssues()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository(repoName) { AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, HasIssues = false };

            _repository = await github.Repository.Edit(Helper.UserName, repoName, update);

            Assert.Equal(false, _repository.HasIssues);
        }

        [IntegrationTest]
        public async Task UpdatesHasWiki()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository(repoName) { AutoInit = true });
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
        [IntegrationTest(Skip = "See https://github.com/octokit/octokit.net/issues/1002 for investigating this failing test")]
        public async Task DeletesRepository()
        {
            var github = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("repo-to-delete");
            await github.Repository.Create(new NewRepository(repoName));

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
            Assert.Equal(AccountType.User, repository.Owner.Type);
        }

        [IntegrationTest]
        public async Task ReturnsOrganizationRepository()
        {
            var github = Helper.GetAuthenticatedClient();

            var repository = await github.Repository.Get("octokit", "octokit.net");

            Assert.Equal("https://github.com/octokit/octokit.net.git", repository.CloneUrl);
            Assert.False(repository.Private);
            Assert.False(repository.Fork);
            Assert.Equal(AccountType.Organization, repository.Owner.Type);
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

    public class TheGetAllPublicMethod
    {
        [IntegrationTest(Skip = "Takes too long to run.")]
        public async Task ReturnsAllPublicRepositories()
        {
            var github = Helper.GetAuthenticatedClient();

            var repositories = await github.Repository.GetAllPublic();

            Assert.True(repositories.Count > 80);
        }

        [IntegrationTest(Skip = "Takes too long to run.")]
        public async Task ReturnsAllPublicRepositoriesSinceLastSeen()
        {
            var github = Helper.GetAuthenticatedClient();

            var request = new PublicRepositoryRequest(32732250);
            var repositories = await github.Repository.GetAllPublic(request);

            Assert.NotNull(repositories);
            Assert.True(repositories.Any());
            Assert.Equal(32732252, repositories[0].Id);
            Assert.False(repositories[0].Private);
            Assert.Equal("zad19", repositories[0].Name);
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

    public class TheGetAllForCurrentMethod
    {
        [IntegrationTest]
        public async Task CanRetrieveResults()
        {
            var github = Helper.GetAuthenticatedClient();

            var repositories = await github.Repository.GetAllForCurrent();

            Assert.NotEmpty(repositories);
        }

        [IntegrationTest]
        public async Task CanSortResults()
        {
            var github = Helper.GetAuthenticatedClient();

            var request = new RepositoryRequest
            {
                Sort = RepositorySort.Created
            };

            var reposByCreated = await github.Repository.GetAllForCurrent(request);

            request.Sort = RepositorySort.FullName;

            var reposByFullName = await github.Repository.GetAllForCurrent(request);

            Assert.NotEmpty(reposByCreated);
            Assert.NotEmpty(reposByFullName);
            Assert.NotEqual(reposByCreated, reposByFullName);
        }


        [IntegrationTest]
        public async Task CanChangeSortDirection()
        {
            var github = Helper.GetAuthenticatedClient();

            var request = new RepositoryRequest
            {
                Direction = SortDirection.Ascending
            };

            var reposAscending = await github.Repository.GetAllForCurrent(request);

            request.Direction = SortDirection.Ascending;

            var reposDescending = await github.Repository.GetAllForCurrent(request);

            Assert.NotEmpty(reposAscending);
            Assert.NotEmpty(reposDescending);
            Assert.NotEqual(reposAscending, reposDescending);
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
