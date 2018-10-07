using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class ObservableEnterpriseLdapClientTests : IDisposable
    {
        readonly IObservableGitHubClient _github;

        readonly string _testUser = "test-user";
        readonly string _distinguishedNameUser = "uid=test-user,ou=users,dc=company,dc=com";

        readonly TeamContext _context;
        readonly string _distinguishedNameTeam = "cn=test-team,ou=groups,dc=company,dc=com";

        public ObservableEnterpriseLdapClientTests()
        {
            _github = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());

            NewTeam newTeam = new NewTeam(Helper.MakeNameWithTimestamp("test-team")) { Description = "Test Team" };
            _context = _github.CreateTeamContext(EnterpriseHelper.Organization, newTeam).Result;
        }

        [GitHubEnterpriseTest]
        public async Task CanUpdateUserMapping()
        {
            var newLDAPMapping = new NewLdapMapping(_distinguishedNameUser);
            var observable =
                _github.Enterprise.Ldap.UpdateUserMapping(_testUser, newLDAPMapping);
            var ldapUser = await observable;

            Assert.NotNull(ldapUser);
            Assert.NotNull(ldapUser.LdapDistinguishedName);
            Assert.Equal(ldapUser.LdapDistinguishedName, _distinguishedNameUser);

            // Get user and check mapping was updated
            var checkUser = await _github.User.Get(_testUser);
            Assert.Equal(checkUser.Login, ldapUser.Login);
            Assert.Equal(checkUser.LdapDistinguishedName, _distinguishedNameUser);
        }

        [GitHubEnterpriseTest]
        public async Task CanQueueSyncUserMapping()
        {
            var observable =
                _github.Enterprise.Ldap.QueueSyncUserMapping(_testUser);
            var response = await observable;

            // Check response message indicates LDAP sync was queued
            Assert.NotNull(response);
            Assert.NotNull(response.Status);
            Assert.True(response.Status == "queued");
        }

        [GitHubEnterpriseTest]
        public async Task CanUpdateTeamMapping()
        {
            var newLDAPMapping = new NewLdapMapping(_distinguishedNameTeam);
            var observable =
                _github.Enterprise.Ldap.UpdateTeamMapping(_context.TeamId, newLDAPMapping);
            var ldapTeam = await observable;

            Assert.NotNull(ldapTeam);
            Assert.NotNull(ldapTeam.LdapDistinguishedName);
            Assert.Equal(ldapTeam.LdapDistinguishedName, _distinguishedNameTeam);

            // Get Team and check mapping was updated
            var checkTeam = await _github.Organization.Team.Get(_context.TeamId);
            Assert.Equal(checkTeam.Name, ldapTeam.Name);
            Assert.Equal(checkTeam.LdapDistinguishedName, _distinguishedNameTeam);
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
            Assert.True(response.Status == "queued");
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
