using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit
{
    public class ObservableOrganizationCustomPropertiesClient : IObservableOrganizationCustomPropertiesClient
    {
        readonly IOrganizationCustomPropertiesClient _client;
        readonly IConnection _connection;

        public ObservableOrganizationCustomPropertiesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Organization.CustomProperty;
            _connection = client.Connection;

            Values = new ObservableOrganizationCustomPropertyValuesClient(client);
        }

        /// <summary>
        /// Get all custom properties for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#get-all-custom-properties-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        public IObservable<IReadOnlyList<OrganizationCustomProperty>> GetAll(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return _client.GetAll(org).ToObservable();
        }

        /// <summary>
        /// Get a single custom property by name.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#get-a-custom-property-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="propertyName">The name of the custom property</param>
        public IObservable<OrganizationCustomProperty> Get(string org, string propertyName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(propertyName, nameof(propertyName));

            return _client.Get(org, propertyName).ToObservable();
        }

        /// <summary>
        /// Create new or update existing custom properties for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#create-or-update-custom-properties-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="properties">The custom properties to create or update</param>
        public IObservable<IReadOnlyList<OrganizationCustomProperty>> CreateOrUpdate(string org, UpsertOrganizationCustomProperties properties)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(properties, nameof(properties));

            return _client.CreateOrUpdate(org, properties).ToObservable();
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
        public IObservable<OrganizationCustomProperty> CreateOrUpdate(string org, string propertyName, OrganizationCustomProperty property)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(propertyName, nameof(propertyName));
            Ensure.ArgumentNotNull(property, nameof(property));

            return _client.CreateOrUpdate(org, propertyName, property).ToObservable();
        }

        /// <summary>
        /// Removes a custom property that is defined for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#remove-a-custom-property-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="propertyName">The name of the custom property</param>
        public IObservable<Unit> Delete(string org, string propertyName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(propertyName, nameof(propertyName));

            return _client.Delete(org, propertyName).ToObservable();
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
