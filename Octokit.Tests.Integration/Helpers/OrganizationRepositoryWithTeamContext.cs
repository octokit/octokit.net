using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Tests.Integration.Helpers
{
    internal class OrganizationRepositoryWithTeamContext : IDisposable
    {
        internal RepositoryContext RepositoryContext { get; set; }
        internal TeamContext TeamContext { get; set; }

        public void Dispose()
        {
            if (RepositoryContext != null)
                RepositoryContext.Dispose();

            if (TeamContext != null)
                TeamContext.Dispose();
        }
    }

    internal static class RepositoryProtectedBranchHelperExtensions
    {
        internal async static Task ProtectDefaultBranch(this IGitHubClient client, RepositoryContext repoContext)
        {
            // Protect default branch
            var update = new BranchProtectionSettingsUpdate(
                new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "build", "test" }),
                new BranchProtectionRequiredReviewsUpdate(true, true, 3),
                null,
                true,
                true,
                true,
                true,
                false,
                true);

            await client.Repository.Branch.UpdateBranchProtection(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch, update);
        }

        internal async static Task<RepositoryContext> CreateRepositoryWithProtectedBranch(this IGitHubClient client)
        {
            // Create user owned repo
            var userRepo = new NewRepository(Helper.MakeNameWithTimestamp("protected-repo")) { AutoInit = true };
            var contextUserRepo = await client.CreateRepositoryContext(userRepo);

            // Protect default branch
            var update = new BranchProtectionSettingsUpdate(
                new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "build", "test" }),
                new BranchProtectionRequiredReviewsUpdate(true, true, 3),
                null,
                true,
                true,
                true,
                true,
                false,
                true);

            await client.Repository.Branch.UpdateBranchProtection(contextUserRepo.RepositoryOwner, contextUserRepo.RepositoryName, "main", update);

            return contextUserRepo;
        }

        internal async static Task<TeamContext> ProtectDefaultBranchWithTeam(this IGitHubClient client, RepositoryContext repoContext)
        {
            // Create team in org
            var team = await client.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team")));

            // Grant team push access to repo
            await client.Organization.Team.AddRepository(
                team.TeamId,
                repoContext.RepositoryOwner,
                repoContext.RepositoryName,
                new RepositoryPermissionRequest(TeamPermissionLegacy.Push));

            // Protect default branch
            var protection = new BranchProtectionSettingsUpdate(
                new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "build", "test" }),
                new BranchProtectionRequiredReviewsUpdate(new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(new BranchProtectionTeamCollection { team.TeamName }), true, true, 3),
                new BranchProtectionPushRestrictionsUpdate(new BranchProtectionTeamCollection { team.TeamName }),
                true);
            await client.Repository.Branch.UpdateBranchProtection(repoContext.RepositoryOwner, repoContext.RepositoryName, repoContext.RepositoryDefaultBranch, protection);

            return team;
        }

        internal async static Task<OrganizationRepositoryWithTeamContext> CreateOrganizationRepositoryWithProtectedBranch(this IGitHubClient client)
        {
            // Create organization owned repo
            var orgRepo = new NewRepository(Helper.MakeNameWithTimestamp("protected-org-repo")) { AutoInit = true };
            var contextOrgRepo = await client.CreateOrganizationRepositoryContext(Helper.Organization, orgRepo);

            // Create team in org
            var contextOrgTeam = await client.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team")));

            // Grant team push access to repo
            await client.Organization.Team.AddRepository(
                contextOrgTeam.TeamId,
                contextOrgRepo.RepositoryOwner,
                contextOrgRepo.RepositoryName,
                new RepositoryPermissionRequest(TeamPermissionLegacy.Push));

            // Protect default branch
            var protection = new BranchProtectionSettingsUpdate(
                new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "build", "test" }),
                new BranchProtectionRequiredReviewsUpdate(new BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(new BranchProtectionTeamCollection { contextOrgTeam.TeamName }), true, true, 3),
                new BranchProtectionPushRestrictionsUpdate(new BranchProtectionTeamCollection { contextOrgTeam.TeamName }),
                true);
            await client.Repository.Branch.UpdateBranchProtection(contextOrgRepo.RepositoryOwner, contextOrgRepo.RepositoryName, "main", protection);

            return new OrganizationRepositoryWithTeamContext
            {
                RepositoryContext = contextOrgRepo,
                TeamContext = contextOrgTeam
            };
        }
    }
}
