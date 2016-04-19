using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Enterprise Management Console API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/enterprise/management_console/">Enterprise Management Console API documentation</a> for more information.
    /// </remarks>
    public class ObservableEnterpriseManagementConsoleClient : IObservableEnterpriseManagementConsoleClient
    {
        readonly IEnterpriseManagementConsoleClient _client;

        public ObservableEnterpriseManagementConsoleClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Enterprise.ManagementConsole;
        }

        /// <summary>
        /// Gets GitHub Enterprise Maintenance Mode Status
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/management_console/#check-maintenance-status
        /// </remarks>
        /// <returns>The <see cref="MaintenanceModeResponse"/>.</returns>
        public IObservable<MaintenanceModeResponse> GetMaintenanceMode(string managementConsolePassword)
        {
            Ensure.ArgumentNotNullOrEmptyString(managementConsolePassword, "managementConsolePassword");

            return _client.GetMaintenanceMode(managementConsolePassword).ToObservable();
        }

        /// <summary>
        /// Sets GitHub Enterprise Maintenance Mode
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/management_console/#check-maintenance-status
        /// </remarks>
        /// <returns>The <see cref="MaintenanceModeResponse"/>.</returns>
        public IObservable<MaintenanceModeResponse> EditMaintenanceMode(UpdateMaintenanceRequest maintenance, string managementConsolePassword)
        {
            Ensure.ArgumentNotNull(maintenance, "maintenance");
            Ensure.ArgumentNotNullOrEmptyString(managementConsolePassword, "managementConsolePassword");

            return _client.EditMaintenanceMode(maintenance, managementConsolePassword).ToObservable();
        }
    }
}
