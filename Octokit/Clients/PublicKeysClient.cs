using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's public keys API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/code-security/secret-scanning/secret-scanning-partner-program#implement-signature-verification-in-your-secret-alert-service">Secret scanning documentation</a> for more details.
    /// </remarks>
    public class PublicKeysClient : ApiClient, IPublicKeysClient
    {
        /// <summary>
        /// Initializes a new GitHub Meta Public Keys API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public PublicKeysClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Retrieves public keys for validating request signatures.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="MetaPublicKeys"/> containing public keys for validating request signatures.</returns>
        [ManualRoute("GET", "/meta/public_keys/{keysType}")]
        public Task<MetaPublicKeys> Get(PublicKeyType keysType)
        {
            return ApiConnection.Get<MetaPublicKeys>(ApiUrls.PublicKeys(keysType));
        }
    }
}
