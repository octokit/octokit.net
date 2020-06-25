using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Secrets API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/actions/secrets/">Repository Secrets API documentation</a> for more details.
    /// </remarks>
    public interface IRepositorySecretsClient
    {
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
        Task<RepositorySecretsPublicKey> GetPublicKey(string owner, string repoName);

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
        Task<IEnumerable<RepositorySecret>> GetSecretsList(string owner, string repoName);

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
        Task<RepositorySecret> GetSecret(string owner, string repoName, string secretName);

        /// <summary>
        /// Create or update a secret in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/actions/secrets/#create-or-update-a-repository-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoName">The owner of the repository</param>
        /// <param name="owner">The name of the repository</param>
        /// <param name="secretName">The name of the secret</param>
        /// <param name="secretValue">The value of the secret</param>
        /// <param name="valueEncrypted">Is the secret value already encrypted</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositorySecret"/> instance for the repository secret that was created or updated.</returns>
        Task<RepositorySecret> CreateOrUpdateSecret(string owner, string repoName, string secretName, string secretValue, bool valueEncrypted = false);

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
        Task DeleteSecret(string owner, string repoName, string secretName);
    }
}
