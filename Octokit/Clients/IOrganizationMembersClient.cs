using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Organization Members API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/orgs/members/">Orgs API documentation</a> for more information.
    /// </remarks>
    public interface IOrganizationMembersClient
    {
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
        Task<IReadOnlyList<User>> GetAll(string org);

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
        Task<IReadOnlyList<User>> GetAll(string org, ApiOptions options);

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
        Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter);

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
        Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter, ApiOptions options);

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
        Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersRole role);

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
        Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersRole role, ApiOptions options);

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
        Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter, OrganizationMembersRole role);

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
        Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter, OrganizationMembersRole role, ApiOptions options);

        /// <summary>
        /// List all users who have publicized their membership of the organization.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/orgs/members/#public-members-list</remarks>
        /// <param name="org">The login for the organization</param>
        /// <returns></returns>
        Task<IReadOnlyList<User>> GetAllPublic(string org);

        /// <summary>
        /// List all users who have publicized their membership of the organization.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/orgs/members/#public-members-list</remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        Task<IReadOnlyList<User>> GetAllPublic(string org, ApiOptions options);

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
        Task<bool> CheckMember(string org, string user);

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
        Task<bool> CheckMemberPublic(string org, string user);

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
        Task Delete(string org, string user);

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
        Task<bool> Publicize(string org, string user);

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
        Task Conceal(string org, string user);
        
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
        Task<OrganizationMembership> GetOrganizationMembership(string org, string user);

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
        Task<OrganizationMembership> AddOrUpdateOrganizationMembership(string org, string user, OrganizationMembershipUpdate addOrUpdateRequest);

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
        Task RemoveOrganizationMembership(string org, string user);

        /// <summary>
        /// List all pending invitations for the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/members/#list-pending-organization-invitations">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <returns></returns>
        Task<IReadOnlyList<OrganizationMembershipInvitation>> GetAllPendingInvitations(string org);

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
        Task<IReadOnlyList<OrganizationMembershipInvitation>> GetAllPendingInvitations(string org, ApiOptions options);
    }
}
