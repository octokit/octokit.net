using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Repository Secrets API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/actions/secrets/">Repository Secrets API documentation</a> for more details.
    /// </remarks>
    public interface IObservableRepositorySecretsClient
    {
        /// <summary>
        /// Get the public signing key to encrypt secrets for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#get-a-repository-public-key">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoName">The owner of the repository</param>
        /// <param name="owner">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="SecretsPublicKey"/> instance for the repository public key.</returns>
        IObservable<SecretsPublicKey> GetPublicKey(string owner, string repoName);

        /// <summary>
        /// List the secrets for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#list-repository-secrets">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoName">The owner of the repository</param>
        /// <param name="owner">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IEnumerable{RepositorySecret}"/> instance for the list of repository secrets.</returns>
        IObservable<RepositorySecretsCollection> GetAll(string owner, string repoName);

        /// <summary>
        /// Get a secret from a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#get-a-repository-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoName">The owner of the repository</param>
        /// <param name="owner">The name of the repository</param>
        /// <param name="secretName">The name of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositorySecret"/> instance for the repository secret.</returns>
        IObservable<RepositorySecret> Get(string owner, string repoName, string secretName);

        /// <summary>
        /// Create or update a secret in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#create-or-update-a-repository-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoName">The owner of the repository</param>
        /// <param name="owner">The name of the repository</param>
        /// <param name="secretName">The name of the secret</param>
        /// <param name="upsertSecret">The encrypted value and id of the encryption key</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositorySecret"/> instance for the repository secret that was created or updated.</returns>
        IObservable<RepositorySecret> CreateOrUpdate(string owner, string repoName, string secretName, UpsertRepositorySecret upsertSecret);

        /// <summary>
        /// Delete a secret in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#delete-a-repository-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoName">The owner of the repository</param>
        /// <param name="owner">The name of the repository</param>
        /// <param name="secretName">The name of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        IObservable<Unit> Delete(string owner, string repoName, string secretName);
    }
}
