using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Organization Members API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/orgs/members/">Orgs API documentation</a> for more information.
    /// </remarks>
    public class OrganizationMembersClient : ApiClient, IOrganizationMembersClient
    {
        /// <summary>
        /// Initializes a new Organization Members API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public OrganizationMembersClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// <para>
        /// List all users who are members of an organization. A member is a user that
        /// belongs to at least 1 team in the organization.
        /// </para>
        /// <para>
        /// If the authenticated user is also an owner of this organization then both
        /// concealed and public member will be returned.
        /// </para>
        /// <para>
        /// If the requester is not an owner of the organization the query will be redirected
        /// to the public members list.
        /// </para>
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/orgs/members/#members-list">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <returns></returns>
        public Task<IReadOnlyList<User>> GetAll(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            return ApiConnection.GetAll<User>(ApiUrls.Members(org));
        }

        /// <summary>
        /// List all users who have publicized their membership of the organization.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/orgs/members/#public-members-list</remarks>
        /// <param name="org">The login for the organization</param>
        /// <returns></returns>
        public Task<IReadOnlyList<User>> GetPublic(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            return ApiConnection.GetAll<User>(ApiUrls.PublicMembers(org));
        }

        /// <summary>
        /// Check if a user is, publicly or privately, a member of the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/orgs/members/#check-membership">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="user">The login for the user</param>
        /// <returns></returns>
        public async Task<bool> CheckMember(string org, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            try
            {
                var response = await Connection.Get<object>(ApiUrls.CheckMember(org, user), null, null)
                                               .ConfigureAwait(false);
                if (response.StatusCode != HttpStatusCode.NotFound 
                    && response.StatusCode != HttpStatusCode.NoContent
                    && response.StatusCode != HttpStatusCode.Found)
                {
                    throw new ApiException("Invalid Status Code returned. Expected a 204, a 302 or a 404", response.StatusCode);
                }
                return response.StatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Check is a user is publicly a member of the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/orgs/members/#check-public-membership">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="user">The login for the user</param>
        /// <returns></returns>
        public async Task<bool> CheckMemberPublic(string org, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            try
            {
                var response = await Connection.Get<object>(ApiUrls.CheckMemberPublic(org, user), null, null)
                                               .ConfigureAwait(false);
                if (response.StatusCode != HttpStatusCode.NotFound 
                    && response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw new ApiException("Invalid Status Code returned. Expected a 204 or a 404", response.StatusCode);
                }
                return response.StatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Removes a user from the organization, this will also remove them from all teams
        /// within the organization and they will no longer have any access to the organization's
        /// repositories.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/orgs/members/#remove-a-member">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="user">The login for the user</param>
        /// <returns></returns>
        public Task Delete(string org, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return ApiConnection.Delete("orgs/{0}/members/{1}".FormatUri(org, user));
        }

        /// <summary>
        /// Make the authenticated user's organization membership public.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://developer.github.com/v3/orgs/members/#publicize-a-users-membership">API documentation</a> 
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="user">The login for the user</param>
        /// <returns></returns>
        public async Task<bool> Publicize(string org, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            try
            {
                var requestData = new { };
                var response = await Connection.Put<object>(ApiUrls.OrganizationMembership(org, user), requestData)
                                               .ConfigureAwait(false);
                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw new ApiException("Invalid Status Code returned. Expected a 204", response.StatusCode);
                }
                return response.StatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Make the authenticated user's organization membership private.
        /// </summary>
        /// <remarks>
        /// This method requries authentication.
        /// See the <a href="http://developer.github.com/v3/orgs/members/#conceal-a-users-membership">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="user">The login for the user</param>
        /// <returns></returns>
        public Task Conceal(string org, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return ApiConnection.Delete(ApiUrls.OrganizationMembership(org, user));
        }
    }
}
