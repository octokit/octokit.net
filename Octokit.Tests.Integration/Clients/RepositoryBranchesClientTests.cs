using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit.Helpers;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class RepositoryBranchesClientTests
    {
        public class TheGetAllMethod : GitHubClientTestBase
        {
            [IntegrationTest]
            public async Task GetsAllBranches()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-1", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-2", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-3", repoContext.Repository.DefaultBranch);

                    var branches = await _github.Repository.Branch.GetAll(repoContext.RepositoryOwner, repoContext.RepositoryName);

                    Assert.NotEmpty(branches);
                }
            }

            [IntegrationTest]
            public async Task GetsAllBranchesWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-1", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-2", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-3", repoContext.Repository.DefaultBranch);

                    var branches = await _github.Repository.Branch.GetAll(repoContext.RepositoryId);

                    Assert.NotEmpty(branches);
                }
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfBranchesWithoutStart()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-1", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-2", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-3", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-4", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-5", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-6", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-7", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-8", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-9", repoContext.Repository.DefaultBranch);

                    var options = new ApiOptions
                    {
                        PageSize = 5,
                        PageCount = 1
                    };

                    var branches = await _github.Repository.Branch.GetAll(repoContext.RepositoryOwner, repoContext.RepositoryName, options);

                    Assert.Equal(5, branches.Count);
                }
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfBranchesWithoutStartWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-1", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-2", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-3", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-4", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-5", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-6", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-7", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-8", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-9", repoContext.Repository.DefaultBranch);

                    var options = new ApiOptions
                    {
                        PageSize = 5,
                        PageCount = 1
                    };

                    var branches = await _github.Repository.Branch.GetAll(repoContext.RepositoryId, options);

                    Assert.Equal(5, branches.Count);
                }
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfBranchesWithStart()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-1", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-2", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-3", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-4", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-5", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-6", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-7", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-8", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-9", repoContext.Repository.DefaultBranch);

                    var options = new ApiOptions
                    {
                        PageSize = 5,
                        PageCount = 1,
                        StartPage = 2
                    };

                    var branches = await _github.Repository.Branch.GetAll(repoContext.RepositoryOwner, repoContext.RepositoryName, options);

                    Assert.Equal(5, branches.Count);
                }
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfBranchesWithStartWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-1", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-2", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-3", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-4", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-5", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-6", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-7", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-8", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-9", repoContext.Repository.DefaultBranch);

                    var options = new ApiOptions
                    {
                        PageSize = 5,
                        PageCount = 1,
                        StartPage = 2
                    };

                    var branches = await _github.Repository.Branch.GetAll(repoContext.RepositoryId, options);

                    Assert.Equal(5, branches.Count);
                }
            }

            [IntegrationTest]
            public async Task GetsPagesOfBranches()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-1", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-2", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-3", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-4", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-5", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-6", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-7", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-8", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-9", repoContext.Repository.DefaultBranch);

                    var firstPageOptions = new ApiOptions
                    {
                        PageSize = 5,
                        StartPage = 1,
                        PageCount = 1
                    };

                    var firstPage = await _github.Repository.Branch.GetAll(repoContext.RepositoryOwner, repoContext.RepositoryName, firstPageOptions);

                    var secondPageOptions = new ApiOptions
                    {
                        PageSize = 5,
                        StartPage = 2,
                        PageCount = 1
                    };

                    var secondPage = await _github.Repository.Branch.GetAll(repoContext.RepositoryOwner, repoContext.RepositoryName, secondPageOptions);

                    Assert.Equal(5, firstPage.Count);
                    Assert.Equal(5, secondPage.Count);

                    Assert.NotEqual(firstPage[0].Name, secondPage[0].Name);
                    Assert.NotEqual(firstPage[1].Name, secondPage[1].Name);
                    Assert.NotEqual(firstPage[2].Name, secondPage[2].Name);
                    Assert.NotEqual(firstPage[3].Name, secondPage[3].Name);
                    Assert.NotEqual(firstPage[4].Name, secondPage[4].Name);
                }
            }

            [IntegrationTest]
            public async Task GetsPagesOfBranchesWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-1", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-2", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-3", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-4", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-5", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-6", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-7", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-8", repoContext.Repository.DefaultBranch);
                    await _github.Git.Reference.CreateBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, "patch-9", repoContext.Repository.DefaultBranch);

                    var firstPageOptions = new ApiOptions
                    {
                        PageSize = 5,
                        StartPage = 1,
                        PageCount = 1
                    };

                    var firstPage = await _github.Repository.Branch.GetAll(repoContext.RepositoryId, firstPageOptions);

                    var secondPageOptions = new ApiOptions
                    {
                        PageSize = 5,
                        StartPage = 2,
                        PageCount = 1
                    };

                    var secondPage = await _github.Repository.Branch.GetAll(repoContext.RepositoryId, secondPageOptions);

                    Assert.Equal(5, firstPage.Count);
                    Assert.Equal(5, secondPage.Count);

                    Assert.NotEqual(firstPage[0].Name, secondPage[0].Name);
                    Assert.NotEqual(firstPage[1].Name, secondPage[1].Name);
                    Assert.NotEqual(firstPage[2].Name, secondPage[2].Name);
                    Assert.NotEqual(firstPage[3].Name, secondPage[3].Name);
                    Assert.NotEqual(firstPage[4].Name, secondPage[4].Name);
                }
            }
        }

        public class TheGetMethod : GitHubClientTestBase
        {
            [IntegrationTest]
            public async Task GetsABranch()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var branch = await _github.Repository.Branch.Get(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.NotNull(branch);
                    Assert.Equal(repoContext.RepositoryDefaultBranch, branch.Name);

                    Assert.True(branch.Protected);
                }
            }

            [IntegrationTest]
            public async Task GetsABranchWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var branch = await _github.Repository.Branch.Get(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.NotNull(branch);
                    Assert.Equal(repoContext.RepositoryDefaultBranch, branch.Name);

                    Assert.True(branch.Protected);
                }
            }
        }

        public class TheGetBranchProtectionMethod : GitHubClientTestBase
        {
            [IntegrationTest]
            public async Task GetsBranchProtection()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var protection = await _github.Repository.Branch.GetBranchProtection(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.True(protection.RequiredStatusChecks.Strict);
                    Assert.Equal(2, protection.RequiredStatusChecks.Contexts.Count);

                    Assert.Null(protection.RequiredPullRequestReviews.DismissalRestrictions);
                    Assert.True(protection.RequiredPullRequestReviews.DismissStaleReviews);
                    Assert.True(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);

                    Assert.Null(protection.Restrictions);

                    Assert.True(protection.EnforceAdmins.Enabled);
                    Assert.True(protection.RequiredLinearHistory.Enabled);
                    Assert.True(protection.AllowForcePushes.Enabled);
                    Assert.True(protection.AllowDeletions.Enabled);
                    Assert.False(protection.BlockCreations.Enabled);
                    Assert.True(protection.RequiredConversationResolution.Enabled);
                }
            }

            [IntegrationTest]
            public async Task GetsBranchProtectionWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var protection = await _github.Repository.Branch.GetBranchProtection(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.True(protection.RequiredStatusChecks.Strict);
                    Assert.Equal(2, protection.RequiredStatusChecks.Contexts.Count);

                    Assert.Null(protection.RequiredPullRequestReviews.DismissalRestrictions);
                    Assert.True(protection.RequiredPullRequestReviews.DismissStaleReviews);
                    Assert.True(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);

                    Assert.Null(protection.Restrictions);

                    Assert.True(protection.EnforceAdmins.Enabled);
                    Assert.True(protection.RequiredLinearHistory.Enabled);
                    Assert.True(protection.AllowForcePushes.Enabled);
                    Assert.True(protection.AllowDeletions.Enabled);
                    Assert.False(protection.BlockCreations.Enabled);
                    Assert.True(protection.RequiredConversationResolution.Enabled);
                }
            }

            [OrganizationTest]
            public async Task GetsBranchProtectionForOrgRepo()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var protection = await _github.Repository.Branch.GetBranchProtection(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.True(protection.RequiredStatusChecks.Strict);
                    Assert.Equal(2, protection.RequiredStatusChecks.Contexts.Count);

                    Assert.Equal(1, protection.RequiredPullRequestReviews.DismissalRestrictions.Teams.Count);
                    Assert.Equal(0, protection.RequiredPullRequestReviews.DismissalRestrictions.Users.Count);
                    Assert.True(protection.RequiredPullRequestReviews.DismissStaleReviews);
                    Assert.True(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);

                    Assert.Equal(1, protection.Restrictions.Teams.Count);
                    Assert.Equal(0, protection.Restrictions.Users.Count);

                    Assert.True(protection.EnforceAdmins.Enabled);
                }
            }

            [OrganizationTest]
            public async Task GetsBranchProtectionForOrgRepoWithRepositoryId()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var protection = await _github.Repository.Branch.GetBranchProtection(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.True(protection.RequiredStatusChecks.Strict);
                    Assert.Equal(2, protection.RequiredStatusChecks.Contexts.Count);

                    Assert.Equal(1, protection.RequiredPullRequestReviews.DismissalRestrictions.Teams.Count);
                    Assert.Equal(0, protection.RequiredPullRequestReviews.DismissalRestrictions.Users.Count);
                    Assert.True(protection.RequiredPullRequestReviews.DismissStaleReviews);
                    Assert.True(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);

                    Assert.Equal(1, protection.Restrictions.Teams.Count);
                    Assert.Equal(0, protection.Restrictions.Users.Count);

                    Assert.True(protection.EnforceAdmins.Enabled);
                }
            }
        }

        public class TheUpdateBranchProtectionMethod : GitHubClientTestBase
        {
            [IntegrationTest]
            public async Task UpdatesBranchProtection()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var update = new BranchProtectionSettingsUpdate(
                        new BranchProtectionRequiredStatusChecksUpdate(false, new[] { "new" }),
                        new BranchProtectionRequiredReviewsUpdate(false, true, 2),
                        false);

                    var protection = await _github.Repository.Branch.UpdateBranchProtection(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch, update);

                    Assert.False(protection.RequiredStatusChecks.Strict);
                    Assert.Equal(1, protection.RequiredStatusChecks.Contexts.Count);

                    Assert.Null(protection.RequiredPullRequestReviews.DismissalRestrictions);
                    Assert.False(protection.RequiredPullRequestReviews.DismissStaleReviews);
                    Assert.True(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);
                    Assert.Equal(2, protection.RequiredPullRequestReviews.RequiredApprovingReviewCount);

                    Assert.Null(protection.Restrictions);

                    Assert.False(protection.EnforceAdmins.Enabled);
                }
            }

            [IntegrationTest]
            public async Task UpdatesBranchProtectionWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var update = new BranchProtectionSettingsUpdate(
                        new BranchProtectionRequiredStatusChecksUpdate(false, new[] { "new" }),
                        new BranchProtectionRequiredReviewsUpdate(false, true, 2),
                        false);

                    var protection = await _github.Repository.Branch.UpdateBranchProtection(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch, update);

                    Assert.False(protection.RequiredStatusChecks.Strict);
                    Assert.Equal(1, protection.RequiredStatusChecks.Contexts.Count);

                    Assert.Null(protection.RequiredPullRequestReviews.DismissalRestrictions);
                    Assert.False(protection.RequiredPullRequestReviews.DismissStaleReviews);
                    Assert.True(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);
                    Assert.Equal(2, protection.RequiredPullRequestReviews.RequiredApprovingReviewCount);

                    Assert.Null(protection.Restrictions);

                    Assert.False(protection.EnforceAdmins.Enabled);
                }
            }

            [OrganizationTest]
            public async Task UpdatesBranchProtectionForOrgRepo()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var update = new BranchProtectionSettingsUpdate(
                        new BranchProtectionRequiredStatusChecksUpdate(false, new[] { "new" }),
                        new BranchProtectionRequiredReviewsUpdate(new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(false), false, false, 2),
                        new BranchProtectionPushRestrictionsUpdate(),
                        false);

                    var protection = await _github.Repository.Branch.UpdateBranchProtection(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch, update);

                    Assert.False(protection.RequiredStatusChecks.Strict);
                    Assert.Equal(1, protection.RequiredStatusChecks.Contexts.Count);

                    Assert.Null(protection.RequiredPullRequestReviews.DismissalRestrictions);
                    Assert.False(protection.RequiredPullRequestReviews.DismissStaleReviews);
                    Assert.False(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);
                    Assert.Equal(2, protection.RequiredPullRequestReviews.RequiredApprovingReviewCount);

                    Assert.Empty(protection.Restrictions.Teams);
                    Assert.Empty(protection.Restrictions.Users);

                    Assert.False(protection.EnforceAdmins.Enabled);
                }
            }

            [OrganizationTest]
            public async Task UpdatesBranchProtectionForOrgRepoWithRepositoryId()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);
                    var update = new BranchProtectionSettingsUpdate(
                    new BranchProtectionRequiredStatusChecksUpdate(false, new[] { "new" }),
                    new BranchProtectionRequiredReviewsUpdate(new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(false), false, false, 2),
                    new BranchProtectionPushRestrictionsUpdate(),
                    false);

                    var protection = await _github.Repository.Branch.UpdateBranchProtection(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch, update);

                    Assert.False(protection.RequiredStatusChecks.Strict);
                    Assert.Equal(1, protection.RequiredStatusChecks.Contexts.Count);

                    Assert.Null(protection.RequiredPullRequestReviews.DismissalRestrictions);
                    Assert.False(protection.RequiredPullRequestReviews.DismissStaleReviews);
                    Assert.False(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);
                    Assert.Equal(2, protection.RequiredPullRequestReviews.RequiredApprovingReviewCount);

                    Assert.Empty(protection.Restrictions.Teams);
                    Assert.Empty(protection.Restrictions.Users);

                    Assert.False(protection.EnforceAdmins.Enabled);
                }
            }
        }

        public class TheDeleteBranchProtectionMethod : GitHubClientTestBase
        {
            [IntegrationTest]
            public async Task DeletesBranchProtection()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var deleted = await _github.Repository.Branch.DeleteBranchProtection(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.True(deleted);
                }
            }

            [IntegrationTest]
            public async Task DeletesBranchProtectionWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var deleted = await _github.Repository.Branch.DeleteBranchProtection(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.True(deleted);
                }
            }

            [OrganizationTest]
            public async Task DeletesBranchProtectionForOrgRepo()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var deleted = await _github.Repository.Branch.DeleteBranchProtection(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.True(deleted);
                }
            }

            [OrganizationTest]
            public async Task DeletesBranchProtectionForOrgRepoWithRepositoryId()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var deleted = await _github.Repository.Branch.DeleteBranchProtection(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.True(deleted);
                }
            }
        }

        public class TheGetRequiredStatusChecksMethod : GitHubClientTestBase
        {
            [IntegrationTest]
            public async Task GetsRequiredStatusChecks()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);
                    var requiredStatusChecks = await _github.Repository.Branch.GetRequiredStatusChecks(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.NotNull(requiredStatusChecks);
                    Assert.NotNull(requiredStatusChecks.Contexts);
                    Assert.True(requiredStatusChecks.Strict);
                    Assert.Equal(2, requiredStatusChecks.Contexts.Count);
                }
            }

            [IntegrationTest]
            public async Task GetsRequiredStatusChecksWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);
                    var requiredStatusChecks = await _github.Repository.Branch.GetRequiredStatusChecks(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.NotNull(requiredStatusChecks);
                    Assert.NotNull(requiredStatusChecks.Contexts);
                    Assert.True(requiredStatusChecks.Strict);
                    Assert.Equal(2, requiredStatusChecks.Contexts.Count);
                }
            }
        }

        public class TheUpdateRequiredStatusChecksMethod : GitHubClientTestBase
        {
            [IntegrationTest]
            public async Task UpdateRequiredStatusChecks()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var update = new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "new" });
                    var requiredStatusChecks = await _github.Repository.Branch.UpdateRequiredStatusChecks(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch, update);

                    Assert.NotNull(requiredStatusChecks);
                    Assert.NotNull(requiredStatusChecks.Contexts);
                    Assert.Contains("new", requiredStatusChecks.Contexts);
                    Assert.True(requiredStatusChecks.Strict);
                    Assert.Equal(1, requiredStatusChecks.Contexts.Count);
                }
            }

            [IntegrationTest]
            public async Task UpdatesRequiredStatusChecksWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var update = new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "new" });
                    var requiredStatusChecks = await _github.Repository.Branch.UpdateRequiredStatusChecks(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch, update);

                    Assert.NotNull(requiredStatusChecks);
                    Assert.NotNull(requiredStatusChecks.Contexts);
                    Assert.Contains("new", requiredStatusChecks.Contexts);
                    Assert.True(requiredStatusChecks.Strict);
                    Assert.Equal(1, requiredStatusChecks.Contexts.Count);
                }
            }
        }

        public class TheDeleteRequiredStatusChecksMethod : GitHubClientTestBase
        {
            [IntegrationTest]
            public async Task DeletesRequiredStatusChecks()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var deleted = await _github.Repository.Branch.DeleteRequiredStatusChecks(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.True(deleted);
                }
            }

            [IntegrationTest]
            public async Task DeletesRequiredStatusChecksWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var deleted = await _github.Repository.Branch.DeleteRequiredStatusChecks(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.True(deleted);
                }
            }
        }

        public class TheGetAllRequiredStatusChecksContextsMethod : GitHubClientTestBase
        {
            [IntegrationTest]
            public async Task GetsRequiredStatusChecksContexts()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var requiredStatusChecksContexts = await _github.Repository.Branch.GetAllRequiredStatusChecksContexts(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.NotNull(requiredStatusChecksContexts);
                    Assert.Equal(2, requiredStatusChecksContexts.Count);
                }
            }

            [IntegrationTest]
            public async Task GetsRequiredStatusChecksContextsWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var requiredStatusChecksContexts = await _github.Repository.Branch.GetAllRequiredStatusChecksContexts(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.NotNull(requiredStatusChecksContexts);
                    Assert.Equal(2, requiredStatusChecksContexts.Count);
                }
            }
        }

        public class TheUpdateRequiredStatusChecksContextsMethod : GitHubClientTestBase
        {
            [IntegrationTest]
            public async Task UpdateRequiredStatusChecksContexts()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var update = new List<string>() { "build2" };
                    var requiredStatusChecksContexts = await _github.Repository.Branch.UpdateRequiredStatusChecksContexts(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch, update);

                    Assert.Equal(1, requiredStatusChecksContexts.Count);
                }
            }

            [IntegrationTest]
            public async Task UpdatesRequiredStatusChecksContextsWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var update = new List<string>() { "build2" };
                    var requiredStatusChecksContexts = await _github.Repository.Branch.UpdateRequiredStatusChecksContexts(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch, update);

                    Assert.Equal(1, requiredStatusChecksContexts.Count);
                }
            }
        }

        public class TheAddRequiredStatusChecksContextsMethod : GitHubClientTestBase
        {
            [IntegrationTest]
            public async Task AddsRequiredStatusChecksContexts()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var update = new List<string>() { "build2", "deploy" };
                    var requiredStatusChecksContexts = await _github.Repository.Branch.AddRequiredStatusChecksContexts(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch, update);

                    Assert.NotNull(requiredStatusChecksContexts);
                    Assert.Equal(4, requiredStatusChecksContexts.Count);
                }
            }

            [IntegrationTest]
            public async Task AddsRequiredStatusChecksContextsWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var update = new List<string>() { "build2", "deploy" };
                    var requiredStatusChecksContexts = await _github.Repository.Branch.AddRequiredStatusChecksContexts(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch, update);

                    Assert.NotNull(requiredStatusChecksContexts);
                    Assert.Equal(4, requiredStatusChecksContexts.Count);
                }
            }
        }

        public class TheDeleteRequiredStatusChecksContextsMethod : GitHubClientTestBase
        {
            [IntegrationTest]
            public async Task DeletesRequiredStatusChecksContexts()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var contextsToRemove = new List<string>() { "build" };
                    var deleted = await _github.Repository.Branch.DeleteRequiredStatusChecksContexts(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch, contextsToRemove);

                    Assert.NotNull(deleted);
                    Assert.Contains("test", deleted);
                }
            }

            [IntegrationTest]
            public async Task DeletesRequiredStatusChecksContextsWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var contextsToRemove = new List<string>() { "build" };
                    var deleted = await _github.Repository.Branch.DeleteRequiredStatusChecksContexts(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch, contextsToRemove);

                    Assert.NotNull(deleted);
                    Assert.Contains("test", deleted);
                }
            }
        }

        public class TheGetReviewEnforcementMethod : GitHubClientTestBase
        {
            [IntegrationTest]
            public async Task GetsReviewEnforcement()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var requiredReviews = await _github.Repository.Branch.GetReviewEnforcement(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.Null(requiredReviews.DismissalRestrictions);
                    Assert.True(requiredReviews.DismissStaleReviews);
                    Assert.True(requiredReviews.RequireCodeOwnerReviews);
                }
            }

            [IntegrationTest]
            public async Task GetsReviewEnforcementWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var requiredReviews = await _github.Repository.Branch.GetReviewEnforcement(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.Null(requiredReviews.DismissalRestrictions);
                    Assert.True(requiredReviews.DismissStaleReviews);
                    Assert.True(requiredReviews.RequireCodeOwnerReviews);
                }
            }

            [OrganizationTest]
            public async Task GetsReviewEnforcementForOrgRepo()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var requiredReviews = await _github.Repository.Branch.GetReviewEnforcement(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.Equal(1, requiredReviews.DismissalRestrictions.Teams.Count);
                    Assert.Equal(0, requiredReviews.DismissalRestrictions.Users.Count);
                    Assert.True(requiredReviews.DismissStaleReviews);
                    Assert.True(requiredReviews.RequireCodeOwnerReviews);
                }
            }

            [OrganizationTest]
            public async Task GetsReviewEnforcementForOrgRepoWithRepositoryId()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var requiredReviews = await _github.Repository.Branch.GetReviewEnforcement(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.Equal(1, requiredReviews.DismissalRestrictions.Teams.Count);
                    Assert.Equal(0, requiredReviews.DismissalRestrictions.Users.Count);
                    Assert.True(requiredReviews.DismissStaleReviews);
                    Assert.True(requiredReviews.RequireCodeOwnerReviews);
                }
            }
        }

        public class TheUpdateReviewEnforcementMethod : GitHubClientTestBase
        {
            [IntegrationTest]
            public async Task UpdatesReviewEnforcement()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var update = new BranchProtectionRequiredReviewsUpdate(false, true, 2);

                    var requiredReviews = await _github.Repository.Branch.UpdateReviewEnforcement(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch, update);

                    Assert.Null(requiredReviews.DismissalRestrictions);
                    Assert.False(requiredReviews.DismissStaleReviews);
                    Assert.True(requiredReviews.RequireCodeOwnerReviews);
                    Assert.Equal(2, requiredReviews.RequiredApprovingReviewCount);
                }
            }

            [IntegrationTest]
            public async Task UpdatesReviewEnforcementWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var update = new BranchProtectionRequiredReviewsUpdate(false, true, 2);

                    var requiredReviews = await _github.Repository.Branch.UpdateReviewEnforcement(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch, update);

                    Assert.Null(requiredReviews.DismissalRestrictions);
                    Assert.False(requiredReviews.DismissStaleReviews);
                    Assert.True(requiredReviews.RequireCodeOwnerReviews);
                    Assert.Equal(2, requiredReviews.RequiredApprovingReviewCount);
                }
            }

            [OrganizationTest]
            public async Task UpdatesReviewEnforcementForOrgRepo()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var update = new BranchProtectionRequiredReviewsUpdate(
                    new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(false),
                    false,
                    false,
                    2);

                    var requiredReviews = await _github.Repository.Branch.UpdateReviewEnforcement(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch, update);

                    Assert.Null(requiredReviews.DismissalRestrictions);
                    Assert.False(requiredReviews.DismissStaleReviews);
                    Assert.False(requiredReviews.RequireCodeOwnerReviews);
                    Assert.Equal(2, requiredReviews.RequiredApprovingReviewCount);
                }
            }

            [OrganizationTest]
            public async Task UpdatesReviewEnforcementForOrgRepoWithRepositoryId()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var update = new BranchProtectionRequiredReviewsUpdate(
                    new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(false),
                    false,
                    false,
                    2);

                    var requiredReviews = await _github.Repository.Branch.UpdateReviewEnforcement(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch, update);

                    Assert.Null(requiredReviews.DismissalRestrictions);
                    Assert.False(requiredReviews.DismissStaleReviews);
                    Assert.False(requiredReviews.RequireCodeOwnerReviews);
                    Assert.Equal(2, requiredReviews.RequiredApprovingReviewCount);
                }
            }

            [OrganizationTest]
            public async Task UpdatesReviewEnforcementForOrgRepoWithAdminOnly()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var update = new BranchProtectionRequiredReviewsUpdate(
                    new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(true),
                    false,
                    false,
                    2);

                    var requiredReviews = await _github.Repository.Branch.UpdateReviewEnforcement(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch, update);

                    Assert.Empty(requiredReviews.DismissalRestrictions.Teams);
                    Assert.Empty(requiredReviews.DismissalRestrictions.Users);
                    Assert.False(requiredReviews.DismissStaleReviews);
                    Assert.False(requiredReviews.RequireCodeOwnerReviews);
                    Assert.Equal(2, requiredReviews.RequiredApprovingReviewCount);
                }
            }

            [OrganizationTest]
            public async Task UpdatesReviewEnforcementForOrgRepoWithAdminOnlyWithRepositoryId()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var update = new BranchProtectionRequiredReviewsUpdate(
                    new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(true),
                    false,
                    false,
                    2);

                    var requiredReviews = await _github.Repository.Branch.UpdateReviewEnforcement(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch, update);

                    Assert.Empty(requiredReviews.DismissalRestrictions.Teams);
                    Assert.Empty(requiredReviews.DismissalRestrictions.Users);
                    Assert.False(requiredReviews.DismissStaleReviews);
                    Assert.False(requiredReviews.RequireCodeOwnerReviews);
                    Assert.Equal(2, requiredReviews.RequiredApprovingReviewCount);
                }
            }
        }

        public class TheRemoveReviewEnforcementMethod : GitHubClientTestBase
        {
            [IntegrationTest]
            public async Task RemovesReviewEnforcement()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var deleted = await _github.Repository.Branch.RemoveReviewEnforcement(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.True(deleted);
                }
            }

            [IntegrationTest]
            public async Task RemovesReviewEnforcementWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var deleted = await _github.Repository.Branch.RemoveReviewEnforcement(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.True(deleted);
                }
            }

            [OrganizationTest]
            public async Task RemovesReviewEnforcementForOrgRepo()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var deleted = await _github.Repository.Branch.RemoveReviewEnforcement(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.True(deleted);
                }
            }

            [OrganizationTest]
            public async Task RemovesReviewEnforcementForOrgRepoWithRepositoryId()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var deleted = await _github.Repository.Branch.RemoveReviewEnforcement(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.True(deleted);
                }
            }
        }

        public class TheGetAdminEnforcementMethod : GitHubClientTestBase
        {
            [IntegrationTest]
            public async Task GetsAdminEnforcement()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var enforceAdmins = await _github.Repository.Branch.GetAdminEnforcement(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.NotNull(enforceAdmins);
                    Assert.True(enforceAdmins.Enabled);
                }
            }

            [IntegrationTest]
            public async Task GetsAdminEnforcementWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var enforceAdmins = await _github.Repository.Branch.GetAdminEnforcement(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.NotNull(enforceAdmins);
                    Assert.True(enforceAdmins.Enabled);
                }
            }
        }

        public class TheAddAdminEnforcementMethod : GitHubClientTestBase
        {
            [IntegrationTest]
            public async Task AddsAdminEnforcement()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    await _github.Repository.Branch.RemoveAdminEnforcement(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);
                    var enforceAdmins = await _github.Repository.Branch.AddAdminEnforcement(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.NotNull(enforceAdmins);
                    Assert.True(enforceAdmins.Enabled);
                }
            }

            [IntegrationTest]
            public async Task AddsAdminEnforcementoWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    await _github.Repository.Branch.RemoveAdminEnforcement(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);
                    var enforceAdmins = await _github.Repository.Branch.AddAdminEnforcement(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.NotNull(enforceAdmins);
                    Assert.True(enforceAdmins.Enabled);
                }
            }
        }

        public class TheRemoveAdminEnforcementMethod : GitHubClientTestBase
        {
            [IntegrationTest]
            public async Task RemovesAdminEnforcement()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var deleted = await _github.Repository.Branch.RemoveAdminEnforcement(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.True(deleted);

                    var enforceAdmins = await _github.Repository.Branch.GetAdminEnforcement(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.NotNull(enforceAdmins);
                    Assert.False(enforceAdmins.Enabled);
                }
            }

            [IntegrationTest]
            public async Task RemovesAdminEnforcementWithRepositoryId()
            {
                using (var repoContext = await _github.CreateUserRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranch(repoContext);

                    var deleted = await _github.Repository.Branch.RemoveAdminEnforcement(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.True(deleted);

                    var enforceAdmins = await _github.Repository.Branch.GetAdminEnforcement(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.NotNull(enforceAdmins);
                    Assert.False(enforceAdmins.Enabled);
                }
            }
        }

        public class TheGetProtectedBranchRestrictionsMethod : GitHubClientTestBase
        {
            [OrganizationTest]
            public async Task GetsProtectedBranchRestrictionsForOrgRepo()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var restrictions = await _github.Repository.Branch.GetProtectedBranchRestrictions(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.Equal(1, restrictions.Teams.Count);
                    Assert.Equal(0, restrictions.Users.Count);
                }
            }

            [OrganizationTest]
            public async Task GetsProtectedBranchRestrictionsForOrgRepoWithRepositoryId()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var restrictions = await _github.Repository.Branch.GetProtectedBranchRestrictions(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.Equal(1, restrictions.Teams.Count);
                    Assert.Equal(0, restrictions.Users.Count);
                }
            }
        }

        public class TheDeleteProtectedBranchRestrictionsMethod : GitHubClientTestBase
        {
            [OrganizationTest]
            public async Task DeletesRProtectedBranchRestrictionsForOrgRepo()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var deleted = await _github.Repository.Branch.DeleteProtectedBranchRestrictions(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.True(deleted);
                }
            }

            [OrganizationTest]
            public async Task DeletesProtectedBranchRestrictionsForOrgRepoWithRepositoryId()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var deleted = await _github.Repository.Branch.DeleteProtectedBranchRestrictions(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.True(deleted);
                }
            }
        }

        public class TheGetAllProtectedBranchTeamRestrictionsMethod : GitHubClientTestBase
        {
            [OrganizationTest]
            public async Task GetsProtectedBranchTeamRestrictionsForOrgRepo()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var restrictions = await _github.Repository.Branch.GetAllProtectedBranchTeamRestrictions(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.NotNull(restrictions);
                    Assert.Equal(1, restrictions.Count);
                }
            }

            [OrganizationTest]
            public async Task GetsProtectedBranchTeamRestrictionsForOrgRepoWithRepositoryId()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var restrictions = await _github.Repository.Branch.GetAllProtectedBranchTeamRestrictions(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.NotNull(restrictions);
                    Assert.Equal(1, restrictions.Count);
                }
            }
        }

        public class TheUpdateProtectedBranchTeamRestrictionsMethod : GitHubClientTestBase
        {
            [OrganizationTest]
            public async Task UpdatesProtectedBranchTeamRestrictionsForOrgRepo()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var team2 = _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team2"))).Result;

                    // Grant team push access to repo
                    await _github.Organization.Team.AddRepository(
                        team2.TeamId,
                        repoContext.RepositoryOwner,
                        repoContext.RepositoryName,
                        new RepositoryPermissionRequest(TeamPermissionLegacy.Push));

                    var newTeam = new BranchProtectionTeamCollection() { team2.TeamName };
                    var restrictions = await _github.Repository.Branch.UpdateProtectedBranchTeamRestrictions(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch, newTeam);

                    Assert.NotNull(restrictions);
                    Assert.Equal(team2.TeamName, restrictions[0].Name);
                }
            }

            [OrganizationTest]
            public async Task UpdatesProtectedBranchTeamRestrictionsForOrgRepoWithRepositoryId()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var team2 = _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team2"))).Result;

                    // Grant team push access to repo
                    await _github.Organization.Team.AddRepository(
                     team2.TeamId,
                    repoContext.RepositoryOwner,
                    repoContext.RepositoryName,
                    new RepositoryPermissionRequest(TeamPermissionLegacy.Push));

                    var newTeam = new BranchProtectionTeamCollection() { team2.TeamName };
                    var restrictions = await _github.Repository.Branch.UpdateProtectedBranchTeamRestrictions(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch, newTeam);

                    Assert.NotNull(restrictions);
                    Assert.Equal(1, restrictions.Count);
                }
            }
        }

        public class TheAddProtectedBranchTeamRestrictionsMethod : GitHubClientTestBase
        {
            [OrganizationTest]
            public async Task AddsProtectedBranchTeamRestrictionsForOrgRepo()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var team2 = _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team2"))).Result;

                    // Grant team push access to repo
                    await _github.Organization.Team.AddRepository(
                    team2.TeamId,
                    repoContext.RepositoryOwner,
                    repoContext.RepositoryName,
                    new RepositoryPermissionRequest(TeamPermissionLegacy.Push));

                    var newTeam = new BranchProtectionTeamCollection() { team2.TeamName };
                    var restrictions = await _github.Repository.Branch.AddProtectedBranchTeamRestrictions(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch, newTeam);

                    Assert.NotNull(restrictions);
                    Assert.Equal(2, restrictions.Count);
                }
            }

            [OrganizationTest]
            public async Task AddsProtectedBranchTeamRestrictionsForOrgRepoWithRepositoryId()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var team2 = _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team2"))).Result;

                    // Grant team push access to repo
                    await _github.Organization.Team.AddRepository(
                    team2.TeamId,
                    repoContext.RepositoryOwner,
                    repoContext.RepositoryName,
                    new RepositoryPermissionRequest(TeamPermissionLegacy.Push));

                    var newTeam = new BranchProtectionTeamCollection() { team2.TeamName };
                    var restrictions = await _github.Repository.Branch.AddProtectedBranchTeamRestrictions(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch, newTeam);

                    Assert.NotNull(restrictions);
                    Assert.Equal(2, restrictions.Count);
                }
            }
        }

        public class TheDeleteProtectedBranchTeamRestrictions : GitHubClientTestBase
        {
            [OrganizationTest]
            public async Task DeletesRProtectedBranchTeamRestrictionsForOrgRepo()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    var team = await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var teamToRemove = new BranchProtectionTeamCollection() { team.TeamName };
                    var deleted = await _github.Repository.Branch.DeleteProtectedBranchTeamRestrictions(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch, teamToRemove);

                    Assert.NotNull(deleted);
                    Assert.Equal(0, deleted.Count);
                }
            }

            [OrganizationTest]
            public async Task DeletesProtectedBranchTeamRestrictionsForOrgRepoWithRepositoryId()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    var team = await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var teamToRemove = new BranchProtectionTeamCollection() { team.TeamName };
                    var deleted = await _github.Repository.Branch.DeleteProtectedBranchTeamRestrictions(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch, teamToRemove);

                    Assert.NotNull(deleted);
                    Assert.Equal(0, deleted.Count);
                }
            }
        }

        public class TheGetAllProtectedBranchUserRestrictionsMethod : GitHubClientTestBase
        {
            [OrganizationTest]
            public async Task GetsProtectedBranchUserRestrictionsForOrgRepo()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var restrictions = await _github.Repository.Branch.GetAllProtectedBranchUserRestrictions(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch);

                    Assert.NotNull(restrictions);
                    Assert.Equal(0, restrictions.Count);
                }
            }

            [OrganizationTest]
            public async Task GetsProtectedBranchUserRestrictionsForOrgRepoWithRepositoryId()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var restrictions = await _github.Repository.Branch.GetAllProtectedBranchUserRestrictions(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);

                    Assert.NotNull(restrictions);
                    Assert.Equal(0, restrictions.Count);
                }
            }
        }

        public class TheUpdateProtectedBranchUserRestrictionsMethod : GitHubClientTestBase
        {
            [OrganizationTest]
            public async Task UpdatesProtectedBranchUserRestrictionsForOrgRepo()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var newUser = new BranchProtectionUserCollection() { _github.User.Current().Result.Login };

                    var restrictions = await _github.Repository.Branch.UpdateProtectedBranchUserRestrictions(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch, newUser);

                    Assert.NotNull(restrictions);
                    Assert.Equal(1, restrictions.Count);
                }
            }

            [OrganizationTest]
            public async Task UpdatesProtectedBranchUserRestrictionsForOrgRepoWithRepositoryId()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var newUser = new BranchProtectionUserCollection() { _github.User.Current().Result.Login };

                    var restrictions = await _github.Repository.Branch.UpdateProtectedBranchUserRestrictions(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch, newUser);

                    Assert.NotNull(restrictions);
                    Assert.Equal(1, restrictions.Count);
                }
            }
        }

        public class TheAddProtectedBranchUserRestrictionsMethod : GitHubClientTestBase
        {
            [OrganizationTest]
            public async Task AddsProtectedBranchUserRestrictionsForOrgRepo()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    var team = await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var newUser = new BranchProtectionUserCollection() { _github.User.Current().Result.Login };

                    var restrictions = await _github.Repository.Branch.AddProtectedBranchUserRestrictions(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch, newUser);

                    Assert.NotNull(restrictions);
                    Assert.Equal(1, restrictions.Count);
                }
            }

            [OrganizationTest]
            public async Task AddsProtectedBranchUserRestrictionsForOrgRepoWithRepositoryId()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var newUser = new BranchProtectionUserCollection() { _github.User.Current().Result.Login };

                    var restrictions = await _github.Repository.Branch.AddProtectedBranchUserRestrictions(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch, newUser);

                    Assert.NotNull(restrictions);
                    Assert.Equal(1, restrictions.Count);
                }
            }
        }

        public class TheDeleteProtectedBranchUserRestrictions : GitHubClientTestBase
        {
            [OrganizationTest]
            public async Task DeletesProtectedBranchUserRestrictionsForOrgRepo()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var user = new BranchProtectionUserCollection() { _github.User.Current().Result.Login };
                    var restrictions = await _github.Repository.Branch.AddProtectedBranchUserRestrictions(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch, user);

                    Assert.NotNull(restrictions);
                    Assert.Equal(1, restrictions.Count);

                    var deleted = await _github.Repository.Branch.DeleteProtectedBranchUserRestrictions(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch, user);

                    Assert.NotNull(deleted);
                    Assert.Equal(0, deleted.Count);
                }
            }

            [OrganizationTest]
            public async Task DeletesProtectedBranchUserRestrictionsForOrgRepoWithRepositoryId()
            {
                using (var repoContext = await _github.CreateOrganizationRepositoryContext(x => x.AutoInit = true))
                {
                    await _github.ProtectDefaultBranchWithTeam(repoContext);

                    var user = new BranchProtectionUserCollection() { _github.User.Current().Result.Login };
                    var restrictions = await _github.Repository.Branch.AddProtectedBranchUserRestrictions(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch, user);

                    Assert.NotNull(restrictions);
                    Assert.Equal(1, restrictions.Count);

                    var deleted = await _github.Repository.Branch.DeleteProtectedBranchUserRestrictions(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch, user);

                    Assert.NotNull(deleted);
                    Assert.Equal(0, deleted.Count);
                }
            }
        }
    }
}