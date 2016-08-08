using System.Collections.Generic;
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

            // Ensure Protection attribute is deserialized
            foreach (var branch in branches)
            {
                Assert.NotNull(branch.Protection);
            }
        }

        [IntegrationTest]
        public async Task GetsAllBranchesWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var branches = await github.Repository.Branch.GetAll(7528679);

            Assert.NotEmpty(branches);
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
        }

        [IntegrationTest]
        public async Task GetsABranchWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();

            var branch = await github.Repository.Branch.Get(7528679, "master");

            Assert.NotNull(branch);
            Assert.Equal("master", branch.Name);
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
}
