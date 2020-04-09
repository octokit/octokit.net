using System;
using System.Collections.Generic;
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
    public class MiscellaneousClient : ApiClient, IMiscellaneousClient
    {
        /// <summary>
        ///     Initializes a new GitHub miscellaneous API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public MiscellaneousClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all code of conducts on GitHub.
        /// </summary>
        /// <remarks>See the <a href="https://developer.github.com/v3/codes_of_conduct/#list-all-codes-of-conduct">API documentation</a> for more information.</remarks>
        /// <returns>A <see cref="IReadOnlyList{CodeOfConduct}"/> on GitHub.</returns>
        [Preview("scarlet-witch")]
        [ManualRoute("GET", "/codes_of_conduct")]
        public Task<IReadOnlyList<CodeOfConduct>> GetAll()
        {
            return ApiConnection.GetAll<CodeOfConduct>(ApiUrls.CodesOfConduct());
        }

        /// <summary>
        /// Gets an individual code of conduct.
        /// </summary>
        /// <remarks>See the <a href="https://developer.github.com/v3/codes_of_conduct/#get-an-individual-code-of-conduct">API documentation</a> for more information.</remarks>
        /// <param name="key">The unique key for the Code of Conduct</param>
        /// <returns>A <see cref="CodeOfConduct"/> that includes the code of conduct key, name, and API/HTML URL.</returns>
        [Preview("scarlet-witch")]
        [ManualRoute("GET", "/codes_of_conduct/{key}")]
        public Task<CodeOfConduct> GetCodeOfConduct(string key)
        {
            Ensure.ArgumentNotNullOrEmptyString(key, nameof(key));

            return ApiConnection.Get<CodeOfConduct>(ApiUrls.CodeOfConduct(key));
        }

        /// <summary>
        /// Gets the code of conduct for a repository, if one is detected.
        /// </summary>
        /// <remarks>See the <a href="https://developer.github.com/v3/codes_of_conduct/#get-the-contents-of-a-repositorys-code-of-conduct">API documentation</a> for more information.</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The <see cref="CodeOfConduct"/> that the repository uses, if one is detected.</returns>
        [Preview("scarlet-witch")]
        [ManualRoute("GET", "/repos/{owner}/{repo}/community/code_of_conduct")]
        public Task<CodeOfConduct> GetCodeOfConduct(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<CodeOfConduct>(ApiUrls.CodeOfConduct(owner, name));
        }

        /// <summary>
        /// Gets all the emojis available to use on GitHub.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="IReadOnlyDictionary{TKey,TValue}"/> of emoji and their URI.</returns>
        [ManualRoute("GET", "/emojis")]
        public Task<IReadOnlyList<Emoji>> GetAllEmojis()
        {
            return ApiConnection.GetAll<Emoji>(ApiUrls.Emojis());
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

        /// <summary>
        /// List all templates available to pass as an option when creating a repository.
        /// </summary>
        /// <returns>A list of template names</returns>
        [ManualRoute("GET", "/gitignore/templates")]
        public Task<IReadOnlyList<string>> GetAllGitIgnoreTemplates()
        {
            return ApiConnection.GetAll<string>(ApiUrls.GitIgnoreTemplates());
        }

        /// <summary>
        /// Retrieves the source for a single GitIgnore template
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns>A template and its source</returns>
        [ManualRoute("GET", "/gitignore/templates/{name}")]
        public Task<GitIgnoreTemplate> GetGitIgnoreTemplate(string templateName)
        {
            Ensure.ArgumentNotNullOrEmptyString(templateName, nameof(templateName));

            return ApiConnection.Get<GitIgnoreTemplate>(ApiUrls.GitIgnoreTemplates(templateName));
        }

        /// <summary>
        /// Returns a list of the licenses shown in the license picker on GitHub.com. This is not a comprehensive
        /// list of all possible OSS licenses.
        /// </summary>
        /// <returns>A list of licenses available on the site</returns>
        [ManualRoute("GET", "/licenses")]
        public Task<IReadOnlyList<LicenseMetadata>> GetAllLicenses()
        {
            return GetAllLicenses(ApiOptions.None);
        }

        /// <summary>
        /// Returns a list of the licenses shown in the license picker on GitHub.com. This is not a comprehensive
        /// list of all possible OSS licenses.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A list of licenses available on the site</returns>
        [ManualRoute("GET", "/licenses")]
        public Task<IReadOnlyList<LicenseMetadata>> GetAllLicenses(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<LicenseMetadata>(ApiUrls.Licenses(), options);
        }

        /// <summary>
        /// Retrieves a license based on the license key such as "mit"
        /// </summary>
        /// <param name="key"></param>
        /// <returns>A <see cref="License" /> that includes the license key, text, and attributes of the license.</returns>
        [ManualRoute("GET", "/licenses/{key}")]
        public Task<License> GetLicense(string key)
        {
            return ApiConnection.Get<License>(ApiUrls.Licenses(key));
        }

        /// <summary>
        /// Gets API Rate Limits (API service rather than header info).
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="MiscellaneousRateLimit"/> of Rate Limits.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        [ManualRoute("GET", "/rate_limit")]
        public Task<MiscellaneousRateLimit> GetRateLimits()
        {
            return ApiConnection.Get<MiscellaneousRateLimit>(ApiUrls.RateLimit());
        }

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
