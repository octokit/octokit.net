using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit
{
    public class ObservableOrganizationCustomPropertyValuesClient : IObservableOrganizationCustomPropertyValuesClient
    {
        readonly IOrganizationCustomPropertyValuesClient _client;
        readonly IConnection _connection;

        public ObservableOrganizationCustomPropertyValuesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Organization.CustomProperty.Values;
            _connection = client.Connection;
        }

        /// <summary>
        /// Get all custom property values for repositories an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#list-custom-property-values-for-organization-repositories">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<IReadOnlyList<OrganizationCustomPropertyValues>> GetAll(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return _client.GetAll(org).ToObservable();
        }

        /// <summary>
        /// Get all custom property values for repositories an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#list-custom-property-values-for-organization-repositories">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<IReadOnlyList<OrganizationCustomPropertyValues>> GetAll(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.GetAll(org, options).ToObservable();
        }

        /// <summary>
        /// Create new or update existing custom property values for repositories an organization.
        /// Using a value of null for a custom property will remove or 'unset' the property value from the repository.
        /// A maximum of 30 repositories can be updated in a single request.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#create-or-update-custom-property-values-for-organization-repositories">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="propertyValues">The custom property values to create or update</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Unit> CreateOrUpdate(string org, UpsertOrganizationCustomPropertyValues propertyValues)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(propertyValues, nameof(propertyValues));
            Ensure.ArgumentNotNullOrEmptyEnumerable(propertyValues.Properties, nameof(propertyValues.Properties));

            return _client.CreateOrUpdate(org, propertyValues).ToObservable();
        }
    }
}
