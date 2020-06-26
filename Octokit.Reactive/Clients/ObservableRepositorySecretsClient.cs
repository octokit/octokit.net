using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using System.Text;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Repository Secrets API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/actions/secrets/">Repository Secrets API documentation</a> for more details.
    /// </remarks>
    public class ObservableRepositorySecretsClient : IObservableRepositorySecretsClient
    {
        readonly IRepositorySecretsClient _client;
        readonly IConnection _connection;

        public ObservableRepositorySecretsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Repository.Actions.Secrets;
            _connection = client.Connection;
        }

        /// <summary>
        /// Get the public signing key to encrypt secrets for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/actions/secrets/#get-a-repository-public-key">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoName">The owner of the repository</param>
        /// <param name="owner">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositorySecretsPublicKey"/> instance for the repository public key.</returns>
        public IObservable<RepositorySecretsPublicKey> GetPublicKey(string owner, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));

            return _client.GetPublicKey(owner, repoName).ToObservable();
        }

        /// <summary>
        /// List the secrets for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/actions/secrets/#list-repository-secrets">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoName">The owner of the repository</param>
        /// <param name="owner">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IEnumerable{RepositorySecret}"/> instance for the list of repository secrets.</returns>
        public IObservable<IReadOnlyList<RepositorySecret>> GetAll(string owner, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));

            return _client.GetAll(owner, repoName).ToObservable();
        }

        /// <summary>
        /// List the secrets for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/actions/secrets/#list-repository-secrets">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoName">The owner of the repository</param>
        /// <param name="owner">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IEnumerable{RepositorySecret}"/> instance for the list of repository secrets.</returns>
        public IObservable<IReadOnlyList<RepositorySecret>> GetAll(string owner, string repoName, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.GetAll(owner, repoName, options).ToObservable();
        }

        /// <summary>
        /// Get a secret from a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/actions/secrets/#get-a-repository-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoName">The owner of the repository</param>
        /// <param name="owner">The name of the repository</param>
        /// <param name="secretName">The name of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositorySecret"/> instance for the repository secret.</returns>
        public IObservable<RepositorySecret> Get(string owner, string repoName, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            return _client.Get(owner, repoName, secretName).ToObservable();
        }

        /// <summary>
        /// Create or update a secret in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/actions/secrets/#create-or-update-a-repository-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoName">The owner of the repository</param>
        /// <param name="owner">The name of the repository</param>
        /// <param name="secretName">The name of the secret</param>
        /// <param name="upsertSecret">The encrypted value and id of the encryption key</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositorySecret"/> instance for the repository secret that was created or updated.</returns>
        public IObservable<RepositorySecret> CreateOrUpdate(string owner, string repoName, string secretName, UpsertRepositorySecret upsertSecret)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));
            Ensure.ArgumentNotNull(upsertSecret, nameof(upsertSecret));
            Ensure.ArgumentNotNullOrEmptyString(upsertSecret.EncryptedValue, nameof(upsertSecret.EncryptedValue));
            Ensure.ArgumentNotNullOrEmptyString(upsertSecret.EncryptionKeyId, nameof(upsertSecret.EncryptionKeyId));

            return _client.CreateOrUpdate(owner, repoName, secretName, upsertSecret).ToObservable();
        }

        /// <summary>
        /// Delete a secret in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/actions/secrets/#delete-a-repository-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoName">The owner of the repository</param>
        /// <param name="owner">The name of the repository</param>
        /// <param name="secretName">The name of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Unit> Delete(string owner, string repoName, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            return _client.Delete(owner, repoName, secretName).ToObservable();
        }
    }
}
