using System;
using System.Collections.Generic;
using System.Text;

namespace Octokit.Reactive
{
    public interface IObservableOrganizationOutsideCollaboratorsClient
    {
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
        IObservable<User> GetAll(string org);

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
        IObservable<User> GetAll(string org, ApiOptions options);

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
        IObservable<User> GetAll(string org, OrganizationMembersFilter filter);

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
        IObservable<User> GetAll(string org, OrganizationMembersFilter filter, ApiOptions options);
    }
}
