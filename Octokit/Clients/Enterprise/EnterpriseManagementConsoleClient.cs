using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Enterprise Management Console API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/enterprise/management_console/">Enterprise Management Console API documentation</a> for more information.
    ///</remarks>
    public class EnterpriseManagementConsoleClient : ApiClient, IEnterpriseManagementConsoleClient
    {
        public EnterpriseManagementConsoleClient(IApiConnection apiConnection)
            : base(apiConnection)
        { }

        /// <summary>
        /// Gets GitHub Enterprise Maintenance Mode Status
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/management_console/#check-maintenance-status
        /// </remarks>
        /// <returns>The <see cref="MaintenanceModeResponse"/>.</returns>
        public Task<MaintenanceModeResponse> GetMaintenanceMode(string managementConsolePassword)
        {
            Ensure.ArgumentNotNullOrEmptyString(managementConsolePassword, "managementConsolePassword");

            var endpoint = ApiUrls.EnterpriseManagementConsoleMaintenance(managementConsolePassword, ApiConnection.Connection.BaseAddress);

            return ApiConnection.Get<MaintenanceModeResponse>(endpoint);
        }

        /// <summary>
        /// Sets GitHub Enterprise Maintenance Mode
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/management_console/#check-maintenance-status
        /// </remarks>
        /// <returns>The <see cref="MaintenanceModeResponse"/>.</returns>
        public Task<MaintenanceModeResponse> EditMaintenanceMode(UpdateMaintenanceRequest maintenance, string managementConsolePassword)
        {
            Ensure.ArgumentNotNull(maintenance, "maintenance");
            Ensure.ArgumentNotNullOrEmptyString(managementConsolePassword, "managementConsolePassword");

            var endpoint = ApiUrls.EnterpriseManagementConsoleMaintenance(managementConsolePassword, ApiConnection.Connection.BaseAddress);

            return ApiConnection.Post<MaintenanceModeResponse>(endpoint, maintenance.ToFormUrlEncodedParameterString());
        }

        /// <summary>
        /// Gets the authorized SSH keys for the GitHub Enterprise instance
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/management_console/#retrieve-authorized-ssh-keys
        /// </remarks>
        public Task<IReadOnlyList<AuthorizedKey>> GetAllAuthorizedKeys(string managementConsolePassword)
        {
            Ensure.ArgumentNotNullOrEmptyString(managementConsolePassword, "managementConsolePassword");

            var endpoint = ApiUrls.EnterpriseManagementConsoleAuthorizedKeys(managementConsolePassword, ApiConnection.Connection.BaseAddress);

            return ApiConnection.Get<IReadOnlyList<AuthorizedKey>>(endpoint);
        }

        /// <summary>
        /// Adds an authorized SSH key to the GitHub Enterprise instance
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/management_console/#add-a-new-authorized-ssh-key
        /// </remarks>
        public Task<IReadOnlyList<AuthorizedKey>> AddAuthorizedKey(AuthorizedKeyRequest authorizedKey, string managementConsolePassword)
        {
            Ensure.ArgumentNotNull(authorizedKey, "authorizedKey");
            Ensure.ArgumentNotNullOrEmptyString(managementConsolePassword, "managementConsolePassword");

            var endpoint = ApiUrls.EnterpriseManagementConsoleAuthorizedKeys(managementConsolePassword, ApiConnection.Connection.BaseAddress);
            endpoint = endpoint.ApplyParameters(authorizedKey.ToParametersDictionary());

            return ApiConnection.Post<IReadOnlyList<AuthorizedKey>>(endpoint);
        }

        /// <summary>
        /// Removes an authorized SSH key from the GitHub Enterprise instance
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/management_console/#remove-an-authorized-ssh-key
        /// </remarks>
        public Task<IReadOnlyList<AuthorizedKey>> DeleteAuthorizedKey(AuthorizedKeyRequest authorizedKey, string managementConsolePassword)
        {
            Ensure.ArgumentNotNull(authorizedKey, "authorizedKey");
            Ensure.ArgumentNotNullOrEmptyString(managementConsolePassword, "managementConsolePassword");

            var endpoint = ApiUrls.EnterpriseManagementConsoleAuthorizedKeys(managementConsolePassword, ApiConnection.Connection.BaseAddress);
            endpoint = endpoint.ApplyParameters(authorizedKey.ToParametersDictionary());

            return ApiConnection.Delete<IReadOnlyList<AuthorizedKey>>(endpoint);
        }
    }
}
