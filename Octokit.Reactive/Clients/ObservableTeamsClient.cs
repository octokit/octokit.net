using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Org Teams API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/orgs/teams/">Orgs API documentation</a> for more information.
    /// </remarks>
    public class ObservableTeamsClient : IObservableTeamsClient
    {
        readonly IConnection _connection;
        readonly ITeamsClient _client;

        /// <summary>
        /// Initializes a new Organization Teams API client.
        /// </summary>
        /// <param name="client">An <see cref="IGitHubClient" /> used to make the requests</param>
        public ObservableTeamsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");
            _connection = client.Connection;
            _client = client.Organization.Team;
        }

        /// <summary>
        /// Gets a single <see cref="Team"/> by identifier.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#get-team
        /// </remarks>
        /// <param name="id">The team identifier.</param>
        /// <returns>The <see cref="Team"/> with the given identifier.</returns>
        public IObservable<Team> Get(int id)
        {
            return _client.Get(id).ToObservable();
        }

        /// <summary>
        /// Returns all <see cref="Team" />s for the current org.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the orgs's teams <see cref="Team"/>s.</returns>
        public IObservable<Team> GetAll(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            return GetAll(org, ApiOptions.None);
        }

        /// <summary>
        /// Returns all <see cref="Team" />s for the current org.
        /// </summary>
        /// <param name="org">Organization to list all teams of.</param>
        /// <param name="options">Options to change API behaviour.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the orgs's teams <see cref="Team"/>s.</returns>
        public IObservable<Team> GetAll(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Team>(ApiUrls.OrganizationTeams(org), null, AcceptHeaders.NestedTeamsPreview, options);
        }

        /// <summary>
        /// Returns all <see cref="Team" />s for the current user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the user's <see cref="Team"/>s.</returns>
        public IObservable<Team> GetAllForCurrent()
        {
            return GetAllForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// Returns all <see cref="Team" />s for the current user.
        /// </summary>
        /// <param name="options">Options to change API behaviour.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the user's <see cref="Team"/>s.</returns>
        public IObservable<Team> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Team>(ApiUrls.UserTeams(), null, AcceptHeaders.NestedTeamsPreview, options);
        }

        /// <summary>
        /// Returns all child teams of the given team.
        /// </summary>
        /// <param name="id">The team identifier</param>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-child-teams
        /// </remarks>
        public IObservable<Team> GetAllChildTeams(int id)
        {
            return GetAllChildTeams(id, ApiOptions.None);
        }

        /// <summary>
        /// Returns all child teams of the given team.
        /// </summary>
        /// <param name="id">The team identifier</param>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-child-teams
        /// </remarks>
        public IObservable<Team> GetAllChildTeams(int id, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Team>(ApiUrls.TeamChildTeams(id), null, AcceptHeaders.NestedTeamsPreview, options);
        }

        /// <summary>
        /// Returns all members of the given team. 
        /// </summary>
        /// <param name="id">The team identifier</param>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-team-members
        /// </remarks>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the team's member <see cref="User"/>s.</returns>
        public IObservable<User> GetAllMembers(int id)
        {
            return GetAllMembers(id, ApiOptions.None);
        }

        /// <summary>
        /// Returns all members of the given team. 
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-team-members
        /// </remarks>
        /// <param name="id">The team identifier</param>
        /// <param name="options">Options to change API behaviour.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the team's member <see cref="User"/>s.</returns>
        public IObservable<User> GetAllMembers(int id, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<User>(ApiUrls.TeamMembers(id), null, AcceptHeaders.NestedTeamsPreview, options);
        }

        /// <summary>
        /// Returns all members with the specified role in the given team of the given role.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-team-members
        /// </remarks>
        /// <param name="id">The team identifier</param>
        /// <param name="request">The request filter</param>
        public IObservable<User> GetAllMembers(int id, TeamMembersRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllMembers(id, request, ApiOptions.None);
        }

        /// <summary>
        /// Returns all members with the specified role in the given team of the given role.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-team-members
        /// </remarks>
        /// <param name="id">The team identifier</param>
        /// <param name="request">The request filter</param>
        /// <param name="options">Options to change API behaviour.</param>
        public IObservable<User> GetAllMembers(int id, TeamMembersRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<User>(ApiUrls.TeamMembers(id), request.ToParametersDictionary(), AcceptHeaders.NestedTeamsPreview, options);
        }

        /// <summary>
        /// Returns newly created <see cref="Team" /> for the current org.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Newly created <see cref="Team"/></returns>
        public IObservable<Team> Create(string org, NewTeam team)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNull(team, "team");

            return _client.Create(org, team).ToObservable();
        }

        /// <summary>
        /// Returns updated <see cref="Team" /> for the current org.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Updated <see cref="Team"/></returns>
        public IObservable<Team> Update(int id, UpdateTeam team)
        {
            Ensure.ArgumentNotNull(team, "team");

            return _client.Update(id, team).ToObservable();
        }

        /// <summary>
        /// Delete a team - must have owner permissions to this
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        public IObservable<Unit> Delete(int id)
        {
            return _client.Delete(id).ToObservable();
        }

        /// <summary>
        /// Adds a <see cref="User"/> to a <see cref="Team"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#add-or-update-team-membership">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The team identifier.</param>
        /// <param name="login">The user to add to the team.</param>
        [Obsolete("Please use AddOrEditMembership instead")]
        public IObservable<TeamMembership> AddMembership(int id, string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            return _client.AddMembership(id, login).ToObservable();
        }

        /// <summary>
        /// Adds a <see cref="User"/> to a <see cref="Team"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#add-or-update-team-membership">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The team identifier.</param>
        /// <param name="login">The user to add to the team.</param>
        /// <param name="request">Additional parameters for the request</param>
        public IObservable<TeamMembershipDetails> AddOrEditMembership(int id, string login, UpdateTeamMembership request)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));
            Ensure.ArgumentNotNull(request, nameof(request));

            return _client.AddOrEditMembership(id, login, request).ToObservable();
        }

        /// <summary>
        /// Removes a <see cref="User"/> from a <see cref="Team"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#remove-team-member">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The team identifier.</param>
        /// <param name="login">The user to remove from the team.</param>
        /// <returns><see langword="true"/> if the user was removed from the team; <see langword="false"/> otherwise.</returns>
        public IObservable<bool> RemoveMembership(int id, string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            return _client.RemoveMembership(id, login).ToObservable();
        }

        /// <summary>
        /// Gets whether the user with the given <paramref name="login"/> 
        /// is a member of the team with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The team to check.</param>
        /// <param name="login">The user to check.</param>
        [Obsolete("Please use GetMembershipDetails instead")]
        public IObservable<TeamMembership> GetMembership(int id, string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            return _client.GetMembership(id, login).ToObservable();
        }

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
        public IObservable<TeamMembershipDetails> GetMembershipDetails(int id, string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            return _client.GetMembershipDetails(id, login).ToObservable();
        }

        /// <summary>
        /// Returns all team's repositories.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The team's repositories</returns>
        public IObservable<Repository> GetAllRepositories(int id)
        {
            return GetAllRepositories(id, ApiOptions.None);
        }

        /// <summary>
        /// Returns all team's repositories.
        /// </summary>
        /// <param name="id">Team Id.</param>
        /// <param name="options">Options to change API behaviour.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The team's repositories</returns>
        public IObservable<Repository> GetAllRepositories(int id, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.TeamRepositories(id), null, AcceptHeaders.NestedTeamsPreview, options);
        }

        /// <summary>
        /// Adds a <see cref="Repository"/> to a <see cref="Team"/>.
        /// </summary>
        /// <param name="id">The team identifier.</param>
        /// <param name="organization">Org to associate the repo with.</param>
        /// <param name="repoName">Name of the repo.</param>
        /// <exception cref="ApiValidationException">Thrown if you attempt to add a repository to a team that is not owned by the organization.</exception>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#add-team-repo">API documentation</a> for more information.
        /// </remarks>
        /// <returns><see langword="true"/> if the repository was added to the team; <see langword="false"/> otherwise.</returns>
        public IObservable<bool> AddRepository(int id, string organization, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");
            Ensure.ArgumentNotNullOrEmptyString(repoName, "repoName");

            return _client.AddRepository(id, organization, repoName).ToObservable();
        }

        /// <summary>
        /// Adds a <see cref="Repository"/> to a <see cref="Team"/>.
        /// </summary>
        /// <param name="id">The team identifier.</param>
        /// <param name="organization">Org to associate the repo with.</param>
        /// <param name="repoName">Name of the repo.</param>
        /// <param name="permission">The permission to grant the team on this repository.</param>
        /// <exception cref="ApiValidationException">Thrown if you attempt to add a repository to a team that is not owned by the organization.</exception>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#add-team-repo">API documentation</a> for more information.
        /// </remarks>
        /// <returns><see langword="true"/> if the repository was added to the team; <see langword="false"/> otherwise.</returns>
        public IObservable<bool> AddRepository(int id, string organization, string repoName, RepositoryPermissionRequest permission)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");
            Ensure.ArgumentNotNullOrEmptyString(repoName, "repoName");

            return _client.AddRepository(id, organization, repoName, permission).ToObservable();
        }

        /// <summary>
        /// Remove a repository from the team
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        public IObservable<bool> RemoveRepository(int id, string organization, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");
            Ensure.ArgumentNotNullOrEmptyString(repoName, "repoName");

            return _client.RemoveRepository(id, organization, repoName).ToObservable();
        }

        /// <summary>
        /// Gets whether or not the given repository is managed by the given team.
        /// </summary>
        /// <param name="id">The team identifier</param>
        /// <param name="owner">Owner of the org the team is associated with.</param>
        /// <param name="repo">Name of the repo.</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#get-team-repo">API documentation</a> for more information.
        /// </remarks>
        /// <returns><see langword="true"/> if the repository is managed by the given team; <see langword="false"/> otherwise.</returns>
        public IObservable<bool> IsRepositoryManagedByTeam(int id, string owner, string repo)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");
            return _client.IsRepositoryManagedByTeam(id, owner, repo).ToObservable();
        }

        /// <summary>
        /// List all pending invitations for the given team.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#list-pending-team-invitations">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="id">The team identifier</param>
        /// <returns></returns>
        public IObservable<OrganizationMembershipInvitation> GetAllPendingInvitations(int id)
        {
            Ensure.ArgumentNotNull(id, nameof(id));

            return GetAllPendingInvitations(id, ApiOptions.None);
        }

        /// <summary>
        /// List all pending invitations for the given team.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#list-pending-team-invitations">API Documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="id">The team identifier</param>
        /// <param name="options">Options to change API behaviour</param>
        /// <returns></returns>
        public IObservable<OrganizationMembershipInvitation> GetAllPendingInvitations(int id, ApiOptions options)
        {
            Ensure.ArgumentNotNull(id, nameof(id));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<OrganizationMembershipInvitation>(ApiUrls.TeamPendingInvitations(id), null, AcceptHeaders.OrganizationMembershipPreview, options);
        }
    }
}
