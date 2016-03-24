﻿using System.Collections.Generic;
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
        /// Gets all public keys for the authenticated user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#list-your-public-keys
        /// </remarks>
        /// <returns></returns>
        public Task<IReadOnlyList<PublicKey>> GetAllForCurrent()
        {
            return ApiConnection.GetAll<PublicKey>(ApiUrls.Keys());
        }

        /// <summary>
        /// Gets all verified public keys for a user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#list-public-keys-for-a-user
        /// </remarks>
        /// <returns></returns>
        public Task<IReadOnlyList<PublicKey>> GetAll(string userName)
        {
            Ensure.ArgumentNotNullOrEmptyString(userName, "userName");

            return ApiConnection.GetAll<PublicKey>(ApiUrls.Keys(userName));
        }

        /// <summary>
        /// Retrieves the <see cref="PublicKey"/> for the specified id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#get-a-single-public-key
        /// </remarks>
        /// <param name="id">The ID of the SSH key</param>
        /// <returns></returns>
        public Task<PublicKey> Get(int id)
        {
            return ApiConnection.Get<PublicKey>(ApiUrls.Keys(id));
        }

        /// <summary>
        /// Create a public key <see cref="NewPublicKey"/>.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#create-a-public-key
        /// </remarks>
        /// <param name="newKey">The SSH Key contents</param>
        /// <returns></returns>
        public Task<PublicKey> Create(NewPublicKey newKey)
        {
            Ensure.ArgumentNotNull(newKey, "newKey");

            return ApiConnection.Post<PublicKey>(ApiUrls.Keys(), newKey);
        }

        /// <summary>
        /// Delete a public key.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#delete-a-public-key
        /// </remarks>
        /// <param name="id">The id of the key to delete</param>
        /// <returns></returns>
        public Task Delete(int id)
        {
            return ApiConnection.Delete(ApiUrls.Keys(id));
        }
    }
}