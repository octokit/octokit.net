using System;
using System.Collections.Generic;
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

        [PaidAccountTest(Skip="Paid plans now have unlimited repositories. We shouldn't test this now.")]
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
        public async Task UpdatesNameWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository(repoName) { AutoInit = true });
            var updatedName = Helper.MakeNameWithTimestamp("updated-repo");
            var update = new RepositoryUpdate { Name = updatedName };

            _repository = await github.Repository.Edit(_repository.Id, update);

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
        public async Task UpdatesDescriptionWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository(repoName) { AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, Description = "Updated description" };

            _repository = await github.Repository.Edit(_repository.Id, update);

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

        [IntegrationTest]
        public async Task UpdatesHomepageWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository(repoName) { AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, Homepage = "http://aUrl.to/nowhere" };

            _repository = await github.Repository.Edit(_repository.Id, update);

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

        [PaidAccountTest]
        public async Task UpdatesPrivateWithRepositoryId()
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

            _repository = await github.Repository.Edit(_repository.Id, update);

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
        public async Task UpdatesHasDownloadsWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository(repoName) { AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, HasDownloads = false };

            _repository = await github.Repository.Edit(_repository.Id, update);

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
        public async Task UpdatesHasIssuesWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository(repoName) { AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, HasIssues = false };

            _repository = await github.Repository.Edit(_repository.Id, update);

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

        [IntegrationTest]
        public async Task UpdatesHasWikiWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = await github.Repository.Create(new NewRepository(repoName) { AutoInit = true });
            var update = new RepositoryUpdate { Name = repoName, HasWiki = false };

            _repository = await github.Repository.Edit(_repository.Id, update);

            Assert.Equal(false, _repository.HasWiki);
        }

        [IntegrationTest]
        public async Task UpdatesMergeMethod()
        {
            var github = Helper.GetAuthenticatedClient();

            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                var updateRepository = new RepositoryUpdate(context.RepositoryName)
                {
                    AllowMergeCommit = false,
                    AllowSquashMerge = false,
                    AllowRebaseMerge = true
                };

                var editedRepository = await github.Repository.Edit(context.RepositoryOwner, context.RepositoryName, updateRepository);
                Assert.False(editedRepository.AllowMergeCommit);
                Assert.False(editedRepository.AllowSquashMerge);
                Assert.True(editedRepository.AllowRebaseMerge);

                var repository = await github.Repository.Get(context.RepositoryOwner, context.RepositoryName);
                Assert.False(repository.AllowMergeCommit);
                Assert.False(repository.AllowSquashMerge);
                Assert.True(repository.AllowRebaseMerge);
            }
        }

        [IntegrationTest]
        public async Task UpdatesMergeMethodWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                var updateRepository = new RepositoryUpdate(context.RepositoryName)
                {
                    AllowMergeCommit = true,
                    AllowSquashMerge = true,
                    AllowRebaseMerge = false
                };

                var editedRepository = await github.Repository.Edit(context.RepositoryId, updateRepository);
                Assert.True(editedRepository.AllowMergeCommit);
                Assert.True(editedRepository.AllowSquashMerge);
                Assert.False(editedRepository.AllowRebaseMerge);

                var repository = await github.Repository.Get(context.RepositoryId);
                Assert.True(repository.AllowMergeCommit);
                Assert.True(repository.AllowSquashMerge);
                Assert.False(repository.AllowRebaseMerge);
            }
        }

        public void Dispose()
        {
            Helper.DeleteRepo(Helper.GetAuthenticatedClient().Connection, _repository);
        }
    }

    public class TheDeleteMethod
    {
        [IntegrationTest]
        public async Task DeletesRepository()
        {
            var github = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("repo-to-delete");

            await github.Repository.Create(new NewRepository(repoName));

            await github.Repository.Delete(Helper.UserName, repoName);
        }

        [IntegrationTest]
        public async Task DeletesRepositoryWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("repo-to-delete");

            var repository = await github.Repository.Create(new NewRepository(repoName));

            await github.Repository.Delete(repository.Id);
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
        public async Task ReturnsSpecifiedRepositoryWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var repository = await github.Repository.Get(3622414);

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
        public async Task ReturnsRenamedRepository()
        {
            var github = Helper.GetAuthenticatedClient();

            var repository = await github.Repository.Get("michael-wolfenden", "Polly");

            Assert.Equal("https://github.com/App-vNext/Polly.git", repository.CloneUrl);
            Assert.False(repository.Private);
            Assert.False(repository.Fork);
            //Assert.Equal(AccountType.User, repository.Owner.Type);

            repository = await github.Repository.Get("fsprojects", "FSharp.Atom");

            Assert.Equal("https://github.com/ionide/ionide-atom-fsharp.git", repository.CloneUrl);
            Assert.False(repository.Private);
            Assert.False(repository.Fork);

            repository = await github.Repository.Get("cabbage89", "Orchard.Weixin");

            Assert.Equal("https://github.com/cabbage89/Orchard.WeChat.git", repository.CloneUrl);
            Assert.False(repository.Private);
            Assert.False(repository.Fork);
        }

        [IntegrationTest]
        public async Task ReturnsOrganizationRepositoryWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var repository = await github.Repository.Get(7528679);

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

        [IntegrationTest]
        public async Task ReturnsForkedRepositoryWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var repository = await github.Repository.Get(4550038);

            Assert.Equal("https://github.com/Haacked/libgit2sharp.git", repository.CloneUrl);
            Assert.True(repository.Fork);
        }

        [IntegrationTest]
        public async Task ReturnsRepositoryMergeOptions()
        {
            var github = Helper.GetAuthenticatedClient();

            using (var context = await github.CreateRepositoryContext(Helper.MakeNameWithTimestamp("public-repo")))
            {
                var repository = await github.Repository.Get(context.RepositoryOwner, context.RepositoryName);

                Assert.NotNull(repository.AllowRebaseMerge);
                Assert.NotNull(repository.AllowSquashMerge);
                Assert.NotNull(repository.AllowMergeCommit);
            }
        }

        [IntegrationTest]
        public async Task ReturnsRepositoryMergeOptionsWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            using (var context = await github.CreateRepositoryContext(Helper.MakeNameWithTimestamp("public-repo")))
            {
                var repository = await github.Repository.Get(context.RepositoryId);

                Assert.NotNull(repository.AllowRebaseMerge);
                Assert.NotNull(repository.AllowSquashMerge);
                Assert.NotNull(repository.AllowMergeCommit);
            }
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

            var request = new PublicRepositoryRequest(32732250L);
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
        public async Task ReturnsRepositoriesForOrganization()
        {
            var github = Helper.GetAuthenticatedClient();
            
            var options = new ApiOptions
            {
                PageSize = 20,
                StartPage = 1,
                PageCount = 1
            };

            var repositories = await github.Repository.GetAllForOrg("github", options);

            Assert.Equal(20, repositories.Count);
        }

        [IntegrationTest]
        public async Task GetsPagesOfRepositories()
        {
            var github = Helper.GetAuthenticatedClient();

            var firstPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 1,
                PageCount = 1
            };

            var firstPage = await github.Repository.GetAllForOrg("github", firstPageOptions);

            var secondPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 2,
                PageCount = 1
            };

            var secondPage = await github.Repository.GetAllForOrg("github", secondPageOptions);

            Assert.Equal(5, firstPage.Count);
            Assert.Equal(5, secondPage.Count);

            Assert.NotEqual(firstPage[0].Name, secondPage[0].Name);
            Assert.NotEqual(firstPage[1].Name, secondPage[1].Name);
            Assert.NotEqual(firstPage[2].Name, secondPage[2].Name);
            Assert.NotEqual(firstPage[3].Name, secondPage[3].Name);
            Assert.NotEqual(firstPage[4].Name, secondPage[4].Name);
        }
    }

    public class TheGetAllForUserMethod
    {
        [IntegrationTest]
        public async Task ReturnsRepositoriesForOrganization()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 20,
                StartPage = 1,
                PageCount = 1
            };

            var repositories = await github.Repository.GetAllForUser("shiftkey", options);

            Assert.Equal(20, repositories.Count);
        }

        [IntegrationTest]
        public async Task GetsPagesOfRepositories()
        {
            var github = Helper.GetAuthenticatedClient();

            var firstPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 1,
                PageCount = 1
            };

            var firstPage = await github.Repository.GetAllForUser("shiftkey", firstPageOptions);

            var secondPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 2,
                PageCount = 1
            };

            var secondPage = await github.Repository.GetAllForUser("shiftkey", secondPageOptions);

            Assert.Equal(5, firstPage.Count);
            Assert.Equal(5, secondPage.Count);

            Assert.NotEqual(firstPage[0].Name, secondPage[0].Name);
            Assert.NotEqual(firstPage[1].Name, secondPage[1].Name);
            Assert.NotEqual(firstPage[2].Name, secondPage[2].Name);
            Assert.NotEqual(firstPage[3].Name, secondPage[3].Name);
            Assert.NotEqual(firstPage[4].Name, secondPage[4].Name);
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

        [IntegrationTest]
        public async Task GetsContributorsWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var contributors = await github.Repository.GetAllContributors(7528679);

            Assert.True(contributors.Any(c => c.Login == "pmacn"));
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfContributorsWithoutStart()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var contributors = await github.Repository.GetAllContributors("octokit", "octokit.net", options);

            Assert.Equal(5, contributors.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfContributorsWithoutStartWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var contributors = await github.Repository.GetAllContributors(7528679, options);

            Assert.Equal(5, contributors.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfContributorsWithStart()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var contributors = await github.Repository.GetAllContributors("octokit", "octokit.net", options);

            Assert.Equal(5, contributors.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfContributorsWithStartWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var contributors = await github.Repository.GetAllContributors(7528679, options);

            Assert.Equal(5, contributors.Count);
        }

        [IntegrationTest]
        public async Task GetsPagesOfContributors()
        {
            var github = Helper.GetAuthenticatedClient();

            var firstPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 1,
                PageCount = 1
            };

            var firstPage = await github.Repository.GetAllContributors("octokit", "octokit.net", firstPageOptions);

            var secondPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 2,
                PageCount = 1
            };

            var secondPage = await github.Repository.GetAllContributors("octokit", "octokit.net", secondPageOptions);

            Assert.Equal(5, firstPage.Count);
            Assert.Equal(5, secondPage.Count);

            Assert.NotEqual(firstPage[0].Login, secondPage[0].Login);
            Assert.NotEqual(firstPage[1].Login, secondPage[1].Login);
            Assert.NotEqual(firstPage[2].Login, secondPage[2].Login);
            Assert.NotEqual(firstPage[3].Login, secondPage[3].Login);
            Assert.NotEqual(firstPage[4].Login, secondPage[4].Login);
        }

        [IntegrationTest]
        public async Task GetsPagesOfContributorsWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var firstPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 1,
                PageCount = 1
            };

            var firstPage = await github.Repository.GetAllContributors(7528679, firstPageOptions);

            var secondPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 2,
                PageCount = 1
            };

            var secondPage = await github.Repository.GetAllContributors(7528679, secondPageOptions);

            Assert.Equal(5, firstPage.Count);
            Assert.Equal(5, secondPage.Count);

            Assert.NotEqual(firstPage[0].Login, secondPage[0].Login);
            Assert.NotEqual(firstPage[1].Login, secondPage[1].Login);
            Assert.NotEqual(firstPage[2].Login, secondPage[2].Login);
            Assert.NotEqual(firstPage[3].Login, secondPage[3].Login);
            Assert.NotEqual(firstPage[4].Login, secondPage[4].Login);
        }

        [IntegrationTest]
        public async Task GetsContributorsIncludeAnonymous()
        {
            var github = Helper.GetAuthenticatedClient();

            var contributors = await github.Repository.GetAllContributors("ruby", "ruby", true);

            Assert.True(contributors.Any(c => c.Type == "Anonymous"));
        }

        [IntegrationTest]
        public async Task GetsContributorsIncludeAnonymousWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var contributors = await github.Repository.GetAllContributors(538746, true);

            Assert.True(contributors.Any(c => c.Type == "Anonymous"));
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfContributorsWithoutStartIncludeAnonymous()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var contributors = await github.Repository.GetAllContributors("ruby", "ruby", true, options);

            Assert.Equal(5, contributors.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfContributorsWithoutStartWithRepositoryIdIncludeAnonymous()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var contributors = await github.Repository.GetAllContributors(538746, true, options);

            Assert.Equal(5, contributors.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfContributorsWithStartIncludeAnonymous()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var contributors = await github.Repository.GetAllContributors("ruby", "ruby", true, options);

            Assert.Equal(5, contributors.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfContributorsWithStartWithRepositoryIdIncludeAnonymous()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var contributors = await github.Repository.GetAllContributors(538746, true, options);

            Assert.Equal(5, contributors.Count);
        }

        [IntegrationTest]
        public async Task GetsPagesOfContributorsIncludeAnonymous()
        {
            var github = Helper.GetAuthenticatedClient();

            var firstPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 1,
                PageCount = 1
            };

            var firstPage = await github.Repository.GetAllContributors("ruby", "ruby",  true, firstPageOptions);

            var secondPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 2,
                PageCount = 1
            };

            var secondPage = await github.Repository.GetAllContributors("ruby", "ruby", true, secondPageOptions);

            Assert.Equal(5, firstPage.Count);
            Assert.Equal(5, secondPage.Count);

            Assert.NotEqual(firstPage[0].Login, secondPage[0].Login);
            Assert.NotEqual(firstPage[1].Login, secondPage[1].Login);
            Assert.NotEqual(firstPage[2].Login, secondPage[2].Login);
            Assert.NotEqual(firstPage[3].Login, secondPage[3].Login);
            Assert.NotEqual(firstPage[4].Login, secondPage[4].Login);
        }

        [IntegrationTest]
        public async Task GetsPagesOfContributorsWithRepositoryIdIncludeAnonymous()
        {
            var github = Helper.GetAuthenticatedClient();

            var firstPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 1,
                PageCount = 1
            };

            var firstPage = await github.Repository.GetAllContributors(538746, true, firstPageOptions);

            var secondPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 2,
                PageCount = 1
            };

            var secondPage = await github.Repository.GetAllContributors(538746, true, secondPageOptions);

            Assert.Equal(5, firstPage.Count);
            Assert.Equal(5, secondPage.Count);

            Assert.NotEqual(firstPage[0].Login, secondPage[0].Login);
            Assert.NotEqual(firstPage[1].Login, secondPage[1].Login);
            Assert.NotEqual(firstPage[2].Login, secondPage[2].Login);
            Assert.NotEqual(firstPage[3].Login, secondPage[3].Login);
            Assert.NotEqual(firstPage[4].Login, secondPage[4].Login);
        }
    }

    public class TheGetAllForCurrentMethod
    {
        [IntegrationTest]
        public async Task CanRetrieveResults()
        {
            var gitHubClient = Helper.GetAuthenticatedClient();

            var repositories = await gitHubClient.Repository.GetAllForCurrent();

            Assert.NotEmpty(repositories);
        }

        [IntegrationTest]
        public async Task GetsPagesOfRepositories()
        {
            var gitHubClient = Helper.GetAuthenticatedClient();

            var firstPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 1,
                PageCount = 1
            };

            var firstPage = await gitHubClient.Repository.GetAllForCurrent(firstPageOptions);

            var secondPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 2,
                PageCount = 1
            };

            var secondPage = await gitHubClient.Repository.GetAllForCurrent(secondPageOptions);

            Assert.Equal(5, firstPage.Count);
            Assert.Equal(5, secondPage.Count);

            Assert.NotEqual(firstPage[0].Name, secondPage[0].Name);
            Assert.NotEqual(firstPage[1].Name, secondPage[1].Name);
            Assert.NotEqual(firstPage[2].Name, secondPage[2].Name);
            Assert.NotEqual(firstPage[3].Name, secondPage[3].Name);
            Assert.NotEqual(firstPage[4].Name, secondPage[4].Name);
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

        [IntegrationTest]
        public async Task GetsLanguagesWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var languages = await github.Repository.GetAllLanguages(7528679);

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

        [IntegrationTest]
        public async Task GetsTagsWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var tags = await github.Repository.GetAllTags(7528679);

            Assert.True(tags.Any(t => t.Name == "v0.1.0"));
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfTagsWithoutStart()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var tags = await github.Repository.GetAllTags("octokit", "octokit.net", options);

            Assert.Equal(5, tags.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfTagsWithoutStartWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var tags = await github.Repository.GetAllTags(7528679, options);

            Assert.Equal(5, tags.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfTagsWithStart()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var tags = await github.Repository.GetAllTags("octokit", "octokit.net", options);

            Assert.Equal(5, tags.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfTagsWithStartWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var tags = await github.Repository.GetAllTags(7528679, options);

            Assert.Equal(5, tags.Count);
        }

        [IntegrationTest]
        public async Task GetsPagesOfTags()
        {
            var github = Helper.GetAuthenticatedClient();

            var firstPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 1,
                PageCount = 1
            };

            var firstPage = await github.Repository.GetAllTags("octokit", "octokit.net", firstPageOptions);

            var secondPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 2,
                PageCount = 1
            };

            var secondPage = await github.Repository.GetAllTags("octokit", "octokit.net", secondPageOptions);

            Assert.Equal(5, firstPage.Count);
            Assert.Equal(5, secondPage.Count);

            Assert.NotEqual(firstPage[0].Name, secondPage[0].Name);
            Assert.NotEqual(firstPage[1].Name, secondPage[1].Name);
            Assert.NotEqual(firstPage[2].Name, secondPage[2].Name);
            Assert.NotEqual(firstPage[3].Name, secondPage[3].Name);
            Assert.NotEqual(firstPage[4].Name, secondPage[4].Name);
        }

        [IntegrationTest]
        public async Task GetsPagesOfTagsWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var firstPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 1,
                PageCount = 1
            };

            var firstPage = await github.Repository.GetAllTags(7528679, firstPageOptions);

            var secondPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 2,
                PageCount = 1
            };

            var secondPage = await github.Repository.GetAllTags(7528679, secondPageOptions);

            Assert.Equal(5, firstPage.Count);
            Assert.Equal(5, secondPage.Count);

            Assert.NotEqual(firstPage[0].Name, secondPage[0].Name);
            Assert.NotEqual(firstPage[1].Name, secondPage[1].Name);
            Assert.NotEqual(firstPage[2].Name, secondPage[2].Name);
            Assert.NotEqual(firstPage[3].Name, secondPage[3].Name);
            Assert.NotEqual(firstPage[4].Name, secondPage[4].Name);
        }
    }

    public class TheGetAllBranchesMethod
    {
        [IntegrationTest]
        public async Task GetsAllBranches()
        {
            var github = Helper.GetAuthenticatedClient();

            var branches = await github.Repository.GetAllBranches("octokit", "octokit.net");

            Assert.NotEmpty(branches);
        }

        [IntegrationTest]
        public async Task GetsAllBranchesWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var branches = await github.Repository.GetAllBranches(7528679);

            Assert.NotEmpty(branches);

            // Ensure Protection attribute is deserialized
            foreach (var branch in branches)
            {
                Assert.NotNull(branch.Protection);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfBranchesWithoutStart()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var branches = await github.Repository.GetAllBranches("octokit", "octokit.net", options);

            Assert.Equal(5, branches.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfBranchesWithoutStartWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var branches = await github.Repository.GetAllBranches(7528679, options);

            Assert.Equal(5, branches.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfBranchesWithStart()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var branches = await github.Repository.GetAllBranches("octokit", "octokit.net", options);

            Assert.Equal(5, branches.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfBranchesWithStartWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var branches = await github.Repository.GetAllBranches(7528679, options);

            Assert.Equal(5, branches.Count);
        }

        [IntegrationTest]
        public async Task GetsPagesOfBranches()
        {
            var github = Helper.GetAuthenticatedClient();

            var firstPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 1,
                PageCount = 1
            };

            var firstPage = await github.Repository.GetAllBranches("octokit", "octokit.net", firstPageOptions);

            var secondPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 2,
                PageCount = 1
            };

            var secondPage = await github.Repository.GetAllBranches("octokit", "octokit.net", secondPageOptions);

            Assert.Equal(5, firstPage.Count);
            Assert.Equal(5, secondPage.Count);

            Assert.NotEqual(firstPage[0].Name, secondPage[0].Name);
            Assert.NotEqual(firstPage[1].Name, secondPage[1].Name);
            Assert.NotEqual(firstPage[2].Name, secondPage[2].Name);
            Assert.NotEqual(firstPage[3].Name, secondPage[3].Name);
            Assert.NotEqual(firstPage[4].Name, secondPage[4].Name);
        }

        [IntegrationTest]
        public async Task GetsPagesOfBranchesWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var firstPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 1,
                PageCount = 1
            };

            var firstPage = await github.Repository.GetAllBranches(7528679, firstPageOptions);

            var secondPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 2,
                PageCount = 1
            };

            var secondPage = await github.Repository.GetAllBranches(7528679, secondPageOptions);

            Assert.Equal(5, firstPage.Count);
            Assert.Equal(5, secondPage.Count);

            Assert.NotEqual(firstPage[0].Name, secondPage[0].Name);
            Assert.NotEqual(firstPage[1].Name, secondPage[1].Name);
            Assert.NotEqual(firstPage[2].Name, secondPage[2].Name);
            Assert.NotEqual(firstPage[3].Name, secondPage[3].Name);
            Assert.NotEqual(firstPage[4].Name, secondPage[4].Name);
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

        [IntegrationTest]
        public async Task GetsABranchWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var branch = await github.Repository.GetBranch(7528679, "master");

            Assert.NotNull(branch);
            Assert.Equal("master", branch.Name);
        }
    }

    public class TheGetAllTeamsMethod
    {
        [IntegrationTest(Skip="Test requires administration rights to access this endpoint")]
        public async Task GetsAllTeams()
        {
            var github = Helper.GetAuthenticatedClient();

            var branches = await github.Repository.GetAllTeams("octokit", "octokit.net");

            Assert.NotEmpty(branches);
        }

        [IntegrationTest(Skip="Test requires administration rights to access this endpoint")]
        public async Task GetsAllTeamsWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var branches = await github.Repository.GetAllTeams(7528679);

            Assert.NotEmpty(branches);
        }

        [IntegrationTest(Skip = "Test requires administration rights to access this endpoint")]
        public async Task ReturnsCorrectCountOfTeamsWithoutStart()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var teams = await github.Repository.GetAllTeams("octokit", "octokit.net", options);

            Assert.Equal(5, teams.Count);
        }

        [IntegrationTest(Skip = "Test requires administration rights to access this endpoint")]
        public async Task ReturnsCorrectCountOfTeamsWithoutStartWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var teams = await github.Repository.GetAllTeams(7528679, options);

            Assert.Equal(5, teams.Count);
        }

        [IntegrationTest(Skip = "Test requires administration rights to access this endpoint")]
        public async Task ReturnsCorrectCountOfTeamsWithStart()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var teams = await github.Repository.GetAllTeams("octokit", "octokit.net", options);

            Assert.Equal(5, teams.Count);
        }

        [IntegrationTest(Skip = "Test requires administration rights to access this endpoint")]
        public async Task ReturnsCorrectCountOfTeamsWithStartWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var teams = await github.Repository.GetAllTeams(7528679, options);

            Assert.Equal(5, teams.Count);
        }

        [IntegrationTest(Skip = "Test requires administration rights to access this endpoint")]
        public async Task GetsPagesOfBranches()
        {
            var github = Helper.GetAuthenticatedClient();

            var firstPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 1,
                PageCount = 1
            };

            var firstPage = await github.Repository.GetAllTeams("octokit", "octokit.net", firstPageOptions);

            var secondPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 1,
                PageCount = 1
            };

            var secondPage = await github.Repository.GetAllTeams("octokit", "octokit.net", secondPageOptions);

            Assert.Equal(5, firstPage.Count);
            Assert.Equal(5, secondPage.Count);

            Assert.NotEqual(firstPage[0].Name, secondPage[0].Name);
            Assert.NotEqual(firstPage[1].Name, secondPage[1].Name);
            Assert.NotEqual(firstPage[2].Name, secondPage[2].Name);
            Assert.NotEqual(firstPage[3].Name, secondPage[3].Name);
            Assert.NotEqual(firstPage[4].Name, secondPage[4].Name);
        }

        [IntegrationTest(Skip = "Test requires administration rights to access this endpoint")]
        public async Task GetsPagesOfBranchesWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var firstPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 1,
                PageCount = 1
            };

            var firstPage = await github.Repository.GetAllTeams(7528679, firstPageOptions);

            var secondPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 1,
                PageCount = 1
            };

            var secondPage = await github.Repository.GetAllTeams(7528679, secondPageOptions);

            Assert.Equal(5, firstPage.Count);
            Assert.Equal(5, secondPage.Count);

            Assert.NotEqual(firstPage[0].Name, secondPage[0].Name);
            Assert.NotEqual(firstPage[1].Name, secondPage[1].Name);
            Assert.NotEqual(firstPage[2].Name, secondPage[2].Name);
            Assert.NotEqual(firstPage[3].Name, secondPage[3].Name);
            Assert.NotEqual(firstPage[4].Name, secondPage[4].Name);
        }
    }

    public class TheEditBranchMethod
    {
        private readonly IRepositoriesClient _fixture;
        private readonly RepositoryContext _context;

        public TheEditBranchMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _context = github.CreateRepositoryContext("source-repo").Result;
            _fixture = github.Repository;
        }

        public async Task CreateTheWorld()
        {
            // Set master branch to be protected, with some status checks
            var requiredStatusChecks = new RequiredStatusChecks(EnforcementLevel.Everyone, new List<string> { "check1", "check2" });

            var update = new BranchUpdate();
            update.Protection = new BranchProtection(true, requiredStatusChecks);

            var newBranch = await _fixture.EditBranch(_context.Repository.Owner.Login, _context.Repository.Name, "master", update);
        }

        [IntegrationTest]
        public async Task ProtectsBranch()
        {
            // Set master branch to be protected, with some status checks
            var requiredStatusChecks = new RequiredStatusChecks(EnforcementLevel.Everyone, new List<string> { "check1", "check2", "check3" });

            var update = new BranchUpdate();
            update.Protection = new BranchProtection(true, requiredStatusChecks);

            var branch = await _fixture.EditBranch(_context.Repository.Owner.Login, _context.Repository.Name, "master", update);

            // Ensure a branch object was returned
            Assert.NotNull(branch);

            // Retrieve master branch
            branch = await _fixture.GetBranch(_context.Repository.Owner.Login, _context.Repository.Name, "master");

            // Assert the changes were made
            Assert.Equal(branch.Protection.Enabled, true);
            Assert.Equal(branch.Protection.RequiredStatusChecks.EnforcementLevel, EnforcementLevel.Everyone);
            Assert.Equal(branch.Protection.RequiredStatusChecks.Contexts.Count, 3);
        }

        [IntegrationTest]
        public async Task RemoveStatusCheckEnforcement()
        {
            await CreateTheWorld();

            // Remove status check enforcement
            var requiredStatusChecks = new RequiredStatusChecks(EnforcementLevel.Off, new List<string> { "check1" });

            var update = new BranchUpdate();
            update.Protection = new BranchProtection(true, requiredStatusChecks);

            var branch = await _fixture.EditBranch(_context.Repository.Owner.Login, _context.Repository.Name, "master", update);

            // Ensure a branch object was returned
            Assert.NotNull(branch);

            // Retrieve master branch
            branch = await _fixture.GetBranch(_context.Repository.Owner.Login, _context.Repository.Name, "master");

            // Assert the changes were made
            Assert.Equal(branch.Protection.Enabled, true);
            Assert.Equal(branch.Protection.RequiredStatusChecks.EnforcementLevel, EnforcementLevel.Off);
            Assert.Equal(branch.Protection.RequiredStatusChecks.Contexts.Count, 1);
        }

        [IntegrationTest]
        public async Task UnprotectsBranch()
        {
            await CreateTheWorld();

            // Unprotect branch
            // Deliberately set Enforcement and Contexts to some values (these should be ignored)
            var requiredStatusChecks = new RequiredStatusChecks(EnforcementLevel.Everyone, new List<string> { "check1" });

            var update = new BranchUpdate();
            update.Protection = new BranchProtection(false, requiredStatusChecks);

            var branch = await _fixture.EditBranch(_context.Repository.Owner.Login, _context.Repository.Name, "master", update);

            // Ensure a branch object was returned
            Assert.NotNull(branch);

            // Retrieve master branch
            branch = await _fixture.GetBranch(_context.Repository.Owner.Login, _context.Repository.Name, "master");

            // Assert the branch is unprotected, and enforcement/contexts are cleared
            Assert.Equal(branch.Protection.Enabled, false);
            Assert.Equal(branch.Protection.RequiredStatusChecks.EnforcementLevel, EnforcementLevel.Off);
            Assert.Equal(branch.Protection.RequiredStatusChecks.Contexts.Count, 0);
        }
    }
}
