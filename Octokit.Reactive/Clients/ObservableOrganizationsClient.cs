using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableOrganizationsClient : IObservableOrganizationsClient
    {
        readonly IOrganizationsClient _client;
        readonly IConnection _connection;

        /// <summary>
        /// Initializes a new Organization API client.
        /// </summary>
        /// <param name="client">An <see cref="IGitHubClient" /> used to make the requests</param>
        public ObservableOrganizationsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            Member = new ObservableOrganizationMembersClient(client);
            Team = new ObservableTeamsClient(client);
            Hook = new ObservableOrganizationHooksClient(client);
            OutsideCollaborator = new ObservableOrganizationOutsideCollaboratorsClient(client);
            Actions = new ObservableOrganizationActionsClient(client);

            _client = client.Organization;
            _connection = client.Connection;
        }

        /// <summary>
        /// Returns a client to manage members of an organization.
        /// </summary>
        public IObservableOrganizationMembersClient Member { get; private set; }

        /// <summary>
        /// Returns a client to manage teams for an organization.
        /// </summary>
        public IObservableTeamsClient Team { get; private set; }

        /// <summary>
        /// A client for GitHub's Organization Hooks API.
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/">Hooks API documentation</a> for more information.</remarks>
        public IObservableOrganizationHooksClient Hook { get; private set; }

        /// <summary>
        /// Returns a client to manage outside collaborators of an organization.
        /// </summary>
        public IObservableOrganizationOutsideCollaboratorsClient OutsideCollaborator { get; private set; }

        /// <summary>
        /// Returns a client to manage organization actions.
        /// </summary>
        public IObservableOrganizationActionsClient Actions { get; private set; }

        /// <summary>
        /// Returns the specified organization.
        /// </summary>
        /// <param name="org">The login of the specified organization,</param>
        /// <returns></returns>
        public IObservable<Organization> Get(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return _client.Get(org).ToObservable();
        }

        /// <summary>
        /// Returns all the organizations for the current user.
        /// </summary>
        /// <returns></returns>
        public IObservable<Organization> GetAllForCurrent()
        {
            return GetAllForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// Returns all the organizations for the current user.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        public IObservable<Organization> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Organization>(ApiUrls.UserOrganizations());
        }

        /// <summary>
        /// Returns all the organizations for the specified user
        /// </summary>
        /// <param name="user">The login for the user</param>
        /// <returns></returns>
        public IObservable<Organization> GetAllForUser(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return _connection.GetAndFlattenAllPages<Organization>(ApiUrls.UserOrganizations(user));
        }

        /// <summary>
        /// Returns all the organizations for the specified user
        /// </summary>
        /// <param name="user">The login for the user</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        public IObservable<Organization> GetAllForUser(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Organization>(ApiUrls.UserOrganizations(user), options);
        }

        /// <summary>
        /// Returns all the organizations
        /// </summary>
        /// <returns></returns>
        public IObservable<Organization> GetAll()
        {
            return _connection.GetAndFlattenAllPages<Organization>(ApiUrls.AllOrganizations());
        }

        /// <summary>
        /// Returns all the organizations
        /// </summary>
        /// <param name="request">Search parameters of the last organization seen</param>
        /// <returns></returns>
        public IObservable<Organization> GetAll(OrganizationRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            var url = ApiUrls.AllOrganizations(request.Since);

            return _connection.GetAndFlattenAllPages<Organization>(url);
        }

        /// <summary>
        /// Update the specified organization with data from <see cref="OrganizationUpdate"/>.
        /// </summary>
        /// <param name="org">The name of the organization to update.</param>
        /// <param name="updateRequest"></param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="Organization"/></returns>
        public IObservable<Organization> Update(string org, OrganizationUpdate updateRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(updateRequest, nameof(updateRequest));

            return _client.Update(org, updateRequest).ToObservable();
        }

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        public IObservable<OrganizationCredential> GetAllAuthorizations(string org)
        {
            Ensure.ArgumentNotNull(org, nameof(org));

            var url = ApiUrls.AllOrganizationCredentials(org);

            return _connection.GetAndFlattenAllPages<OrganizationCredential>(url);
        }

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        public IObservable<OrganizationCredential> GetAllAuthorizations(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNull(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            var url = ApiUrls.AllOrganizationCredentials(org);

            return _connection.GetAndFlattenAllPages<OrganizationCredential>(url, options);
        }

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <param name="login">Limits the list of credentials authorizations for an organization to a specific login</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        public IObservable<OrganizationCredential> GetAllAuthorizations(string org, string login)
        {
            Ensure.ArgumentNotNull(org, nameof(org));
            Ensure.ArgumentNotNull(login, nameof(login));

            var url = ApiUrls.AllOrganizationCredentials(org, login);

            return _connection.GetAndFlattenAllPages<OrganizationCredential>(url);
        }

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <param name="login">Limits the list of credentials authorizations for an organization to a specific login</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        public IObservable<OrganizationCredential> GetAllAuthorizations(string org, string login, ApiOptions options)
        {
            Ensure.ArgumentNotNull(org, nameof(org));
            Ensure.ArgumentNotNull(login, nameof(login));
            Ensure.ArgumentNotNull(options, nameof(options));

            var url = ApiUrls.AllOrganizationCredentials(org, login);

            return _connection.GetAndFlattenAllPages<OrganizationCredential>(url, options);
        }
    }
}
