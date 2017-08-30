using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class ObservableRepositoryBranchesClientTests
    {
        public class TheGetBranchProtectionMethod : IDisposable
        {
            IObservableRepositoryBranchesClient _client;
            RepositoryContext _userRepoContext;
            OrganizationRepositoryWithTeamContext _orgRepoContext;

            public TheGetBranchProtectionMethod()
            {
                var github = Helper.GetAuthenticatedClient();
                _client = new ObservableRepositoryBranchesClient(github);

                _userRepoContext = github.CreateRepositoryWithProtectedBranch().Result;
                _orgRepoContext = github.CreateOrganizationRepositoryWithProtectedBranch().Result;
            }

            [IntegrationTest]
            public async Task GetsBranchProtection()
            {
                var repoOwner = _userRepoContext.RepositoryOwner;
                var repoName = _userRepoContext.RepositoryName;
                var protection = await _client.GetBranchProtection(repoOwner, repoName, "master");

                Assert.True(protection.RequiredStatusChecks.Strict);
                Assert.Equal(2, protection.RequiredStatusChecks.Contexts.Count);

                Assert.Null(protection.RequiredPullRequestReviews.DismissalRestrictions);
                Assert.True(protection.RequiredPullRequestReviews.DismissStaleReviews);
                Assert.True(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);

                Assert.Null(protection.Restrictions);

                Assert.True(protection.EnforceAdmins.Enabled);
            }

            [IntegrationTest]
            public async Task GetsBranchProtectionWithRepositoryId()
            {
                var repoId = _userRepoContext.RepositoryId;
                var protection = await _client.GetBranchProtection(repoId, "master");

                Assert.True(protection.RequiredStatusChecks.Strict);
                Assert.Equal(2, protection.RequiredStatusChecks.Contexts.Count);

                Assert.Null(protection.RequiredPullRequestReviews.DismissalRestrictions);
                Assert.True(protection.RequiredPullRequestReviews.DismissStaleReviews);
                Assert.True(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);

                Assert.Null(protection.Restrictions);

                Assert.True(protection.EnforceAdmins.Enabled);
            }

            [IntegrationTest]
            public async Task GetsBranchProtectionForOrgRepo()
            {
                var repoOwner = _orgRepoContext.RepositoryContext.RepositoryOwner;
                var repoName = _orgRepoContext.RepositoryContext.RepositoryName;
                var protection = await _client.GetBranchProtection(repoOwner, repoName, "master");

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

            [IntegrationTest]
            public async Task GetsBranchProtectionForOrgRepoWithRepositoryId()
            {
                var repoId = _orgRepoContext.RepositoryContext.RepositoryId;
                var protection = await _client.GetBranchProtection(repoId, "master");

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
            IObservableRepositoryBranchesClient _client;
            RepositoryContext _userRepoContext;
            OrganizationRepositoryWithTeamContext _orgRepoContext;

            public TheUpdateBranchProtectionMethod()
            {
                var github = Helper.GetAuthenticatedClient();
                _client = new ObservableRepositoryBranchesClient(github);

                _userRepoContext = github.CreateRepositoryWithProtectedBranch().Result;
                _orgRepoContext = github.CreateOrganizationRepositoryWithProtectedBranch().Result;
            }

            [IntegrationTest]
            public async Task UpdatesBranchProtection()
            {
                var repoOwner = _userRepoContext.RepositoryOwner;
                var repoName = _userRepoContext.RepositoryName;
                var update = new BranchProtectionSettingsUpdate(
                    new BranchProtectionRequiredStatusChecksUpdate(false, new[] { "new" }),
                    new BranchProtectionRequiredReviewsUpdate(false, true),
                    false);

                var protection = await _client.UpdateBranchProtection(repoOwner, repoName, "master", update);

                Assert.False(protection.RequiredStatusChecks.Strict);
                Assert.Equal(1, protection.RequiredStatusChecks.Contexts.Count);

                Assert.Null(protection.RequiredPullRequestReviews.DismissalRestrictions);
                Assert.False(protection.RequiredPullRequestReviews.DismissStaleReviews);
                Assert.True(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);

                Assert.Null(protection.Restrictions);

                Assert.False(protection.EnforceAdmins.Enabled);
            }

            [IntegrationTest]
            public async Task UpdatesBranchProtectionWithRepositoryId()
            {
                var repoId = _userRepoContext.RepositoryId;
                var update = new BranchProtectionSettingsUpdate(
                    new BranchProtectionRequiredStatusChecksUpdate(false, new[] { "new" }),
                    new BranchProtectionRequiredReviewsUpdate(false, true),
                    false);

                var protection = await _client.UpdateBranchProtection(repoId, "master", update);

                Assert.False(protection.RequiredStatusChecks.Strict);
                Assert.Equal(1, protection.RequiredStatusChecks.Contexts.Count);

                Assert.Null(protection.RequiredPullRequestReviews.DismissalRestrictions);
                Assert.False(protection.RequiredPullRequestReviews.DismissStaleReviews);
                Assert.True(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);

                Assert.Null(protection.Restrictions);

                Assert.False(protection.EnforceAdmins.Enabled);
            }

            [IntegrationTest]
            public async Task UpdatesBranchProtectionForOrgRepo()
            {
                var repoOwner = _orgRepoContext.RepositoryContext.RepositoryOwner;
                var repoName = _orgRepoContext.RepositoryContext.RepositoryName;
                var update = new BranchProtectionSettingsUpdate(
                    new BranchProtectionRequiredStatusChecksUpdate(false, new[] { "new" }),
                    new BranchProtectionRequiredReviewsUpdate(new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(false), false, false),
                    new BranchProtectionPushRestrictionsUpdate(),
                    false);

                var protection = await _client.UpdateBranchProtection(repoOwner, repoName, "master", update);

                Assert.False(protection.RequiredStatusChecks.Strict);
                Assert.Equal(1, protection.RequiredStatusChecks.Contexts.Count);

                Assert.Null(protection.RequiredPullRequestReviews.DismissalRestrictions);
                Assert.False(protection.RequiredPullRequestReviews.DismissStaleReviews);
                Assert.False(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);

                Assert.Empty(protection.Restrictions.Teams);
                Assert.Empty(protection.Restrictions.Users);

                Assert.False(protection.EnforceAdmins.Enabled);
            }

            [IntegrationTest]
            public async Task UpdatesBranchProtectionForOrgRepoWithRepositoryId()
            {
                var repoId = _orgRepoContext.RepositoryContext.RepositoryId;
                var update = new BranchProtectionSettingsUpdate(
                    new BranchProtectionRequiredStatusChecksUpdate(false, new[] { "new" }),
                    new BranchProtectionRequiredReviewsUpdate(new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(false), false, false),
                    new BranchProtectionPushRestrictionsUpdate(),
                    false);

                var protection = await _client.UpdateBranchProtection(repoId, "master", update);

                Assert.False(protection.RequiredStatusChecks.Strict);
                Assert.Equal(1, protection.RequiredStatusChecks.Contexts.Count);

                Assert.Null(protection.RequiredPullRequestReviews.DismissalRestrictions);
                Assert.False(protection.RequiredPullRequestReviews.DismissStaleReviews);
                Assert.False(protection.RequiredPullRequestReviews.RequireCodeOwnerReviews);

                Assert.Empty(protection.Restrictions.Teams);
                Assert.Empty(protection.Restrictions.Users);

                Assert.False(protection.EnforceAdmins.Enabled);
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

        public class TheGetReviewEnforcementMethod : IDisposable
        {
            IObservableRepositoryBranchesClient _client;
            RepositoryContext _userRepoContext;
            OrganizationRepositoryWithTeamContext _orgRepoContext;

            public TheGetReviewEnforcementMethod()
            {
                var github = Helper.GetAuthenticatedClient();
                _client = new ObservableRepositoryBranchesClient(github);

                _userRepoContext = github.CreateRepositoryWithProtectedBranch().Result;
                _orgRepoContext = github.CreateOrganizationRepositoryWithProtectedBranch().Result;
            }

            [IntegrationTest]
            public async Task GetsReviewEnforcement()
            {
                var repoOwner = _userRepoContext.RepositoryOwner;
                var repoName = _userRepoContext.RepositoryName;
                var requiredReviews = await _client.GetReviewEnforcement(repoOwner, repoName, "master");

                Assert.Null(requiredReviews.DismissalRestrictions);
                Assert.True(requiredReviews.DismissStaleReviews);
                Assert.True(requiredReviews.RequireCodeOwnerReviews);
            }

            [IntegrationTest]
            public async Task GetsReviewEnforcementWithRepositoryId()
            {
                var repoId = _userRepoContext.RepositoryId;
                var requiredReviews = await _client.GetReviewEnforcement(repoId, "master");

                Assert.Null(requiredReviews.DismissalRestrictions);
                Assert.True(requiredReviews.DismissStaleReviews);
                Assert.True(requiredReviews.RequireCodeOwnerReviews);
            }

            [IntegrationTest]
            public async Task GetsReviewEnforcementForOrgRepo()
            {
                var repoOwner = _orgRepoContext.RepositoryContext.RepositoryOwner;
                var repoName = _orgRepoContext.RepositoryContext.RepositoryName;
                var requiredReviews = await _client.GetReviewEnforcement(repoOwner, repoName, "master");

                Assert.Equal(1, requiredReviews.DismissalRestrictions.Teams.Count);
                Assert.Equal(0, requiredReviews.DismissalRestrictions.Users.Count);
                Assert.True(requiredReviews.DismissStaleReviews);
                Assert.True(requiredReviews.RequireCodeOwnerReviews);
            }

            [IntegrationTest]
            public async Task GetsReviewEnforcementForOrgRepoWithRepositoryId()
            {
                var repoId = _orgRepoContext.RepositoryContext.RepositoryId;
                var requiredReviews = await _client.GetReviewEnforcement(repoId, "master");

                Assert.Equal(1, requiredReviews.DismissalRestrictions.Teams.Count);
                Assert.Equal(0, requiredReviews.DismissalRestrictions.Users.Count);
                Assert.True(requiredReviews.DismissStaleReviews);
                Assert.True(requiredReviews.RequireCodeOwnerReviews);
            }

            public void Dispose()
            {
                if (_userRepoContext != null)
                    _userRepoContext.Dispose();

                if (_orgRepoContext != null)
                    _orgRepoContext.Dispose();
            }
        }

        public class TheUpdateReviewEnforcementMethod : IDisposable
        {
            IObservableRepositoryBranchesClient _client;
            RepositoryContext _userRepoContext;
            OrganizationRepositoryWithTeamContext _orgRepoContext;

            public TheUpdateReviewEnforcementMethod()
            {
                var github = Helper.GetAuthenticatedClient();
                _client = new ObservableRepositoryBranchesClient(github);

                _userRepoContext = github.CreateRepositoryWithProtectedBranch().Result;
                _orgRepoContext = github.CreateOrganizationRepositoryWithProtectedBranch().Result;
            }

            [IntegrationTest]
            public async Task UpdatesReviewEnforcement()
            {
                var repoOwner = _userRepoContext.RepositoryOwner;
                var repoName = _userRepoContext.RepositoryName;
                var update = new BranchProtectionRequiredReviewsUpdate(false, true);

                var requiredReviews = await _client.UpdateReviewEnforcement(repoOwner, repoName, "master", update);

                Assert.Null(requiredReviews.DismissalRestrictions);
                Assert.False(requiredReviews.DismissStaleReviews);
                Assert.True(requiredReviews.RequireCodeOwnerReviews);
            }

            [IntegrationTest]
            public async Task UpdatesReviewEnforcementWithRepositoryId()
            {
                var repoId = _userRepoContext.RepositoryId;
                var update = new BranchProtectionRequiredReviewsUpdate(false, true);

                var requiredReviews = await _client.UpdateReviewEnforcement(repoId, "master", update);

                Assert.Null(requiredReviews.DismissalRestrictions);
                Assert.False(requiredReviews.DismissStaleReviews);
                Assert.True(requiredReviews.RequireCodeOwnerReviews);
            }

            [IntegrationTest]
            public async Task UpdatesReviewEnforcementForOrgRepo()
            {
                var repoOwner = _orgRepoContext.RepositoryContext.RepositoryOwner;
                var repoName = _orgRepoContext.RepositoryContext.RepositoryName;
                var update = new BranchProtectionRequiredReviewsUpdate(
                    new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(false),
                    false,
                    false);

                var requiredReviews = await _client.UpdateReviewEnforcement(repoOwner, repoName, "master", update);

                Assert.Null(requiredReviews.DismissalRestrictions);
                Assert.False(requiredReviews.DismissStaleReviews);
                Assert.False(requiredReviews.RequireCodeOwnerReviews);
            }

            [IntegrationTest]
            public async Task UpdatesReviewEnforcementForOrgRepoWithRepositoryId()
            {
                var repoId = _orgRepoContext.RepositoryContext.RepositoryId;
                var update = new BranchProtectionRequiredReviewsUpdate(
                    new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(false),
                    false,
                    false);

                var requiredReviews = await _client.UpdateReviewEnforcement(repoId, "master", update);

                Assert.Null(requiredReviews.DismissalRestrictions);
                Assert.False(requiredReviews.DismissStaleReviews);
                Assert.False(requiredReviews.RequireCodeOwnerReviews);
            }

            [IntegrationTest]
            public async Task UpdatesReviewEnforcementForOrgRepoWithAdminOnly()
            {
                var repoOwner = _orgRepoContext.RepositoryContext.RepositoryOwner;
                var repoName = _orgRepoContext.RepositoryContext.RepositoryName;
                var update = new BranchProtectionRequiredReviewsUpdate(
                    new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(true),
                    false,
                    false);

                var requiredReviews = await _client.UpdateReviewEnforcement(repoOwner, repoName, "master", update);

                Assert.Empty(requiredReviews.DismissalRestrictions.Teams);
                Assert.Empty(requiredReviews.DismissalRestrictions.Users);
                Assert.False(requiredReviews.DismissStaleReviews);
                Assert.False(requiredReviews.RequireCodeOwnerReviews);
            }

            [IntegrationTest]
            public async Task UpdatesReviewEnforcementForOrgRepoWithAdminOnlyWithRepositoryId()
            {
                var repoId = _orgRepoContext.RepositoryContext.RepositoryId;
                var update = new BranchProtectionRequiredReviewsUpdate(
                    new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(true),
                    false,
                    false);

                var requiredReviews = await _client.UpdateReviewEnforcement(repoId, "master", update);

                Assert.Empty(requiredReviews.DismissalRestrictions.Teams);
                Assert.Empty(requiredReviews.DismissalRestrictions.Users);
                Assert.False(requiredReviews.DismissStaleReviews);
                Assert.False(requiredReviews.RequireCodeOwnerReviews);
            }

            public void Dispose()
            {
                if (_userRepoContext != null)
                    _userRepoContext.Dispose();

                if (_orgRepoContext != null)
                    _orgRepoContext.Dispose();
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
                    var deleted = await _client.RemoveReviewEnforcement(repoOwner, repoName, "master");

                    Assert.True(deleted);
                }
            }

            [IntegrationTest]
            public async Task RemovesReviewEnforcementWithRepositoryId()
            {
                using (var context = await _github.CreateRepositoryWithProtectedBranch())
                {
                    var repoId = context.RepositoryId;
                    var deleted = await _client.RemoveReviewEnforcement(repoId, "master");

                    Assert.True(deleted);
                }
            }

            [IntegrationTest]
            public async Task RemovesReviewEnforcementForOrgRepo()
            {
                using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
                {
                    var repoOwner = context.RepositoryContext.RepositoryOwner;
                    var repoName = context.RepositoryContext.RepositoryName;
                    var deleted = await _client.RemoveReviewEnforcement(repoOwner, repoName, "master");

                    Assert.True(deleted);
                }
            }

            [IntegrationTest]
            public async Task RemovesReviewEnforcementForOrgRepoWithRepositoryId()
            {
                using (var context = await _github.CreateOrganizationRepositoryWithProtectedBranch())
                {
                    var repoId = context.RepositoryContext.RepositoryId;
                    var deleted = await _client.RemoveReviewEnforcement(repoId, "master");

                    Assert.True(deleted);
                }
            }
        }
    }
}
