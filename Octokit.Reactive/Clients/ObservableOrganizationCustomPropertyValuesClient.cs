using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
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
        public IObservable<OrganizationCustomPropertyValues> GetAll(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return GetAll(org, new OrganizationCustomPropertyValuesRequest());
        }

        /// <summary>
        /// Get all custom property values for repositories an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#list-custom-property-values-for-organization-repositories">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<OrganizationCustomPropertyValues> GetAll(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            var url = ApiUrls.OrganizationCustomPropertyValues(org);

            return _connection.GetAndFlattenAllPages<OrganizationCustomPropertyValues>(url, options);
        }

        /// <summary>
        /// Get all custom property values for repositories an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#list-custom-property-values-for-organization-repositories">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="repositoryQuery">Finds repositories in the organization with a query containing one or more search keywords and qualifiers.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<OrganizationCustomPropertyValues> GetAll(string org, OrganizationCustomPropertyValuesRequest repositoryQuery)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(repositoryQuery, nameof(repositoryQuery));

            var url = ApiUrls.OrganizationCustomPropertyValues(org);

            return _connection.GetAndFlattenAllPages<OrganizationCustomPropertyValues>(url, repositoryQuery.Parameters);
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
            Ensure.ArgumentNotNullOrEmptyEnumerable(propertyValues.RepositoryNames, nameof(propertyValues.RepositoryNames));

            return _client.CreateOrUpdate(org, propertyValues).ToObservable();
        }
    }
}
