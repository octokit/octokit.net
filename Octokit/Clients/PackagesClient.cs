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
            PackageVersions = new PackageVersionsClient(apiConnection);
        }

        public IPackageVersionsClient PackageVersions { get; private set; }

        /// <summary>
        /// List all packages for an organisations, readable by the current user
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#list-packages-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageVisibility">Optional: The visibility of the package</param>
        [ManualRoute("GET", "/orgs/{org}/packages")]
        public Task<IReadOnlyList<Package>> GetAll(string org, PackageType packageType, PackageVisibility? packageVisibility = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            var route = ApiUrls.Packages(org);
            var parameters = ParameterBuilder.AddParameter("package_type", packageType).AddOptionalParameter("visibility", packageVisibility);

            return ApiConnection.GetAll<Package>(route, parameters);
        }

        /// <summary>
        /// Get a specific package for an Organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-a-package-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        [ManualRoute("GET", "/orgs/{org}/packages/{package_type}/{package_name}")]
        public Task<Package> Get(string org, PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            var route = ApiUrls.Package(org, packageType, packageName);
            
            return ApiConnection.Get<Package>(route);
        }

        /// <summary>
        /// Delete a specific package for an Organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#delete-a-package-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        [ManualRoute("DELETE", "/orgs/{org}/packages/{package_type}/{package_name}")]
        public Task Delete(string org, PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            var route = ApiUrls.Package(org, packageType, packageName);

            return ApiConnection.Delete(route);
        }

        /// <summary>
        /// Restore a specific package for an Organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#restore-a-package-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        [ManualRoute("POST", "/orgs/{org}/packages/{package_type}/{package_name}/restore")]
        public Task Restore(string org, PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            var route = ApiUrls.PackageRestore(org, packageType, packageName);
            
            return ApiConnection.Post(route);
        }
    }
}
