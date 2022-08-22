using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Packages API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/packages">Packages API documentation</a> for more details.
    /// </remarks>
    public class PackagesClient : ApiClient, IPackagesClient
    {
        public PackagesClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// List all packages for an organisations, readable by the current user
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/packages#list-packages-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageVisibility">Optional: The visibility of the package</param>
        [ManualRoute("GET", "/orgs/{org}/packages")]
        public Task<IReadOnlyList<Package>> List(string org, PackageType packageType, PackageVisibility? packageVisibility = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return ApiConnection.GetAll<Package>(ApiUrls.Packages(org, packageType, packageVisibility));
        }
    }
}
