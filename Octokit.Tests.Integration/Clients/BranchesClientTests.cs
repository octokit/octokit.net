using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class BranchesClientTests
{
    public class TheGetBranchesMethod
    {
        public TheGetBranchesMethod() { }

        [IntegrationTest]
        public async Task ReturnsBranches()
        {
            var github = Helper.GetAuthenticatedClient();

            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                var branches = await github.Repository.GetAllBranches(context.Repository.Owner.Login, context.Repository.Name);

                Assert.NotEmpty(branches);
                Assert.Equal(branches[0].Name, "master");
                Assert.NotNull(branches[0].Protection);
            }
        }
    }

    public class TheEditBranchesMethod
    {
        private readonly IRepositoriesClient _fixture;
        private readonly RepositoryContext _context;

        public TheEditBranchesMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _context = github.CreateRepositoryContext("source-repo").Result;
            _fixture = github.Repository;
        }

        public async Task CreateTheWorld()
        {
            // Set master branch to be protected, with some status checks
            var update = new BranchUpdate();
            update.Protection.Enabled = true;
            update.Protection.RequiredStatusChecks.EnforcementLevel = EnforcementLevel.Everyone;
            update.Protection.RequiredStatusChecks.AddContext("check1");
            update.Protection.RequiredStatusChecks.AddContext("check2");

            var newBranch = await _fixture.EditBranch(_context.Repository.Owner.Login, _context.Repository.Name, "master", update);
        }

        [IntegrationTest]
        public async Task ProtectsBranch()
        {
            // Set master branch to be protected, with some status checks
            var update = new BranchUpdate();
            update.Protection.Enabled = true;
            update.Protection.RequiredStatusChecks.EnforcementLevel = EnforcementLevel.Everyone;
            update.Protection.RequiredStatusChecks.AddContext("check1");
            update.Protection.RequiredStatusChecks.AddContext("check2");
            update.Protection.RequiredStatusChecks.AddContext("check3");

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

            // Clear status checks
            var update = new BranchUpdate();
            update.Protection.Enabled = true;
            update.Protection.RequiredStatusChecks.EnforcementLevel = EnforcementLevel.Off;
            update.Protection.RequiredStatusChecks.AddContext("check1");
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
            var update = new BranchUpdate();
            update.Protection.Enabled = false;

            // Deliberately set Enforcement and Contexts to some values (these should be ignored)
            update.Protection.RequiredStatusChecks.EnforcementLevel = EnforcementLevel.Everyone;
            update.Protection.RequiredStatusChecks.AddContext("check1");
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