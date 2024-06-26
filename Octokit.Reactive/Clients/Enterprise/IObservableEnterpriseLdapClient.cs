using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Enterprise LDAP API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/enterprise/ldap/">Enterprise LDAP API documentation</a> for more information.
    ///</remarks>
    public interface IObservableEnterpriseLdapClient
    {
        /// <summary>
        /// Update the LDAP mapping for a user on a GitHub Enterprise appliance (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/ldap/#update-ldap-mapping-for-a-user
        /// </remarks>
        /// <param name="userName">The username to update LDAP mapping</param>
        /// <param name="newLdapMapping">The <see cref="NewLdapMapping"/></param>
        /// <returns>The <see cref="User"/> object.</returns>
        IObservable<User> UpdateUserMapping(string userName, NewLdapMapping newLdapMapping);

        /// <summary>
        /// Queue an LDAP Sync job for a user on a GitHub Enterprise appliance (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/ldap/#sync-ldap-mapping-for-a-user
        /// </remarks>
        /// <param name="userName">The username to sync LDAP mapping</param>
        /// <returns>The <see cref="LdapSyncResponse"/> of the queue request.</returns>
        IObservable<LdapSyncResponse> QueueSyncUserMapping(string userName);

        /// <summary>
        /// Update the LDAP mapping for a team on a GitHub Enterprise appliance (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/ldap/#update-ldap-mapping-for-a-team
        /// </remarks>
        /// <param name="teamId">The teamId to update LDAP mapping</param>
        /// <param name="newLdapMapping">The <see cref="NewLdapMapping"/></param>
        /// <returns>The <see cref="Team"/> object.</returns>
        IObservable<Team> UpdateTeamMapping(long teamId, NewLdapMapping newLdapMapping);

        /// <summary>
        /// Queue an LDAP Sync job for a team on a GitHub Enterprise appliance (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/ldap/#sync-ldap-mapping-for-a-team
        /// </remarks>
        /// <param name="teamId">The teamId to update LDAP mapping</param>
        /// <returns>The <see cref="LdapSyncResponse"/> of the queue request.</returns>
        IObservable<LdapSyncResponse> QueueSyncTeamMapping(long teamId);
    }
}
