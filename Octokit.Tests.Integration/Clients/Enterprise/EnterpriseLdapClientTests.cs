using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class EnterpriseLdapClientTests
{
    readonly IGitHubClient _github;
    readonly EnterpriseTeamContext _context;
    readonly string _distinguishedNameUser = "uid=test-user,ou=users,dc=company,dc=com";
    readonly string _distinguishedNameTeam = "uid=DG-Test-Team,ou=groups,dc=company,dc=com";

    public EnterpriseLdapClientTests()
    {
        _github = EnterpriseHelper.GetAuthenticatedClient();
        
        NewTeam newTeam = new NewTeam(Helper.MakeNameWithTimestamp("test-team")) { Description = "Test Team" };
        _context = _github.CreateEnterpriseTeamContext(EnterpriseHelper.Organization, newTeam).Result;
    }

    [GitHubEnterpriseTest]
    public async Task CanUpdateUserMapping()
    {
        var newLDAPMapping = new NewLdapMapping(_distinguishedNameUser);
        var ldapUser = await
            _github.Enterprise.Ldap.UpdateUserMapping(EnterpriseHelper.UserName, newLDAPMapping);

        Assert.NotNull(ldapUser);

        // Get user and check mapping was updated
        var checkUser = await _github.User.Get(EnterpriseHelper.UserName);
        Assert.Equal(checkUser.Login, ldapUser.Login);
        //Assert.Equal(checkUser.LdapDN, _distinguishedNameUser);
    }

    [GitHubEnterpriseTest]
    public async Task CanQueueSyncUserMapping()
    {
        var response = await
            _github.Enterprise.Ldap.QueueSyncUserMapping(EnterpriseHelper.UserName);

        // Check response message indicates LDAP sync was queued
        Assert.NotNull(response);
        Assert.NotNull(response.Status);
        Assert.True(response.Status.All(m => m.Contains("was added to the indexing queue")));
    }

    [GitHubEnterpriseTest]
    public async Task CanUpdateTeamMapping()
    {
        var newLDAPMapping = new NewLdapMapping(_distinguishedNameTeam);
        var ldapTeam = await
            _github.Enterprise.Ldap.UpdateTeamMapping(_context.TeamId, newLDAPMapping);

        Assert.NotNull(ldapTeam);

        // Get Team and check mapping was updated
        var checkTeam = await _github.Organization.Team.Get(_context.TeamId);
        Assert.Equal(checkTeam.Name, ldapTeam.Name);
        //Assert.Equal(checkTeam.LDAPDN, _fixtureDistinguishedNameTeam);
    }

    [GitHubEnterpriseTest]
    public async Task CanQueueSyncTeamMapping()
    {
        var response = await
            _github.Enterprise.Ldap.QueueSyncTeamMapping(_context.TeamId);

        // Check response message indicates LDAP sync was queued
        Assert.NotNull(response);
        Assert.NotNull(response.Status);
        Assert.True(response.Status.All(m => m.Contains("was added to the indexing queue")));
    }
}
