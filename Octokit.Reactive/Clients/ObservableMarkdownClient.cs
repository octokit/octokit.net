using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's markdown APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/markdown">Markdown API documentation</a> for more details.
    /// </remarks>
    public class ObservableMarkdownClient : IObservableMarkdownClient
    {
        private readonly IMarkdownClient _client;

        public ObservableMarkdownClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Markdown;
        }

        /// <summary>
        /// Gets the rendered Markdown for an arbitrary markdown document.
        /// </summary>
        /// <param name="markdown">An arbitrary Markdown document</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The rendered Markdown.</returns>
        public IObservable<string> RenderArbitraryMarkdown(NewArbitraryMarkdown markdown)
        {
            return _client.RenderArbitraryMarkdown(markdown).ToObservable();
        }

        /// <summary>
        /// Gets the rendered Markdown for the specified plain-text Markdown document.
        /// </summary>
        /// <param name="markdown">A plain-text Markdown document</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The rendered Markdown.</returns>
        public IObservable<string> RenderRawMarkdown(string markdown)
        {
            return _client.RenderRawMarkdown(markdown).ToObservable();
        }
    }
}
