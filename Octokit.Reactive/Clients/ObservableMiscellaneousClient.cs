using System;
using System.Threading;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    [Obsolete("Use individual clients available on the GitHubClient for these methods")]
    public class ObservableMiscellaneousClient : IObservableMiscellaneousClient
    {
        readonly IMiscellaneousClient _client;

        public ObservableMiscellaneousClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Miscellaneous;
        }

        /// <summary>
        /// Gets all the emojis available to use on GitHub.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="IObservable{Emoji}"/> of emoji and their URI.</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use EmojisClient.GetAllEmojis instead.")]
        public IObservable<Emoji> GetAllEmojis(CancellationToken cancellationToken = default)
        {
            return _client.GetAllEmojis(cancellationToken).ToObservable().SelectMany(e => e);
        }

        /// <summary>
        /// Gets the rendered Markdown for an arbitrary markdown document.
        /// </summary>
        /// <param name="markdown">An arbitrary Markdown document</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The rendered Markdown.</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use MarkdownClient.RenderArbitraryMarkdown instead.")]
        public IObservable<string> RenderArbitraryMarkdown(NewArbitraryMarkdown markdown, CancellationToken cancellationToken = default)
        {
            return _client.RenderArbitraryMarkdown(markdown, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets the rendered Markdown for the specified plain-text Markdown document.
        /// </summary>
        /// <param name="markdown">A plain-text Markdown document</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The rendered Markdown.</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use MarkdownClient.RenderRawMarkdown instead.")]
        public IObservable<string> RenderRawMarkdown(string markdown, CancellationToken cancellationToken = default)
        {
            return _client.RenderRawMarkdown(markdown, cancellationToken).ToObservable();
        }

        /// <summary>
        /// List all templates available to pass as an option when creating a repository.
        /// </summary>
        /// <returns>An observable list of gitignore template names.</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use GitIgnoreClient.GetAllGitIgnoreTemplates instead.")]
        public IObservable<string> GetAllGitIgnoreTemplates(CancellationToken cancellationToken = default)
        {
            return _client.GetAllGitIgnoreTemplates(cancellationToken).ToObservable().SelectMany(t => t);
        }

        /// <summary>
        /// Retrieves the source for a single GitIgnore template
        /// </summary>
        /// <param name="templateName">Returns the template source for the given template</param>
        [Obsolete("This client is being deprecated and will be removed in the future. Use GitIgnoreClient.GetGitIgnoreTemplate instead.")]
        public IObservable<GitIgnoreTemplate> GetGitIgnoreTemplate(string templateName, CancellationToken cancellationToken = default)
        {
            return _client.GetGitIgnoreTemplate(templateName, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Returns a list of the licenses shown in the license picker on GitHub.com. This is not a comprehensive
        /// list of all possible OSS licenses.
        /// </summary>
        /// <returns>A list of licenses available on the site</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use LicensesClient.GetAllLicenses instead.")]
        public IObservable<LicenseMetadata> GetAllLicenses(CancellationToken cancellationToken = default)
        {
            return GetAllLicenses(ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Returns a list of the licenses shown in the license picker on GitHub.com. This is not a comprehensive
        /// list of all possible OSS licenses.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A list of licenses available on the site</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use LicensesClient.GetAllLicenses instead.")]
        public IObservable<LicenseMetadata> GetAllLicenses(ApiOptions options, CancellationToken cancellationToken = default)
        {
            return _client.GetAllLicenses(options, cancellationToken).ToObservable().SelectMany(l => l);
        }

        /// <summary>
        /// Retrieves a license based on the license key such as "MIT"
        /// </summary>
        /// <param name="key"></param>
        /// <returns>A <see cref="License" /> that includes the license key, text, and attributes of the license.</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use LicensesClient.GetLicense instead.")]
        public IObservable<License> GetLicense(string key, CancellationToken cancellationToken = default)
        {
            return _client.GetLicense(key, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets API Rate Limits (API service rather than header info).
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="MiscellaneousRateLimit"/> of Rate Limits.</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use RateLimitClient.GetRateLimits instead.")]
        public IObservable<MiscellaneousRateLimit> GetRateLimits(CancellationToken cancellationToken = default)
        {
            return _client.GetRateLimits(cancellationToken).ToObservable();
        }

        /// <summary>
        /// Retrieves information about GitHub.com, the service or a GitHub Enterprise installation.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="Meta"/> containing metadata about the GitHub instance.</returns>
        [Obsolete("This client is being deprecated and will be removed in the future. Use MetaClient.GetMetadata instead.")]
        public IObservable<Meta> GetMetadata(CancellationToken cancellationToken = default)
        {
            return _client.GetMetadata(cancellationToken).ToObservable();
        }
    }
}
