using System;
using System.Collections.Generic;
using System.Reactive;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Organization Custom Property Values API.
    /// </summary>
    /// <remarks>
    /// See <a href="https://docs.github.com/rest/orgs/custom-properties">Custom Properties API documentation</a> for more information.
    /// </remarks>
    public interface IObservableOrganizationCustomPropertyValuesClient
    {
        /// <summary>
        /// Get all custom property values for repositories an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#list-custom-property-values-for-organization-repositories">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        IObservable<IReadOnlyList<OrganizationCustomPropertyValues>> GetAll(string org);

        /// <summary>
        /// Get all custom property values for repositories an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#list-custom-property-values-for-organization-repositories">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="repositoryQuery">Finds repositories in the organization with a query containing one or more search keywords and qualifiers.</param>
        IObservable<IReadOnlyList<OrganizationCustomPropertyValues>> GetAll(string org, SearchRepositoriesRequest repositoryQuery);

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
        IObservable<Unit> CreateOrUpdate(string org, UpsertOrganizationCustomPropertyValues propertyValues);
    }
}
