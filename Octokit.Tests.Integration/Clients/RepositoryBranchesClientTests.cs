﻿using System;
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

            Assert.True(protection.EnforceAdmins.Enabled);
            Assert.True(protection.RequiredStatusChecks.Strict);
            Assert.Equal(2, protection.RequiredStatusChecks.Contexts.Count);

            Assert.Null(protection.Restrictions);
        }

        [IntegrationTest]
        public async Task GetsBranchProtectionWithRepositoryId()
        {
            var repoId = _userRepoContext.RepositoryId;
            var protection = await _client.GetBranchProtection(repoId, "master");

            Assert.True(protection.EnforceAdmins.Enabled);
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

            Assert.True(protection.EnforceAdmins.Enabled);
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

            Assert.True(protection.EnforceAdmins.Enabled);
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
                new BranchProtectionRequiredStatusChecksUpdate(false, new[] { "new" }));

            var protection = await _client.UpdateBranchProtection(repoOwner, repoName, "master", update);

            Assert.False(protection.EnforceAdmins.Enabled);
            Assert.False(protection.RequiredStatusChecks.Strict);
            Assert.Equal(1, protection.RequiredStatusChecks.Contexts.Count);

            Assert.Null(protection.Restrictions);
        }

        [IntegrationTest]
        public async Task UpdatesBranchProtectionWithRepositoryId()
        {
            var repoId = _userRepoContext.RepositoryId;
            var update = new BranchProtectionSettingsUpdate(
                new BranchProtectionRequiredStatusChecksUpdate(false, new[] { "new" }));

            var protection = await _client.UpdateBranchProtection(repoId, "master", update);

            Assert.False(protection.EnforceAdmins.Enabled);
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
                new BranchProtectionRequiredStatusChecksUpdate(false, new[] { "new" }),
                new BranchProtectionPushRestrictionsUpdate(),
                false);

            var protection = await _client.UpdateBranchProtection(repoOwner, repoName, "master", update);

            Assert.False(protection.EnforceAdmins.Enabled);
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
                new BranchProtectionRequiredStatusChecksUpdate(false, new[] { "new" }),
                new BranchProtectionPushRestrictionsUpdate(),
                false);

            var protection = await _client.UpdateBranchProtection(repoId, "master", update);

            Assert.False(protection.EnforceAdmins.Enabled);
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

    public class TheGetRequiredStatusChecksMethod : IDisposable
    {
        IRepositoryBranchesClient _client;
        RepositoryContext _userRepoContext;

        public TheGetRequiredStatusChecksMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _client = github.Repository.Branch;

            _userRepoContext = github.CreateRepositoryWithProtectedBranch().Result;
        }

        [IntegrationTest]
        public async Task GetsRequiredStatusChecks()
        {
            var repoOwner = _userRepoContext.RepositoryOwner;
            var repoName = _userRepoContext.RepositoryName;
            var requiredStatusChecks = await _client.GetRequiredStatusChecks(repoOwner, repoName, "master");

            Assert.NotNull(requiredStatusChecks);
            Assert.NotNull(requiredStatusChecks.Contexts);
            Assert.True(requiredStatusChecks.Strict);
            Assert.Equal(2, requiredStatusChecks.Contexts.Count);
        }

        [IntegrationTest]
        public async Task GetsRequiredStatusChecksWithRepositoryId()
        {
            var repoId = _userRepoContext.RepositoryId;
            var requiredStatusChecks = await _client.GetRequiredStatusChecks(repoId, "master");

            Assert.NotNull(requiredStatusChecks);
            Assert.NotNull(requiredStatusChecks.Contexts);
            Assert.True(requiredStatusChecks.Strict);
            Assert.Equal(2, requiredStatusChecks.Contexts.Count);
        }

        public void Dispose()
        {
            if (_userRepoContext != null)
                _userRepoContext.Dispose();
        }
    }

    public class TheUpdateRequiredStatusChecksMethod : IDisposable
    {
        IRepositoryBranchesClient _client;
        RepositoryContext _userRepoContext;

        public TheUpdateRequiredStatusChecksMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _client = github.Repository.Branch;

            _userRepoContext = github.CreateRepositoryWithProtectedBranch().Result;
        }

        [IntegrationTest]
        public async Task UpdateRequiredStatusChecks()
        {
            var repoOwner = _userRepoContext.RepositoryOwner;
            var repoName = _userRepoContext.RepositoryName;
            var update = new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "new" });
            var requiredStatusChecks = await _client.UpdateRequiredStatusChecks(repoOwner, repoName, "master", update);

            Assert.NotNull(requiredStatusChecks);
            Assert.NotNull(requiredStatusChecks.Contexts);
            Assert.True(requiredStatusChecks.Contexts.Contains("new"));
            Assert.True(requiredStatusChecks.Strict);
            Assert.Equal(1, requiredStatusChecks.Contexts.Count);
        }

        [IntegrationTest]
        public async Task UpdatesRequiredStatusChecksWithRepositoryId()
        {
            var repoId = _userRepoContext.RepositoryId;
            var update = new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "new" });
            var requiredStatusChecks = await _client.UpdateRequiredStatusChecks(repoId, "master", update);

            Assert.NotNull(requiredStatusChecks);
            Assert.NotNull(requiredStatusChecks.Contexts);
            Assert.True(requiredStatusChecks.Contexts.Contains("new"));
            Assert.True(requiredStatusChecks.Strict);
            Assert.Equal(1, requiredStatusChecks.Contexts.Count);
        }

        public void Dispose()
        {
            if (_userRepoContext != null)
                _userRepoContext.Dispose();
        }
    }

    public class TheDeleteRequiredStatusChecksMethod
    {
        IGitHubClient _github;
        IRepositoryBranchesClient _client;

        public TheDeleteRequiredStatusChecksMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _client = _github.Repository.Branch;
        }

        [IntegrationTest]
        public async Task DeletesRequiredStatusChecks()
        {
            using (var context = await _github.CreateRepositoryWithProtectedBranch())
            {
                var repoOwner = context.RepositoryOwner;
                var repoName = context.RepositoryName;
                var deleted = await _client.DeleteRequiredStatusChecks(repoOwner, repoName, "master");

                Assert.True(deleted);
            }
        }

        [IntegrationTest]
        public async Task DeletesRequiredStatusChecksWithRepositoryId()
        {
            using (var context = await _github.CreateRepositoryWithProtectedBranch())
            {
                var repoId = context.RepositoryId;
                var deleted = await _client.DeleteRequiredStatusChecks(repoId, "master");

                Assert.True(deleted);
            }
        }
    }

    public class TheGetRequiredStatusChecksContextsMethod : IDisposable
    {
        IRepositoryBranchesClient _client;
        RepositoryContext _userRepoContext;

        public TheGetRequiredStatusChecksContextsMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _client = github.Repository.Branch;

            _userRepoContext = github.CreateRepositoryWithProtectedBranch().Result;
        }

        [IntegrationTest]
        public async Task GetsRequiredStatusChecksContexts()
        {
            var repoOwner = _userRepoContext.RepositoryOwner;
            var repoName = _userRepoContext.RepositoryName;
            var requiredStatusChecksContexts = await _client.GetRequiredStatusChecksContexts(repoOwner, repoName, "master");

            Assert.NotNull(requiredStatusChecksContexts);
            Assert.Equal(2, requiredStatusChecksContexts.Count);
        }

        [IntegrationTest]
        public async Task GetsRequiredStatusChecksContextsWithRepositoryId()
        {
            var repoId = _userRepoContext.RepositoryId;
            var requiredStatusChecksContexts = await _client.GetRequiredStatusChecksContexts(repoId, "master");

            Assert.NotNull(requiredStatusChecksContexts);
            Assert.Equal(2, requiredStatusChecksContexts.Count);
        }

        public void Dispose()
        {
            if (_userRepoContext != null)
                _userRepoContext.Dispose();
        }
    }

    public class TheUpdateRequiredStatusChecksContextsMethod : IDisposable
    {
        IRepositoryBranchesClient _client;
        RepositoryContext _userRepoContext;

        public TheUpdateRequiredStatusChecksContextsMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _client = github.Repository.Branch;

            _userRepoContext = github.CreateRepositoryWithProtectedBranch().Result;
        }

        [IntegrationTest]
        public async Task UpdateRequiredStatusChecksContexts()
        {
            var repoOwner = _userRepoContext.RepositoryOwner;
            var repoName = _userRepoContext.RepositoryName;
            var update = new List<string>() { "build2" };
            var requiredStatusChecksContexts = await _client.UpdateRequiredStatusChecksContexts(repoOwner, repoName, "master", update);

            Assert.Equal(1, requiredStatusChecksContexts.Count);
        }

        [IntegrationTest]
        public async Task UpdatesRequiredStatusChecksContextsWithRepositoryId()
        {
            var repoId = _userRepoContext.RepositoryId;
            var update = new List<string>() { "build2" };
            var requiredStatusChecksContexts = await _client.UpdateRequiredStatusChecksContexts(repoId, "master", update);

            Assert.Equal(1, requiredStatusChecksContexts.Count);
        }

        public void Dispose()
        {
            if (_userRepoContext != null)
                _userRepoContext.Dispose();
        }
    }

    public class TheAddRequiredStatusChecksContextsMethod : IDisposable
    {
        IRepositoryBranchesClient _client;
        RepositoryContext _userRepoContext;

        public TheAddRequiredStatusChecksContextsMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _client = github.Repository.Branch;

            _userRepoContext = github.CreateRepositoryWithProtectedBranch().Result;
        }

        [IntegrationTest]
        public async Task AddsRequiredStatusChecksContexts()
        {
            var repoOwner = _userRepoContext.RepositoryOwner;
            var repoName = _userRepoContext.RepositoryName;
            var update = new List<string>() { "build2", "deploy" };
            var requiredStatusChecksContexts = await _client.AddRequiredStatusChecksContexts(repoOwner, repoName, "master", update);

            Assert.NotNull(requiredStatusChecksContexts);
            Assert.NotNull(requiredStatusChecksContexts.Count);
            Assert.Equal(4, requiredStatusChecksContexts.Count);
        }

        [IntegrationTest]
        public async Task AddsRequiredStatusChecksContextsWithRepositoryId()
        {
            var repoId = _userRepoContext.RepositoryId;
            var update = new List<string>() { "build2", "deploy" };
            var requiredStatusChecksContexts = await _client.AddRequiredStatusChecksContexts(repoId, "master", update);

            Assert.NotNull(requiredStatusChecksContexts);
            Assert.NotNull(requiredStatusChecksContexts.Count);
            Assert.Equal(4, requiredStatusChecksContexts.Count);
        }

        public void Dispose()
        {
            if (_userRepoContext != null)
                _userRepoContext.Dispose();
        }
    }

    public class TheDeleteRequiredStatusChecksContextsMethod
    {
        IGitHubClient _github;
        IRepositoryBranchesClient _client;

        public TheDeleteRequiredStatusChecksContextsMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _client = _github.Repository.Branch;
        }

        [IntegrationTest]
        public async Task DeletesRequiredStatusChecksContexts()
        {
            using (var context = await _github.CreateRepositoryWithProtectedBranch())
            {
                var repoOwner = context.RepositoryOwner;
                var repoName = context.RepositoryName;
                var contextsToRemove = new List<string>() { "build" };
                var deleted = await _client.DeleteRequiredStatusChecksContexts(repoOwner, repoName, "master", contextsToRemove);

                Assert.NotNull(deleted);
                Assert.True(deleted.Contains("test"));
            }
        }

        [IntegrationTest]
        public async Task DeletesRequiredStatusChecksContextsWithRepositoryId()
        {
            using (var context = await _github.CreateRepositoryWithProtectedBranch())
            {
                var repoId = context.RepositoryId;
                var contextsToRemove = new List<string>() { "build" };
                var deleted = await _client.DeleteRequiredStatusChecksContexts(repoId, "master", contextsToRemove);

                Assert.NotNull(deleted);
                Assert.True(deleted.Contains("test"));
            }
        }
    }

    public class TheGetAdminEnforcementMethod : IDisposable
    {
        private readonly IRepositoryBranchesClient _client;
        private readonly RepositoryContext _userRepoContext;

        public TheGetAdminEnforcementMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _client = github.Repository.Branch;

            _userRepoContext = github.CreateRepositoryWithProtectedBranch().Result;
        }

        [IntegrationTest]
        public async Task GetsAdminEnforcement()
        {
            var repoOwner = _userRepoContext.RepositoryOwner;
            var repoName = _userRepoContext.RepositoryName;
            var enforceAdmins = await _client.GetAdminEnforcement(repoOwner, repoName, "master");

            Assert.NotNull(enforceAdmins);
            Assert.True(enforceAdmins.Enabled);
        }

        [IntegrationTest]
        public async Task GetsAdminEnforcementWithRepositoryId()
        {
            var repoId = _userRepoContext.RepositoryId;
            var enforceAdmins = await _client.GetAdminEnforcement(repoId, "master");

            Assert.NotNull(enforceAdmins);
            Assert.True(enforceAdmins.Enabled);
        }

        public void Dispose()
        {
            if (_userRepoContext != null)
            {
                _userRepoContext.Dispose();
            }
        }
    }

    public class TheAddAdminEnforcementMethod : IDisposable
    {
        private readonly IRepositoryBranchesClient _client;
        private readonly RepositoryContext _userRepoContext;

        public TheAddAdminEnforcementMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _client = github.Repository.Branch;

            _userRepoContext = github.CreateRepositoryWithProtectedBranch().Result;
        }

        [IntegrationTest]
        public async Task AddsAdminEnforcement()
        {
            var repoOwner = _userRepoContext.RepositoryOwner;
            var repoName = _userRepoContext.RepositoryName;

            await _client.RemoveAdminEnforcement(repoOwner, repoName, "master");
            var enforceAdmins = await _client.AddAdminEnforcement(repoOwner, repoName, "master");

            Assert.NotNull(enforceAdmins);
            Assert.True(enforceAdmins.Enabled);
        }

        [IntegrationTest]
        public async Task AddsAdminEnforcementoWithRepositoryId()
        {
            var repoId = _userRepoContext.RepositoryId;

            await _client.RemoveAdminEnforcement(repoId, "master");
            var enforceAdmins = await _client.AddAdminEnforcement(repoId, "master");

            Assert.NotNull(enforceAdmins);
            Assert.True(enforceAdmins.Enabled);
        }

        public void Dispose()
        {
            if (_userRepoContext != null)
            {
                _userRepoContext.Dispose();
            }
        }
    }

    public class TheRemoveAdminEnforcementMethod
    {
        private readonly IRepositoryBranchesClient _client;
        private readonly IGitHubClient _github;

        public TheRemoveAdminEnforcementMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _client = _github.Repository.Branch;
        }

        [IntegrationTest]
        public async Task RemovesAdminEnforcement()
        {
            using (var context = await _github.CreateRepositoryWithProtectedBranch())
            {
                var repoOwner = context.RepositoryOwner;
                var repoName = context.RepositoryName;
                var deleted = await _client.RemoveAdminEnforcement(repoOwner, repoName, "master");

                Assert.True(deleted);

                var enforceAdmins = await _client.GetAdminEnforcement(repoOwner, repoName, "master");

                Assert.NotNull(enforceAdmins);
                Assert.False(enforceAdmins.Enabled);
            }
        }

        [IntegrationTest]
        public async Task RemovesAdminEnforcementWithRepositoryId()
        {
            using (var context = await _github.CreateRepositoryWithProtectedBranch())
            {
                var repoId = context.RepositoryId;
                var deleted = await _client.RemoveAdminEnforcement(repoId, "master");

                Assert.True(deleted);

                var enforceAdmins = await _client.GetAdminEnforcement(repoId, "master");

                Assert.NotNull(enforceAdmins);
                Assert.False(enforceAdmins.Enabled);
            }
        }
    }

    public class TheGetProtectedBranchRestrictionsMethod : IDisposable
    {
        IRepositoryBranchesClient _client;
        OrganizationRepositoryWithTeamContext _orgRepoContext;

        public TheGetProtectedBranchRestrictionsMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _client = github.Repository.Branch;

            _orgRepoContext = github.CreateOrganizationRepositoryWithProtectedBranch().Result;
        }

        [IntegrationTest]
        public async Task GetsRequirProtectedBranchRestrictionsForOrgRepo()
        {
            var repoOwner = _orgRepoContext.RepositoryContext.RepositoryOwner;
            var repoName = _orgRepoContext.RepositoryContext.RepositoryName;
            var restrictions = await _client.GetProtectedBranchRestrictions(repoOwner, repoName, "master");

            Assert.Equal(1, restrictions.Teams.Count);
            Assert.Equal(0, restrictions.Users.Count);
        }

        [IntegrationTest]
        public async Task GetsProtectedBranchRestrictionsForOrgRepoWithRepositoryId()
        {
            var repoId = _orgRepoContext.RepositoryContext.RepositoryId;
            var restrictions = await _client.GetProtectedBranchRestrictions(repoId, "master");

            Assert.Equal(1, restrictions.Teams.Count);
            Assert.Equal(0, restrictions.Users.Count);
        }

        public void Dispose()
        {
            if (_orgRepoContext != null)
                _orgRepoContext.Dispose();
        }
    }

    public class TheDeleteProtectedBranchRestrictionsMethod
    {
        IGitHubClient _github;
        IRepositoryBranchesClient _client;

        public TheDeleteProtectedBranchRestrictionsMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _client = _github.Repository.Branch;
        }

        [IntegrationTest]
        public async Task DeletesRProtectedBranchRestrictionsForOrgRepo()
        {
            using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
            {
                var repoOwner = context.RepositoryContext.RepositoryOwner;
                var repoName = context.RepositoryContext.RepositoryName;
                var deleted = await _client.DeleteProtectedBranchRestrictions(repoOwner, repoName, "master");

                Assert.True(deleted);
            }
        }

        [IntegrationTest]
        public async Task DeletesProtectedBranchRestrictionsForOrgRepoWithRepositoryId()
        {
            using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
            {
                var repoId = context.RepositoryContext.RepositoryId;
                var deleted = await _client.DeleteProtectedBranchRestrictions(repoId, "master");

                Assert.True(deleted);
            }
        }
    }

    public class TheGetProtectedBranchTeamRestrictionsMethod : IDisposable
    {
        IRepositoryBranchesClient _client;
        OrganizationRepositoryWithTeamContext _orgRepoContext;

        public TheGetProtectedBranchTeamRestrictionsMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _client = github.Repository.Branch;

            _orgRepoContext = github.CreateOrganizationRepositoryWithProtectedBranch().Result;
        }

        [IntegrationTest]
        public async Task GetsProtectedBranchTeamRestrictionsForOrgRepo()
        {
            var repoOwner = _orgRepoContext.RepositoryContext.RepositoryOwner;
            var repoName = _orgRepoContext.RepositoryContext.RepositoryName;
            var restrictions = await _client.GetProtectedBranchTeamRestrictions(repoOwner, repoName, "master");

            Assert.NotNull(restrictions);
            Assert.Equal(1, restrictions.Count);
        }

        [IntegrationTest]
        public async Task GetsProtectedBranchTeamRestrictionsForOrgRepoWithRepositoryId()
        {
            var repoId = _orgRepoContext.RepositoryContext.RepositoryId;
            var restrictions = await _client.GetProtectedBranchTeamRestrictions(repoId, "master");

            Assert.NotNull(restrictions);
            Assert.Equal(1, restrictions.Count);
        }

        public void Dispose()
        {
            if (_orgRepoContext != null)
                _orgRepoContext.Dispose();
        }
    }

    public class TheUpdateProtectedBranchTeamRestrictionsMethod : IDisposable
    {
        IRepositoryBranchesClient _client;
        OrganizationRepositoryWithTeamContext _orgRepoContext;
        TeamContext _contextOrgTeam2;
        IGitHubClient _github;

        public TheUpdateProtectedBranchTeamRestrictionsMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _client = _github.Repository.Branch;

            _contextOrgTeam2 = _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team2"))).Result;
            _orgRepoContext = _github.CreateOrganizationRepositoryWithProtectedBranch().Result;
        }

        [IntegrationTest]
        public async Task UpdatesProtectedBranchTeamRestrictionsForOrgRepo()
        {
            var repoOwner = _orgRepoContext.RepositoryContext.RepositoryOwner;
            var repoName = _orgRepoContext.RepositoryContext.RepositoryName;

            // Grant team push access to repo
            await _github.Organization.Team.AddRepository(
                _contextOrgTeam2.TeamId,
                repoOwner,
                repoName,
                new RepositoryPermissionRequest(Permission.Push));

            var newTeam = new BranchProtectionTeamCollection() { _contextOrgTeam2.TeamName };
            var restrictions = await _client.UpdateProtectedBranchTeamRestrictions(repoOwner, repoName, "master", newTeam);

            Assert.NotNull(restrictions);
            Assert.Equal(_contextOrgTeam2.TeamName, restrictions[0].Name);
        }

        [IntegrationTest]
        public async Task UpdatesProtectedBranchTeamRestrictionsForOrgRepoWithRepositoryId()
        {
            var repoId = _orgRepoContext.RepositoryContext.RepositoryId;
            var repoOwner = _orgRepoContext.RepositoryContext.RepositoryOwner;
            var repoName = _orgRepoContext.RepositoryContext.RepositoryName;

            // Grant team push access to repo
            await _github.Organization.Team.AddRepository(
                 _contextOrgTeam2.TeamId,
                repoOwner,
                repoName,
                new RepositoryPermissionRequest(Permission.Push));

            var newTeam = new BranchProtectionTeamCollection() { _contextOrgTeam2.TeamName };
            var restrictions = await _client.UpdateProtectedBranchTeamRestrictions(repoId, "master", newTeam);

            Assert.NotNull(restrictions);
            Assert.Equal(1, restrictions.Count);
        }

        public void Dispose()
        {
            if (_orgRepoContext != null)
                _orgRepoContext.Dispose();

            if (_contextOrgTeam2 != null)
                _contextOrgTeam2.Dispose();
        }
    }

    public class TheAddProtectedBranchTeamRestrictionsMethod : IDisposable
    {
        IRepositoryBranchesClient _client;
        OrganizationRepositoryWithTeamContext _orgRepoContext;
        TeamContext _contextOrgTeam2;
        IGitHubClient _github;

        public TheAddProtectedBranchTeamRestrictionsMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _client = _github.Repository.Branch;

            _contextOrgTeam2 = _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team2"))).Result;
            _orgRepoContext = _github.CreateOrganizationRepositoryWithProtectedBranch().Result;
        }

        [IntegrationTest]
        public async Task AddsProtectedBranchTeamRestrictionsForOrgRepo()
        {
            var repoOwner = _orgRepoContext.RepositoryContext.RepositoryOwner;
            var repoName = _orgRepoContext.RepositoryContext.RepositoryName;

            // Grant team push access to repo
            await _github.Organization.Team.AddRepository(
                _contextOrgTeam2.TeamId,
                repoOwner,
                repoName,
                new RepositoryPermissionRequest(Permission.Push));

            var newTeam = new BranchProtectionTeamCollection() { _contextOrgTeam2.TeamName };
            var restrictions = await _client.AddProtectedBranchTeamRestrictions(repoOwner, repoName, "master", newTeam);

            Assert.NotNull(restrictions);
            Assert.Equal(2, restrictions.Count);
        }

        [IntegrationTest]
        public async Task AddsProtectedBranchTeamRestrictionsForOrgRepoWithRepositoryId()
        {
            var repoId = _orgRepoContext.RepositoryContext.RepositoryId;
            var repoOwner = _orgRepoContext.RepositoryContext.RepositoryOwner;
            var repoName = _orgRepoContext.RepositoryContext.RepositoryName;

            // Grant team push access to repo
            await _github.Organization.Team.AddRepository(
                _contextOrgTeam2.TeamId,
                repoOwner,
                repoName,
                new RepositoryPermissionRequest(Permission.Push));

            var newTeam = new BranchProtectionTeamCollection() { _contextOrgTeam2.TeamName };
            var restrictions = await _client.AddProtectedBranchTeamRestrictions(repoId, "master", newTeam);

            Assert.NotNull(restrictions);
            Assert.Equal(2, restrictions.Count);
        }

        public void Dispose()
        {
            if (_orgRepoContext != null)
                _orgRepoContext.Dispose();

            if (_contextOrgTeam2 != null)
                _contextOrgTeam2.Dispose();
        }
    }

    public class TheDeleteProtectedBranchTeamRestrictions
    {
        IGitHubClient _github;
        IRepositoryBranchesClient _client;

        public TheDeleteProtectedBranchTeamRestrictions()
        {
            _github = Helper.GetAuthenticatedClient();
            _client = _github.Repository.Branch;
        }

        [IntegrationTest]
        public async Task DeletesRProtectedBranchTeamRestrictionsForOrgRepo()
        {
            using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
            {
                var repoOwner = context.RepositoryContext.RepositoryOwner;
                var repoName = context.RepositoryContext.RepositoryName;
                var teamToRemove = new BranchProtectionTeamCollection() { context.TeamContext.TeamName };
                var deleted = await _client.DeleteProtectedBranchTeamRestrictions(repoOwner, repoName, "master", teamToRemove);

                Assert.NotNull(deleted);
                Assert.Equal(0, deleted.Count);
            }
        }

        [IntegrationTest]
        public async Task DeletesProtectedBranchTeamRestrictionsForOrgRepoWithRepositoryId()
        {
            using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
            {
                var repoId = context.RepositoryContext.RepositoryId;
                var teamToRemove = new BranchProtectionTeamCollection() { context.TeamContext.TeamName };
                var deleted = await _client.DeleteProtectedBranchTeamRestrictions(repoId, "master", teamToRemove);

                Assert.NotNull(deleted);
                Assert.Equal(0, deleted.Count);
            }
        }
    }

    public class TheGetProtectedBranchUserRestrictionsMethod : IDisposable
    {
        IRepositoryBranchesClient _client;
        OrganizationRepositoryWithTeamContext _orgRepoContext;

        public TheGetProtectedBranchUserRestrictionsMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _client = github.Repository.Branch;

            _orgRepoContext = github.CreateOrganizationRepositoryWithProtectedBranch().Result;
        }

        [IntegrationTest]
        public async Task GetsProtectedBranchUserRestrictionsForOrgRepo()
        {
            var repoOwner = _orgRepoContext.RepositoryContext.RepositoryOwner;
            var repoName = _orgRepoContext.RepositoryContext.RepositoryName;
            var restrictions = await _client.GetProtectedBranchUserRestrictions(repoOwner, repoName, "master");

            Assert.NotNull(restrictions);
            Assert.Equal(0, restrictions.Count);
        }

        [IntegrationTest]
        public async Task GetsProtectedBranchUserRestrictionsForOrgRepoWithRepositoryId()
        {
            var repoId = _orgRepoContext.RepositoryContext.RepositoryId;
            var restrictions = await _client.GetProtectedBranchUserRestrictions(repoId, "master");

            Assert.NotNull(restrictions);
            Assert.Equal(0, restrictions.Count);
        }

        public void Dispose()
        {
            if (_orgRepoContext != null)
                _orgRepoContext.Dispose();
        }
    }

    public class TheUpdateProtectedBranchUserRestrictionsMethod : IDisposable
    {
        IRepositoryBranchesClient _client;
        IGitHubClient _github;
        OrganizationRepositoryWithTeamContext _orgRepoContext;

        public TheUpdateProtectedBranchUserRestrictionsMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _client = _github.Repository.Branch;

            _orgRepoContext = _github.CreateOrganizationRepositoryWithProtectedBranch().Result;
        }

        [IntegrationTest]
        public async Task UpdatesProtectedBranchUserRestrictionsForOrgRepo()
        {
            var repoOwner = _orgRepoContext.RepositoryContext.RepositoryOwner;
            var repoName = _orgRepoContext.RepositoryContext.RepositoryName;
            var newUser = new BranchProtectionUserCollection() { _github.User.Current().Result.Login };

            var restrictions = await _client.UpdateProtectedBranchUserRestrictions(repoOwner, repoName, "master", newUser);

            Assert.NotNull(restrictions);
            Assert.Equal(1, restrictions.Count);
        }

        [IntegrationTest]
        public async Task UpdatesProtectedBranchUserRestrictionsForOrgRepoWithRepositoryId()
        {
            var repoId = _orgRepoContext.RepositoryContext.RepositoryId;
            var newUser = new BranchProtectionUserCollection() { _github.User.Current().Result.Login };

            var restrictions = await _client.UpdateProtectedBranchUserRestrictions(repoId, "master", newUser);

            Assert.NotNull(restrictions);
            Assert.Equal(1, restrictions.Count);
        }

        public void Dispose()
        {
            if (_orgRepoContext != null)
                _orgRepoContext.Dispose();
        }
    }

    public class TheAddProtectedBranchUserRestrictionsMethod : IDisposable
    {
        IRepositoryBranchesClient _client;
        IGitHubClient _github;
        OrganizationRepositoryWithTeamContext _orgRepoContext;

        public TheAddProtectedBranchUserRestrictionsMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _client = _github.Repository.Branch;

            _orgRepoContext = _github.CreateOrganizationRepositoryWithProtectedBranch().Result;
        }

        [IntegrationTest]
        public async Task AddsProtectedBranchUserRestrictionsForOrgRepo()
        {
            var repoOwner = _orgRepoContext.RepositoryContext.RepositoryOwner;
            var repoName = _orgRepoContext.RepositoryContext.RepositoryName;
            var newUser = new BranchProtectionUserCollection() { _github.User.Current().Result.Login };

            var restrictions = await _client.AddProtectedBranchUserRestrictions(repoOwner, repoName, "master", newUser);

            Assert.NotNull(restrictions);
            Assert.Equal(1, restrictions.Count);
        }

        [IntegrationTest]
        public async Task AddsProtectedBranchUserRestrictionsForOrgRepoWithRepositoryId()
        {
            var repoId = _orgRepoContext.RepositoryContext.RepositoryId;
            var newUser = new BranchProtectionUserCollection() { _github.User.Current().Result.Login };

            var restrictions = await _client.AddProtectedBranchUserRestrictions(repoId, "master", newUser);

            Assert.NotNull(restrictions);
            Assert.Equal(1, restrictions.Count);
        }

        public void Dispose()
        {
            if (_orgRepoContext != null)
                _orgRepoContext.Dispose();
        }
    }

    public class TheDeleteProtectedBranchUserRestrictions
    {
        IGitHubClient _github;
        IRepositoryBranchesClient _client;

        public TheDeleteProtectedBranchUserRestrictions()
        {
            _github = Helper.GetAuthenticatedClient();
            _client = _github.Repository.Branch;
        }

        [IntegrationTest]
        public async Task DeletesProtectedBranchUserRestrictionsForOrgRepo()
        {
            using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
            {
                var repoOwner = context.RepositoryContext.RepositoryOwner;
                var repoName = context.RepositoryContext.RepositoryName;
                var user = new BranchProtectionUserCollection() { _github.User.Current().Result.Login };
                var restrictions = await _client.AddProtectedBranchUserRestrictions(repoOwner, repoName, "master", user);

                Assert.NotNull(restrictions);
                Assert.Equal(1, restrictions.Count);

                var deleted = await _client.DeleteProtectedBranchUserRestrictions(repoOwner, repoName, "master", user);

                Assert.NotNull(deleted);
                Assert.Equal(0, deleted.Count);
            }
        }

        [IntegrationTest]
        public async Task DeletesProtectedBranchUserRestrictionsForOrgRepoWithRepositoryId()
        {
            using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
            {
                var repoId = context.RepositoryContext.RepositoryId;
                var user = new BranchProtectionUserCollection() { _github.User.Current().Result.Login };
                var restrictions = await _client.AddProtectedBranchUserRestrictions(repoId, "master", user);

                Assert.NotNull(restrictions);
                Assert.Equal(1, restrictions.Count);

                var deleted = await _client.DeleteProtectedBranchUserRestrictions(repoId, "master", user);

                Assert.NotNull(deleted);
                Assert.Equal(0, deleted.Count);
            }
        }
    }
}
