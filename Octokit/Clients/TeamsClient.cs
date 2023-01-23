﻿using System;
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
        [ManualRoute("GET", "/teams/{team_id}")]
        public Task<Team> Get(int id)
        {
            var endpoint = ApiUrls.Teams(id);

            return ApiConnection.Get<Team>(endpoint);
        }

        /// <summary>
        /// Returns all <see cref="Team" />s for the current org.
        /// </summary>
        /// <param name="org">Organization to list teams of.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the orgs's teams <see cref="Team"/>s.</returns>
        [ManualRoute("GET", "/orgs/{org}/teams")]
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
        [ManualRoute("GET", "/orgs/{org}/teams")]
        public Task<IReadOnlyList<Team>> GetAll(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.OrganizationTeams(org);
            return ApiConnection.GetAll<Team>(endpoint, options);
        }

        /// <summary>
        /// Returns all <see cref="Team" />s for the current user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the user's <see cref="Team"/>s.</returns>
        [ManualRoute("GET", "/user/teams")]
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
        [ManualRoute("GET", "/user/teams")]
        public Task<IReadOnlyList<Team>> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.UserTeams();

            return ApiConnection.GetAll<Team>(endpoint, options);
        }

        /// <summary>
        /// Returns all child teams of the given team.
        /// </summary>
        /// <param name="id">The team identifier</param>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-child-teams
        /// </remarks>
        [ManualRoute("GET", "/teams{id}/teams")]
        public Task<IReadOnlyList<Team>> GetAllChildTeams(int id)
        {
            return GetAllChildTeams(id, ApiOptions.None);
        }

        /// <summary>
        /// Returns all child teams of the given team.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-child-teams
        /// </remarks>
        /// <param name="id">The team identifier</param>
        /// <param name="options">Options to change API behaviour.</param>
        [ManualRoute("GET", "/teams{id}/teams")]
        public Task<IReadOnlyList<Team>> GetAllChildTeams(int id, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.TeamChildTeams(id);

            return ApiConnection.GetAll<Team>(endpoint, options);
        }

        /// <summary>
        /// Returns all members of the given team.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-team-members
        /// </remarks>
        /// <param name="id">The team identifier</param>
        [ManualRoute("GET", "/teams{id}/members")]
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
        [ManualRoute("GET", "/teams{id}/members")]
        public Task<IReadOnlyList<User>> GetAllMembers(int id, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.TeamMembers(id);

            return ApiConnection.GetAll<User>(endpoint, options);
        }

        /// <summary>
        /// Returns all members with the specified role in the given team of the given role.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-team-members
        /// </remarks>
        /// <param name="id">The team identifier</param>
        /// <param name="request">The request filter</param>
        [ManualRoute("GET", "/teams{id}/members")]
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
        [ManualRoute("GET", "/teams{id}/members")]
        public Task<IReadOnlyList<User>> GetAllMembers(int id, TeamMembersRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.TeamMembers(id);

            return ApiConnection.GetAll<User>(endpoint, request.ToParametersDictionary(), options);
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
        [ManualRoute("GET", "/teams/{team_id}/memberships/{username}")]
        public Task<TeamMembershipDetails> GetMembershipDetails(int id, string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));

            var endpoint = ApiUrls.TeamMember(id, login);

            return ApiConnection.Get<TeamMembershipDetails>(endpoint);
        }

        /// <summary>
        /// Returns newly created <see cref="Team" /> for the current org.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Newly created <see cref="Team"/></returns>
        [ManualRoute("POST", "/orgs/{org}/teams")]
        public Task<Team> Create(string org, NewTeam team)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(team, nameof(team));

            var endpoint = ApiUrls.OrganizationTeams(org);
            return ApiConnection.Post<Team>(endpoint, team);
        }

        /// <summary>
        /// Updates a team
        /// To edit a team, the authenticated user must either be an organization owner or a team maintainer
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/teams/teams?apiVersion=2022-11-28#update-a-team">API documentation</a> 
        /// for more information.
        /// </remarks>
        /// <returns>updated <see cref="Team" /> for the current org</returns>
        [ManualRoute("PATCH", "/orgs/{org}/teams/{team_slug}")]
        public Task<Team> Update(string org, string teamSlug, UpdateTeam team)
        {
            Ensure.ArgumentNotNull(org, nameof(org));
            Ensure.ArgumentNotNull(teamSlug, nameof(teamSlug));
            Ensure.ArgumentNotNull(team, nameof(team));

            var endpoint = ApiUrls.TeamsByOrganizationAndSlug(org, teamSlug);
            return ApiConnection.Patch<Team>(endpoint, team);
        }

        /// <summary>
        /// Returns updated <see cref="Team" /> for the current org.
        /// This endpoint route is deprecated and will be removed from the Teams API.
        /// We recommend migrating your existing code to use the new Update a team endpoint.
        /// <see cref="Update(string, string, UpdateTeam)"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/teams/teams?apiVersion=2022-11-28#update-a-team-legacy">API documentation</a> 
        /// for more information.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Updated <see cref="Team"/></returns>
        [ManualRoute("PATCH", "/teams/{team_id}")]
        public Task<Team> Update(int id, UpdateTeam team)
        {
            Ensure.ArgumentNotNull(team, nameof(team));

            var endpoint = ApiUrls.Teams(id);
            return ApiConnection.Patch<Team>(endpoint, team);
        }

        /// <summary>
        /// To delete a team, the authenticated user must be an organization owner or team maintainer.
        /// If you are an organization owner, deleting a parent team will delete all of its child teams as well.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/teams/teams?apiVersion=2022-11-28#delete-a-team">API documentation</a> 
        /// </remarks>
        /// <param name="org">The organization name. The name is not case sensitive.</param>
        /// <param name="teamSlug">The slug of the team name.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        [ManualRoute("DELETE", "/orgs/{org}/teams/{team_slug}")]
        public Task Delete(string org, string teamSlug)
        {
            Ensure.ArgumentNotNull(org, nameof(org));
            Ensure.ArgumentNotNull(teamSlug, nameof(teamSlug));

            var endpoint = ApiUrls.TeamsByOrganizationAndSlug(org, teamSlug);

            return ApiConnection.Delete(endpoint);
        }

        /// <summary>
        /// Delete a team - must have owner permissions to do this
        /// This endpoint route is deprecated and will be removed from the Teams API.
        /// We recommend migrating your existing code to use the new Delete a team endpoint.
        /// <see cref="Delete(string, string)"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/teams/teams?apiVersion=2022-11-28#delete-a-team-legacy">API documentation</a> 
        /// </remarks>
        /// <param name="id">The unique identifier of the team.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        [ManualRoute("DELETE", "/teams/{team_id}")]
        public Task Delete(int id)
        {
            var endpoint = ApiUrls.Teams(id);

            return ApiConnection.Delete(endpoint);
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
        [ManualRoute("PUT", "/teams/{team_id}/memberships/{username}")]
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
        [ManualRoute("DELETE", "/teams/{team_id}/memberships/{username}")]
        public async Task<bool> RemoveMembership(int id, string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));

            var endpoint = ApiUrls.TeamMember(id, login);

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
        /// Returns all team's repositories.
        /// </summary>
        /// <param name="id">Team Id.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The team's repositories</returns>
        [ManualRoute("GET", "/teams/{team_id}/repos")]
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
        [ManualRoute("GET", "/teams/{team_id}/repos")]
        public Task<IReadOnlyList<Repository>> GetAllRepositories(int id, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.TeamRepositories(id);

            return ApiConnection.GetAll<Repository>(endpoint, options);
        }

        /// <summary>
        /// Add or update team repository permissions (Legacy)
        /// Deprecation Notice: This endpoint route is deprecated and will be removed from the Teams API.
        /// We recommend migrating your existing code to use the new "Add or update team repository permissions" endpoint.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        [ManualRoute("PUT", "/teams/{team_id}/repos/{owner}/{repo}")]
        public async Task<bool> AddRepository(int id, string organization, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));

            // TODO: I am very suspicious of this due to the documentation
            // https://developer.github.com/v3/teams/#add-or-update-team-repository
            //
            // Recommended:
            // PUT /orgs/:org/teams/:team_slug/repos/:owner/:repo
            //
            // or
            //
            // PUT /organizations/:org_id/team/:team_id/repos/:owner/:repo
            //
            // Likely will require a breaking change

            var endpoint = ApiUrls.TeamRepository(id, organization, repoName);

            try
            {
                var httpStatusCode = await ApiConnection.Connection.Put(endpoint).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Add or update team repository permissions (Legacy)
        /// Deprecation Notice: This endpoint route is deprecated and will be removed from the Teams API.
        /// We recommend migrating your existing code to use the new "Add or update team repository permissions" endpoint.
        /// </summary>
        /// <param name="id">The team identifier.</param>
        /// <param name="organization">Org to associate the repo with.</param>
        /// <param name="repoName">Name of the repo.</param>
        /// <param name="permission">The permission to grant the team on this repository.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        [ManualRoute("PUT", "/teams/{team_id}/repos/{owner}/{repo}")]
        public async Task<bool> AddRepository(int id, string organization, string repoName, RepositoryPermissionRequest permission)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));

            // TODO: I am very suspicious of this due to the documentation
            // https://developer.github.com/v3/teams/#add-or-update-team-repository
            //
            // Recommended:
            // PUT /orgs/:org/teams/:team_slug/repos/:owner/:repo
            //
            // or
            //
            // PUT /organizations/:org_id/team/:team_id/repos/:owner/:repo
            //
            // Likely will require a breaking change

            var endpoint = ApiUrls.TeamRepository(id, organization, repoName);

            try
            {
                var httpStatusCode = await ApiConnection.Connection.Put<HttpStatusCode>(endpoint, permission).ConfigureAwait(false);
                return httpStatusCode.HttpResponse.StatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Remove a repository from a team (Legacy)
        /// Deprecation Notice: This endpoint route is deprecated and will be removed from the Teams API.
        /// We recommend migrating your existing code to use the new Remove a repository from a team endpoint.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        [ManualRoute("DELETE", "/teams/{team_id}/repos/{owner}/{repo}")]
        public async Task<bool> RemoveRepository(int id, string organization, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));

            // TODO: I am very suspicious of this due to the documentation
            // https://developer.github.com/v3/teams/#remove-team-repository
            //
            // Recommended:
            // DELETE /orgs/:org/teams/:team_slug/repos/:owner/:repo
            //
            // or
            //
            // DELETE /organizations/:org_id/team/:team_id/repos/:owner/:repo
            //
            // Likely will require a breaking change

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
        [ManualRoute("GET", "/teams/{team_id}/repos/{owner}/{name}")]
        public async Task<bool> IsRepositoryManagedByTeam(int id, string owner, string repo)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

            var endpoint = ApiUrls.TeamRepository(id, owner, repo);

            try
            {
                var response = await ApiConnection.Connection.Get<string>(endpoint, null, AcceptHeaders.StableVersionJson).ConfigureAwait(false);
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
        [ManualRoute("GET", "/teams/{team_id}/invitations")]
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
        [ManualRoute("GET", "/teams/{team_id}/invitations")]
        public Task<IReadOnlyList<OrganizationMembershipInvitation>> GetAllPendingInvitations(int id, ApiOptions options)
        {
            return ApiConnection.GetAll<OrganizationMembershipInvitation>(ApiUrls.TeamPendingInvitations(id), null, options);
        }

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
        /// <returns></returns>
        [ManualRoute("GET", "/orgs/{org}/teams/{team_slug}/repos/{owner}/{repo}")]
        public async Task<bool> CheckTeamPermissionsForARepository(string org, string teamSlug, string owner, string repo)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(teamSlug, nameof(teamSlug));
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

            var endpoint = ApiUrls.TeamPermissionsForARepository(org, teamSlug, owner, repo);

            try
            {
                var response = await ApiConnection.Get<TeamRepository>(endpoint);
                return response == null;
            }
            catch(NotFoundException)
            {
                return false;
            }
        }

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
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        [ManualRoute("GET", "/orgs/{org}/teams/{team_slug}/repos/{owner}/{repo}")]
        public Task<TeamRepository> CheckTeamPermissionsForARepositoryWithCustomAcceptHeader(string org, string teamSlug, string owner, string repo)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(teamSlug, nameof(teamSlug));
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

            var endpoint = ApiUrls.TeamPermissionsForARepository(org, teamSlug, owner, repo);

            return ApiConnection.Get<TeamRepository>(endpoint, null, AcceptHeaders.RepositoryContentMediaType);
        }

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
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        [ManualRoute("PUT", "/orgs/{org}/teams/{team_slug}/repos/{owner}/{repo}")]
        public Task AddOrUpdateTeamRepositoryPermissions(string org, string teamSlug, string owner, string repo, string permission)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(teamSlug, nameof(teamSlug));
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

            var endpoint = ApiUrls.TeamPermissionsForARepository(org, teamSlug, owner, repo);

            return ApiConnection.Put(endpoint, new { permission });
        }

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
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        [ManualRoute("DELETE", "/orgs/{org}/teams/{team_slug}/repos/{owner}/{repo}")]
        public Task RemoveRepositoryFromATeam(string org, string teamSlug, string owner, string repo)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(teamSlug, nameof(teamSlug));
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

            var endpoint = ApiUrls.TeamPermissionsForARepository(org, teamSlug, owner, repo);

            return ApiConnection.Delete(endpoint);
        }
    }
}
