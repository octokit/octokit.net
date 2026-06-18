using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Threading;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's miscellaneous APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/misc/">Miscellaneous API documentation</a> for more details.
    /// </remarks>
    [Obsolete("Use individual clients available on the GitHubClient for these methods")]
    public interface IMiscellaneousClient
    {
        /// <summary>
        /// Gets all the emojis available to use on GitHub.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="IReadOnlyDictionary{TKey,TValue}"/> of emoji and their URI.</returns>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ExcludeFromPaginationApiOptionsConventionTest("Pagination not supported by GitHub API (tested 29/08/2017)")]
        [Obsolete("This client is being deprecated and will be removed in the future. Use EmojisClient.GetAllEmojis instead.")]
        Task<IReadOnlyList<Emoji>> GetAllEmojis(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the rendered Markdown for the specified plain-text Markdown document.
        /// </summary>
        /// <param name="markdown">A plain-text Markdown document</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The rendered Markdown.</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use MarkdownClient.RenderRawMarkdown instead.")]
        Task<string> RenderRawMarkdown(string markdown, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the rendered Markdown for an arbitrary markdown document.
        /// </summary>
        /// <param name="markdown">An arbitrary Markdown document</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The rendered Markdown.</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use MarkdownClient.RenderArbitraryMarkdown instead.")]
        Task<string> RenderArbitraryMarkdown(NewArbitraryMarkdown markdown, CancellationToken cancellationToken = default);

        /// <summary>
        /// List all templates available to pass as an option when creating a repository.
        /// </summary>
        /// <returns>A list of template names</returns>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ExcludeFromPaginationApiOptionsConventionTest("Pagination not supported by GitHub API (tested 29/08/2017)")]
        [Obsolete("This client is being deprecated and will be removed in the future. Use GitIgnoreClient.GetAllGitIgnoreTemplates instead.")]
        Task<IReadOnlyList<string>> GetAllGitIgnoreTemplates(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the source for a single GitIgnore template
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A template and its source</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use GitIgnoreClient.GetAllGitIgnoreTemplates instead.")]
        Task<GitIgnoreTemplate> GetGitIgnoreTemplate(string templateName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a list of the licenses shown in the license picker on GitHub.com. This is not a comprehensive
        /// list of all possible OSS licenses.
        /// </summary>
        /// <returns>A list of licenses available on the site</returns>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [Obsolete("This client is being deprecated and will be removed in the future. Use GitIgnoreClient.GetGitIgnoreTemplate instead.")]
        Task<IReadOnlyList<LicenseMetadata>> GetAllLicenses(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a list of the licenses shown in the license picker on GitHub.com. This is not a comprehensive
        /// list of all possible OSS licenses.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A list of licenses available on the site</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use LicensesClient.GetAllLicenses instead.")]
        Task<IReadOnlyList<LicenseMetadata>> GetAllLicenses(ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a license based on the license key such as "MIT"
        /// </summary>
        /// <param name="key">The license identifier to look for</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A <see cref="License" /> that includes the license key, text, and attributes of the license.</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use LicensesClient.GetLicense instead.")]
        Task<License> GetLicense(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets API Rate Limits (API service rather than header info).
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="MiscellaneousRateLimit"/> of Rate Limits.</returns>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [Obsolete("This client is being deprecated and will be removed in the future. Use RateLimitClient.GetRateLimits instead.")]
        Task<MiscellaneousRateLimit> GetRateLimits(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves information about GitHub.com, the service or a GitHub Enterprise installation.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="Meta"/> containing metadata about the GitHub instance.</returns>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [Obsolete("This client is being deprecated and will be removed in the future. Use MetaClient.GetMetadata instead.")]
        Task<Meta> GetMetadata(CancellationToken cancellationToken = default);
    }
}
