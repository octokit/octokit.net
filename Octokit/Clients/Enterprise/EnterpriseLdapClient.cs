using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Enterprise LDAP API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/enterprise/ldap/">Enterprise LDAP API documentation</a> for more information.
    ///</remarks>
    public class EnterpriseLdapClient : ApiClient, IEnterpriseLdapClient
    {
        public EnterpriseLdapClient(IApiConnection apiConnection)
            : base(apiConnection)
        { }

        /// <summary>
        /// Update the LDAP mapping for a user on a GitHub Enterprise appliance (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/ldap/#update-ldap-mapping-for-a-user
        /// </remarks>
        /// <param name="userName">The username to update LDAP mapping</param>
        /// <param name="newLdapMapping">The <see cref="NewLdapMapping"/></param>
        /// <returns>The <see cref="User"/> object.</returns>
        [ManualRoute("PATCH", "/admin/ldap/users/{username}/mapping")]
        public Task<User> UpdateUserMapping(string userName, NewLdapMapping newLdapMapping)
        {
            Ensure.ArgumentNotNull(userName, nameof(userName));
            Ensure.ArgumentNotNull(newLdapMapping, nameof(newLdapMapping));

            var endpoint = ApiUrls.EnterpriseLdapUserMapping(userName);

            return ApiConnection.Patch<User>(endpoint, newLdapMapping);
        }

        /// <summary>
        /// Queue an LDAP Sync job for a user on a GitHub Enterprise appliance (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/ldap/#sync-ldap-mapping-for-a-user
        /// </remarks>
        /// <param name="userName">The userName to sync LDAP mapping</param>
        /// <returns>The <see cref="LdapSyncResponse"/> of the queue request.</returns>
        [ManualRoute("POST", "/admin/ldap/users/{username}/sync")]
        public async Task<LdapSyncResponse> QueueSyncUserMapping(string userName)
        {
            Ensure.ArgumentNotNull(userName, nameof(userName));

            var endpoint = ApiUrls.EnterpriseLdapUserSync(userName);

            var response = await Connection.Post<LdapSyncResponse>(endpoint).ConfigureAwait(false);
            if (response.HttpResponse.StatusCode != HttpStatusCode.Created)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 201", response.HttpResponse.StatusCode);
            }

            return response.Body;
        }

        /// <summary>
        /// Update the LDAP mapping for a team on a GitHub Enterprise appliance (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/ldap/#update-ldap-mapping-for-a-team
        /// </remarks>
        /// <param name="teamId">The teamId to update LDAP mapping</param>
        /// <param name="newLdapMapping">The <see cref="NewLdapMapping"/></param>
        /// <returns>The <see cref="Team"/> object.</returns>
        [ManualRoute("PATCH", "/admin/ldap/teams/{team_id}/mapping")]
        public Task<Team> UpdateTeamMapping(int teamId, NewLdapMapping newLdapMapping)
        {
            Ensure.ArgumentNotNull(teamId, nameof(teamId));
            Ensure.ArgumentNotNull(newLdapMapping, nameof(newLdapMapping));

            var endpoint = ApiUrls.EnterpriseLdapTeamMapping(teamId);

            return ApiConnection.Patch<Team>(endpoint, newLdapMapping);
        }

        /// <summary>
        /// Queue an LDAP Sync job for a team on a GitHub Enterprise appliance (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/ldap/#sync-ldap-mapping-for-a-team
        /// </remarks>
        /// <param name="teamId">The teamId to update LDAP mapping</param>
        /// <returns>The <see cref="LdapSyncResponse"/> of the queue request.</returns>
        [ManualRoute("POST", "/admin/ldap/teams/{team_id}/sync")]
        public async Task<LdapSyncResponse> QueueSyncTeamMapping(int teamId)
        {
            Ensure.ArgumentNotNull(teamId, nameof(teamId));

            var endpoint = ApiUrls.EnterpriseLdapTeamSync(teamId);

            var response = await Connection.Post<LdapSyncResponse>(endpoint).ConfigureAwait(false);
            if (response.HttpResponse.StatusCode != HttpStatusCode.Created)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 201", response.HttpResponse.StatusCode);
            }

            return response.Body;
        }
    }
}
