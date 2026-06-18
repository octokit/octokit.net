using System;
using System.Threading;

namespace Octokit.Reactive
{
    [Obsolete("Use individual clients for these methods")]
    public interface IObservableMiscellaneousClient
    {
        /// <summary>
        /// Gets all the emojis available to use on GitHub.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="IObservable{Emoji}"/> of emoji and their URI.</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use EmojisClient.GetAllEmojis instead.")]
        IObservable<Emoji> GetAllEmojis(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the rendered Markdown for an arbitrary markdown document.
        /// </summary>
        /// <param name="markdown">An arbitrary Markdown document</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The rendered Markdown.</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use MarkdownClient.RenderArbitraryMarkdown instead.")]
        IObservable<string> RenderArbitraryMarkdown(NewArbitraryMarkdown markdown, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the rendered Markdown for the specified plain-text Markdown document.
        /// </summary>
        /// <param name="markdown">A plain-text Markdown document</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The rendered Markdown.</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use MarkdownClient.RenderRawMarkdown instead.")]
        IObservable<string> RenderRawMarkdown(string markdown, CancellationToken cancellationToken = default);

        /// <summary>
        /// List all templates available to pass as an option when creating a repository.
        /// </summary>
        /// <returns>An observable list of gitignore template names.</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use GitIgnoreClient.GetAllGitIgnoreTemplates instead.")]
        IObservable<string> GetAllGitIgnoreTemplates(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the source for a single GitIgnore template
        /// </summary>
        /// <param name="templateName">Returns the template source for the given template</param>
        [Obsolete("This client is being deprecated and will be removed in the future. Use GitIgnoreClient.GetGitIgnoreTemplate instead.")]
        IObservable<GitIgnoreTemplate> GetGitIgnoreTemplate(string templateName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a list of the licenses shown in the license picker on GitHub.com. This is not a comprehensive
        /// list of all possible OSS licenses.
        /// </summary>
        /// <returns>A list of licenses available on the site</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use LicensesClient.GetAllLicenses instead.")]
        IObservable<LicenseMetadata> GetAllLicenses(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a list of the licenses shown in the license picker on GitHub.com. This is not a comprehensive
        /// list of all possible OSS licenses.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A list of licenses available on the site</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use LicensesClient.GetAllLicenses instead.")]
        IObservable<LicenseMetadata> GetAllLicenses(ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a license based on the license key such as "MIT"
        /// </summary>
        /// <param name="key"></param>
        /// <returns>A <see cref="License" /> that includes the license key, text, and attributes of the license.</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use LicensesClient.GetLicense instead.")]
        IObservable<License> GetLicense(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets API Rate Limits (API service rather than header info).
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="MiscellaneousRateLimit"/> of Rate Limits.</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use RateLimitClient.GetRateLimits instead.")]
        IObservable<MiscellaneousRateLimit> GetRateLimits(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves information about GitHub.com, the service or a GitHub Enterprise installation.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="Meta"/> containing metadata about the GitHub instance.</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use MetaClient.GetMetadata instead.")]
        IObservable<Meta> GetMetadata(CancellationToken cancellationToken = default);
    }
}
