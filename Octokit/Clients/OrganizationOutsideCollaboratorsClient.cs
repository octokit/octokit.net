using System.Collections.Generic;
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
        /// are not a member of the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/outside_collaborators/#list-outside-collaborators">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <returns>The users</returns>
        public Task<IReadOnlyList<User>> GetAll(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            return GetAll(org, ApiOptions.None);
        }

        /// <summary>
        /// List all users who are outside collaborators of an organization. An outside collaborator is a user that
        /// are not a member of the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/outside_collaborators/#list-outside-collaborators">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>The users</returns>
        public Task<IReadOnlyList<User>> GetAll(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<User>(ApiUrls.OutsideCollaborators(org), null, AcceptHeaders.OrganizationMembershipPreview, options);
        }

        /// <summary>
        /// List all users who are outside collaborators of an organization. An outside collaborator is a user that
        /// are not a member of the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/outside_collaborators/#list-outside-collaborators">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="filter">The filter to use when getting the users, <see cref="OrganizationMembersFilter"/></param>
        /// <returns>The users</returns>
        public Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            return GetAll(org, filter, ApiOptions.None);
        }

        /// <summary>
        /// List all users who are outside collaborators of an organization. An outside collaborator is a user that
        /// are not a member of the organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/outside_collaborators/#list-outside-collaborators">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="org">The login for the organization</param>
        /// <param name="filter">The filter to use when getting the users, <see cref="OrganizationMembersFilter"/></param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>The users</returns>
        public Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<User>(ApiUrls.OutsideCollaborators(org, filter), null, AcceptHeaders.OrganizationMembershipPreview, options);
        }
    }
}
