using System;
using System.Reactive.Threading.Tasks;
using Octokit;


namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Enterprise License API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/enterprise/license/">Enterprise License API documentation</a> for more information.
    ///</remarks>
    public class ObservableEnterpriseLicenseClient : IObservableEnterpriseLicenseClient
    {
        readonly IEnterpriseLicenseClient _client;

        public ObservableEnterpriseLicenseClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Enterprise.License;
        }

        /// <summary>
        /// Gets GitHub Enterprise License Information (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/license/#get-license-information
        /// </remarks>
        /// <returns>The <see cref="LicenseInfo"/> statistics.</returns>
        public IObservable<LicenseInfo> Get()
        {
            return _client.Get().ToObservable();
        }
    }
}
