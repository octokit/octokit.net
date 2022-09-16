using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's markdown APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/markdown">Markdown API documentation</a> for more details.
    /// </remarks>
    public class MarkdownClient : ApiClient, IMarkdownClient
    {
        /// <summary>
        ///     Initializes a new GitHub markdown API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public MarkdownClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets the rendered Markdown for the specified plain-text Markdown document.
        /// </summary>
        /// <param name="markdown">A plain-text Markdown document</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The rendered Markdown.</returns>
        [ManualRoute("POST", "/markdown/raw")]
        public Task<string> RenderRawMarkdown(string markdown)
        {
            return ApiConnection.Post<string>(ApiUrls.RawMarkdown(), markdown, "text/html", "text/plain");
        }

        /// <summary>
        /// Gets the rendered Markdown for an arbitrary markdown document.
        /// </summary>
        /// <param name="markdown">An arbitrary Markdown document</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The rendered Markdown.</returns>
        [ManualRoute("POST", "/markdown")]
        public Task<string> RenderArbitraryMarkdown(NewArbitraryMarkdown markdown)
        {
            return ApiConnection.Post<string>(ApiUrls.Markdown(), markdown, "text/html", "text/plain");
        }
    }
}
