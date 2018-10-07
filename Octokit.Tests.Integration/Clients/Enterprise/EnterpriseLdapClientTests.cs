using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class EnterpriseLdapClientTests : IDisposable
{
    readonly IGitHubClient _github;

    readonly string _testUser = "test-user";
    readonly string _distinguishedNameUser = "uid=test-user,ou=users,dc=company,dc=com";

    readonly TeamContext _context;
    readonly string _distinguishedNameTeam = "cn=test-team,ou=groups,dc=company,dc=com";

    public EnterpriseLdapClientTests()
    {
        _github = EnterpriseHelper.GetAuthenticatedClient();

        NewTeam newTeam = new NewTeam(Helper.MakeNameWithTimestamp("test-team")) { Description = "Test Team" };
        _context = _github.CreateTeamContext(EnterpriseHelper.Organization, newTeam).Result;
    }

    [GitHubEnterpriseTest]
    public async Task CanUpdateUserMapping()
    {
        var newLDAPMapping = new NewLdapMapping(_distinguishedNameUser);
        var ldapUser = await
            _github.Enterprise.Ldap.UpdateUserMapping(_testUser, newLDAPMapping);

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
        var response = await
            _github.Enterprise.Ldap.QueueSyncUserMapping(_testUser);

        // Check response message indicates LDAP sync was queued
        Assert.NotNull(response);
        Assert.NotNull(response.Status);
        Assert.True(response.Status == "queued");
    }

    [GitHubEnterpriseTest]
    public async Task CanUpdateTeamMapping()
    {
        var newLDAPMapping = new NewLdapMapping(_distinguishedNameTeam);
        var ldapTeam = await
            _github.Enterprise.Ldap.UpdateTeamMapping(_context.TeamId, newLDAPMapping);

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
        var response = await
            _github.Enterprise.Ldap.QueueSyncTeamMapping(_context.TeamId);

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
