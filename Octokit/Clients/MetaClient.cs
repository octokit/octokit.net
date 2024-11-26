using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's meta APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/meta">Meta API documentation</a> for more details.
    /// </remarks>
    public class MetaClient : ApiClient, IMetaClient
    {
        /// <summary>
        ///     Initializes a new GitHub meta API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public MetaClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
            PublicKeys = new PublicKeysClient(apiConnection);
        }

        /// <summary>
        /// Returns a client to manage get public keys for validating request signatures.
        /// </summary>
        public IPublicKeysClient PublicKeys { get; private set; }

        /// <summary>
        /// Retrieves information about GitHub.com, the service or a GitHub Enterprise installation.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="Meta"/> containing metadata about the GitHub instance.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        [ManualRoute("GET", "/meta")]
        public Task<Meta> GetMetadata()
        {
            return ApiConnection.Get<Meta>(ApiUrls.Meta());
        }
    }
}