using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableRepositoryBranchesClientTests
    {
        public class TheGetBranchProtectionMethod
        {
            IGitHubClient _github;
            IObservableRepositoryBranchesClient _client;

            public TheGetBranchProtectionMethod()
            {
                _github = Helper.GetAuthenticatedClient();
                _client = new ObservableRepositoryBranchesClient(_github);
            }

            [IntegrationTest]
            public async Task GetsBranchProtection()
            {
                using (var context = await _github.CreateRepositoryWithProtectedBranch())
                {
                    var repoOwner = context.RepositoryOwner;
                    var repoName = context.RepositoryName;
                    var protection = await _client.GetBranchProtection(repoOwner, repoName, "main");

                    Assert.True(protection.RequiredStatusChecks.Strict);
                    Assert.Equal(2, protection.RequiredStatusChecks.Contexts.Count);

                    Assert.Null(protection.RequiredPullRequestReviews.DismissalRestrictions);
                    Assert.True(protection.RequiredPullRequestReviews.DismissStaleReviews);
                    Assert.True(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);
                    Assert.Equal(3, protection.RequiredPullRequestReviews.RequiredApprovingReviewCount);

                    Assert.Null(protection.Restrictions);

                    Assert.True(protection.EnforceAdmins.Enabled);
                }
            }

            [IntegrationTest]
            public async Task GetsBranchProtectionWithRepositoryId()
            {
                using (var context = await _github.CreateRepositoryWithProtectedBranch())
                {
                    var repoId = context.RepositoryId;
                    var protection = await _client.GetBranchProtection(repoId, "main");

                    Assert.True(protection.RequiredStatusChecks.Strict);
                    Assert.Equal(2, protection.RequiredStatusChecks.Contexts.Count);

                    Assert.Null(protection.RequiredPullRequestReviews.DismissalRestrictions);
                    Assert.True(protection.RequiredPullRequestReviews.DismissStaleReviews);
                    Assert.True(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);
                    Assert.Equal(3, protection.RequiredPullRequestReviews.RequiredApprovingReviewCount);

                    Assert.Null(protection.Restrictions);

                    Assert.True(protection.EnforceAdmins.Enabled);
                }
            }

            [OrganizationTest]
            public async Task GetsBranchProtectionForOrgRepo()
            {
                using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
                {
                    var repoOwner = context.RepositoryContext.RepositoryOwner;
                    var repoName = context.RepositoryContext.RepositoryName;
                    var protection = await _client.GetBranchProtection(repoOwner, repoName, "main");

                    Assert.True(protection.RequiredStatusChecks.Strict);
                    Assert.Equal(2, protection.RequiredStatusChecks.Contexts.Count);

                    Assert.Equal(1, protection.RequiredPullRequestReviews.DismissalRestrictions.Teams.Count);
                    Assert.Equal(0, protection.RequiredPullRequestReviews.DismissalRestrictions.Users.Count);
                    Assert.True(protection.RequiredPullRequestReviews.DismissStaleReviews);
                    Assert.True(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);
                    Assert.Equal(3, protection.RequiredPullRequestReviews.RequiredApprovingReviewCount);

                    Assert.Equal(1, protection.Restrictions.Teams.Count);
                    Assert.Equal(0, protection.Restrictions.Users.Count);

                    Assert.True(protection.EnforceAdmins.Enabled);
                }
            }

            [OrganizationTest]
            public async Task GetsBranchProtectionForOrgRepoWithRepositoryId()
            {
                using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
                {
                    var repoId = context.RepositoryContext.RepositoryId;
                    var protection = await _client.GetBranchProtection(repoId, "main");

                    Assert.True(protection.RequiredStatusChecks.Strict);
                    Assert.Equal(2, protection.RequiredStatusChecks.Contexts.Count);

                    Assert.Equal(1, protection.RequiredPullRequestReviews.DismissalRestrictions.Teams.Count);
                    Assert.Equal(0, protection.RequiredPullRequestReviews.DismissalRestrictions.Users.Count);
                    Assert.True(protection.RequiredPullRequestReviews.DismissStaleReviews);
                    Assert.True(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);
                    Assert.Equal(3, protection.RequiredPullRequestReviews.RequiredApprovingReviewCount);

                    Assert.Equal(1, protection.Restrictions.Teams.Count);
                    Assert.Equal(0, protection.Restrictions.Users.Count);

                    Assert.True(protection.EnforceAdmins.Enabled);
                }
            }
        }

        public class TheUpdateBranchProtectionMethod
        {
            IGitHubClient _github;
            IObservableRepositoryBranchesClient _client;

            public TheUpdateBranchProtectionMethod()
            {
                _github = Helper.GetAuthenticatedClient();
                _client = new ObservableRepositoryBranchesClient(_github);
            }

            [IntegrationTest]
            public async Task UpdatesBranchProtection()
            {
                using (var context = await _github.CreateRepositoryWithProtectedBranch())
                {
                    var repoOwner = context.RepositoryOwner;
                    var repoName = context.RepositoryName;
                    var update = new BranchProtectionSettingsUpdate(
                        new BranchProtectionRequiredStatusChecksUpdate(false, new[] { "new" }),
                        new BranchProtectionRequiredReviewsUpdate(false, true, 2),
                        false);

                    var protection = await _client.UpdateBranchProtection(repoOwner, repoName, "main", update);

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
                using (var context = await _github.CreateRepositoryWithProtectedBranch())
                {
                    var repoId = context.RepositoryId;
                    var update = new BranchProtectionSettingsUpdate(
                        new BranchProtectionRequiredStatusChecksUpdate(false, new[] { "new" }),
                        new BranchProtectionRequiredReviewsUpdate(false, true, 2),
                        false);

                    var protection = await _client.UpdateBranchProtection(repoId, "main", update);

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
                using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
                {
                    var repoOwner = context.RepositoryContext.RepositoryOwner;
                    var repoName = context.RepositoryContext.RepositoryName;
                    var update = new BranchProtectionSettingsUpdate(
                        new BranchProtectionRequiredStatusChecksUpdate(false, new[] { "new" }),
                        new BranchProtectionRequiredReviewsUpdate(new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(false), false, false, 2),
                        new BranchProtectionPushRestrictionsUpdate(),
                        false,
                        true,
                        true,
                        true,
                        true,
                        true);

                    var protection = await _client.UpdateBranchProtection(repoOwner, repoName, "main", update);

                    Assert.False(protection.RequiredStatusChecks.Strict);
                    Assert.Equal(1, protection.RequiredStatusChecks.Contexts.Count);

                    Assert.Null(protection.RequiredPullRequestReviews.DismissalRestrictions);
                    Assert.False(protection.RequiredPullRequestReviews.DismissStaleReviews);
                    Assert.False(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);
                    Assert.Equal(2, protection.RequiredPullRequestReviews.RequiredApprovingReviewCount);

                    Assert.Empty(protection.Restrictions.Teams);
                    Assert.Empty(protection.Restrictions.Users);

                    Assert.False(protection.EnforceAdmins.Enabled);
                    Assert.True(protection.RequiredLinearHistory.Enabled);
                    Assert.True(protection.AllowForcePushes.Enabled);
                    Assert.True(protection.AllowDeletions.Enabled);
                    Assert.True(protection.BlockCreations.Enabled);
                    Assert.True(protection.RequiredConversationResolution.Enabled);
                }
            }

            [OrganizationTest]
            public async Task UpdatesBranchProtectionForOrgRepoWithRepositoryId()
            {
                using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
                {
                    var repoId = context.RepositoryContext.RepositoryId;
                    var update = new BranchProtectionSettingsUpdate(
                        new BranchProtectionRequiredStatusChecksUpdate(false, new[] { "new" }),
                        new BranchProtectionRequiredReviewsUpdate(new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(false), false, false, 2),
                        new BranchProtectionPushRestrictionsUpdate(),
                        false,
                        true,
                        true,
                        true,
                        true,
                        true);

                    var protection = await _client.UpdateBranchProtection(repoId, "main", update);

                    Assert.False(protection.RequiredStatusChecks.Strict);
                    Assert.Equal(1, protection.RequiredStatusChecks.Contexts.Count);

                    Assert.Null(protection.RequiredPullRequestReviews.DismissalRestrictions);
                    Assert.False(protection.RequiredPullRequestReviews.DismissStaleReviews);
                    Assert.False(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);
                    Assert.Equal(2, protection.RequiredPullRequestReviews.RequiredApprovingReviewCount);

                    Assert.Empty(protection.Restrictions.Teams);
                    Assert.Empty(protection.Restrictions.Users);

                    Assert.False(protection.EnforceAdmins.Enabled);
                    Assert.True(protection.RequiredLinearHistory.Enabled);
                    Assert.True(protection.AllowForcePushes.Enabled);
                    Assert.True(protection.AllowDeletions.Enabled);
                    Assert.True(protection.BlockCreations.Enabled);
                    Assert.True(protection.RequiredConversationResolution.Enabled);
                }
            }
        }

        public class TheDeleteBranchProtectionMethod
        {
            IGitHubClient _github;
            IObservableRepositoryBranchesClient _client;

            public TheDeleteBranchProtectionMethod()
            {
                _github = Helper.GetAuthenticatedClient();
                _client = new ObservableRepositoryBranchesClient(_github);
            }

            [IntegrationTest]
            public async Task DeletesBranchProtection()
            {
                using (var context = await _github.CreateRepositoryWithProtectedBranch())
                {
                    var repoOwner = context.RepositoryOwner;
                    var repoName = context.RepositoryName;
                    var deleted = await _client.DeleteBranchProtection(repoOwner, repoName, "main");

                    Assert.True(deleted);
                }
            }

            [IntegrationTest]
            public async Task DeletesBranchProtectionWithRepositoryId()
            {
                using (var context = await _github.CreateRepositoryWithProtectedBranch())
                {
                    var repoId = context.RepositoryId;
                    var deleted = await _client.DeleteBranchProtection(repoId, "main");

                    Assert.True(deleted);
                }
            }

            [OrganizationTest]
            public async Task DeletesBranchProtectionForOrgRepo()
            {
                using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
                {
                    var repoOwner = context.RepositoryContext.RepositoryOwner;
                    var repoName = context.RepositoryContext.RepositoryName;
                    var deleted = await _client.DeleteBranchProtection(repoOwner, repoName, "main");

                    Assert.True(deleted);
                }
            }

            [OrganizationTest]
            public async Task DeletesBranchProtectionForOrgRepoWithRepositoryId()
            {
                using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
                {
                    var repoId = context.RepositoryContext.RepositoryId;
                    var deleted = await _client.DeleteBranchProtection(repoId, "main");

                    Assert.True(deleted);
                }
            }
        }

        public class TheGetReviewEnforcementMethod
        {
            IGitHubClient _github;
            IObservableRepositoryBranchesClient _client;

            public TheGetReviewEnforcementMethod()
            {
                _github = Helper.GetAuthenticatedClient();
                _client = new ObservableRepositoryBranchesClient(_github);
            }

            [IntegrationTest]
            public async Task GetsReviewEnforcement()
            {
                using (var context = await _github.CreateRepositoryWithProtectedBranch())
                {
                    var repoOwner = context.RepositoryOwner;
                    var repoName = context.RepositoryName;
                    var requiredReviews = await _client.GetReviewEnforcement(repoOwner, repoName, "main");

                    Assert.Null(requiredReviews.DismissalRestrictions);
                    Assert.True(requiredReviews.DismissStaleReviews);
                    Assert.True(requiredReviews.RequireCodeOwnerReviews);
                }
            }

            [IntegrationTest]
            public async Task GetsReviewEnforcementWithRepositoryId()
            {
                using (var context = await _github.CreateRepositoryWithProtectedBranch())
                {
                    var repoId = context.RepositoryId;
                    var requiredReviews = await _client.GetReviewEnforcement(repoId, "main");

                    Assert.Null(requiredReviews.DismissalRestrictions);
                    Assert.True(requiredReviews.DismissStaleReviews);
                    Assert.True(requiredReviews.RequireCodeOwnerReviews);
                }
            }

            [OrganizationTest]
            public async Task GetsReviewEnforcementForOrgRepo()
            {
                using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
                {
                    var repoOwner = context.RepositoryContext.RepositoryOwner;
                    var repoName = context.RepositoryContext.RepositoryName;
                    var requiredReviews = await _client.GetReviewEnforcement(repoOwner, repoName, "main");

                    Assert.Equal(1, requiredReviews.DismissalRestrictions.Teams.Count);
                    Assert.Equal(0, requiredReviews.DismissalRestrictions.Users.Count);
                    Assert.True(requiredReviews.DismissStaleReviews);
                    Assert.True(requiredReviews.RequireCodeOwnerReviews);
                }
            }

            [OrganizationTest]
            public async Task GetsReviewEnforcementForOrgRepoWithRepositoryId()
            {
                using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
                {
                    var repoId = context.RepositoryContext.RepositoryId;
                    var requiredReviews = await _client.GetReviewEnforcement(repoId, "main");

                    Assert.Equal(1, requiredReviews.DismissalRestrictions.Teams.Count);
                    Assert.Equal(0, requiredReviews.DismissalRestrictions.Users.Count);
                    Assert.True(requiredReviews.DismissStaleReviews);
                    Assert.True(requiredReviews.RequireCodeOwnerReviews);
                }
            }
        }

        public class TheUpdateReviewEnforcementMethod
        {
            IGitHubClient _github;
            IObservableRepositoryBranchesClient _client;

            public TheUpdateReviewEnforcementMethod()
            {
                _github = Helper.GetAuthenticatedClient();
                _client = new ObservableRepositoryBranchesClient(_github);
            }

            [IntegrationTest]
            public async Task UpdatesReviewEnforcement()
            {
                using (var context = await _github.CreateRepositoryWithProtectedBranch())
                {
                    var repoOwner = context.RepositoryOwner;
                    var repoName = context.RepositoryName;
                    var update = new BranchProtectionRequiredReviewsUpdate(false, true, 2);

                    var requiredReviews = await _client.UpdateReviewEnforcement(repoOwner, repoName, "main", update);

                    Assert.Null(requiredReviews.DismissalRestrictions);
                    Assert.False(requiredReviews.DismissStaleReviews);
                    Assert.True(requiredReviews.RequireCodeOwnerReviews);
                    Assert.Equal(2, requiredReviews.RequiredApprovingReviewCount);
                }
            }

            [IntegrationTest]
            public async Task UpdatesReviewEnforcementWithRepositoryId()
            {
                using (var context = await _github.CreateRepositoryWithProtectedBranch())
                {
                    var repoId = context.RepositoryId;
                    var update = new BranchProtectionRequiredReviewsUpdate(false, true, 2);

                    var requiredReviews = await _client.UpdateReviewEnforcement(repoId, "main", update);

                    Assert.Null(requiredReviews.DismissalRestrictions);
                    Assert.False(requiredReviews.DismissStaleReviews);
                    Assert.True(requiredReviews.RequireCodeOwnerReviews);
                    Assert.Equal(2, requiredReviews.RequiredApprovingReviewCount);
                }
            }

            [OrganizationTest]
            public async Task UpdatesReviewEnforcementForOrgRepo()
            {
                using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
                {
                    var repoOwner = context.RepositoryContext.RepositoryOwner;
                    var repoName = context.RepositoryContext.RepositoryName;
                    var update = new BranchProtectionRequiredReviewsUpdate(
                        new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(false),
                        false,
                        false,
                        2);

                    var requiredReviews = await _client.UpdateReviewEnforcement(repoOwner, repoName, "main", update);

                    Assert.Null(requiredReviews.DismissalRestrictions);
                    Assert.False(requiredReviews.DismissStaleReviews);
                    Assert.False(requiredReviews.RequireCodeOwnerReviews);
                    Assert.Equal(2, requiredReviews.RequiredApprovingReviewCount);
                }
            }

            [OrganizationTest]
            public async Task UpdatesReviewEnforcementForOrgRepoWithRepositoryId()
            {
                using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
                {
                    var repoId = context.RepositoryContext.RepositoryId;
                    var update = new BranchProtectionRequiredReviewsUpdate(
                        new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(false),
                        false,
                        false,
                        2);

                    var requiredReviews = await _client.UpdateReviewEnforcement(repoId, "main", update);

                    Assert.Null(requiredReviews.DismissalRestrictions);
                    Assert.False(requiredReviews.DismissStaleReviews);
                    Assert.False(requiredReviews.RequireCodeOwnerReviews);
                    Assert.Equal(2, requiredReviews.RequiredApprovingReviewCount);
                }
            }

            [OrganizationTest]
            public async Task UpdatesReviewEnforcementForOrgRepoWithAdminOnly()
            {
                using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
                {
                    var repoOwner = context.RepositoryContext.RepositoryOwner;
                    var repoName = context.RepositoryContext.RepositoryName;
                    var update = new BranchProtectionRequiredReviewsUpdate(
                        new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(true),
                        false,
                        false,
                        2);

                    var requiredReviews = await _client.UpdateReviewEnforcement(repoOwner, repoName, "main", update);

                    Assert.Empty(requiredReviews.DismissalRestrictions.Teams);
                    Assert.Empty(requiredReviews.DismissalRestrictions.Users);
                    Assert.False(requiredReviews.DismissStaleReviews);
                    Assert.False(requiredReviews.RequireCodeOwnerReviews);
                }
            }

            [OrganizationTest]
            public async Task UpdatesReviewEnforcementForOrgRepoWithAdminOnlyWithRepositoryId()
            {
                using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
                {
                    var repoId = context.RepositoryContext.RepositoryId;
                    var update = new BranchProtectionRequiredReviewsUpdate(
                        new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(true),
                        false,
                        false,
                        2);

                    var requiredReviews = await _client.UpdateReviewEnforcement(repoId, "main", update);

                    Assert.Empty(requiredReviews.DismissalRestrictions.Teams);
                    Assert.Empty(requiredReviews.DismissalRestrictions.Users);
                    Assert.False(requiredReviews.DismissStaleReviews);
                    Assert.False(requiredReviews.RequireCodeOwnerReviews);
                    Assert.Equal(2, requiredReviews.RequiredApprovingReviewCount);
                }
            }
        }

        public class TheRemoveReviewEnforcementMethod
        {
            IGitHubClient _github;
            IObservableRepositoryBranchesClient _client;

            public TheRemoveReviewEnforcementMethod()
            {
                _github = Helper.GetAuthenticatedClient();
                _client = new ObservableRepositoryBranchesClient(_github);
            }

            [IntegrationTest]
            public async Task RemovesReviewEnforcement()
            {
                using (var context = await _github.CreateRepositoryWithProtectedBranch())
                {
                    var repoOwner = context.RepositoryOwner;
                    var repoName = context.RepositoryName;
                    var deleted = await _client.RemoveReviewEnforcement(repoOwner, repoName, "main");

                    Assert.True(deleted);
                }
            }

            [IntegrationTest]
            public async Task RemovesReviewEnforcementWithRepositoryId()
            {
                using (var context = await _github.CreateRepositoryWithProtectedBranch())
                {
                    var repoId = context.RepositoryId;
                    var deleted = await _client.RemoveReviewEnforcement(repoId, "main");

                    Assert.True(deleted);
                }
            }

            [OrganizationTest]
            public async Task RemovesReviewEnforcementForOrgRepo()
            {
                using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
                {
                    var repoOwner = context.RepositoryContext.RepositoryOwner;
                    var repoName = context.RepositoryContext.RepositoryName;
                    var deleted = await _client.RemoveReviewEnforcement(repoOwner, repoName, "main");

                    Assert.True(deleted);
                }
            }

            [OrganizationTest]
            public async Task RemovesReviewEnforcementForOrgRepoWithRepositoryId()
            {
                using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
                {
                    var repoId = context.RepositoryContext.RepositoryId;
                    var deleted = await _client.RemoveReviewEnforcement(repoId, "main");

                    Assert.True(deleted);
                }
            }
        }
    }
}
