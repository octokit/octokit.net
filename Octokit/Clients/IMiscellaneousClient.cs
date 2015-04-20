#if NET_45
using System.Collections.Generic;
#endif
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's miscellaneous APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/misc/">Miscellaneous API documentation</a> for more details.
    /// </remarks>
    public interface IMiscellaneousClient
    {
        /// <summary>
        /// Gets all the emojis available to use on GitHub.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="IReadOnlyDictionary{TKey,TValue}"/> of emoji and their URI.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<Emoji>> GetAllEmojis();

        /// <summary>
        /// Gets the rendered Markdown for the specified plain-text Markdown document.
        /// </summary>
        /// <param name="markdown">A plain-text Markdown document</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The rendered Markdown.</returns>
        Task<string> RenderRawMarkdown(string markdown);

        /// <summary>
        /// List all templates available to pass as an option when creating a repository.
        /// </summary>
        /// <returns>A list of template names</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<string>> GetAllGitIgnoreTemplates();

        /// <summary>
        /// Retrieves the source for a single GitIgnore template
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns>A template and its source</returns>
        Task<GitIgnoreTemplate> GetGitIgnoreTemplate(string templateName);

        /// <summary>
        /// Returns a list of the licenses shown in the license picker on GitHub.com. This is not a comprehensive
        /// list of all possible OSS licenses.
        /// </summary>
        /// <remarks>This is a PREVIEW API! Use it at your own risk.</remarks>
        /// <returns>A list of licenses available on the site</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<LicenseMetadata>> GetAllLicenses();

        /// <summary>
        /// Retrieves a license based on the licence key such as "mit"
        /// </summary>
        /// <param name="key"></param>
        /// <returns>A <see cref="License" /> that includes the license key, text, and attributes of the license.</returns>
        Task<License> GetLicense(string key);
    }
}
