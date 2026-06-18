using System.Collections.Generic;
using System.Threading;
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>The users</returns>
        Task<IReadOnlyList<User>> GetAll(string org, CancellationToken cancellationToken = default);

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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>The users</returns>
        Task<IReadOnlyList<User>> GetAll(string org, ApiOptions options, CancellationToken cancellationToken = default);

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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>The users</returns>
        Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter, CancellationToken cancellationToken = default);

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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>The users</returns>
        Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter, ApiOptions options, CancellationToken cancellationToken = default);

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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>The users</returns>
        Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersRole role, CancellationToken cancellationToken = default);

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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>The users</returns>
        Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersRole role, ApiOptions options, CancellationToken cancellationToken = default);

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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>The users</returns>
        Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter, OrganizationMembersRole role, CancellationToken cancellationToken = default);

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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>The users</returns>
        Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter, OrganizationMembersRole role, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// List all users who have publicized their membership of the organization.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/orgs/members/#public-members-list</remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<User>> GetAllPublic(string org, CancellationToken cancellationToken = default);

        /// <summary>
        /// List all users who have publicized their membership of the organization.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/orgs/members/#public-members-list</remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<User>> GetAllPublic(string org, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Check if a user is, publicly or privately, a member of the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/orgs/members/#check-membership">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="user">The login for the user</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<bool> CheckMember(string org, string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Check is a user is publicly a member of the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/orgs/members/#check-public-membership">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="user">The login for the user</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<bool> CheckMemberPublic(string org, string user, CancellationToken cancellationToken = default);

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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task Delete(string org, string user, CancellationToken cancellationToken = default);

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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<bool> Publicize(string org, string user, CancellationToken cancellationToken = default);

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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task Conceal(string org, string user, CancellationToken cancellationToken = default);

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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<OrganizationMembership> GetOrganizationMembership(string org, string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add a user to the organization or update the user's role within the organization.
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<OrganizationMembership> AddOrUpdateOrganizationMembership(string org, string user, OrganizationMembershipUpdate addOrUpdateRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create an organization invitation for a user
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// The authenticated user must be an organization owner.
        /// See the <a href="https://developer.github.com/v3/orgs/members/#create-an-organization-invitation">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="invitationRequest">An <see cref="OrganizationInvitationRequest"/> instance containing the
        /// details of the organization invitation</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<OrganizationMembershipInvitation> CreateOrganizationInvitation(string org, OrganizationInvitationRequest invitationRequest, CancellationToken cancellationToken = default);

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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task RemoveOrganizationMembership(string org, string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// List all pending invitations for the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/members/#list-pending-organization-invitations">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<OrganizationMembershipInvitation>> GetAllPendingInvitations(string org, CancellationToken cancellationToken = default);

        /// <summary>
        /// List all pending invitations for the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/members/#list-pending-organization-invitations">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="options">Options to change API behaviour</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<OrganizationMembershipInvitation>> GetAllPendingInvitations(string org, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// List failed organization invitations.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/members#list-failed-organization-invitations">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<OrganizationMembershipInvitation>> GetAllFailedInvitations(string org, CancellationToken cancellationToken = default);

        /// <summary>
        /// List failed organization invitations.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/members#list-failed-organization-invitations">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="options">Options to change API behaviour</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<OrganizationMembershipInvitation>> GetAllFailedInvitations(string org, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Cancel an organization invitation. In order to cancel an organization invitation, the authenticated user must be an organization owner.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/orgs/members#cancel-an-organization-invitation">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="invitationId">The unique identifier of the invitation</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task CancelOrganizationInvitation(string org, long invitationId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all <see cref="OrganizationMembership" />s for the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/orgs/members#list-organization-memberships-for-the-authenticated-user">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the current user's <see cref="OrganizationMembership"/>s.</returns>
        Task<IReadOnlyList<OrganizationMembership>> GetAllOrganizationMembershipsForCurrent(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all <see cref="OrganizationMembership" />s for the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/orgs/members#list-organization-memberships-for-the-authenticated-user">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="options">Options to change API behaviour</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the current user's <see cref="OrganizationMembership"/>s.</returns>
        Task<IReadOnlyList<OrganizationMembership>> GetAllOrganizationMembershipsForCurrent(ApiOptions options, CancellationToken cancellationToken = default);
    }
}
