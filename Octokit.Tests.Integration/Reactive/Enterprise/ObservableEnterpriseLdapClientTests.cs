using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class ObservableEnterpriseLdapClientTests
    {
        readonly IObservableGitHubClient _github;
        readonly EnterpriseTeamContext _context;
        readonly string _distinguishedNameUser = "uid=test-user,ou=users,dc=company,dc=com";
        readonly string _distinguishedNameTeam = "uid=DG-Test-Team,ou=groups,dc=company,dc=com";

        public ObservableEnterpriseLdapClientTests()
        {
            var gitHub = EnterpriseHelper.GetAuthenticatedClient();
            _github = new ObservableGitHubClient(gitHub);

            NewTeam newTeam = new NewTeam(Helper.MakeNameWithTimestamp("test-team")) { Description = "Test Team" };
            _context = gitHub.CreateEnterpriseTeamContext(EnterpriseHelper.Organization, newTeam).Result;
        }

        [GitHubEnterpriseTest]
        public async Task CanUpdateUserMapping()
        {
            var newLDAPMapping = new NewLdapMapping(_distinguishedNameUser);
            var observable =
                _github.Enterprise.Ldap.UpdateUserMapping(EnterpriseHelper.UserName, newLDAPMapping);
            var ldapUser = await observable;

            Assert.NotNull(ldapUser);

            // Get user and check mapping was updated
            var checkUser = await _github.User.Get(EnterpriseHelper.UserName);
            Assert.Equal(checkUser.Login, ldapUser.Login);
            //Assert.Equal(checkUser.LdapDN, _distinguishedNameUser);
        }

        [GitHubEnterpriseTest]
        public async Task CanQueueSyncUserMapping()
        {
            var observable =
                _github.Enterprise.Ldap.QueueSyncUserMapping(EnterpriseHelper.UserName);
            var response = await observable;

            // Check response message indicates LDAP sync was queued
            Assert.NotNull(response);
            Assert.NotNull(response.Status);
            Assert.True(response.Status.All(m => m.Contains("was added to the indexing queue")));
        }

        [GitHubEnterpriseTest]
        public async Task CanUpdateTeamMapping()
        {
            var newLDAPMapping = new NewLdapMapping(_distinguishedNameTeam);
            var observable =
                _github.Enterprise.Ldap.UpdateTeamMapping(_context.TeamId, newLDAPMapping);
            var ldapTeam = await observable;

            Assert.NotNull(ldapTeam);

            // Get Team and check mapping was updated
            var checkTeam = await _github.Organization.Team.Get(_context.TeamId);
            Assert.Equal(checkTeam.Name, ldapTeam.Name);
            //Assert.Equal(checkTeam.LdapDN, _distinguishedNameTeam);
        }

        [GitHubEnterpriseTest]
        public async Task CanQueueSyncTeamMapping()
        {
            var observable =
                _github.Enterprise.Ldap.QueueSyncTeamMapping(_context.TeamId);
            var response = await observable;

            // Check response message indicates LDAP sync was queued
            Assert.NotNull(response);
            Assert.NotNull(response.Status);
            Assert.True(response.Status.All(m => m.Contains("was added to the indexing queue")));
        }
    }
}
