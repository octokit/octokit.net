using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Organization Outside Collaborators API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/orgs/outside_collaborators/">Orgs API documentation</a> for more information.
    /// </remarks>
    public class OrganizationOutsideCollaboratorsClient : ApiClient, IOrganizationOutsideCollaboratorsClient
    {
        /// <summary>
        /// Initializes a new Organization Outside Collaborators API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public OrganizationOutsideCollaboratorsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// List all users who are outside collaborators of an organization. An outside collaborator is a user that
        /// is not a member of the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/outside_collaborators/#list-outside-collaborators">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <returns>The users</returns>
        [ManualRoute("GET", "/orgs/{org}/outside_collaborators")]
        public Task<IReadOnlyList<User>> GetAll(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return GetAll(org, ApiOptions.None);
        }

        /// <summary>
        /// List all users who are outside collaborators of an organization. An outside collaborator is a user that
        /// is not a member of the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/outside_collaborators/#list-outside-collaborators">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>The users</returns>
        [ManualRoute("GET", "/orgs/{org}/outside_collaborators")]
        public Task<IReadOnlyList<User>> GetAll(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<User>(ApiUrls.OutsideCollaborators(org), null, options);
        }

        /// <summary>
        /// List all users who are outside collaborators of an organization. An outside collaborator is a user that
        /// is not a member of the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/outside_collaborators/#list-outside-collaborators">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="filter">The filter to use when getting the users, <see cref="OrganizationMembersFilter"/></param>
        /// <returns>The users</returns>
        [ManualRoute("GET", "/orgs/{org}/outside_collaborators")]
        public Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return GetAll(org, filter, ApiOptions.None);
        }

        /// <summary>
        /// List all users who are outside collaborators of an organization. An outside collaborator is a user that
        /// is not a member of the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/outside_collaborators/#list-outside-collaborators">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="filter">The filter to use when getting the users, <see cref="OrganizationMembersFilter"/></param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>The users</returns>
        [ManualRoute("GET", "/orgs/{org}/outside_collaborators")]
        public Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<User>(ApiUrls.OutsideCollaborators(org, filter), null, options);
        }

        /// <summary>
        /// Removes a user as an outside collaborator from the organization, this will remove them from all repositories
        /// within the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/outside_collaborators/#remove-outside-collaborator">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="user">The login of the user</param>
        /// <returns></returns>
        [ManualRoute("DELETE", "/orgs/{org}/outside_collaborators/{username}")]
        public async Task<bool> Delete(string org, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            try
            {
                var statusCode = await Connection.Delete(ApiUrls.OutsideCollaborator(org, user)).ConfigureAwait(false);

                if (statusCode != HttpStatusCode.NoContent
                    && statusCode != (HttpStatusCode)422)
                {
                    throw new ApiException("Invalid Status Code returned. Expected a 204 or a 422", statusCode);
                }
                return statusCode == HttpStatusCode.NoContent;
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == (HttpStatusCode)422)
                {
                    throw new UserIsOrganizationMemberException(ex.HttpResponse);
                }

                throw;
            }
        }

        /// <summary>
        /// Converts an organization member to an outside collaborator, 
        /// when an organization member is converted to an outside collaborator, 
        /// they'll only have access to the repositories that their current team membership allows. 
        /// The user will no longer be a member of the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/outside_collaborators/#convert-member-to-outside-collaborator"> API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="user">The login for the user</param>
        /// <returns></returns>
        [ManualRoute("PUT", "/orgs/{org}/outside_collaborators/{username}")]
        public async Task<bool> ConvertFromMember(string org, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            try
            {
                var statusCode = await Connection.Put(ApiUrls.OutsideCollaborator(org, user));

                if (statusCode != HttpStatusCode.NoContent
                    && statusCode != HttpStatusCode.Forbidden)
                {
                    throw new ApiException("Invalid Status Code returned. Expected a 204 or a 403", statusCode);
                }

                return statusCode == HttpStatusCode.NoContent;
            }
            catch (ForbiddenException fex)
            {
                if (string.Equals(
                    "Cannot convert the last owner to an outside collaborator",
                    fex.Message,
                    StringComparison.OrdinalIgnoreCase))
                {
                    throw new UserIsLastOwnerOfOrganizationException(fex.HttpResponse);
                }
                throw;
            }
            catch (NotFoundException nfex)
            {
                if (string.Equals(
                    $"{user} is not a member of the {org} organization.",
                    nfex.Message,
                    StringComparison.OrdinalIgnoreCase))
                {
                    throw new UserIsNotMemberOfOrganizationException(nfex.HttpResponse);
                }

                throw;
            }
        }
    }
}
