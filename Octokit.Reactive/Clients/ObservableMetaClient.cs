using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's meta APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/meta">Meta API documentation</a> for more details.
    /// </remarks>
    public class ObservableMetaClient : IObservableMetaClient
    {
        private readonly IMetaClient _client;

        public ObservableMetaClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Meta;
        }

        /// <summary>
        /// Retrieves information about GitHub.com, the service or a GitHub Enterprise installation.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="Meta"/> containing metadata about the GitHub instance.</returns>
        public IObservable<Meta> GetMetadata()
        {
            return _client.GetMetadata().ToObservable();
        }
    }
}
