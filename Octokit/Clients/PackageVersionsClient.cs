using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class PackageVersionsClient : ApiClient, IPackageVersionsClient
    {
        public PackageVersionsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        #region Organization
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
        public Task<IReadOnlyList<PackageVersion>> GetAllForOrg(string org, PackageType packageType, string packageName, PackageVersionState state = PackageVersionState.Active, ApiOptions options = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.ApiOptionsNotNull(ref options);

            var route = ApiUrls.PackageVersionsOrg(org, packageType, packageName);
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
        public Task<PackageVersion> GetForOrg(string org, PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            var route = ApiUrls.PackageVersionOrg(org, packageType, packageName, packageVersionId);

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
        public Task DeleteForOrg(string org, PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            var route = ApiUrls.PackageVersionOrg(org, packageType, packageName, packageVersionId);
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
        public Task RestoreForOrg(string org, PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            var route = ApiUrls.PackageVersionRestoreOrg(org, packageType, packageName, packageVersionId);
            
            return ApiConnection.Post(route);
        }
        #endregion

        #region Active User
        /// <summary>
        /// Returns all package versions for a package owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-all-package-versions-for-a-package-owned-by-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="state">Optional: Return packages with a state. Defaults to Active</param>
        /// <param name="options">Optional: Paging options</param>
        [ManualRoute("GET", "/user/packages/{package_type}/{package_name}/versions")]
        public Task<IReadOnlyList<PackageVersion>> GetAllForActiveUser(PackageType packageType, string packageName, PackageVersionState state = PackageVersionState.Active, ApiOptions options = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.ApiOptionsNotNull(ref options);

            var route = ApiUrls.PackageVersionsActiveUser(packageType, packageName);
            var parameters = ParameterBuilder.AddParameter("state", state);

            return ApiConnection.GetAll<PackageVersion>(route, parameters, options);
        }

        /// <summary>
        /// Gets a specific package version for a package owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-a-package-version-for-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        [ManualRoute("GET", "/user/packages/{package_type}/{package_name}/versions/{package_version_id}")]
        public Task<PackageVersion> GetForActiveUser(PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            var route = ApiUrls.PackageVersionActiveUser( packageType, packageName, packageVersionId);

            return ApiConnection.Get<PackageVersion>(route);
        }

        /// <summary>
        /// Deletes a specific package version for a package owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#delete-a-package-version-for-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        [ManualRoute("DELETE", "/user/packages/{package_type}/{package_name}/versions/{package_version_id}")]
        public Task DeleteForActiveUser(PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            var route = ApiUrls.PackageVersionActiveUser(packageType, packageName, packageVersionId);
            return ApiConnection.Delete(route);
        }

        /// <summary>
        /// Restores a package version owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#restore-a-package-version-for-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        [ManualRoute("POST", "/user/packages/{package_type}/{package_name}/versions/{package_version_id}/restore")]
        public Task RestoreForActiveUser(PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            var route = ApiUrls.PackageVersionRestoreActiveUser(packageType, packageName, packageVersionId);

            return ApiConnection.Post(route);
        }
        #endregion

        #region Specific User
        /// <summary>
        /// Returns all package versions for a public package owned by a specified user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-all-package-versions-for-a-package-owned-by-a-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="username">Required: Username</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="state">Optional: Return packages with a state. Defaults to Active</param>
        /// <param name="options">Optional: Paging options</param>
        [ManualRoute("GET", "/users/{username}/packages/{package_type}/{package_name}/versions")]
        public Task<IReadOnlyList<PackageVersion>> GetAllForUser(string username, PackageType packageType, string packageName, PackageVersionState state = PackageVersionState.Active, ApiOptions options = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(username, nameof(username));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.ApiOptionsNotNull(ref options);

            var route = ApiUrls.PackageVersionsUser(username, packageType, packageName);
            var parameters = ParameterBuilder.AddParameter("state", state);

            return ApiConnection.GetAll<PackageVersion>(route, parameters, options);
        }

        /// <summary>
        /// Gets a specific package version for a public package owned by a specified user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-a-package-version-for-a-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="username">Required: Username</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        [ManualRoute("GET", "/users/{username}/packages/{package_type}/{package_name}/versions/{package_version_id}")]
        public Task<PackageVersion> GetForUser(string username, PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(username, nameof(username));
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            var route = ApiUrls.PackageVersionUser(username, packageType, packageName, packageVersionId);

            return ApiConnection.Get<PackageVersion>(route);
        }

        /// <summary>
        /// Deletes a specific package version for a user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#delete-package-version-for-a-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="username">Required: Username</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        [ManualRoute("DELETE", "/users/{username}/packages/{package_type}/{package_name}/versions/{package_version_id}")]
        public Task DeleteForUser(string username, PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(username, nameof(username));
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            var route = ApiUrls.PackageVersionUser(username, packageType, packageName, packageVersionId);
            return ApiConnection.Delete(route);
        }

        /// <summary>
        /// Restores a specific package version for a user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#restore-package-version-for-a-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="username">Required: Username</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        [ManualRoute("POST", "/users/{username}/packages/{package_type}/{package_name}/versions/{package_version_id}/restore")]
        public Task RestoreForUser(string username, PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(username, nameof(username));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            var route = ApiUrls.PackageVersionRestoreUser(username, packageType, packageName, packageVersionId);

            return ApiConnection.Post(route);
        }
        #endregion
    }
}
