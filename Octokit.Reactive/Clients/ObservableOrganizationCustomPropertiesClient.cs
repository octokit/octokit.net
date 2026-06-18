using System;
using System.Threading;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableOrganizationCustomPropertiesClient : IObservableOrganizationCustomPropertiesClient
    {
        readonly IOrganizationCustomPropertiesClient _client;
        readonly IConnection _connection;

        public ObservableOrganizationCustomPropertiesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            Values = new ObservableOrganizationCustomPropertyValuesClient(client);

            _client = client.Organization.CustomProperty;
            _connection = client.Connection;
        }

        /// <summary>
        /// Get all custom properties for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#get-all-custom-properties-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        public IObservable<OrganizationCustomProperty> GetAll(string org, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return _client.GetAll(org, cancellationToken).ToObservable().SelectMany(p => p);
        }

        /// <summary>
        /// Get a single custom property by name.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#get-a-custom-property-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="propertyName">The name of the custom property</param>
        public IObservable<OrganizationCustomProperty> Get(string org, string propertyName, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(propertyName, nameof(propertyName));

            return _client.Get(org, propertyName, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Create new or update existing custom properties for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#create-or-update-custom-properties-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="properties">The custom properties to create or update</param>
        public IObservable<OrganizationCustomProperty> CreateOrUpdate(string org, UpsertOrganizationCustomProperties properties, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(properties, nameof(properties));
            Ensure.ArgumentNotNullOrEmptyEnumerable(properties.Properties, nameof(properties.Properties));

            return _client.CreateOrUpdate(org, properties, cancellationToken).ToObservable().SelectMany(p => p);
        }

        /// <summary>
        /// Create new or update existing custom property for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#create-or-update-a-custom-property-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="propertyName">The name of the custom property</param>
        /// <param name="property">The custom property to create or update</param>
        public IObservable<OrganizationCustomProperty> CreateOrUpdate(string org, string propertyName, UpsertOrganizationCustomProperty property, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(propertyName, nameof(propertyName));
            Ensure.ArgumentNotNull(property, nameof(property));
            Ensure.ArgumentNotNullOrDefault(property.ValueType, nameof(property.ValueType));

            return _client.CreateOrUpdate(org, propertyName, property, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Removes a custom property that is defined for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#remove-a-custom-property-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="propertyName">The name of the custom property</param>
        public IObservable<Unit> Delete(string org, string propertyName, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(propertyName, nameof(propertyName));

            return _client.Delete(org, propertyName, cancellationToken).ToObservable();
        }

        /// <summary>
        /// A client for GitHub's Organization Custom Property Values API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties">Custom Properties API documentation</a> for more information.
        /// </remarks>
        public IObservableOrganizationCustomPropertyValuesClient Values { get; private set; }
    }
}
