using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's licenses APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/licenses">Licenses API documentation</a> for more details.
    /// </remarks>
    public class LicensesClient : ApiClient, ILicensesClient
    {
        /// <summary>
        ///     Initializes a new GitHub gitignore API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public LicensesClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
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
    }
}