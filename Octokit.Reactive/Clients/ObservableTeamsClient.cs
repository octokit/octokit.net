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
            return _connection.GetAndFlattenAllPages<Team>(ApiUrls.OrganizationTeams(org));
        }

        /// <summary>
        /// Returns all <see cref="Team" />s for the current user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the user's <see cref="Team"/>s.</returns>
        public IObservable<Team> GetAllForCurrent()
        {
            return _connection.GetAndFlattenAllPages<Team>(ApiUrls.UserTeams());
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
            return _connection.GetAndFlattenAllPages<User>(ApiUrls.TeamMembers(id));
        }

        /// <summary>
        /// Returns newly created <see cref="Team" /> for the current org.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Newly created <see cref="Team"/></returns>
        public IObservable<Team> Create(string org, NewTeam team)
        {
            return _client.Create(org, team).ToObservable();
        }

        /// <summary>
        /// Returns updated <see cref="Team" /> for the current org.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Updated <see cref="Team"/></returns>
        public IObservable<Team> Update(int id, UpdateTeam team)
        {
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
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#add-team-member">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The team identifier.</param>
        /// <param name="login">The user to add to the team.</param>
        /// <exception cref="ApiValidationException">Thrown if you attempt to add an organization to a team.</exception>
        /// <returns>A <see cref="TeamMembership"/> result indicating the membership status</returns>
        public IObservable<TeamMembership> AddMembership(int id, string login)
        {
            return _client.AddMembership(id, login).ToObservable();
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
            return _client.RemoveMembership(id, login).ToObservable();
        }

        /// <summary>
        /// Gets whether the user with the given <paramref name="login"/> 
        /// is a member of the team with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The team to check.</param>
        /// <param name="login">The user to check.</param>
        /// <returns><see langword="true"/> if the user is a member of the team; <see langword="false"/> otherwise.</returns>
        [Obsolete("Use GetMembership(id, login) to detect pending memberships")]
        public IObservable<bool> IsMember(int id, string login)
        {
            return _client.IsMember(id, login).ToObservable();
        }

        /// <summary>
        /// Gets whether the user with the given <paramref name="login"/> 
        /// is a member of the team with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The team to check.</param>
        /// <param name="login">The user to check.</param>
        /// <returns>A <see cref="TeamMembership"/> result indicating the membership status</returns>
        public IObservable<TeamMembership> GetMembership(int id, string login)
        {
            return _client.GetMembership(id, login).ToObservable();
        }

        /// <summary>
        /// Returns all team's repositories.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The team's repositories</returns>
        public IObservable<Repository> GetAllRepositories(int id)
        {
            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.TeamRepositories(id));
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
            return _client.AddRepository(id, organization, repoName).ToObservable();
        }


        /// <summary>
        /// Remove a repository from the team
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        public IObservable<bool> RemoveRepository(int id, string organization, string repoName)
        {
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
            return _client.IsRepositoryManagedByTeam(id, owner, repo).ToObservable();
        }
    }
}
