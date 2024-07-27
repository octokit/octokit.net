using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
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
                Assert.Null(repository.License);
                Assert.False(repository.AllowAutoMerge);
                Assert.True(repository.AllowMergeCommit);
                Assert.True(repository.AllowRebaseMerge);
                Assert.True(repository.AllowSquashMerge);
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
        public async Task CreatesARepositoryWithAllowAutoMergeSet()
        {
            var github = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("repo-auto-merge");

            var newRepository = new NewRepository(repoName) { AllowAutoMerge = true };

            using (var context = await github.CreateRepositoryContext(newRepository))
            {
                var createdRepository = context.Repository;

                // Default is false if unset, so check for true to ensure change
                Assert.True(createdRepository.AllowAutoMerge);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.True(repository.AllowAutoMerge);

            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryWithAllowMergeCommitSet()
        {
            var github = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("repo-merge-commit");

            var newRepository = new NewRepository(repoName) { AllowMergeCommit = false };

            using (var context = await github.CreateRepositoryContext(newRepository))
            {
                var createdRepository = context.Repository;

                // Default is true if unset, so check for false to ensure change
                Assert.False(createdRepository.AllowMergeCommit);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.False(repository.AllowMergeCommit);
            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryWithAllowRebaseMergeSet()
        {
            var github = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("repo-rebase-merge");

            var newRepository = new NewRepository(repoName) { AllowRebaseMerge = false };

            using (var context = await github.CreateRepositoryContext(newRepository))
            {
                var createdRepository = context.Repository;

                // Default is true if unset, so check for false to ensure change
                Assert.False(createdRepository.AllowRebaseMerge);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.False(repository.AllowRebaseMerge);

            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryWithAllowSquashMergeSet()
        {
            var github = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("repo-squash-merge");

            var newRepository = new NewRepository(repoName) { AllowSquashMerge = false };

            using (var context = await github.CreateRepositoryContext(newRepository))
            {
                var createdRepository = context.Repository;

                // Default is true if unset, so check for false to ensure change
                Assert.False(createdRepository.AllowSquashMerge);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.False(repository.AllowSquashMerge);

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
        public async Task CreatesARepositoryWithALicenseTemplate()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("repo-with-license");

            var newRepository = new NewRepository(repoName)
            {
                AutoInit = true,
                LicenseTemplate = "mit"
            };

            using (var context = await github.CreateRepositoryContext(newRepository))
            {
                var createdRepository = context.Repository;

                // NOTE: the License attribute is empty for newly created repositories
                Assert.Null(createdRepository.License);

                // license information is not immediatelly available after the repository is created
                await Task.Delay(TimeSpan.FromSeconds(1));

                // check for actual license by reloading repository info
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.NotNull(repository.License);
                Assert.Equal("mit", repository.License.Key);
            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryAsTemplate()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("repo-as-template");

            var newRepository = new NewRepository(repoName)
            {
                IsTemplate = true
            };

            using (var context = await github.CreateRepositoryContext(newRepository))
            {
                var createdRepository = context.Repository;

                var repository = await github.Repository.Get(Helper.UserName, repoName);

                Assert.True(repository.IsTemplate);
            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryFromTemplate()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoTemplateName = Helper.MakeNameWithTimestamp("repo-template");
            var repoFromTemplateName = Helper.MakeNameWithTimestamp("repo-from-template");
            var owner = github.User.Current().Result.Login;

            var newTemplate = new NewRepository(repoTemplateName)
            {
                IsTemplate = true
            };

            var newRepo = new NewRepositoryFromTemplate(repoFromTemplateName);

            using (var templateContext = await github.CreateRepositoryContext(newTemplate))
            using (var context = await github.Generate(owner, repoFromTemplateName, newRepo))
            {
                var repository = await github.Repository.Get(Helper.UserName, repoFromTemplateName);

                Assert.Equal(repoFromTemplateName, repository.Name);
            }
        }

        [IntegrationTest]
        public async Task CreatesARepositoryWithDeleteBranchOnMergeEnabled()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("repo-with-delete-branch-on-merge");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName) { DeleteBranchOnMerge = true }))
            {
                var createdRepository = context.Repository;

                Assert.True(createdRepository.DeleteBranchOnMerge);
                var repository = await github.Repository.Get(Helper.UserName, repoName);
                Assert.True(repository.DeleteBranchOnMerge);
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
    }

    public class TheCreateMethodForOrganization
    {
        [OrganizationTest]
        public async Task CreatesANewPublicRepository()
        {
            var github = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("public-org-repo");

            using (var context = await github.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(repoName)))
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

            using (var context = await github.CreateOrganizationRepositoryContext(Helper.Organization, repository))
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

    public class TheEditMethod : GitHubClientTestBase
    {
        [IntegrationTest]
        public async Task UpdatesNothing()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var update = new RepositoryUpdate();

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryOwner, repoContext.RepositoryName, update);

                Assert.Equal(repoContext.Repository.Name, updatedRepository.Name);
                Assert.Equal(repoContext.Repository.Description, updatedRepository.Description);
                Assert.Equal(repoContext.Repository.Homepage, updatedRepository.Homepage);
                Assert.Equal(repoContext.Repository.Private, updatedRepository.Private);
                Assert.Equal(repoContext.Repository.Visibility, updatedRepository.Visibility);
                Assert.Equal(repoContext.Repository.HasIssues, updatedRepository.HasIssues);
                //Assert.Equal(_repository.HasProjects, updatedRepository.HasProjects); - not on response!
                Assert.Equal(repoContext.Repository.HasWiki, updatedRepository.HasWiki);
                Assert.Equal(repoContext.Repository.HasDownloads, updatedRepository.HasDownloads);
                Assert.Equal(repoContext.Repository.IsTemplate, updatedRepository.IsTemplate);
                Assert.Equal(repoContext.Repository.DefaultBranch, updatedRepository.DefaultBranch);
                Assert.Equal(repoContext.Repository.AllowSquashMerge, updatedRepository.AllowSquashMerge);
                Assert.Equal(repoContext.Repository.AllowMergeCommit, updatedRepository.AllowMergeCommit);
                Assert.Equal(repoContext.Repository.AllowRebaseMerge, updatedRepository.AllowRebaseMerge);
                Assert.Equal(repoContext.Repository.AllowAutoMerge, updatedRepository.AllowAutoMerge);
                Assert.Equal(repoContext.Repository.DeleteBranchOnMerge, updatedRepository.DeleteBranchOnMerge);
                // Assert.Equal(_repository.UseSquashPrTitleAsDefault, updatedRepository.UseSquashPrTitleAsDefault); - not on response!
                Assert.Equal(repoContext.Repository.Archived, updatedRepository.Archived);
                //Assert.Equal(_repository.AllowForking, updatedRepository.AllowForking); - not on response!
            }
        }

        [IntegrationTest]
        public async Task UpdatesName()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var updatedName = Helper.MakeNameWithTimestamp("updated-repo");
                var update = new RepositoryUpdate() { Name = updatedName };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryOwner, repoContext.RepositoryName, update);

                Assert.Equal(update.Name, updatedRepository.Name);
            }
        }

        [IntegrationTest]
        public async Task UpdatesNameWithRepositoryId()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var updatedName = Helper.MakeNameWithTimestamp("updated-repo");
                var update = new RepositoryUpdate() { Name = updatedName };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryId, update);

                Assert.Equal(update.Name, updatedRepository.Name);
            }
        }

        [IntegrationTest]
        public async Task UpdatesDescription()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var update = new RepositoryUpdate() { Description = "Updated description" };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryOwner, repoContext.RepositoryName, update);

                Assert.Equal(update.Description, updatedRepository.Description);
            }
        }

        [IntegrationTest]
        public async Task UpdatesDescriptionWithRepositoryId()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var update = new RepositoryUpdate() { Description = "Updated description" };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryId, update);

                Assert.Equal(update.Description, updatedRepository.Description);
            }
        }

        [IntegrationTest]
        public async Task UpdatesHomepage()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var update = new RepositoryUpdate() { Homepage = "http://aUrl.to/nowhere" };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryOwner, repoContext.RepositoryName, update);

                Assert.Equal(update.Homepage, updatedRepository.Homepage);
            }
        }

        [IntegrationTest]
        public async Task UpdatesHomepageWithRepositoryId()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var update = new RepositoryUpdate() { Homepage = "http://aUrl.to/nowhere" };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryId, update);

                Assert.Equal(update.Homepage, updatedRepository.Homepage);
            }
        }

        [PaidAccountTest]
        public async Task UpdatesPrivate()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var update = new RepositoryUpdate() { Private = true };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryOwner, repoContext.RepositoryName, update);

                Assert.True(updatedRepository.Private);
            }
        }

        [PaidAccountTest]
        public async Task UpdatesPrivateWithRepositoryId()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var update = new RepositoryUpdate() { Private = true };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryId, update);

                Assert.True(updatedRepository.Private);
            }
        }

        [IntegrationTest]
        public async Task UpdatesHasIssues()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var update = new RepositoryUpdate() { HasIssues = false };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryOwner, repoContext.RepositoryName, update);

                Assert.False(updatedRepository.HasIssues);
            }
        }

        [IntegrationTest]
        public async Task UpdatesHasIssuesWithRepositoryId()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var update = new RepositoryUpdate() { HasIssues = false };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryId, update);

                Assert.False(updatedRepository.HasIssues);
            }
        }

        [IntegrationTest]
        public async Task UpdatesHasWiki()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var update = new RepositoryUpdate() { HasWiki = false };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryOwner, repoContext.RepositoryName, update);

                Assert.False(updatedRepository.HasWiki);
            }
        }

        [IntegrationTest]
        public async Task UpdatesHasWikiWithRepositoryId()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var update = new RepositoryUpdate() { HasWiki = false };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryId, update);

                Assert.False(updatedRepository.HasWiki);
            }
        }

        [IntegrationTest]
        public async Task UpdatesHasDownloads()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var update = new RepositoryUpdate() { HasDownloads = false };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryOwner, repoContext.RepositoryName, update);

                Assert.False(updatedRepository.HasDownloads);
            }
        }

        [IntegrationTest]
        public async Task UpdatesHasDownloadsWithRepositoryId()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var update = new RepositoryUpdate() { HasDownloads = false };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryId, update);

                Assert.False(updatedRepository.HasDownloads);
            }
        }

        [IntegrationTest]
        public async Task UpdatesIsTemplate()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var update = new RepositoryUpdate() { IsTemplate = true };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryOwner, repoContext.RepositoryName, update);

                Assert.True(updatedRepository.IsTemplate);
            }
        }

        [IntegrationTest]
        public async Task UpdatesIsTemplateWithRepositoryId()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var update = new RepositoryUpdate() { IsTemplate = true };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryId, update);

                Assert.True(updatedRepository.IsTemplate);
            }
        }

        [IntegrationTest]
        public async Task UpdatesDefaultBranch()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var reference = _github.Git.Reference.GetAll(repoContext.RepositoryOwner, repoContext.RepositoryName).Result.First();
                _github.Git.Reference.Create(repoContext.RepositoryId, new NewReference("refs/heads/primary", reference.Object.Sha)).Wait();
                var update = new RepositoryUpdate() { DefaultBranch = "primary" };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryOwner, repoContext.RepositoryName, update);

                Assert.Equal(update.DefaultBranch, updatedRepository.DefaultBranch);
            }
        }

        [IntegrationTest]
        public async Task UpdatesDefaultBranchWithRepositoryId()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var reference = _github.Git.Reference.GetAll(repoContext.RepositoryOwner, repoContext.RepositoryName).Result.First();
                _github.Git.Reference.Create(repoContext.RepositoryId, new NewReference("refs/heads/primary", reference.Object.Sha)).Wait();
                var update = new RepositoryUpdate() { DefaultBranch = "primary" };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryId, update);

                Assert.Equal(update.DefaultBranch, updatedRepository.DefaultBranch);
            }
        }

        [IntegrationTest]
        public async Task UpdatesMergeMethod()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var updateRepository = new RepositoryUpdate()
                {
                    AllowMergeCommit = false,
                    AllowSquashMerge = false,
                    AllowRebaseMerge = true, // this is the default, but the value is tested in UpdatesMergeMethodWithRepositoryId test
                    AllowAutoMerge = true
                };

                var editedRepository = await _github.Repository.Edit(repoContext.RepositoryOwner, repoContext.RepositoryName, updateRepository);
                Assert.False(editedRepository.AllowMergeCommit);
                Assert.False(editedRepository.AllowSquashMerge);
                Assert.True(editedRepository.AllowRebaseMerge);
                Assert.True(editedRepository.AllowAutoMerge);
            }
        }

        [IntegrationTest]
        public async Task UpdatesMergeMethodWithRepositoryId()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var updateRepository = new RepositoryUpdate()
                {
                    AllowMergeCommit = true,
                    AllowSquashMerge = true,
                    AllowRebaseMerge = false,
                    AllowAutoMerge = true
                };

                var editedRepository = await _github.Repository.Edit(repoContext.RepositoryId, updateRepository);
                Assert.True(editedRepository.AllowMergeCommit);
                Assert.True(editedRepository.AllowSquashMerge);
                Assert.False(editedRepository.AllowRebaseMerge);
                Assert.True(editedRepository.AllowAutoMerge);
            }
        }

        [IntegrationTest]
        public async Task UpdatesDeleteBranchOnMergeMethod()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var updateRepository = new RepositoryUpdate() { DeleteBranchOnMerge = true };

                var editedRepository = await _github.Repository.Edit(repoContext.RepositoryOwner, repoContext.RepositoryName, updateRepository);
                Assert.True(editedRepository.DeleteBranchOnMerge);

                var repository = await _github.Repository.Get(repoContext.RepositoryOwner, repoContext.RepositoryName);
                Assert.True(repository.DeleteBranchOnMerge);
            }
        }

        [IntegrationTest]
        public async Task UpdatesDeleteBranchOnMergeMethodWithRepositoryId()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var updateRepository = new RepositoryUpdate() { DeleteBranchOnMerge = true };

                var editedRepository = await _github.Repository.Edit(repoContext.RepositoryId, updateRepository);
                Assert.True(editedRepository.DeleteBranchOnMerge);
            }
        }

        [IntegrationTest]
        public async Task UpdatesArchive()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var update = new RepositoryUpdate() { Archived = true };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryOwner, repoContext.RepositoryName, update);

                Assert.Equal(update.Archived, updatedRepository.Archived);
            }
        }

        [IntegrationTest]
        public async Task UpdatesArchiveWithRepositoryId()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                var update = new RepositoryUpdate() { Archived = true };

                var updatedRepository = await _github.Repository.Edit(repoContext.RepositoryId, update);

                Assert.Equal(update.Archived, updatedRepository.Archived);
            }
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

    public class TheGetMethod : GitHubClientTestBase
    {
        [IntegrationTest]
        [PotentiallyFlakyTest]
        public async Task ReturnsSpecifiedRepository()
        {
            var github = Helper.GetAuthenticatedClient();

            var repository = await github.Repository.Get("haacked", "seegit");

            Assert.Equal("https://github.com/Haacked/SeeGit.git", repository.CloneUrl, ignoreCase: true);
            Assert.False(repository.Private);
            Assert.False(repository.Fork);
            Assert.Equal(AccountType.User, repository.Owner.Type);
        }

        [IntegrationTest]
        [PotentiallyFlakyTest]
        public async Task ReturnsSpecifiedRepositoryWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var repository = await github.Repository.Get(3622414);

            Assert.Equal("https://github.com/Haacked/SeeGit.git", repository.CloneUrl, ignoreCase: true);
            Assert.False(repository.Private);
            Assert.False(repository.Fork);
            Assert.Equal(AccountType.User, repository.Owner.Type);
        }

        [IntegrationTest]
        [PotentiallyFlakyTest]
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
        [PotentiallyFlakyTest]
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
        [PotentiallyFlakyTest]
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
        [PotentiallyFlakyTest]
        public async Task ReturnsForkedRepository()
        {
            var github = Helper.GetAuthenticatedClient();

            var repository = await github.Repository.Get("octokitnet-test1", "octokit.net");

            Assert.Equal("https://github.com/octokitnet-test1/octokit.net.git", repository.CloneUrl);
            Assert.True(repository.Fork);
        }

        [IntegrationTest]
        [PotentiallyFlakyTest]
        public async Task ReturnsForkedRepositoryWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var repository = await github.Repository.Get(100559458);

            Assert.Equal("https://github.com/octokitnet-test1/octokit.net.git", repository.CloneUrl);
            Assert.True(repository.Fork);
        }

        [IntegrationTest]
        public async Task ReturnsRepositoryMergeOptions()
        {
            var github = Helper.GetAuthenticatedClient();

            using (var context = await github.CreateUserRepositoryContext(Helper.MakeNameWithTimestamp("public-repo")))
            {
                var repository = await github.Repository.Get(context.RepositoryOwner, context.RepositoryName);

                Assert.NotNull(repository.AllowRebaseMerge);
                Assert.NotNull(repository.AllowSquashMerge);
                Assert.NotNull(repository.AllowMergeCommit);
                Assert.NotNull(repository.AllowAutoMerge);
            }
        }

        [IntegrationTest]
        public async Task ReturnsRepositoryMergeOptionsWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            using (var context = await github.CreateUserRepositoryContext(Helper.MakeNameWithTimestamp("public-repo")))
            {
                var repository = await github.Repository.Get(context.RepositoryId);

                Assert.NotNull(repository.AllowRebaseMerge);
                Assert.NotNull(repository.AllowSquashMerge);
                Assert.NotNull(repository.AllowMergeCommit);
                Assert.NotNull(repository.AllowAutoMerge);
            }
        }

        [IntegrationTest]
        [PotentiallyFlakyTest]
        public async Task ReturnsSpecifiedRepositoryWithLicenseInformation()
        {
            var github = Helper.GetAuthenticatedClient();

            var repository = await github.Repository.Get("github", "choosealicense.com");

            Assert.NotNull(repository.License);
            Assert.Equal("mit", repository.License.Key);
            Assert.Equal("MIT License", repository.License.Name);
        }

        [IntegrationTest]
        public async Task ReturnsRepositoryDeleteBranchOnMergeOptions()
        {
            var github = Helper.GetAuthenticatedClient();

            using (var context = await github.CreateUserRepositoryContext(Helper.MakeNameWithTimestamp("public-repo")))
            {
                var repository = await github.Repository.Get(context.RepositoryOwner, context.RepositoryName);

                Assert.NotNull(repository.DeleteBranchOnMerge);
            }
        }

        [IntegrationTest]
        public async Task ReturnsRepositoryDeleteBranchOnMergeOptionsWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            using (var context = await github.CreateUserRepositoryContext(Helper.MakeNameWithTimestamp("public-repo")))
            {
                var repository = await github.Repository.Get(context.RepositoryId);

                Assert.NotNull(repository.DeleteBranchOnMerge);
            }
        }

        [IntegrationTest]
        public async Task ReceivesCorrectArchiveUrl()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"),
            new ObservableCredentialProvider());

            var repo = await github.Repository.Get("octokit", "octokit.net");

            Assert.Equal("https://api.github.com/repos/octokit/octokit.net/{archive_format}{/ref}", repo.ArchiveUrl);
        }

        class ObservableCredentialProvider : ICredentialStore
        {
            public async Task<Credentials> GetCredentials()
            {
                return await Observable.Return(Helper.Credentials);
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

            Assert.Contains(contributors, c => c.Login == "pmacn");
        }

        [IntegrationTest]
        public async Task GetsContributorsWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var contributors = await github.Repository.GetAllContributors(7528679);

            Assert.Contains(contributors, c => c.Login == "pmacn");
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

            Assert.Contains(contributors, c => c.Type == "Anonymous");
        }

        [IntegrationTest]
        public async Task GetsContributorsIncludeAnonymousWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var contributors = await github.Repository.GetAllContributors(538746, true);

            Assert.Contains(contributors, c => c.Type == "Anonymous");
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

            var firstPage = await github.Repository.GetAllContributors("ruby", "ruby", true, firstPageOptions);

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

    public class TheGetAllLanguagesMethod : GitHubClientTestBase
    {
        [IntegrationTest]
        public async Task GetsLanguages()
        {
            var github = Helper.GetAuthenticatedClient();

            var languages = await github.Repository.GetAllLanguages("octokit", "octokit.net");

            Assert.NotEmpty(languages);
            Assert.Contains(languages, l => l.Name == "C#");
        }

        [IntegrationTest]
        public async Task GetsLanguagesWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var languages = await github.Repository.GetAllLanguages(7528679);

            Assert.NotEmpty(languages);
            Assert.Contains(languages, l => l.Name == "C#");
        }

        [IntegrationTest]
        public async Task GetsEmptyLanguagesWhenNone()
        {
            var github = Helper.GetAuthenticatedClient();
            using (var context = await github.CreateUserRepositoryContext(Helper.MakeNameWithTimestamp("public-repo")))
            {
                var languages = await github.Repository.GetAllLanguages(context.RepositoryOwner, context.RepositoryName);

                Assert.Empty(languages);
            }
        }

        [IntegrationTest]
        public async Task GetsEmptyLanguagesWhenNoneWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();
            using (var context = await github.CreateUserRepositoryContext(Helper.MakeNameWithTimestamp("public-repo")))
            {
                var languages = await github.Repository.GetAllLanguages(context.RepositoryId);

                Assert.Empty(languages);
            }
        }
    }

    public class TheReplaceAllTopicsMethod : IDisposable
    {
        readonly IGitHubClient _github = Helper.GetAuthenticatedClient();
        private readonly RepositoryTopics _defaultTopics = new RepositoryTopics(new List<string> { "blog", "ruby", "jekyll" });
        private readonly RepositoryContext _context;
        private readonly string _theRepository;
        private readonly string _theRepoOwner;

        public TheReplaceAllTopicsMethod()
        {
            _theRepoOwner = Helper.Organization;
            _theRepository = Helper.MakeNameWithTimestamp("topics");
            _context = _github.CreateOrganizationRepositoryContext(_theRepoOwner, new NewRepository(_theRepository)).Result;
            var defaultTopicAssignmentResult = _github.Repository.ReplaceAllTopics(_context.RepositoryId, _defaultTopics).Result;
        }

        [IntegrationTest]
        public async Task ClearsTopicsWithAnEmptyList()
        {
            var result = await _github.Repository.ReplaceAllTopics(_theRepoOwner, _theRepository, new RepositoryTopics());
            Assert.Empty(result.Names);

            var doubleCheck = await _github.Repository.GetAllTopics(_theRepoOwner, _theRepository);
            Assert.Empty((doubleCheck.Names));
        }

        [IntegrationTest]
        public async Task ClearsTopicsWithAnEmptyListWhenUsingRepoId()
        {
            var repo = await _github.Repository.Get(_theRepoOwner, _theRepository);
            var result = await _github.Repository.ReplaceAllTopics(repo.Id, new RepositoryTopics());
            Assert.Empty(result.Names);

            var doubleCheck = await _github.Repository.GetAllTopics(_theRepoOwner, _theRepository);
            Assert.Empty((doubleCheck.Names));
        }

        [IntegrationTest]
        public async Task ReplacesTopicsWithAList()
        {
            var defaultTopicsList = new RepositoryTopics(_defaultTopics.Names);
            var result = await _github.Repository.ReplaceAllTopics(_theRepoOwner, _theRepository, defaultTopicsList);

            Assert.NotEmpty(result.Names);
            Assert.Contains(result.Names, item => _defaultTopics.Names.Contains(item, StringComparer.InvariantCultureIgnoreCase));

            var doubleCheck = await _github.Repository.GetAllTopics(_theRepoOwner, _theRepository);
            Assert.Contains(doubleCheck.Names, item => _defaultTopics.Names.Contains(item, StringComparer.InvariantCultureIgnoreCase));
        }

        [IntegrationTest]
        public async Task ReplacesTopicsWithAListWhenUsingRepoId()
        {
            var defaultTopicsList = new RepositoryTopics(_defaultTopics.Names);
            var repo = await _github.Repository.Get(_theRepoOwner, _theRepository);
            var result = await _github.Repository.ReplaceAllTopics(repo.Id, defaultTopicsList);

            Assert.NotEmpty(result.Names);
            Assert.Contains(result.Names, item => _defaultTopics.Names.Contains(item, StringComparer.InvariantCultureIgnoreCase));

            var doubleCheck = await _github.Repository.GetAllTopics(_theRepoOwner, _theRepository);
            Assert.Contains(doubleCheck.Names, item => _defaultTopics.Names.Contains(item, StringComparer.InvariantCultureIgnoreCase));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
    public class TheGetAllTopicsMethod
    {
        private readonly string _repoOwner = "SeanKilleen";
        private readonly string _repoName = "seankilleen.github.io";

        [IntegrationTest]
        public async Task GetsTopicsByOwnerAndName()
        {
            var github = Helper.GetAnonymousClient();
            var result = await github.Repository.GetAllTopics(_repoOwner, _repoName);

            Assert.Contains("blog", result.Names);
            Assert.Contains("ruby", result.Names);
            Assert.Contains("jekyll", result.Names);
        }

        [IntegrationTest]
        public async Task GetsTopicsByRepoID()
        {
            var github = Helper.GetAnonymousClient();
            var repo = await github.Repository.Get(_repoOwner, _repoName);
            var result = await github.Repository.GetAllTopics(repo.Id);

            Assert.Contains("blog", result.Names);
            Assert.Contains("ruby", result.Names);
            Assert.Contains("jekyll", result.Names);
        }
    }

    public class TheGetAllTagsMethod
    {
        [IntegrationTest]
        public async Task GetsTags()
        {
            var github = Helper.GetAuthenticatedClient();

            var tags = await github.Repository.GetAllTags("octokit", "octokit.net");

            Assert.Contains(tags, t => t.Name == "v0.1.0");
        }

        [IntegrationTest]
        public async Task GetsTagsWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var tags = await github.Repository.GetAllTags(7528679);

            Assert.Contains(tags, t => t.Name == "v0.1.0");
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

    public class TheGetAllTeamsMethod
    {
        [IntegrationTest(Skip = "Test requires administration rights to access this endpoint")]
        public async Task GetsAllTeams()
        {
            var github = Helper.GetAuthenticatedClient();

            var branches = await github.Repository.GetAllTeams("octokit", "octokit.net");

            Assert.NotEmpty(branches);
        }

        [IntegrationTest(Skip = "Test requires administration rights to access this endpoint")]
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

    public class TheGetLicenseContentsMethod
    {
        [IntegrationTest]
        [PotentiallyFlakyTest]
        public async Task ReturnsLicenseContent()
        {
            var github = Helper.GetAuthenticatedClient();

            var license = await github.Repository.GetLicenseContents("octokit", "octokit.net");
            Assert.Equal("LICENSE.txt", license.Name);
            Assert.NotNull(license.License);
            Assert.Equal("mit", license.License.Key);
            Assert.Equal("MIT License", license.License.Name);
        }

        [IntegrationTest]
        [PotentiallyFlakyTest]
        public async Task ReturnsLicenseContentWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var license = await github.Repository.GetLicenseContents(7528679);
            Assert.Equal("LICENSE.txt", license.Name);
            Assert.NotNull(license.License);
            Assert.Equal("mit", license.License.Key);
            Assert.Equal("MIT License", license.License.Name);
        }
    }

    public class TheGetCodeOwnersErrorsMethod : GitHubClientTestBase
    {
        [IntegrationTest]
        public async Task ReturnsCodeOwnersErrors()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                await _github.Repository.Content.CreateFile(repoContext.RepositoryOwner, repoContext.RepositoryName, ".github/codeowners", new CreateFileRequest("Create codeowners", @"* snyrting6@hotmail.com"));

                // Sometimes it takes a second to create the file
                Thread.Sleep(TimeSpan.FromSeconds(2));

                var license = await _github.Repository.GetAllCodeOwnersErrors(repoContext.RepositoryOwner, repoContext.RepositoryName);
                Assert.NotEmpty(license.Errors);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCodeOwnersErrorsWithRepositoryId()
        {
            using (var repoContext = await _github.CreateUserRepositoryContext())
            {
                await _github.Repository.Content.CreateFile(repoContext.RepositoryId, ".github/codeowners", new CreateFileRequest("Create codeowners", @"* snyrting6@hotmail.com"));

                // Sometimes it takes a second to create the file
                Thread.Sleep(TimeSpan.FromSeconds(2));

                var license = await _github.Repository.GetAllCodeOwnersErrors(repoContext.RepositoryId);
                Assert.NotEmpty(license.Errors);
            }
        }
    }

    public class TheTransferMethod
    {
        [IntegrationTest]
        public async Task TransfersFromOrgToUser()
        {
            var github = Helper.GetAuthenticatedClient();
            var newRepo = new NewRepository(Helper.MakeNameWithTimestamp("transferred-repo"));
            var newOwner = Helper.UserName;
            using (var context = await github.CreateOrganizationRepositoryContext(Helper.Organization, newRepo))
            {
                var transfer = new RepositoryTransfer(newOwner);
                await github.Repository.Transfer(context.RepositoryOwner, context.RepositoryName, transfer);
                var transferred = await github.Repository.Get(newOwner, context.RepositoryName);

                Assert.Equal(newOwner, transferred.Owner.Login);
            }
        }

        [IntegrationTest]
        public async Task TransfersFromOrgToUserById()
        {
            var github = Helper.GetAuthenticatedClient();
            var newRepo = new NewRepository(Helper.MakeNameWithTimestamp("transferred-repo"));
            var newOwner = Helper.UserName;
            using (var context = await github.CreateOrganizationRepositoryContext(Helper.Organization, newRepo))
            {
                var transfer = new RepositoryTransfer(newOwner);
                await github.Repository.Transfer(context.RepositoryId, transfer);
                var transferred = await github.Repository.Get(context.RepositoryId);

                Assert.Equal(newOwner, transferred.Owner.Login);
            }
        }

        [IntegrationTest]
        public async Task TransfersFromUserToOrg()
        {
            var github = Helper.GetAuthenticatedClient();
            var newRepo = new NewRepository(Helper.MakeNameWithTimestamp("transferred-repo"));
            var newOwner = Helper.Organization;
            using (var context = await github.CreateRepositoryContext(newRepo))
            {
                var transfer = new RepositoryTransfer(newOwner);
                await github.Repository.Transfer(context.RepositoryOwner, context.RepositoryName, transfer);
                var transferred = await github.Repository.Get(newOwner, context.RepositoryName);

                Assert.Equal(newOwner, transferred.Owner.Login);
            }
        }

        [IntegrationTest]
        public async Task TransfersFromUserToOrgById()
        {
            var github = Helper.GetAuthenticatedClient();
            var newRepo = new NewRepository(Helper.MakeNameWithTimestamp("transferred-repo"));
            var newOwner = Helper.Organization;
            using (var context = await github.CreateRepositoryContext(newRepo))
            {
                var transfer = new RepositoryTransfer(newOwner);
                await github.Repository.Transfer(context.RepositoryId, transfer);
                var transferred = await github.Repository.Get(context.RepositoryId);

                Assert.Equal(newOwner, transferred.Owner.Login);
            }
        }

        [IntegrationTest]
        public async Task TransfersFromUserToOrgWithTeams()
        {
            // FIXME API doesn't add teams when transferring to an organization
            var github = Helper.GetAuthenticatedClient();
            var newRepo = new NewRepository(Helper.MakeNameWithTimestamp("transferred-repo"));
            var newOwner = Helper.Organization;

            using (var repositoryContext = await github.CreateRepositoryContext(newRepo))
            {
                NewTeam team = new NewTeam(Helper.MakeNameWithTimestamp("transfer-team"));
                using (var teamContext = await github.CreateTeamContext(Helper.Organization, team))
                {
                    var transferTeamIds = new long[] { teamContext.TeamId };
                    var transfer = new RepositoryTransfer(newOwner, transferTeamIds);
                    await github.Repository.Transfer(
                        repositoryContext.RepositoryOwner, repositoryContext.RepositoryName, transfer);
                    var transferred = await github.Repository.Get(repositoryContext.RepositoryId);
                    var repoTeams = await github.Repository.GetAllTeams(repositoryContext.RepositoryId);

                    Assert.Equal(newOwner, transferred.Owner.Login);
                    // transferTeamIds is a subset of repoTeams
                    Assert.Empty(
                        transferTeamIds.Except(
                            repoTeams.Select(t => t.Id)));
                }
            }
        }

        [IntegrationTest]
        public async Task TransfersFromUserToOrgWithTeamsById()
        {
            // FIXME API doesn't add teams when transferring to an organization
            var github = Helper.GetAuthenticatedClient();
            var newRepo = new NewRepository(Helper.MakeNameWithTimestamp("transferred-repo"));
            var newOwner = Helper.Organization;

            using (var repositoryContext = await github.CreateRepositoryContext(newRepo))
            {
                NewTeam team = new NewTeam(Helper.MakeNameWithTimestamp("transfer-team"));
                using (var teamContext = await github.CreateTeamContext(Helper.Organization, team))
                {
                    var transferTeamIds = new long[] { teamContext.TeamId };
                    var transfer = new RepositoryTransfer(newOwner, transferTeamIds);
                    await github.Repository.Transfer(repositoryContext.RepositoryId, transfer);
                    var transferred = await github.Repository.Get(repositoryContext.RepositoryId);
                    var repoTeams = await github.Repository.GetAllTeams(repositoryContext.RepositoryId);

                    Assert.Equal(newOwner, transferred.Owner.Login);
                    // transferTeamIds is a subset of repoTeams
                    Assert.Empty(
                        transferTeamIds.Except(
                            repoTeams.Select(t => t.Id)));
                }
            }
        }
    }

    public class TheAreVulnerabilityAlertsEnabledMethod
    {
        [IntegrationTest]
        public async Task AreVulnerabilityAlertsEnabledReturnsTrue()
        {
            var github = Helper.GetAuthenticatedClient();
            var enabled = await github.Repository.AreVulnerabilityAlertsEnabled("owner", "name");
            Assert.True(enabled);
        }
    }
}
