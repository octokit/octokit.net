using System;
using System.Net;
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

            return ApiConnection.Get<Team>(endpoint, null, AcceptHeaders.NestedTeamsPreview);
        }

        /// <summary>
        /// Returns all <see cref="Team" />s for the current org.
        /// </summary>
        /// <param name="org">Organization to list teams of.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the orgs's teams <see cref="Team"/>s.</returns>
        public Task<IReadOnlyList<Team>> GetAll(string org)
        {
            return GetAll(org, ApiOptions.None);
        }

        /// <summary>
        /// Returns all <see cref="Team" />s for the current org.
        /// </summary>
        /// <param name="org">Organization to list teams of.</param>
        /// <param name="options">Options to change API behaviour.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the orgs's teams <see cref="Team"/>s.</returns>
        public Task<IReadOnlyList<Team>> GetAll(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNull(options, "options");

            var endpoint = ApiUrls.OrganizationTeams(org);
            return ApiConnection.GetAll<Team>(endpoint, null, AcceptHeaders.NestedTeamsPreview, options);
        }

        /// <summary>
        /// Returns all <see cref="Team" />s for the current user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the user's <see cref="Team"/>s.</returns>
        public Task<IReadOnlyList<Team>> GetAllForCurrent()
        {
            return GetAllForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// Returns all <see cref="Team" />s for the current user.
        /// </summary>
        /// <param name="options">Options to change API behaviour.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the user's <see cref="Team"/>s.</returns>
        public Task<IReadOnlyList<Team>> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            var endpoint = ApiUrls.UserTeams();

            return ApiConnection.GetAll<Team>(endpoint, null, AcceptHeaders.NestedTeamsPreview, options);
        }

        /// <summary>
        /// Returns all child teams of the given team.
        /// </summary>
        /// <param name="id">The team identifier</param>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-child-teams
        /// </remarks>
        public Task<IReadOnlyList<Team>> GetAllChildTeams(int id)
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
        public Task<IReadOnlyList<Team>> GetAllChildTeams(int id, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.TeamChildTeams(id);

            return ApiConnection.GetAll<Team>(endpoint, null, AcceptHeaders.NestedTeamsPreview, options);
        }

        /// <summary>
        /// Returns all members of the given team. 
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-team-members
        /// </remarks>
        /// <param name="id">The team identifier</param>
        public Task<IReadOnlyList<User>> GetAllMembers(int id)
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
        public Task<IReadOnlyList<User>> GetAllMembers(int id, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            var endpoint = ApiUrls.TeamMembers(id);

            return ApiConnection.GetAll<User>(endpoint, null, AcceptHeaders.NestedTeamsPreview, options);
        }

        /// <summary>
        /// Returns all members with the specified role in the given team of the given role.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-team-members
        /// </remarks>
        /// <param name="id">The team identifier</param>
        /// <param name="request">The request filter</param>
        public Task<IReadOnlyList<User>> GetAllMembers(int id, TeamMembersRequest request)
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
        public Task<IReadOnlyList<User>> GetAllMembers(int id, TeamMembersRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.TeamMembers(id);

            return ApiConnection.GetAll<User>(endpoint, request.ToParametersDictionary(), AcceptHeaders.NestedTeamsPreview, options);
        }

        /// <summary>
        /// Gets whether the user with the given <paramref name="login"/> 
        /// is a member of the team with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The team to check.</param>
        /// <param name="login">The user to check.</param>
        [Obsolete("Please use GetMembershipDetails instead")]
        public async Task<TeamMembership> GetMembership(int id, string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            var endpoint = ApiUrls.TeamMember(id, login);

            Dictionary<string, string> response;

            try
            {
                response = await ApiConnection.Get<Dictionary<string, string>>(endpoint, null, AcceptHeaders.NestedTeamsPreview).ConfigureAwait(false);
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
        /// Gets whether the user with the given <paramref name="login"/> 
        /// is a member of the team with the given <paramref name="id"/>.
        /// A <see cref="NotFoundException"/> is thrown if the user is not a member.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#get-team-membership">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The team to check.</param>
        /// <param name="login">The user to check.</param>
        public Task<TeamMembershipDetails> GetMembershipDetails(int id, string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));

            var endpoint = ApiUrls.TeamMember(id, login);

            return ApiConnection.Get<TeamMembershipDetails>(endpoint, null, AcceptHeaders.NestedTeamsPreview);
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
            return ApiConnection.Post<Team>(endpoint, team, AcceptHeaders.NestedTeamsPreview);
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
            return ApiConnection.Patch<Team>(endpoint, team, AcceptHeaders.NestedTeamsPreview);
        }

        /// <summary>
        /// Delte a team - must have owner permissions to this
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        public Task Delete(int id)
        {
            var endpoint = ApiUrls.Teams(id);

            return ApiConnection.Delete(endpoint, new object(), AcceptHeaders.NestedTeamsPreview);
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
        public async Task<TeamMembership> AddMembership(int id, string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            var endpoint = ApiUrls.TeamMember(id, login);

            Dictionary<string, string> response;

            try
            {
                response = await ApiConnection.Put<Dictionary<string, string>>(endpoint, RequestBody.Empty).ConfigureAwait(false);
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
        /// Adds a <see cref="User"/> to a <see cref="Team"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#add-or-update-team-membership">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The team identifier.</param>
        /// <param name="login">The user to add to the team.</param>
        /// <param name="request">Additional parameters for the request</param>
        public Task<TeamMembershipDetails> AddOrEditMembership(int id, string login, UpdateTeamMembership request)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));
            Ensure.ArgumentNotNull(request, nameof(request));

            var endpoint = ApiUrls.TeamMember(id, login);

            return ApiConnection.Put<TeamMembershipDetails>(endpoint, request);
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
                var httpStatusCode = await ApiConnection.Connection.Delete(endpoint, null, AcceptHeaders.NestedTeamsPreview).ConfigureAwait(false);

                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns all team's repositories.
        /// </summary>
        /// <param name="id">Team Id.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The team's repositories</returns>
        public Task<IReadOnlyList<Repository>> GetAllRepositories(int id)
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
        public Task<IReadOnlyList<Repository>> GetAllRepositories(int id, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            var endpoint = ApiUrls.TeamRepositories(id);

            return ApiConnection.GetAll<Repository>(endpoint, null, AcceptHeaders.NestedTeamsPreview, options);
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
                var httpStatusCode = await ApiConnection.Connection.Put(endpoint, AcceptHeaders.NestedTeamsPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Add a repository to the team
        /// </summary>
        /// <param name="id">The team identifier.</param>
        /// <param name="organization">Org to associate the repo with.</param>
        /// <param name="repoName">Name of the repo.</param>
        /// <param name="permission">The permission to grant the team on this repository.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        public async Task<bool> AddRepository(int id, string organization, string repoName, RepositoryPermissionRequest permission)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");
            Ensure.ArgumentNotNullOrEmptyString(repoName, "repoName");

            var endpoint = ApiUrls.TeamRepository(id, organization, repoName);

            try
            {
                var httpStatusCode = await ApiConnection.Connection.Put<HttpStatusCode>(endpoint, permission, "", AcceptHeaders.NestedTeamsPreview).ConfigureAwait(false);
                return httpStatusCode.HttpResponse.StatusCode == HttpStatusCode.NoContent;
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
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");
            Ensure.ArgumentNotNullOrEmptyString(repoName, "repoName");

            var endpoint = ApiUrls.TeamRepository(id, organization, repoName);

            try
            {
                var httpStatusCode = await ApiConnection.Connection.Delete(endpoint).ConfigureAwait(false);

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
                var response = await ApiConnection.Connection.Get<string>(endpoint, null, AcceptHeaders.NestedTeamsPreview).ConfigureAwait(false);
                return response.HttpResponse.StatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
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
        public Task<IReadOnlyList<OrganizationMembershipInvitation>> GetAllPendingInvitations(int id)
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
        public Task<IReadOnlyList<OrganizationMembershipInvitation>> GetAllPendingInvitations(int id, ApiOptions options)
        {
            return ApiConnection.GetAll<OrganizationMembershipInvitation>(ApiUrls.TeamPendingInvitations(id), null, AcceptHeaders.OrganizationMembershipPreview, options);
        }
    }
}
