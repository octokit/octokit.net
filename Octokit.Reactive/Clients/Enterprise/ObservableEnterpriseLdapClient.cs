using System;
using System.Reactive.Threading.Tasks;
using Octokit;


namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Enterprise LDAP API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/enterprise/ldap/">Enterprise LDAP API documentation</a> for more information.
    ///</remarks>
    public class ObservableEnterpriseLdapClient : IObservableEnterpriseLdapClient
    {
        readonly IEnterpriseLdapClient _client;

        public ObservableEnterpriseLdapClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Enterprise.Ldap;
        }

        /// <summary>
        /// Update the LDAP mapping for a user on a GitHub Enterprise appliance (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/ldap/#update-ldap-mapping-for-a-user
        /// </remarks>
        /// <param name="userName">The username to update LDAP mapping</param>
        /// <param name="newLdapMapping">The <see cref="NewLdapMapping"/></param>
        /// <returns>The <see cref="LdapUser"/> object.</returns>
        public IObservable<LdapUser> UpdateUserMapping(string userName, NewLdapMapping newLdapMapping)
        {
            return _client.UpdateUserMapping(userName, newLdapMapping).ToObservable();
        }
        
        /// <summary>
        /// Queue an LDAP Sync job for a user on a GitHub Enterprise appliance (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/ldap/#sync-ldap-mapping-for-a-user
        /// </remarks>
        /// <param name="userName">The username to sync LDAP mapping</param>
        /// <returns>The <see cref="LdapSyncResponse"/> to the queue request.</returns>
        public IObservable<LdapSyncResponse> QueueSyncUserMapping(string userName)
        {
            return _client.QueueSyncUserMapping(userName).ToObservable();
        }
        
        /// <summary>
        /// Update the LDAP mapping for a team on a GitHub Enterprise appliance (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/ldap/#update-ldap-mapping-for-a-team
        /// </remarks>
        /// <param name="teamId">The teamId to update LDAP mapping</param>
        /// <param name="newLdapMapping">The <see cref="NewLdapMapping"/></param>
        /// <returns>The <see cref="LdapTeam"/> object.</returns>
        public IObservable<LdapTeam> UpdateTeamMapping(int teamId, NewLdapMapping newLdapMapping)
        {
            return _client.UpdateTeamMapping(teamId, newLdapMapping).ToObservable();
        }
        
        /// <summary>
        /// Queue an LDAP Sync job for a team on a GitHub Enterprise appliance (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/ldap/#sync-ldap-mapping-for-a-team
        /// </remarks>
        /// <param name="teamId">The teamId to update LDAP mapping</param>
        /// <returns>The <see cref="LDAPSyncResponse"/> to the queue request.</returns>
        public IObservable<LdapSyncResponse> QueueSyncTeamMapping(int teamId)
        {
            return _client.QueueSyncTeamMapping(teamId).ToObservable();
        }
    }
}
