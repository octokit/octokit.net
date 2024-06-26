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
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.User.GitSshKey;
        }

        /// <summary>
        /// Gets all verified public keys for a user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#list-public-keys-for-a-user
        /// </remarks>
        /// <param name="userName">The @ handle of the user.</param>
        /// <returns>Lists the verified public keys for a user.</returns>
        public IObservable<PublicKey> GetAll(string userName)
        {
            Ensure.ArgumentNotNullOrEmptyString(userName, nameof(userName));

            return GetAll(userName, ApiOptions.None);
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
        public IObservable<PublicKey> GetAll(string userName, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(userName, nameof(userName));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.GetAll(userName, options).ToObservable().SelectMany(k => k);
        }

        /// <summary>
        /// Gets all public keys for the authenticated user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#list-your-public-keys
        /// </remarks>
        /// <returns>Lists the current user's keys.</returns>
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
        /// <param name="options">Options to change API's behavior.</param>
        /// <returns>Lists the current user's keys.</returns>
        public IObservable<PublicKey> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.GetAllForCurrent(options).ToObservable().SelectMany(k => k);
        }

        /// <summary>
        /// Retrieves the <see cref="PublicKey"/> for the specified id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#get-a-single-public-key
        /// </remarks>
        /// <param name="id">The Id of the SSH key</param>
        /// <returns>View extended details for a single public key.</returns>
        public IObservable<PublicKey> Get(long id)
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
        /// <returns>Creates a public key.</returns>
        public IObservable<PublicKey> Create(NewPublicKey newKey)
        {
            Ensure.ArgumentNotNull(newKey, nameof(newKey));

            return _client.Create(newKey).ToObservable();
        }

        /// <summary>
        /// Delete a public key.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#delete-a-public-key
        /// </remarks>
        /// <param name="id">The id of the key to delete</param>
        /// <returns>Removes a public key.</returns>
        public IObservable<Unit> Delete(long id)
        {
            return _client.Delete(id).ToObservable();
        }
    }
}
