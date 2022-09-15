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

    public enum MembershipRole
    {
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
        [ManualRoute("GET", "/orgs/{org}/members")]
        public Task<IReadOnlyList<User>> GetAll(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return GetAll(org, ApiOptions.None);
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
        /// <param name="options">Options for changing the API response</param>
        /// <returns>The users</returns>
        [ManualRoute("GET", "/orgs/{org}/members")]
        public Task<IReadOnlyList<User>> GetAll(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<User>(ApiUrls.Members(org), options);
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
        [ManualRoute("GET", "/orgs/{org}/members")]
        public Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return GetAll(org, filter, ApiOptions.None);
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
        /// <param name="options">Options for changing the API response</param>
        /// <returns>The users</returns>
        [ManualRoute("GET", "/orgs/{org}/members")]
        public Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<User>(ApiUrls.Members(org, filter), options);
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
        [ManualRoute("GET", "/orgs/{org}/members?role={1}")]
        public Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersRole role)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return GetAll(org, role, ApiOptions.None);
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
        /// <param name="options">Options for changing the API response</param>
        /// <returns>The users</returns>
        [ManualRoute("GET", "/orgs/{org}/members?role={1}")]
        public Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersRole role, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<User>(ApiUrls.Members(org, role), options);
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
        [ManualRoute("GET", "/orgs/{org}/members?filter={1}&role={2}")]
        public Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter, OrganizationMembersRole role)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return GetAll(org, filter, role, ApiOptions.None);
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
        /// <param name="options">Options for changing the API response</param>
        /// <returns>The users</returns>
        [ManualRoute("GET", "/orgs/{org}/members?filter={1}&role={2}")]
        public Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter, OrganizationMembersRole role, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<User>(ApiUrls.Members(org, filter, role), options);
        }

        /// <summary>
        /// List all users who have publicized their membership of the organization.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/orgs/members/#public-members-list</remarks>
        /// <param name="org">The login for the organization</param>
        /// <returns></returns>
        [ManualRoute("GET", "/orgs/{org}/public_members")]
        public Task<IReadOnlyList<User>> GetAllPublic(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return GetAllPublic(org, ApiOptions.None);
        }

        /// <summary>
        /// List all users who have publicized their membership of the organization.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/orgs/members/#public-members-list</remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        [ManualRoute("GET", "/orgs/{org}/public_members")]
        public Task<IReadOnlyList<User>> GetAllPublic(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<User>(ApiUrls.PublicMembers(org), options);
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
        [ManualRoute("GET", "/orgs/{org}/members/{username}")]
        public async Task<bool> CheckMember(string org, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            try
            {
                var response = await Connection.Get<object>(ApiUrls.CheckMember(org, user), null, null).ConfigureAwait(false);
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
        [ManualRoute("GET", "/orgs/{org}/public_members/{username}")]
        public async Task<bool> CheckMemberPublic(string org, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            try
            {
                var response = await Connection.Get<object>(ApiUrls.CheckMemberPublic(org, user), null, null).ConfigureAwait(false);
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
        [ManualRoute("DELETE", "/orgs/{org}/members/{username}")]
        public Task Delete(string org, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

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
        [ManualRoute("PUT", "/orgs/{org}/public_members/{username}")]
        public async Task<bool> Publicize(string org, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            try
            {
                var requestData = new { };
                var response = await Connection.Put<object>(ApiUrls.OrganizationMembership(org, user), requestData).ConfigureAwait(false);
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
        /// This method requires authentication.
        /// See the <a href="http://developer.github.com/v3/orgs/members/#conceal-a-users-membership">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="user">The login for the user</param>
        /// <returns></returns>
        [ManualRoute("DELETE", "/orgs/{org}/public_members/{username}")]
        public Task Conceal(string org, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return ApiConnection.Delete(ApiUrls.OrganizationMembership(org, user));
        }

        /// <summary>
        /// Get a user's membership with an organization.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// The authenticated user must be an organization member.
        /// See the <a href="https://developer.github.com/v3/orgs/members/#get-organization-membership">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="user">The login for the user</param>
        /// <returns></returns>
        [ManualRoute("GET", "/orgs/{org}/memberships/{username}")]
        public Task<OrganizationMembership> GetOrganizationMembership(string org, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return ApiConnection.Get<OrganizationMembership>(ApiUrls.OrganizationMemberships(org, user));
        }

        /// <summary>
        /// Add a user to the organization or update the user's role withing the organization.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// The authenticated user must be an organization owner.
        /// See the <a href="https://developer.github.com/v3/orgs/members/#add-or-update-organization-membership">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="user">The login for the user</param>
        /// <param name="addOrUpdateRequest">An <see cref="OrganizationMembershipUpdate"/> instance describing the
        /// changes to make to the user's organization membership</param>
        /// <returns></returns>
        [ManualRoute("PUT", "/orgs/{org}/memberships/{username}")]
        public Task<OrganizationMembership> AddOrUpdateOrganizationMembership(string org, string user, OrganizationMembershipUpdate addOrUpdateRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(addOrUpdateRequest, nameof(addOrUpdateRequest));

            return ApiConnection.Put<OrganizationMembership>(ApiUrls.OrganizationMemberships(org, user), addOrUpdateRequest);
        }

        /// <summary>
        /// Remove a user's membership with an organization.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// The authenticated user must be an organization owner.
        /// See the <a href="https://developer.github.com/v3/orgs/members/#remove-organization-membership">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="user">The login for the user</param>
        /// <returns></returns>
        [ManualRoute("DELETE", "/orgs/{org}/memberships/{username}")]
        public Task RemoveOrganizationMembership(string org, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return ApiConnection.Delete(ApiUrls.OrganizationMemberships(org, user));
        }

        /// <summary>
        /// List all pending invitations for the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/members/#list-pending-organization-invitations">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <returns></returns>
        [ManualRoute("GET", "/orgs/{org}/invitations")]
        public Task<IReadOnlyList<OrganizationMembershipInvitation>> GetAllPendingInvitations(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return GetAllPendingInvitations(org, ApiOptions.None);
        }

        /// <summary>
        /// List all pending invitations for the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/members/#list-pending-organization-invitations">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="options">Options to change API behaviour</param>
        /// <returns></returns>
        [ManualRoute("GET", "/orgs/{org}/invitations")]
        public Task<IReadOnlyList<OrganizationMembershipInvitation>> GetAllPendingInvitations(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<OrganizationMembershipInvitation>(ApiUrls.OrganizationPendingInvitations(org), null, options);
        }

        /// <summary>
        /// List failed organization invitations.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/members#list-failed-organization-invitations">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <returns></returns>
        [ManualRoute("GET", "/orgs/{org}/failed_invitations")]
        public Task<IReadOnlyList<OrganizationMembershipInvitation>> GetAllFailedInvitations(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return GetAllFailedInvitations(org, ApiOptions.None);
        }

        /// <summary>
        /// List failed organization invitations.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/members#list-failed-organization-invitations">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="options">Options to change API behaviour</param>
        /// <returns></returns>
        [ManualRoute("GET", "/orgs/{org}/failed_invitations")]
        public Task<IReadOnlyList<OrganizationMembershipInvitation>> GetAllFailedInvitations(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<OrganizationMembershipInvitation>(ApiUrls.OrganizationFailedInvitations(org), null, options);
        }
    }
}
