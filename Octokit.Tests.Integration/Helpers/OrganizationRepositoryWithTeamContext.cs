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
        internal async static Task<RepositoryContext> CreateRepositoryWithProtectedBranch(this IGitHubClient client)
        {
            // Create user owned repo
            var userRepo = new NewRepository(Helper.MakeNameWithTimestamp("protected-repo")) { AutoInit = true };
            var contextUserRepo = await client.CreateRepositoryContext(userRepo);

            // Protect master branch
            var update = new BranchProtectionSettingsUpdate(
                new BranchProtectionRequiredStatusChecksUpdate(true, true, new[] { "build", "test" }));

            await client.Repository.Branch.UpdateBranchProtection(contextUserRepo.RepositoryOwner, contextUserRepo.RepositoryName, "master", update);

            return contextUserRepo;
        }

        internal async static Task<OrganizationRepositoryWithTeamContext> CreateOrganizationRepositoryWithProtectedBranch(this IGitHubClient client)
        {
            // Create organization owned repo
            var orgRepo = new NewRepository(Helper.MakeNameWithTimestamp("protected-org-repo")) { AutoInit = true };
            var contextOrgRepo = await client.CreateRepositoryContext(Helper.Organization, orgRepo);

            // Create team in org
            var contextOrgTeam = await client.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team")));

            // Grant team push access to repo
            await client.Organization.Team.AddRepository(
                contextOrgTeam.TeamId,
                contextOrgRepo.RepositoryOwner,
                contextOrgRepo.RepositoryName,
                new RepositoryPermissionRequest(Permission.Push));

            // Protect master branch
            var protection = new BranchProtectionSettingsUpdate(
                new BranchProtectionRequiredStatusChecksUpdate(true, true, new[] { "build", "test" }),
                new BranchProtectionPushRestrictionsUpdate(new BranchProtectionTeamCollection { contextOrgTeam.TeamName }));
            await client.Repository.Branch.UpdateBranchProtection(contextOrgRepo.RepositoryOwner, contextOrgRepo.RepositoryName, "master", protection);

            return new OrganizationRepositoryWithTeamContext
            {
                RepositoryContext = contextOrgRepo,
                TeamContext = contextOrgTeam
            };
        }
    }
}
