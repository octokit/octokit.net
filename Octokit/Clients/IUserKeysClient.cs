using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's User Keys API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/users/keys/">User Keys API documentation</a> for more information.
    /// </remarks>
    public interface IUserKeysClient
    {
        /// <summary>
        /// Gets all verified public keys for a user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#list-public-keys-for-a-user
        /// </remarks>
        /// <param name="userName">The @ handle of the user.</param>
        /// <returns>Lists the verified public keys for a user.</returns>
        Task<IReadOnlyList<PublicKey>> GetAll(string userName);

        /// <summary>
        /// Gets all verified public keys for a user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#list-public-keys-for-a-user
        /// </remarks>
        /// <param name="userName">The @ handle of the user.</param>
        /// <param name="options">Options to change API's behavior.</param>
        /// <returns>Lists the verified public keys for a user.</returns>
        Task<IReadOnlyList<PublicKey>> GetAll(string userName, ApiOptions options);

        /// <summary>
        /// Gets all public keys for the authenticated user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#list-your-public-keys
        /// </remarks>
        /// <returns>Lists the current user's keys.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<PublicKey>> GetAllForCurrent();

        /// <summary>
        /// Gets all public keys for the authenticated user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#list-your-public-keys
        /// </remarks>
        /// <param name="options">Options to change API's behavior.</param>
        /// <returns>Lists the current user's keys.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<PublicKey>> GetAllForCurrent(ApiOptions options);

        /// <summary>
        /// Retrieves the <see cref="PublicKey"/> for the specified id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#get-a-single-public-key
        /// </remarks>
        /// <param name="id">The Id of the SSH key</param>
        /// <returns>View extended details for a single public key.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        Task<PublicKey> Get(int id);

        /// <summary>
        /// Create a public key <see cref="NewPublicKey"/>.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#create-a-public-key
        /// </remarks>
        /// <param name="newKey">The SSH Key contents</param>
        /// <returns>Creates a public key.</returns>
        Task<PublicKey> Create(NewPublicKey newKey);

        /// <summary>
        /// Delete a public key.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#delete-a-public-key
        /// </remarks>
        /// <param name="id">The id of the key to delete</param>
        /// <returns>Removes a public key.</returns>
        Task Delete(int id);
    }
}
