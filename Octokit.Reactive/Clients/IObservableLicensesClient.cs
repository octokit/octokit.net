using System;
using System.Threading;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's licenses APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/licenses">Licenses API documentation</a> for more details.
    /// </remarks>
    public interface IObservableLicensesClient
    {
        /// <summary>
        /// Returns a list of the licenses shown in the license picker on GitHub.com. This is not a comprehensive
        /// list of all possible OSS licenses.
        /// </summary>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A list of licenses available on the site</returns>
        IObservable<LicenseMetadata> GetAllLicenses(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a list of the licenses shown in the license picker on GitHub.com. This is not a comprehensive
        /// list of all possible OSS licenses.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A list of licenses available on the site</returns>
        IObservable<LicenseMetadata> GetAllLicenses(ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a license based on the license key such as "MIT"
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A <see cref="License" /> that includes the license key, text, and attributes of the license.</returns>
        IObservable<License> GetLicense(string key, CancellationToken cancellationToken = default);
    }
}
