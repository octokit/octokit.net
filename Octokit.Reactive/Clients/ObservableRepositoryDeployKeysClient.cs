using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Repository Deploy Keys API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/keys/">Deploy Keys API documentation</a> for more information.
    /// </remarks>
    public class ObservableRepositoryDeployKeysClient : IObservableRepositoryDeployKeysClient
    {
        readonly IRepositoryDeployKeysClient _client;
        readonly IConnection _connection;

        public ObservableRepositoryDeployKeysClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Repository.DeployKeys;
            _connection = client.Connection;
        }

        /// <summary>
        /// Get a single deploy key by number for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#get"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="deployKeyId">The id of the deploy key.</param>
        public IObservable<DeployKey> Get(string owner, string name, int deployKeyId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, deployKeyId).ToObservable();
        }

        /// <summary>
        /// Get a single deploy key by number for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#get"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="deployKeyId">The id of the deploy key.</param>
        public IObservable<DeployKey> Get(long repositoryId, int deployKeyId)
        {
            return _client.Get(repositoryId, deployKeyId).ToObservable();
        }

        /// <summary>
        /// Get all deploy keys for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#list"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        public IObservable<DeployKey> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Get all deploy keys for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#list"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        public IObservable<DeployKey> GetAll(long repositoryId)
        {
            return GetAll(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Get all deploy keys for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#list"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<DeployKey> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<DeployKey>(ApiUrls.RepositoryDeployKeys(owner, name), options);
        }

        /// <summary>
        /// Get all deploy keys for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#list"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<DeployKey> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<DeployKey>(ApiUrls.RepositoryDeployKeys(repositoryId), options);
        }

        /// <summary>
        /// Creates a new deploy key for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#create"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="newDeployKey">The deploy key to create for the repository.</param>
        public IObservable<DeployKey> Create(string owner, string name, NewDeployKey newDeployKey)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newDeployKey, nameof(newDeployKey));


            if (string.IsNullOrWhiteSpace(newDeployKey.Title))
                throw new ArgumentException("The new deploy key's title must not be null.");

            if (string.IsNullOrWhiteSpace(newDeployKey.Key))
                throw new ArgumentException("The new deploy key's key must not be null.");

            return _client.Create(owner, name, newDeployKey).ToObservable();
        }

        /// <summary>
        /// Creates a new deploy key for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#create"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="newDeployKey">The deploy key to create for the repository.</param>
        public IObservable<DeployKey> Create(long repositoryId, NewDeployKey newDeployKey)
        {
            Ensure.ArgumentNotNull(newDeployKey, nameof(newDeployKey));


            if (string.IsNullOrWhiteSpace(newDeployKey.Title))
                throw new ArgumentException("The new deploy key's title must not be null.");

            if (string.IsNullOrWhiteSpace(newDeployKey.Key))
                throw new ArgumentException("The new deploy key's key must not be null.");

            return _client.Create(repositoryId, newDeployKey).ToObservable();
        }

        /// <summary>
        /// Deletes a deploy key from a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#delete"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="deployKeyId">The id of the deploy key to delete.</param>
        public IObservable<Unit> Delete(string owner, string name, int deployKeyId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Delete(owner, name, deployKeyId).ToObservable();
        }

        /// <summary>
        /// Deletes a deploy key from a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#delete"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="deployKeyId">The id of the deploy key to delete.</param>
        public IObservable<Unit> Delete(long repositoryId, int deployKeyId)
        {
            return _client.Delete(repositoryId, deployKeyId).ToObservable();
        }
    }
}
