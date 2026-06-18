using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's User Keys API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/users/keys/">User Keys API documentation</a> for more information.
    /// </remarks>
    public class UserKeysClient : ApiClient, IUserKeysClient
    {
        public UserKeysClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all verified public keys for a user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#list-public-keys-for-a-user
        /// </remarks>
        /// <param name="userName">The @ handle of the user.</param>
        /// <returns>Lists the verified public keys for a user.</returns>
        [ManualRoute("GET", "/users/{username}/keys")]
        public Task<IReadOnlyList<PublicKey>> GetAll(string userName, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(userName, nameof(userName));

            return GetAll(userName, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all verified public keys for a user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#list-public-keys-for-a-user
        /// </remarks>
        /// <param name="userName">The @ handle of the user.</param>
        /// <param name="options">Options to change API's behavior.</param>
        /// <returns>Lists the verified public keys for a user.</returns>
        [ManualRoute("GET", "/users/{username}/keys")]
        public Task<IReadOnlyList<PublicKey>> GetAll(string userName, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(userName, nameof(userName));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<PublicKey>(ApiUrls.Keys(userName), options);
        }

        /// <summary>
        /// Gets all public keys for the authenticated user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#list-your-public-keys
        /// </remarks>
        /// <returns>Lists the current user's keys.</returns>
        [ManualRoute("GET", "/user/keys")]
        public Task<IReadOnlyList<PublicKey>> GetAllForCurrent(CancellationToken cancellationToken = default)
        {
            return GetAllForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// Gets all public keys for the authenticated user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#list-your-public-keys
        /// </remarks>
        /// <param name="options">Options to change API's behavior.</param>
        /// <returns>Lists the current user's keys.</returns>
        [ManualRoute("GET", "/user/keys")]
        public Task<IReadOnlyList<PublicKey>> GetAllForCurrent(ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<PublicKey>(ApiUrls.Keys(), options);
        }

        /// <summary>
        /// Retrieves the <see cref="PublicKey"/> for the specified id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#get-a-single-public-key
        /// </remarks>
        /// <param name="keyId">The Id of the SSH key</param>
        /// <returns></returns>
        [ManualRoute("GET", "/user/keys/{key_id}")]
        public Task<PublicKey> Get(long keyId, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Get<PublicKey>(ApiUrls.Keys(keyId));
        }

        /// <summary>
        /// Create a public key <see cref="NewPublicKey"/>.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#create-a-public-key
        /// </remarks>
        /// <param name="newKey">The SSH Key contents</param>
        /// <returns></returns>
        [ManualRoute("POST", "/user/keys")]
        public Task<PublicKey> Create(NewPublicKey newKey, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(newKey, nameof(newKey));

            return ApiConnection.Post<PublicKey>(ApiUrls.Keys(), newKey, cancellationToken);
        }

        /// <summary>
        /// Delete a public key.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#delete-a-public-key
        /// </remarks>
        /// <param name="keyId">The id of the key to delete</param>
        /// <returns></returns>
        [ManualRoute("DELETE", "/user/keys/{key_id}")]
        public Task Delete(long keyId, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Delete(ApiUrls.Keys(keyId));
        }
    }
}
