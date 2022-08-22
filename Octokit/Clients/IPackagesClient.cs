using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IPackagesClient
    {
        /// <summary>
        /// List all packages for an organisations, readable by the current user
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#list-packages-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageVisibility">Optional: The visibility of the package</param>
        [ExcludeFromPaginationApiOptionsConventionTest("No api options available according to the documentation")]
        Task<IReadOnlyList<Package>> GetAll(string org, PackageType packageType, PackageVisibility? packageVisibility = null);

        /// <summary>
        /// Get a specific package for an Organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-a-package-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        Task<Package> Get(string org, PackageType packageType, string packageName);

        /// <summary>
        /// Delete a specific package for an Organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#delete-a-package-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        Task Delete(string org, PackageType packageType, string packageName);
    }
}