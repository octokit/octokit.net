using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IPackageVersionsClient
    {
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
        Task<IReadOnlyList<PackageVersion>> GetAll(string org, PackageType packageType, string packageName, PackageVersionState state = PackageVersionState.Active, ApiOptions options = null);

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
        Task<PackageVersion> Get(string org, PackageType packageType, string packageName, int packageVersionId);

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
        Task Delete(string org, PackageType packageType, string packageName, int packageVersionId);

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
        Task Restore(string org, PackageType packageType, string packageName, int packageVersionId);
    }
}