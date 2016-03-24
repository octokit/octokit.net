using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Filter members in the list
    /// </summary>
    /// <remarks>
    /// see https://developer.github.com/v3/orgs/members/#members-list for details
    /// </remarks>
    public enum OrganizationMembersFilter
    {
        /// <summary>
        ///  All members the authenticated user can see.
        /// </summary>
        [Parameter(Value = "all")]
        All,
        /// <summary>
        /// Members without two-factor authentication enabled
        /// </summary>
        [Parameter(Value = "2fa_disabled")]
        TwoFactorAuthenticationDisabled
    }

    public enum OrganizationMembersRole
    {
        [Parameter(Value = "all")]
        All,

        [Parameter(Value = "admin")]
        Admin,

        [Parameter(Value = "member")]
        Member
    }

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
        /// <returns>The users</returns>
        public Task<IReadOnlyList<User>> GetAll(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            return ApiConnection.GetAll<User>(ApiUrls.Members(org));
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
        /// <param name="filter">The filter to use when getting the users, <see cref="OrganizationMembersFilter"/></param>
        /// <returns>The users</returns>
        public Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            return ApiConnection.GetAll<User>(ApiUrls.Members(org, filter));
        }

        /// <summary>
        /// Obsolete, <see cref="GetAll(string,OrganizationMembersFilter)"/>
        /// </summary>
        /// <param name="org">The login for the organization</param>
        /// <param name="filter">The user filter</param>
        /// <returns>The users</returns>
        [Obsolete("No longer supported, use GetAll(string, OrganizationMembersFilter) instead")]
        public Task<IReadOnlyList<User>> GetAll(string org, string filter)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNullOrEmptyString(filter, "filter");

            return ApiConnection.GetAll<User>(ApiUrls.Members(org, filter));
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
        /// <param name="role">The role filter to use when getting the users, <see cref="OrganizationMembersRole"/></param>
        /// <returns>The users</returns>
        public Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersRole role)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            return ApiConnection.GetAll<User>(ApiUrls.Members(org, role));
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
        /// <param name="filter">The filter to use when getting the users, <see cref="OrganizationMembersFilter"/></param>
        /// <param name="role">The role filter to use when getting the users, <see cref="OrganizationMembersRole"/></param>
        /// <returns>The users</returns>
        public Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter, OrganizationMembersRole role)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            return ApiConnection.GetAll<User>(ApiUrls.Members(org, filter, role));
        }

        /// <summary>
        /// List all users who have publicized their membership of the organization.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/orgs/members/#public-members-list</remarks>
        /// <param name="org">The login for the organization</param>
        /// <returns></returns>
        public Task<IReadOnlyList<User>> GetAllPublic(string org)
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
                var statusCode = response.HttpResponse.StatusCode;
                if (statusCode != HttpStatusCode.NotFound
                    && statusCode != HttpStatusCode.NoContent
                    && statusCode != HttpStatusCode.Found)
                {
                    throw new ApiException("Invalid Status Code returned. Expected a 204, a 302 or a 404", statusCode);
                }
                return statusCode == HttpStatusCode.NoContent;
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
                return response.HttpResponse.IsTrue();
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
                if (response.HttpResponse.StatusCode != HttpStatusCode.NoContent)
                {
                    throw new ApiException("Invalid Status Code returned. Expected a 204", response.HttpResponse.StatusCode);
                }
                return response.HttpResponse.StatusCode == HttpStatusCode.NoContent;
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
