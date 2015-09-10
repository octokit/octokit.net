using System;
using System.Net;
using System.Threading.Tasks;
#if NET_45
using System.Collections.Generic;
#endif

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Organization Teams API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/orgs/teams/">Organization Teams API documentation</a> for more information.
    /// </remarks>
    public class TeamsClient : ApiClient, ITeamsClient
    {
        /// <summary>
        /// Initializes a new GitHub Orgs Team API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public TeamsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets a single <see cref="Team"/> by identifier.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#get-team
        /// </remarks>
        /// <param name="id">The team identifier.</param>
        /// <returns>The <see cref="Team"/> with the given identifier.</returns>
        public Task<Team> Get(int id)
        {
            var endpoint = ApiUrls.Teams(id);

            return ApiConnection.Get<Team>(endpoint);
        }

        /// <summary>
        /// Returns all <see cref="Team" />s for the current org.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the orgs's teams <see cref="Team"/>s.</returns>
        public Task<IReadOnlyList<Team>> GetAll(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            var endpoint = ApiUrls.OrganizationTeams(org);
            return ApiConnection.GetAll<Team>(endpoint);
        }

        /// <summary>
        /// Returns all <see cref="Team" />s for the current user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the user's <see cref="Team"/>s.</returns>
        public Task<IReadOnlyList<Team>> GetAllForCurrent()
        {
            var endpoint = ApiUrls.UserTeams();
            return ApiConnection.GetAll<Team>(endpoint);
        }

        /// <summary>
        /// Returns all members of the given team. 
        /// </summary>
        /// <param name="id">The team identifier</param>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-team-members
        /// </remarks>
        /// <returns>A list of the team's member <see cref="User"/>s.</returns>
        public Task<IReadOnlyList<User>> GetAllMembers(int id)
        {
            var endpoint = ApiUrls.TeamMembers(id);

            return ApiConnection.GetAll<User>(endpoint);
        }

        /// <summary>
        /// Gets whether the user with the given <paramref name="login"/> 
        /// is a member of the team with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The team to check.</param>
        /// <param name="login">The user to check.</param>
        /// <returns>A <see cref="TeamMembership"/> result indicating the membership status</returns>
        public async Task<TeamMembership> GetMembership(int id, string login)
        {
            var endpoint = ApiUrls.TeamMember(id, login);

            Dictionary<string, string> response;

            try
            {
                response = await ApiConnection.Get<Dictionary<string, string>>(endpoint);
            }
            catch (NotFoundException)
            {
                return TeamMembership.NotFound;
            }

            return response["state"] == "active"
                ? TeamMembership.Active
                : TeamMembership.Pending;
        }

        /// <summary>
        /// Returns newly created <see cref="Team" /> for the current org.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Newly created <see cref="Team"/></returns>
        public Task<Team> Create(string org, NewTeam team)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNull(team, "team");

            var endpoint = ApiUrls.OrganizationTeams(org);
            return ApiConnection.Post<Team>(endpoint, team);
        }

        /// <summary>
        /// Returns updated <see cref="Team" /> for the current org.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Updated <see cref="Team"/></returns>
        public Task<Team> Update(int id, UpdateTeam team)
        {
            Ensure.ArgumentNotNull(team, "team");

            var endpoint = ApiUrls.Teams(id);
            return ApiConnection.Patch<Team>(endpoint, team);
        }

        /// <summary>
        /// Delte a team - must have owner permissions to this
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        public Task Delete(int id)
        {
            var endpoint = ApiUrls.Teams(id);

            return ApiConnection.Delete(endpoint);
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
        public async Task<TeamMembership> AddMembership(int id, string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            var endpoint = ApiUrls.TeamMember(id, login);

            Dictionary<string, string> response;

            try
            {
                response = await ApiConnection.Put<Dictionary<string, string>>(endpoint, RequestBody.Empty);
            }
            catch (NotFoundException)
            {
                return TeamMembership.NotFound;
            }

            if (response == null || !response.ContainsKey("state"))
            {
                return TeamMembership.NotFound;
            }

            return response["state"] == "active"
                ? TeamMembership.Active
                : TeamMembership.Pending;
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
        public async Task<bool> RemoveMembership(int id, string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            var endpoint = ApiUrls.TeamMember(id, login);

            try
            {
                var httpStatusCode = await ApiConnection.Connection.Delete(endpoint);

                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets whether the user with the given <paramref name="login"/> 
        /// is a member of the team with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The team to check.</param>
        /// <param name="login">The user to check.</param>
        /// <returns><see langword="true"/> if the user is a member of the team; <see langword="false"/> otherwise.</returns>
        [Obsolete("Use GetMembership(id, login) as this will report on pending requests")]
        public async Task<bool> IsMember(int id, string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            var endpoint = ApiUrls.TeamMember(id, login);

            try
            {
                var response = await ApiConnection.Connection.GetResponse<string>(endpoint);
                return response.HttpResponse.StatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns all team's repositories.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The team's repositories</returns>
        public Task<IReadOnlyList<Repository>> GetAllRepositories(int id)
        {
            var endpoint = ApiUrls.TeamRepositories(id);

            return ApiConnection.GetAll<Repository>(endpoint);
        }

        /// <summary>
        /// Returns all <see cref="Repository"/>(ies) associated with the given team. 
        /// </summary>
        /// <param name="id">The team identifier</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#list-team-repos">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A list of the team's <see cref="Repository"/>(ies).</returns>
        public Task<IReadOnlyList<Repository>> GetRepositories(int id)
        {
            var endpoint = ApiUrls.TeamRepositories(id);

            return ApiConnection.GetAll<Repository>(endpoint);
        }

        /// <summary>
        /// Add a repository to the team
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        public async Task<bool> AddRepository(int id, string organization, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");
            Ensure.ArgumentNotNullOrEmptyString(repoName, "repoName");

            var endpoint = ApiUrls.TeamRepository(id, organization, repoName);

            try
            {
                var httpStatusCode = await ApiConnection.Connection.Put(endpoint);
                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Remove a repository from the team
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        public async Task<bool> RemoveRepository(int id, string organization, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repoName, "repoName");

            var endpoint = ApiUrls.TeamRepository(id, organization, repoName);

            try
            {
                var httpStatusCode = await ApiConnection.Connection.Delete(endpoint);

                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
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
        public async Task<bool> IsRepositoryManagedByTeam(int id, string owner, string repo)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");

            var endpoint = ApiUrls.TeamRepository(id, owner, repo);

            try
            {
                var response = await ApiConnection.Connection.GetResponse<string>(endpoint);
                return response.HttpResponse.StatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }
    }
}
