using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's User Keys API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/users/keys/">User Keys API documentation</a> for more information.
    /// </remarks>
    public class ObservableUserKeysClient : IObservableUserKeysClient
    {
        readonly IUserKeysClient _client;

        public ObservableUserKeysClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.User.Keys;
        }

        /// <summary>
        /// Gets all public keys for the authenticated user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#list-your-public-keys
        /// </remarks>
        /// <returns></returns>
        public IObservable<PublicKey> GetAllForCurrent()
        {
            return GetAllForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// Gets all public keys for the authenticated user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#list-your-public-keys
        /// </remarks>
        /// <returns></returns>
        public IObservable<PublicKey> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return _client.GetAllForCurrent(options).ToObservable().SelectMany(k => k);
        }

        /// <summary>
        /// Gets all verified public keys for a user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#list-public-keys-for-a-user
        /// </remarks>
        /// <returns></returns>
        public IObservable<PublicKey> GetAll(string userName)
        {
            Ensure.ArgumentNotNullOrEmptyString(userName, "userName");

            return GetAll(userName, ApiOptions.None);
        }

        /// <summary>
        /// Gets all verified public keys for a user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#list-public-keys-for-a-user
        /// </remarks>
        /// <returns></returns>
        public IObservable<PublicKey> GetAll(string userName, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(userName, "userName");
            Ensure.ArgumentNotNull(options, "options");

            return _client.GetAll(userName, options).ToObservable().SelectMany(k => k);
        }

        /// <summary>
        /// Retrieves the <see cref="PublicKey"/> for the specified id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#get-a-single-public-key
        /// </remarks>
        /// <param name="id">The ID of the SSH key</param>
        /// <returns></returns>
        public IObservable<PublicKey> Get(int id)
        {
            return _client.Get(id).ToObservable();
        }

        /// <summary>
        /// Create a public key <see cref="NewPublicKey"/>.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#create-a-public-key
        /// </remarks>
        /// <param name="newKey">The SSH Key contents</param>
        /// <returns></returns>
        public IObservable<PublicKey> Create(NewPublicKey newKey)
        {
            Ensure.ArgumentNotNull(newKey, "newKey");

            return _client.Create(newKey).ToObservable();
        }

        /// <summary>
        /// Delete a public key.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#delete-a-public-key
        /// </remarks>
        /// <param name="id">The id of the key to delete</param>
        /// <returns></returns>
        public IObservable<Unit> Delete(int id)
        {
            return _client.Delete(id).ToObservable();
        }
    }
}