using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableMiscellaneousClient : IObservableMiscellaneousClient
    {
        readonly IMiscellaneousClient _client;

        public ObservableMiscellaneousClient(IMiscellaneousClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client;
        }

        /// <summary>
        /// Gets all the emojis available to use on GitHub.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="IObservable{Emoji}"/> of emoji and their URI.</returns>
        public IObservable<Emoji> GetAllEmojis()
        {
            return _client.GetAllEmojis().ToObservable().SelectMany(e => e);
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

        /// <summary>
        /// List all templates available to pass as an option when creating a repository.
        /// </summary>
        /// <returns>An observable list of gitignore template names.</returns>
        public IObservable<string> GetAllGitIgnoreTemplates()
        {
            return _client.GetAllGitIgnoreTemplates().ToObservable().SelectMany(t => t);
        }

        /// <summary>
        /// Retrieves the source for a single GitIgnore template
        /// </summary>
        /// <param name="templateName">Returns the template source for the given template</param>
        public IObservable<GitIgnoreTemplate> GetGitIgnoreTemplate(string templateName)
        {
            return _client.GetGitIgnoreTemplate(templateName).ToObservable();
        }

        /// <summary>
        /// Returns a list of the licenses shown in the license picker on GitHub.com. This is not a comprehensive
        /// list of all possible OSS licenses.
        /// </summary>
        /// <remarks>This is a PREVIEW API! Use it at your own risk.</remarks>
        /// <returns>A list of licenses available on the site</returns>
        public IObservable<LicenseMetadata> GetAllLicenses()
        {
            return _client.GetAllLicenses().ToObservable().SelectMany(l => l);
        }

        /// <summary>
        /// Retrieves a license based on the licence key such as "mit"
        /// </summary>
        /// <param name="key"></param>
        /// <returns>A <see cref="License" /> that includes the license key, text, and attributes of the license.</returns>
        public IObservable<License> GetLicense(string key)
        {
            return _client.GetLicense(key).ToObservable();
        }

        /// <summary>
        /// Gets API Rate Limits (API service rather than header info).
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="MiscellaneousRateLimit"/> of Rate Limits.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public IObservable<MiscellaneousRateLimit> GetRateLimits()
        {
            return _client.GetRateLimits().ToObservable();
        }

        /// <summary>
        /// Retrieves information about GitHub.com, the service or a GitHub Enterprise installation.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="Meta"/> containing metadata about the GitHub instance.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public IObservable<Meta> GetMetadata()
        {
            return _client.GetMetadata().ToObservable();
        }
    }
}
