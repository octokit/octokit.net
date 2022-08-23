using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class PackageVersionsClient : ApiClient, IPackageVersionsClient
    {
        public PackageVersionsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// List all versions of a package.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-all-package-versions-for-a-package-owned-by-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="state">Optional: Return packages with a state. Defaults to Active</param>
        /// <param name="options">Optional: Paging options</param>
        [ManualRoute("GET", "/orgs/{org}/packages/{package_type}/{package_name}/versions")]
        public Task<IReadOnlyList<PackageVersion>> GetAll(string org, PackageType packageType, string packageName, PackageVersionState state = PackageVersionState.Active, ApiOptions options = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.ApiOptionsNotNull(ref options);

            var route = ApiUrls.PackageVersions(org, packageType, packageName);
            var parameters = ParameterBuilder.AddParameter("state", state);

            return ApiConnection.GetAll<PackageVersion>(route, parameters, options);
        }

        /// <summary>
        /// Get a specific version of a package.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-a-package-version-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        [ManualRoute("GET", "/orgs/{org}/packages/{package_type}/{package_name}/versions/{package_version_id}")]
        public Task<PackageVersion> Get(string org, PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            var route = ApiUrls.PackageVersion(org, packageType, packageName, packageVersionId);

            return ApiConnection.Get<PackageVersion>(route);
        }

        /// <summary>
        /// Deletes a specific package version in an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#delete-package-version-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        [ManualRoute("DELETE", "/orgs/{org}/packages/{package_type}/{package_name}/versions/{package_version_id}")]
        public Task Delete(string org, PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            var route = ApiUrls.PackageVersion(org, packageType, packageName, packageVersionId);
            return ApiConnection.Delete(route);
        }

        /// <summary>
        /// Restores a specific package version in an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#restore-package-version-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        [ManualRoute("POST", "/orgs/{org}/packages/{package_type}/{package_name}/versions/{package_version_id}/restore")]
        public Task Restore(string org, PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            var route = ApiUrls.PackageVersionRestore(org, packageType, packageName, packageVersionId);
            
            return ApiConnection.Post(route);
        }
    }
}
