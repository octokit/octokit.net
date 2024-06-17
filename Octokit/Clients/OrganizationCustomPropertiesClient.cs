using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class OrganizationCustomPropertiesClient : ApiClient, IOrganizationCustomPropertiesClient
    {
        /// <summary>
        /// Initializes a new GitHub Organization Custom Properties API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public OrganizationCustomPropertiesClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
            Values = new OrganizationCustomPropertyValuesClient(apiConnection);
        }

        /// <summary>
        /// Get all custom properties for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#get-all-custom-properties-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        [ManualRoute("GET", "orgs/{org}/properties/schema")]
        public Task<IReadOnlyList<OrganizationCustomProperty>> GetAll(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            var url = ApiUrls.OrganizationCustomProperties(org);

            return ApiConnection.Get<IReadOnlyList<OrganizationCustomProperty>>(url, null);
        }

        /// <summary>
        /// Get a single custom property by name.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#get-a-custom-property-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="propertyName">The name of the custom property</param>
        [ManualRoute("GET", "orgs/{org}/properties/schema/{propertyName}")]
        public Task<OrganizationCustomProperty> Get(string org, string propertyName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(propertyName, nameof(propertyName));

            var url = ApiUrls.OrganizationCustomProperty(org, propertyName);

            return ApiConnection.Get<OrganizationCustomProperty>(url);
        }

        /// <summary>
        /// Create new or update existing custom properties for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#create-or-update-custom-properties-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="properties">The custom properties to create or update</param>
        [ManualRoute("PATCH", "orgs/{org}/properties/schema")]
        public Task<IReadOnlyList<OrganizationCustomProperty>> CreateOrUpdate(string org, UpsertOrganizationCustomProperties properties)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(properties, nameof(properties));
            Ensure.ArgumentNotNullOrEmptyEnumerable(properties.Properties, nameof(properties.Properties));

            var url = ApiUrls.OrganizationCustomProperties(org);

            return ApiConnection.Patch<IReadOnlyList<OrganizationCustomProperty>>(url, properties);
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
        [ManualRoute("PUT", "orgs/{org}/properties/schema/{propertyName}")]
        public Task<OrganizationCustomProperty> CreateOrUpdate(string org, string propertyName, UpsertOrganizationCustomProperty property)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(propertyName, nameof(propertyName));
            Ensure.ArgumentNotNull(property, nameof(property));
            Ensure.ArgumentNotNullOrDefault(property.ValueType, nameof(property.ValueType));

            var url = ApiUrls.OrganizationCustomProperty(org, propertyName);

            return ApiConnection.Put<OrganizationCustomProperty>(url, property);
        }

        /// <summary>
        /// Removes a custom property that is defined for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#remove-a-custom-property-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="propertyName">The name of the custom property</param>
        [ManualRoute("DELETE", "orgs/{org}/properties/schema/{propertyName}")]
        public Task Delete(string org, string propertyName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(propertyName, nameof(propertyName));

            var url = ApiUrls.OrganizationCustomProperty(org, propertyName);

            return ApiConnection.Delete(url);
        }

        /// <summary>
        /// A client for GitHub's Organization Custom Property Values API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties">Custom Properties API documentation</a> for more information.
        /// </remarks>
        public IOrganizationCustomPropertyValuesClient Values { get; private set; }
    }
}
