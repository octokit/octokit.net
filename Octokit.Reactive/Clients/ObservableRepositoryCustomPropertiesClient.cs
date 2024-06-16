using System.Reactive.Threading.Tasks;
using System;
using System.Reactive;
using System.Reactive.Linq;

namespace Octokit.Reactive
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
        public IObservable<CustomPropertyValue> GetAll(string owner, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));

            return _client.GetAll(owner, repoName).ToObservable().SelectMany(p => p);
        }

        /// <summary>
        /// Create new or update existing custom property values for a repository. Using a value of null for a custom property will remove or 'unset' the property value from the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/repos/custom-properties#create-or-update-custom-property-values-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="propertyValues">The custom property values to create or update</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Unit> CreateOrUpdate(string owner, string repoName, UpsertRepositoryCustomPropertyValues propertyValues)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNull(propertyValues, nameof(propertyValues));
            Ensure.ArgumentNotNullOrEmptyEnumerable(propertyValues.Properties, nameof(propertyValues.Properties));

            return _client.CreateOrUpdate(owner, repoName, propertyValues).ToObservable();
        }
    }
}
