using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's public keys API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/code-security/secret-scanning/secret-scanning-partner-program#implement-signature-verification-in-your-secret-alert-service">Secret scanning documentation</a> for more details.
    /// </remarks>
    public class ObservablePublicKeysClient : IObservablePublicKeysClient
    {
        private readonly IPublicKeysClient _client;

        public ObservablePublicKeysClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Meta.PublicKeys;
        }

        /// <summary>
        /// Retrieves public keys for validating request signatures.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="MetaPublicKeys"/> containing public keys for validating request signatures.</returns>
        public IObservable<MetaPublicKeys> Get(PublicKeyType keysType)
        {
            return _client.Get(keysType).ToObservable();
        }
    }
}
