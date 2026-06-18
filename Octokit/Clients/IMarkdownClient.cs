using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's markdown APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/markdown">Markdown API documentation</a> for more details.
    /// </remarks>
    public interface IMarkdownClient
    {
        /// <summary>
        /// Gets the rendered Markdown for the specified plain-text Markdown document.
        /// </summary>
        /// <param name="markdown">A plain-text Markdown document</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The rendered Markdown.</returns>
        Task<string> RenderArbitraryMarkdown(NewArbitraryMarkdown markdown, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the rendered Markdown for an arbitrary markdown document.
        /// </summary>
        /// <param name="markdown">An arbitrary Markdown document</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The rendered Markdown.</returns>
        Task<string> RenderRawMarkdown(string markdown, CancellationToken cancellationToken = default);
    }
}
