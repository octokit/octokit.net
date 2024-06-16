using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class OrganizationCustomPropertyValuesClient : ApiClient, IOrganizationCustomPropertyValuesClient
    {
        /// <summary>
        /// Initializes a new GitHub Organization Custom Property Values API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public OrganizationCustomPropertyValuesClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Get all custom property values for repositories an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#list-custom-property-values-for-organization-repositories">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "orgs/{org}/properties/values")]
        public Task<IReadOnlyList<OrganizationCustomPropertyValues>> GetAll(string org)
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
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "orgs/{org}/properties/values")]
        public Task<IReadOnlyList<OrganizationCustomPropertyValues>> GetAll(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            var url = ApiUrls.OrganizationCustomPropertyValues(org);

            return ApiConnection.GetAll<OrganizationCustomPropertyValues>(url, options);
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
        [ManualRoute("GET", "orgs/{org}/properties/values")]
        public Task<IReadOnlyList<OrganizationCustomPropertyValues>> GetAll(string org, OrganizationCustomPropertyValuesRequest repositoryQuery)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(repositoryQuery, nameof(repositoryQuery));

            var url = ApiUrls.OrganizationCustomPropertyValues(org);

            return ApiConnection.GetAll<OrganizationCustomPropertyValues>(url, repositoryQuery.Parameters);
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
        [ManualRoute("PATCH", "orgs/{org}/properties/values")]
        public Task CreateOrUpdate(string org, UpsertOrganizationCustomPropertyValues propertyValues)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(propertyValues, nameof(propertyValues));
            Ensure.ArgumentNotNullOrEmptyEnumerable(propertyValues.Properties, nameof(propertyValues.Properties));

            var url = ApiUrls.OrganizationCustomPropertyValues(org);

            return ApiConnection.Patch(url, propertyValues);
        }
    }
}
