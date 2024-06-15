using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using System;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Custom Property Values API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/repos/custom-properties">Custom Properties API documentation</a> for more information.
    /// </remarks>
    public class ObservableRepositoryCustomPropertiesClient : IObservableRepositoryCustomPropertiesClient
    {
        readonly IRepositoryCustomPropertiesClient _client;
        readonly IConnection _connection;

        public ObservableRepositoryCustomPropertiesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Repository.CustomProperty;
            _connection = client.Connection;
        }

        /// <summary>
        /// Get all custom property values for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/repos/custom-properties#get-all-custom-property-values-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="repoName">The name of the repository.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<IReadOnlyList<CustomPropertyValue>> GetAll(string owner, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));

            return _client.GetAll(owner, repoName).ToObservable();
        }

        /// <summary>
        /// Create new or update existing custom property values for a repository. Using a value of null for a custom property will remove or 'unset' the property value from the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/repos/custom-properties#create-or-update-custom-property-values-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="upsertPropertyValues">The custom property values to create or update</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<IReadOnlyList<CustomPropertyValue>> CreateOrUpdate(string owner, string repoName, UpsertRepositoryCustomPropertyValues upsertPropertyValues)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNull(upsertPropertyValues, nameof(upsertPropertyValues));

            return _client.CreateOrUpdate(owner, repoName, upsertPropertyValues).ToObservable();
        }
    }
}
