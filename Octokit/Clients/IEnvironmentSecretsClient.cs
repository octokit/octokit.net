using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
	/// <summary>
	/// A client for GitHub's Environment Secrets API.
	/// </summary>
	/// <remarks>
	/// See the <a href="https://docs.github.com/rest/actions/secrets">Secrets API documentation</a> for more details.
	/// </remarks>
	public interface IEnvironmentSecretsClient
    {
		/// <summary>
		/// Get the public signing key to encrypt secrets for a repository environment (environment).
		/// </summary>
		/// <remarks>
		/// See the <a href="https://docs.github.com/rest/actions/secrets/#get-an-environment-public-key">API documentation</a> for more information.
		/// </remarks>
		/// <param name="repoId">The unique identifier of the repository for the environment.</param>
		/// <param name="envName">The name of the environment. The name must be URL encoded. For example, any slashes in the name must be replaced with %2F</param>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns>A <see cref="SecretsPublicKey"/> instance for the environment public key.</returns>
		Task<SecretsPublicKey> GetPublicKey(int repoId, string envName);

		/// <summary>
		/// List the secrets for an environment.
		/// </summary>
		/// <remarks>
		/// See the <a href="https://docs.github.com/rest/actions/secrets/#list-environment-secrets">API documentation</a> for more information.
		/// </remarks>
		/// <param name="repoId">The unique identifier of the repository for the environment.</param>
		/// <param name="envName">The name of the environment. The name must be URL encoded. For example, any slashes in the name must be replaced with %2F</param>
		/// <param name="perPage">Optional number of pages to return.  Default is 30, per GitHub</param>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns>A <see cref="EnvironmentSecretsCollection"/> instance for the list of environment secrets.</returns>
		Task<EnvironmentSecretsCollection> GetAll (int repoId, string envName, int? perPage = 30);

		/// <summary>
		/// Get a secret from an environment.
		/// </summary>
		/// <remarks>
		/// See the <a href="https://docs.github.com/rest/actions/secrets/#get-an-environment-secret">API documentation</a> for more information.
		/// </remarks>
		/// <param name="repoId">The unique identifier of the repository for the environment.</param>
		/// <param name="envName">The name of the environment. The name must be URL encoded. For example, any slashes in the name must be replaced with %2F</param>
		/// <param name="secretName">The name of the secret</param>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns>A <see cref="EnvironmentSecret"/> instance for the environment secret.</returns>
		Task<EnvironmentSecret> Get(int repoId, string envName, string secretName);

		/// <summary>
		/// Create or update a secret in an environment.
		/// </summary>
		/// <remarks>
		/// See the <a href="https://docs.github.com/rest/actions/secrets/#create-or-update-an-environment-secret">API documentation</a> for more information.
		/// </remarks>
		/// <param name="repoId">The unique identifier of the repository for the environment.</param>
		/// <param name="envName">The name of the environment. The name must be URL encoded. For example, any slashes in the name must be replaced with %2F</param>
		/// <param name="secretName">The name of the secret</param>
		/// <param name="upsertSecret">The encrypted value and id of the encryption key</param>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns>A <see cref="EnvironmentSecret"/> instance for the environment secret that was created or updated.</returns>
		Task<EnvironmentSecret> CreateOrUpdate(int repoId, string envName, string secretName, UpsertEnvironmentSecret upsertSecret);

		/// <summary>
		/// Delete a secret in an environment.
		/// </summary>
		/// <remarks>
		/// See the <a href="https://docs.github.com/rest/actions/secrets/#delete-an-environment-secret">API documentation</a> for more information.
		/// </remarks>
		/// <param name="repoId">The unique identifier of the repository for the environment.</param>
		/// <param name="envName">The name of the environment. The name must be URL encoded. For example, any slashes in the name must be replaced with %2F</param>
		/// <param name="secretName">The name of the secret</param>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		Task Delete(int repoId, string envName, string secretName);
    }
}
