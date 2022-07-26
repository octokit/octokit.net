using System;
using System.Reactive.Threading.Tasks;
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
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Organization.OutsideCollaborator;
            _connection = client.Connection;
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
        public IObservable<User> GetAll(string org)
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
        public IObservable<User> GetAll(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<User>(ApiUrls.OutsideCollaborators(org), null, options);
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
        public IObservable<User> GetAll(string org, OrganizationMembersFilter filter)
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
        public IObservable<User> GetAll(string org, OrganizationMembersFilter filter, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<User>(ApiUrls.OutsideCollaborators(org, filter), null, options);
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
        public IObservable<bool> Delete(string org, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return _client.Delete(org, user).ToObservable();
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
        public IObservable<bool> ConvertFromMember(string org, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return _client.ConvertFromMember(org, user).ToObservable();
        }
    }
}
