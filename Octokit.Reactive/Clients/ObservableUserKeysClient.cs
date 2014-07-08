using System;
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
        /// <returns>The <see cref="PublicKey"/>s for the authenticated user.</returns>
        public IObservable<PublicKey> GetAll()
        {
            return _client.GetAll().ToObservable().SelectMany(k => k);
        }

        /// <summary>
        /// Gets all verified public keys for a user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/keys/#list-public-keys-for-a-user
        /// </remarks>
        /// <returns>The <see cref="PublicKey"/>s for the user.</returns>
        public IObservable<PublicKey> GetAll(string userName)
        {
            return _client.GetAll(userName).ToObservable().SelectMany(k => k);
        }
    }
}