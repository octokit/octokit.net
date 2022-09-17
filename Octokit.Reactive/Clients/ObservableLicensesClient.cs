using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's licenses APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/licenses">Licenses API documentation</a> for more details.
    /// </remarks>
    public class ObservableLicensesClient : IObservableLicensesClient
    {
        private readonly ILicensesClient _client;

        public ObservableLicensesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Licenses;
        }

        /// <summary>
        /// Returns a list of the licenses shown in the license picker on GitHub.com. This is not a comprehensive
        /// list of all possible OSS licenses.
        /// </summary>
        /// <returns>A list of licenses available on the site</returns>
        public IObservable<LicenseMetadata> GetAllLicenses()
        {
            return GetAllLicenses(ApiOptions.None);
        }

        /// <summary>
        /// Returns a list of the licenses shown in the license picker on GitHub.com. This is not a comprehensive
        /// list of all possible OSS licenses.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A list of licenses available on the site</returns>
        public IObservable<LicenseMetadata> GetAllLicenses(ApiOptions options)
        {
            return _client.GetAllLicenses(options).ToObservable().SelectMany(l => l);
        }

        /// <summary>
        /// Retrieves a license based on the license key such as "MIT"
        /// </summary>
        /// <param name="key"></param>
        /// <returns>A <see cref="License" /> that includes the license key, text, and attributes of the license.</returns>
        public IObservable<License> GetLicense(string key)
        {
            return _client.GetLicense(key).ToObservable();
        }
    }
}
