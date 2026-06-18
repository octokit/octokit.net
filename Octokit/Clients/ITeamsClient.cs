using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Organization Teams API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/orgs/teams/">Organization Teams API documentation</a> for more information.
    /// </remarks>
    public interface ITeamsClient
    {
        /// <summary>
        /// Gets a single <see cref="Team"/> by identifier.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#get-team
        /// </remarks>
        /// <param name="id">The team identifier.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>The <see cref="Team"/> with the given identifier.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        Task<Team> Get(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all <see cref="Team" />s for the current org.
        /// </summary>
        /// <param name="org">Organization for which to list all teams.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the orgs's teams <see cref="Team"/>s.</returns>
        Task<IReadOnlyList<Team>> GetAll(string org, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all <see cref="Team" />s for the current org.
        /// </summary>
        /// <param name="org">Organization for which to list all teams.</param>
        /// <param name="options">Options to change API behaviour.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the orgs's teams <see cref="Team"/>s.</returns>
        Task<IReadOnlyList<Team>> GetAll(string org, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all <see cref="Team" />s for the current user.
        /// </summary>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the user's <see cref="Team"/>s.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<Team>> GetAllForCurrent(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all <see cref="Team" />s for the current user.
        /// </summary>
        /// <param name="options">Options to change API behaviour.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the user's <see cref="Team"/>s.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<Team>> GetAllForCurrent(ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all child teams of the given team.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-child-teams
        /// </remarks>
        /// <param name="id">The team identifier</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<Team>> GetAllChildTeams(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all child teams of the given team.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-child-teams
        /// </remarks>
        /// <param name="id">The team identifier</param>
        /// <param name="options">Options to change API behaviour.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<Team>> GetAllChildTeams(long id, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all members of the given team.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-team-members
        /// </remarks>
        /// <param name="id">The team identifier</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<User>> GetAllMembers(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all members of the given team.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-team-members
        /// </remarks>
        /// <param name="id">The team identifier</param>
        /// <param name="options">Options to change API behaviour.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<User>> GetAllMembers(long id, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all members with the specified role in the given team of the given role.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-team-members
        /// </remarks>
        /// <param name="id">The team identifier</param>
        /// <param name="request">The request filter</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<User>> GetAllMembers(long id, TeamMembersRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all members with the specified role in the given team of the given role.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-team-members
        /// </remarks>
        /// <param name="id">The team identifier</param>
        /// <param name="request">The request filter</param>
        /// <param name="options">Options to change API behaviour.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<User>> GetAllMembers(long id, TeamMembersRequest request, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns newly created <see cref="Team" /> for the current org.
        /// </summary>
        /// <param name="org">The org to create the team in.</param>
        /// <param name="team">The team to create.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Newly created <see cref="Team"/></returns>
        Task<Team> Create(string org, NewTeam team, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a team
        /// To edit a team, the authenticated user must either be an organization owner or a team maintainer
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/teams/teams?apiVersion=2022-11-28#update-a-team">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The organization name. The name is not case sensitive.</param>
        /// <param name="teamSlug">The slug of the team name.</param>
        /// <param name="team">The team update parameters.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>updated <see cref="Team" /> for the current org</returns>
        Task<Team> Update(string org, string teamSlug, UpdateTeam team, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns updated <see cref="Team" /> for the current org.
        /// This endpoint route is deprecated and will be removed from the Teams API.
        /// We recommend migrating your existing code to use the new Update a team endpoint.
        /// <see cref="Update(string, string, UpdateTeam, CancellationToken)"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/teams/teams?apiVersion=2022-11-28#update-a-team-legacy">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="id">The team identifier.</param>
        /// <param name="team">The team update parameters.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Updated <see cref="Team"/></returns>
        Task<Team> Update(long id, UpdateTeam team, CancellationToken cancellationToken = default);

        /// <summary>
        /// To delete a team, the authenticated user must be an organization owner or team maintainer.
        /// If you are an organization owner, deleting a parent team will delete all of its child teams as well.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/teams/teams?apiVersion=2022-11-28#delete-a-team">API documentation</a>
        /// </remarks>
        /// <param name="org">The organization name. The name is not case sensitive.</param>
        /// <param name="teamSlug">The slug of the team name.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task Delete(string org, string teamSlug, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a team - must have owner permissions to do this
        /// This endpoint route is deprecated and will be removed from the Teams API.
        /// We recommend migrating your existing code to use the new Delete a team endpoint.
        /// <see cref="Delete(string, string, CancellationToken)"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/teams/teams?apiVersion=2022-11-28#delete-a-team-legacy">API documentation</a>
        /// </remarks>
        /// <param name="id">The unique identifier of the team.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task Delete(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a <see cref="User"/> to a <see cref="Team"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#add-or-update-team-membership">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The team identifier.</param>
        /// <param name="login">The user to add to the team.</param>
        /// <param name="request">Additional parameters for the request</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<TeamMembershipDetails> AddOrEditMembership(long id, string login, UpdateTeamMembership request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes a <see cref="User"/> from a <see cref="Team"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#remove-team-member">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The team identifier.</param>
        /// <param name="login">The user to remove from the team.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns><see langword="true"/> if the user was removed from the team; <see langword="false"/> otherwise.</returns>
        Task<bool> RemoveMembership(long id, string login, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets whether the user with the given <paramref name="login"/>
        /// is a member of the team with the given <paramref name="id"/>.
        /// A <see cref="NotFoundException"/> is thrown if the user is not a member.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#get-team-membership">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The team to check.</param>
        /// <param name="login">The user to check.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<TeamMembershipDetails> GetMembershipDetails(long id, string login, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all team's repositories.
        /// </summary>
        /// <param name="id">Team Id to list repos.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The team's repositories</returns>
        Task<IReadOnlyList<Repository>> GetAllRepositories(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all team's repositories.
        /// </summary>
        /// <param name="id">Team Id to list repos.</param>
        /// <param name="options">Options to change API behaviour.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The team's repositories</returns>
        Task<IReadOnlyList<Repository>> GetAllRepositories(long id, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add a repository to the team
        /// </summary>
        /// <param name="id">The team identifier.</param>
        /// <param name="organization">Org to associate the repo with.</param>
        /// <param name="repoName">Name of the repo.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<bool> AddRepository(long id, string organization, string repoName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add a repository to the team
        /// </summary>
        /// <param name="id">The team identifier.</param>
        /// <param name="organization">Org to associate the repo with.</param>
        /// <param name="repoName">Name of the repo.</param>
        /// <param name="permission">The permission to grant the team on this repository.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<bool> AddRepository(long id, string organization, string repoName, RepositoryPermissionRequest permission, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove a repository from the team
        /// </summary>
        /// <param name="id">The team identifier.</param>
        /// <param name="organization">Org the repo is associated with.</param>
        /// <param name="repoName">Name of the repo.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<bool> RemoveRepository(long id, string organization, string repoName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets whether or not the given repository is managed by the given team.
        /// </summary>
        /// <param name="id">The team identifier</param>
        /// <param name="owner">Owner of the org the team is associated with.</param>
        /// <param name="repo">Name of the repo.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#get-team-repo">API documentation</a> for more information.
        /// </remarks>
        /// <returns><see langword="true"/> if the repository is managed by the given team; <see langword="false"/> otherwise.</returns>
        Task<bool> IsRepositoryManagedByTeam(long id, string owner, string repo, CancellationToken cancellationToken = default);

        /// <summary>
        /// List all pending invitations for the given team.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#list-pending-team-invitations">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="id">The team identifier</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<OrganizationMembershipInvitation>> GetAllPendingInvitations(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// List all pending invitations for the given team.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#list-pending-team-invitations">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="id">The team identifier</param>
        /// <param name="options">Options to change API behaviour.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<OrganizationMembershipInvitation>> GetAllPendingInvitations(long id, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks whether a team has admin, push, maintain, triage, or pull permission for a repository.
        /// Repositories inherited through a parent team will also be checked.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/teams/teams?apiVersion=2022-11-28#check-team-permissions-for-a-repository">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The organization name. The name is not case sensitive.</param>
        /// <param name="teamSlug">The slug of the team name.</param>
        /// <param name="owner">The account owner of the repository. The name is not case sensitive.</param>
        /// <param name="repo">The name of the repository. The name is not case sensitive.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<bool> CheckTeamPermissionsForARepository(string org, string teamSlug, string owner, string repo, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks whether a team has admin, push, maintain, triage, or pull permission for a repository.
        /// Repositories inherited through a parent team will also be checked.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/teams/teams?apiVersion=2022-11-28#check-team-permissions-for-a-repository">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The organization name. The name is not case sensitive.</param>
        /// <param name="teamSlug">The slug of the team name.</param>
        /// <param name="owner">The account owner of the repository. The name is not case sensitive.</param>
        /// <param name="repo">The name of the repository. The name is not case sensitive.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<TeamRepository> CheckTeamPermissionsForARepositoryWithCustomAcceptHeader(string org, string teamSlug, string owner, string repo, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add or update team repository permissions
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/teams/teams?apiVersion=2022-11-28#add-or-update-team-repository-permissions">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The organization name. The name is not case sensitive.</param>
        /// <param name="teamSlug">The slug of the team name.</param>
        /// <param name="owner">The account owner of the repository. The name is not case sensitive.</param>
        /// <param name="repo">The name of the repository. The name is not case sensitive.</param>
        /// <param name="permission">
        /// The permission to grant the team on this repository. We accept the following permissions to be set:
        /// pull, triage, push, maintain, admin and you can also specify a custom repository role name, if the
        /// owning organization has defined any. If no permission is specified, the team's permission attribute
        /// will be used to determine what permission to grant the team on this repository
        /// </param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task AddOrUpdateTeamRepositoryPermissions(string org, string teamSlug, string owner, string repo, string permission, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove a repository from a team
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/teams/teams?apiVersion=2022-11-28#remove-a-repository-from-a-team">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The organization name. The name is not case sensitive.</param>
        /// <param name="teamSlug">The slug of the team name.</param>
        /// <param name="owner">The account owner of the repository. The name is not case sensitive.</param>
        /// <param name="repo">The name of the repository. The name is not case sensitive.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task RemoveRepositoryFromATeam(string org, string teamSlug, string owner, string repo, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a team by slug name
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/teams/teams?apiVersion=2022-11-28#get-a-team-by-name">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The organization name. The name is not case sensitive.</param>
        /// <param name="teamSlug">The slug of the team name.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <exception cref="NotFoundException">Thrown when the team wasn't found</exception>
        /// <returns>A <see cref="Team"/> instance if found, otherwise a <see cref="NotFoundException"/></returns>
        Task<Team> GetByName(string org, string teamSlug, CancellationToken cancellationToken = default);
    }
}
