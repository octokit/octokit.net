using System;
using System.Reactive;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Organization Custom Properties API.
    /// </summary>
    /// <remarks>
    /// See <a href="https://docs.github.com/rest/orgs/custom-properties">Custom Properties API documentation</a> for more information.
    /// </remarks>
    public interface IObservableOrganizationCustomPropertiesClient
    {
        /// <summary>
        /// Get all custom properties for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#get-all-custom-properties-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        IObservable<OrganizationCustomProperty> GetAll(string org);

        /// <summary>
        /// Get a single custom property by name.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#get-a-custom-property-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="propertyName">The name of the custom property</param>
        IObservable<OrganizationCustomProperty> Get(string org, string propertyName);

        /// <summary>
        /// Create new or update existing custom properties for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#create-or-update-custom-properties-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="properties">The custom properties to create or update</param>
        IObservable<OrganizationCustomProperty> CreateOrUpdate(string org, UpsertOrganizationCustomProperties properties);

        /// <summary>
        /// Create new or update existing custom property for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#create-or-update-a-custom-property-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="propertyName">The name of the custom property</param>
        /// <param name="property">The custom property to create or update</param>
        IObservable<OrganizationCustomProperty> CreateOrUpdate(string org, string propertyName, UpsertOrganizationCustomProperty property);

        /// <summary>
        /// Removes a custom property that is defined for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#remove-a-custom-property-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="propertyName">The name of the custom property</param>
        IObservable<Unit> Delete(string org, string propertyName);

        /// <summary>
        /// A client for GitHub's Organization Custom Property Values API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties">Custom Properties API documentation</a> for more information.
        /// </remarks>
        IObservableOrganizationCustomPropertyValuesClient Values { get; }
    }
}
