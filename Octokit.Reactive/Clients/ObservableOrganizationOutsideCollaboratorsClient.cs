using System;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableOrganizationOutsideCollaboratorsClient : IObservableOrganizationOutsideCollaboratorsClient
    {
        readonly IOrganizationOutsideCollaboratorsClient _client;
        readonly IConnection _connection;

        /// <summary>
        /// Initializes a new Organization Outside Collaborators API client.
        /// </summary>
        /// <param name="client"></param>
        public ObservableOrganizationOutsideCollaboratorsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Organization.OutsideCollaborators;
            _connection = client.Connection;
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
        public IObservable<User> GetAll(string org)
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
        public IObservable<User> GetAll(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<User>(ApiUrls.OutsideCollaborators(org), null, AcceptHeaders.OrganizationMembershipPreview, options);
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
        public IObservable<User> GetAll(string org, OrganizationMembersFilter filter)
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
        public IObservable<User> GetAll(string org, OrganizationMembersFilter filter, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<User>(ApiUrls.OutsideCollaborators(org, filter), null, AcceptHeaders.OrganizationMembershipPreview, options);
        }
    }
}
