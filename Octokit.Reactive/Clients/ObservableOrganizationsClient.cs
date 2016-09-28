using System;
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
            Ensure.ArgumentNotNull(client, "client");

            Member = new ObservableOrganizationMembersClient(client);
            Team = new ObservableTeamsClient(client);

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
        /// Returns the specified organization.
        /// </summary>
        /// <param name="org">The login of the specified organization,</param>
        /// <returns></returns>
        public IObservable<Organization> Get(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

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
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Organization>(ApiUrls.UserOrganizations());
        }

        /// <summary>
        /// Returns all the organizations for the specified user
        /// </summary>
        /// <param name="user">The login for the user</param>
        /// <returns></returns>
        [Obsolete("Please use ObservableOrganizationsClient.GetAllForUser() instead. This method will be removed in a future version")]
        public IObservable<Organization> GetAll(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return _connection.GetAndFlattenAllPages<Organization>(ApiUrls.UserOrganizations(user));
        }

        /// <summary>
        /// Returns all the organizations for the specified user
        /// </summary>
        /// <param name="user">The login for the user</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        [Obsolete("Please use ObservableOrganizationsClient.GetAllForUser() instead. This method will be removed in a future version")]
        public IObservable<Organization> GetAll(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Organization>(ApiUrls.UserOrganizations(user), options);
        }

        /// <summary>
        /// Returns all the organizations for the specified user
        /// </summary>
        /// <param name="user">The login for the user</param>
        /// <returns></returns>
        public IObservable<Organization> GetAllForUser(string user)
        {
          Ensure.ArgumentNotNullOrEmptyString(user, "user");

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
          Ensure.ArgumentNotNullOrEmptyString(user, "user");
          Ensure.ArgumentNotNull(options, "options");

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
            Ensure.ArgumentNotNull(request, "request");

            var url = ApiUrls.AllOrganizations(request.Since);

            return _connection.GetAndFlattenAllPages<Organization>(url);
        }

        /// <summary>
        /// Update the specified organization with data from <see cref="OrganizationUpdate"/>.
        /// </summary>
        /// <param name="organizationName">The name of the organization to update.</param>
        /// <param name="updateRequest"></param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="Organization"/></returns>
        public IObservable<Organization> Update(string organizationName, OrganizationUpdate updateRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(organizationName, "organizationName");
            Ensure.ArgumentNotNull(updateRequest, "updateRequest");

            return _client.Update(organizationName, updateRequest).ToObservable();
        }
    }
}
