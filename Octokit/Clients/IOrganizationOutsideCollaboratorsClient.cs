using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IOrganizationOutsideCollaboratorsClient
    {
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
        Task<IReadOnlyList<User>> GetAll(string org);

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
        Task<IReadOnlyList<User>> GetAll(string org, ApiOptions options);

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
        Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter);

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
        Task<IReadOnlyList<User>> GetAll(string org, OrganizationMembersFilter filter, ApiOptions options);

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
        Task<bool> Delete(string org, string user);

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
        Task<bool> ConvertFromMember(string org, string user);
    }
}
