#if NET_45
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#endif

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's User Keys API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/users/keys/">Users API documentation</a> for more information.
    /// </remarks>
    public class SshKeysClient : ApiClient, ISshKeysClient
    {
        /// <summary>
        /// Instantiates a new SSH Key Client.
        /// </summary>
        /// <param name="apiConnection">The connection used to make requests</param>
        public SshKeysClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Retrieves the <see cref="SshKey"/> for the specified id.
        /// </summary>
        /// <param name="id">The ID of the SSH key</param>
        /// <returns>A <see cref="SshKey"/></returns>
        [Obsolete("This method is obsolete. Please use User.Keys.Get(int) instead.")]
        public Task<SshKey> Get(int id)
        {
            var endpoint = "user/keys/{0}".FormatUri(id);

            return ApiConnection.Get<SshKey>(endpoint);
        }

        /// <summary>
        /// Retrieves the <see cref="SshKey"/> for the specified id.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <returns>A <see cref="IReadOnlyPagedCollection{SshKey}"/> of <see cref="SshKey"/>.</returns>
        [Obsolete("This method is obsolete. Please use User.Keys.GetAll(string) instead.")]
        public Task<IReadOnlyList<SshKey>> GetAll(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return ApiConnection.GetAll<SshKey>(ApiUrls.SshKeys(user));
        }

        /// <summary>
        /// Retrieves the <see cref="SshKey"/> for the specified id.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{SshKey}"/> of <see cref="SshKey"/>.</returns>
        [Obsolete("This method is obsolete. Please use User.Keys.GetAll() instead.")]
        public Task<IReadOnlyList<SshKey>> GetAllForCurrent()
        {
            return ApiConnection.GetAll<SshKey>(ApiUrls.SshKeys());
        }

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="key">The SSH Key contents</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        [Obsolete("This method is obsolete. Please use User.Keys.Create(NewPublicKey) instead.")]
        public Task<SshKey> Create(SshKeyUpdate key)
        {
            Ensure.ArgumentNotNull(key, "key");

            return ApiConnection.Post<SshKey>(ApiUrls.SshKeys(), key);
        }

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="id">The ID of the SSH key</param>
        /// <param name="key">The SSH Key contents</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        [Obsolete("This method is no longer supported in the GitHub API. Delete and Create the key again instead.")]
        public Task<SshKey> Update(int id, SshKeyUpdate key)
        {
            Ensure.ArgumentNotNull(key, "key");

            var endpoint = "user/keys/{0}".FormatUri(id);
            return ApiConnection.Patch<SshKey>(endpoint, key);
        }

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="id">The id of the SSH key</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        [Obsolete("This method is obsolete. Please use User.Keys.Delete(int) instead.")]
        public Task Delete(int id)
        {
            var endpoint = "user/keys/{0}".FormatUri(id);

            return ApiConnection.Delete(endpoint);
        }
    }
}
