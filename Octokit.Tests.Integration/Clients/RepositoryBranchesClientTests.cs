using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class RepositoryBranchesClientTests
{
    public class TheGetAllMethod
    {
        [IntegrationTest]
        public async Task GetsAllBranches()
        {
            var github = Helper.GetAuthenticatedClient();

            var branches = await github.Repository.Branch.GetAll("octokit", "octokit.net");

            Assert.NotEmpty(branches);

            foreach (var branch in branches)
            {
                Assert.NotNull(branch.Protection);
            }

            Assert.True(branches.First(x => x.Name == "master").Protected);
        }

        [IntegrationTest]
        public async Task GetsAllBranchesWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var branches = await github.Repository.Branch.GetAll(7528679);

            Assert.NotEmpty(branches);

            foreach (var branch in branches)
            {
                Assert.NotNull(branch.Protection);
            }

            Assert.True(branches.First(x => x.Name == "master").Protected);
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

            var branches = await github.Repository.Branch.GetAll("octokit", "octokit.net", options);

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

            var branches = await github.Repository.Branch.GetAll(7528679, options);

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

            var branches = await github.Repository.Branch.GetAll("octokit", "octokit.net", options);

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

            var branches = await github.Repository.Branch.GetAll(7528679, options);

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

            var firstPage = await github.Repository.Branch.GetAll("octokit", "octokit.net", firstPageOptions);

            var secondPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 2,
                PageCount = 1
            };

            var secondPage = await github.Repository.Branch.GetAll("octokit", "octokit.net", secondPageOptions);

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

            var firstPage = await github.Repository.Branch.GetAll(7528679, firstPageOptions);

            var secondPageOptions = new ApiOptions
            {
                PageSize = 5,
                StartPage = 2,
                PageCount = 1
            };

            var secondPage = await github.Repository.Branch.GetAll(7528679, secondPageOptions);

            Assert.Equal(5, firstPage.Count);
            Assert.Equal(5, secondPage.Count);

            Assert.NotEqual(firstPage[0].Name, secondPage[0].Name);
            Assert.NotEqual(firstPage[1].Name, secondPage[1].Name);
            Assert.NotEqual(firstPage[2].Name, secondPage[2].Name);
            Assert.NotEqual(firstPage[3].Name, secondPage[3].Name);
            Assert.NotEqual(firstPage[4].Name, secondPage[4].Name);
        }
    }

    public class TheGetMethod
    {
        [IntegrationTest]
        public async Task GetsABranch()
        {
            var github = Helper.GetAuthenticatedClient();

            var branch = await github.Repository.Branch.Get("octokit", "octokit.net", "master");

            Assert.NotNull(branch);
            Assert.Equal("master", branch.Name);
            Assert.NotNull(branch.Protection);
            Assert.True(branch.Protected);
        }

        [IntegrationTest]
        public async Task GetsABranchWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var branch = await github.Repository.Branch.Get(7528679, "master");

            Assert.NotNull(branch);
            Assert.Equal("master", branch.Name);

            Assert.NotNull(branch.Protection);
            Assert.True(branch.Protected);
        }
    }

    public class TheEditMethod
    {
        private readonly IRepositoryBranchesClient _fixture;
        private readonly RepositoryContext _context;

        public TheEditMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _context = github.CreateRepositoryContext("source-repo").Result;
            _fixture = github.Repository.Branch;
        }

        public async Task CreateTheWorld()
        {
            // Set master branch to be protected, with some status checks
            var requiredStatusChecks = new RequiredStatusChecks(EnforcementLevel.Everyone, new List<string> { "check1", "check2" });

            var update = new BranchUpdate();
            update.Protection = new BranchProtection(true, requiredStatusChecks);

            var newBranch = await _fixture.Edit(_context.Repository.Owner.Login, _context.Repository.Name, "master", update);
        }

        [IntegrationTest]
        public async Task ProtectsBranch()
        {
            // Set master branch to be protected, with some status checks
            var requiredStatusChecks = new RequiredStatusChecks(EnforcementLevel.Everyone, new List<string> { "check1", "check2", "check3" });

            var update = new BranchUpdate();
            update.Protection = new BranchProtection(true, requiredStatusChecks);

            var branch = await _fixture.Edit(_context.Repository.Owner.Login, _context.Repository.Name, "master", update);

            // Ensure a branch object was returned
            Assert.NotNull(branch);

            // Retrieve master branch
            branch = await _fixture.Get(_context.Repository.Owner.Login, _context.Repository.Name, "master");

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

            var branch = await _fixture.Edit(_context.Repository.Owner.Login, _context.Repository.Name, "master", update);

            // Ensure a branch object was returned
            Assert.NotNull(branch);

            // Retrieve master branch
            branch = await _fixture.Get(_context.Repository.Owner.Login, _context.Repository.Name, "master");

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

            var branch = await _fixture.Edit(_context.Repository.Owner.Login, _context.Repository.Name, "master", update);

            // Ensure a branch object was returned
            Assert.NotNull(branch);

            // Retrieve master branch
            branch = await _fixture.Get(_context.Repository.Owner.Login, _context.Repository.Name, "master");

            // Assert the branch is unprotected, and enforcement/contexts are cleared
            Assert.Equal(branch.Protection.Enabled, false);
            Assert.Equal(branch.Protection.RequiredStatusChecks.EnforcementLevel, EnforcementLevel.Off);
            Assert.Equal(branch.Protection.RequiredStatusChecks.Contexts.Count, 0);
        }
    }

    public class TheGetBranchProtectionMethod : IDisposable
    {
        IRepositoryBranchesClient _client;
        RepositoryContext _userRepoContext;
        OrganizationRepositoryWithTeamContext _orgRepoContext;

        public TheGetBranchProtectionMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _client = github.Repository.Branch;

            _userRepoContext = github.CreateRepositoryWithProtectedBranch().Result;
            _orgRepoContext = github.CreateOrganizationRepositoryWithProtectedBranch().Result;
        }

        [IntegrationTest]
        public async Task GetsBranchProtection()
        {
            var repoOwner = _userRepoContext.RepositoryOwner;
            var repoName = _userRepoContext.RepositoryName;
            var protection = await _client.GetBranchProtection(repoOwner, repoName, "master");

            Assert.True(protection.RequiredStatusChecks.IncludeAdmins);
            Assert.True(protection.RequiredStatusChecks.Strict);
            Assert.Equal(2, protection.RequiredStatusChecks.Contexts.Count);

            Assert.Null(protection.Restrictions);
        }

        [IntegrationTest]
        public async Task GetsBranchProtectionWithRepositoryId()
        {
            var repoId = _userRepoContext.RepositoryId;
            var protection = await _client.GetBranchProtection(repoId, "master");

            Assert.True(protection.RequiredStatusChecks.IncludeAdmins);
            Assert.True(protection.RequiredStatusChecks.Strict);
            Assert.Equal(2, protection.RequiredStatusChecks.Contexts.Count);

            Assert.Null(protection.Restrictions);
        }

        [IntegrationTest]
        public async Task GetsBranchProtectionForOrgRepo()
        {
            var repoOwner = _orgRepoContext.RepositoryContext.RepositoryOwner;
            var repoName = _orgRepoContext.RepositoryContext.RepositoryName;
            var protection = await _client.GetBranchProtection(repoOwner, repoName, "master");

            Assert.True(protection.RequiredStatusChecks.IncludeAdmins);
            Assert.True(protection.RequiredStatusChecks.Strict);
            Assert.Equal(2, protection.RequiredStatusChecks.Contexts.Count);

            Assert.Equal(1, protection.Restrictions.Teams.Count);
            Assert.Equal(0, protection.Restrictions.Users.Count);
        }

        [IntegrationTest]
        public async Task GetsBranchProtectionForOrgRepoWithRepositoryId()
        {
            var repoId = _orgRepoContext.RepositoryContext.RepositoryId;
            var protection = await _client.GetBranchProtection(repoId, "master");

            Assert.True(protection.RequiredStatusChecks.IncludeAdmins);
            Assert.True(protection.RequiredStatusChecks.Strict);
            Assert.Equal(2, protection.RequiredStatusChecks.Contexts.Count);

            Assert.Equal(1, protection.Restrictions.Teams.Count);
            Assert.Equal(0, protection.Restrictions.Users.Count);
        }

        public void Dispose()
        {
            if (_userRepoContext != null)
                _userRepoContext.Dispose();

            if (_orgRepoContext != null)
                _orgRepoContext.Dispose();
        }
    }

    public class TheUpdateBranchProtectionMethod : IDisposable
    {
        IRepositoryBranchesClient _client;
        RepositoryContext _userRepoContext;
        OrganizationRepositoryWithTeamContext _orgRepoContext;

        public TheUpdateBranchProtectionMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _client = github.Repository.Branch;

            _userRepoContext = github.CreateRepositoryWithProtectedBranch().Result;
            _orgRepoContext = github.CreateOrganizationRepositoryWithProtectedBranch().Result;
        }

        [IntegrationTest]
        public async Task UpdatesBranchProtection()
        {
            var repoOwner = _userRepoContext.RepositoryOwner;
            var repoName = _userRepoContext.RepositoryName;
            var update = new BranchProtectionSettingsUpdate(
                new BranchProtectionRequiredStatusChecksUpdate(false, false, new[] { "new" }));

            var protection = await _client.UpdateBranchProtection(repoOwner, repoName, "master", update);

            Assert.False(protection.RequiredStatusChecks.IncludeAdmins);
            Assert.False(protection.RequiredStatusChecks.Strict);
            Assert.Equal(1, protection.RequiredStatusChecks.Contexts.Count);

            Assert.Null(protection.Restrictions);
        }

        [IntegrationTest]
        public async Task UpdatesBranchProtectionWithRepositoryId()
        {
            var repoId = _userRepoContext.RepositoryId;
            var update = new BranchProtectionSettingsUpdate(
                new BranchProtectionRequiredStatusChecksUpdate(false, false, new[] { "new" }));

            var protection = await _client.UpdateBranchProtection(repoId, "master", update);

            Assert.False(protection.RequiredStatusChecks.IncludeAdmins);
            Assert.False(protection.RequiredStatusChecks.Strict);
            Assert.Equal(1, protection.RequiredStatusChecks.Contexts.Count);

            Assert.Null(protection.Restrictions);
        }

        [IntegrationTest]
        public async Task UpdatesBranchProtectionForOrgRepo()
        {
            var repoOwner = _orgRepoContext.RepositoryContext.RepositoryOwner;
            var repoName = _orgRepoContext.RepositoryContext.RepositoryName;
            var update = new BranchProtectionSettingsUpdate(
                new BranchProtectionRequiredStatusChecksUpdate(false, false, new[] { "new" }),
                new BranchProtectionPushRestrictionsUpdate());

            var protection = await _client.UpdateBranchProtection(repoOwner, repoName, "master", update);

            Assert.False(protection.RequiredStatusChecks.IncludeAdmins);
            Assert.False(protection.RequiredStatusChecks.Strict);
            Assert.Equal(1, protection.RequiredStatusChecks.Contexts.Count);

            Assert.Empty(protection.Restrictions.Teams);
            Assert.Empty(protection.Restrictions.Users);
        }

        [IntegrationTest]
        public async Task UpdatesBranchProtectionForOrgRepoWithRepositoryId()
        {
            var repoId = _orgRepoContext.RepositoryContext.RepositoryId;
            var update = new BranchProtectionSettingsUpdate(
                new BranchProtectionRequiredStatusChecksUpdate(false, false, new[] { "new" }),
                new BranchProtectionPushRestrictionsUpdate());

            var protection = await _client.UpdateBranchProtection(repoId, "master", update);

            Assert.False(protection.RequiredStatusChecks.IncludeAdmins);
            Assert.False(protection.RequiredStatusChecks.Strict);
            Assert.Equal(1, protection.RequiredStatusChecks.Contexts.Count);

            Assert.Empty(protection.Restrictions.Teams);
            Assert.Empty(protection.Restrictions.Users);
        }

        public void Dispose()
        {
            if (_userRepoContext != null)
                _userRepoContext.Dispose();

            if (_orgRepoContext != null)
                _orgRepoContext.Dispose();
        }
    }

    public class TheDeleteBranchProtectionMethod
    {
        IGitHubClient _github;
        IRepositoryBranchesClient _client;
        
        public TheDeleteBranchProtectionMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _client = _github.Repository.Branch;
        }

        [IntegrationTest]
        public async Task DeletesBranchProtection()
        {
            using (var context = await _github.CreateRepositoryWithProtectedBranch())
            {
                var repoOwner = context.RepositoryOwner;
                var repoName = context.RepositoryName;
                var deleted = await _client.DeleteBranchProtection(repoOwner, repoName, "master");

                Assert.True(deleted);
            }
        }

        [IntegrationTest]
        public async Task DeletesBranchProtectionWithRepositoryId()
        {
            using (var context = await _github.CreateRepositoryWithProtectedBranch())
            {
                var repoId = context.RepositoryId;
                var deleted = await _client.DeleteBranchProtection(repoId, "master");

                Assert.True(deleted);
            }
        }

        [IntegrationTest]
        public async Task DeletesBranchProtectionForOrgRepo()
        {
            using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
            {
                var repoOwner = context.RepositoryContext.RepositoryOwner;
                var repoName = context.RepositoryContext.RepositoryName;
                var deleted = await _client.DeleteBranchProtection(repoOwner, repoName, "master");

                Assert.True(deleted);
            }
        }

        [IntegrationTest]
        public async Task DeletesBranchProtectionForOrgRepoWithRepositoryId()
        {
            using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
            {
                var repoId = context.RepositoryContext.RepositoryId;
                var deleted = await _client.DeleteBranchProtection(repoId, "master");

                Assert.True(deleted);
            }
        }
    }
}
