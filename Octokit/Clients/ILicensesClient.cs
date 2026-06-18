using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's licenses APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/licenses">Licenses API documentation</a> for more details.
    /// </remarks>
    public interface ILicensesClient
    {
        /// <summary>
        /// Returns a list of the licenses shown in the license picker on GitHub.com. This is not a comprehensive
        /// list of all possible OSS licenses.
        /// </summary>
        /// <returns>A list of licenses available on the site</returns>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<LicenseMetadata>> GetAllLicenses(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a list of the licenses shown in the license picker on GitHub.com. This is not a comprehensive
        /// list of all possible OSS licenses.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A list of licenses available on the site</returns>
        Task<IReadOnlyList<LicenseMetadata>> GetAllLicenses(ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a license based on the license key such as "mit"
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A <see cref="License" /> that includes the license key, text, and attributes of the license.</returns>
        Task<License> GetLicense(string key, CancellationToken cancellationToken = default);
    }
}
