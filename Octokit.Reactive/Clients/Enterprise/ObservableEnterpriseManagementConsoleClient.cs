using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

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
        readonly IConnection _connection;

        public ObservableEnterpriseManagementConsoleClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Enterprise.ManagementConsole;
            _connection = client.Connection;
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

        /// <summary>
        /// Gets the authorized SSH keys for the GitHub Enterprise instance
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/management_console/#retrieve-authorized-ssh-keys
        /// </remarks>
        public IObservable<AuthorizedKey> GetAllAuthorizedKeys(string managementConsolePassword)
        {
            Ensure.ArgumentNotNullOrEmptyString(managementConsolePassword, "managementConsolePassword");

            return _connection.GetAndFlattenAllPages<AuthorizedKey>(ApiUrls.EnterpriseManagementConsoleAuthorizedKeys(managementConsolePassword, _connection.BaseAddress));
        }

        /// <summary>
        /// Adds an authorized SSH key to the GitHub Enterprise instance
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/management_console/#add-a-new-authorized-ssh-key
        /// </remarks>
        public IObservable<AuthorizedKey> AddAuthorizedKey(AuthorizedKeyRequest authorizedKey, string managementConsolePassword)
        {
            Ensure.ArgumentNotNull(authorizedKey, "authorizedKey");
            Ensure.ArgumentNotNullOrEmptyString(managementConsolePassword, "managementConsolePassword");

            return _client.AddAuthorizedKey(authorizedKey, managementConsolePassword)
                .ToObservable()
                .SelectMany(x => x); // HACK: POST is not compatible with GetAndFlattenPages
        }

        /// <summary>
        /// Removes an authorized SSH key from the GitHub Enterprise instance
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/management_console/#remove-an-authorized-ssh-key
        /// </remarks>
        public IObservable<AuthorizedKey> DeleteAuthorizedKey(AuthorizedKeyRequest authorizedKey, string managementConsolePassword)
        {
            Ensure.ArgumentNotNull(authorizedKey, "authorizedKey");
            Ensure.ArgumentNotNullOrEmptyString(managementConsolePassword, "managementConsolePassword");

            return _client.DeleteAuthorizedKey(authorizedKey, managementConsolePassword)
                .ToObservable()
                .SelectMany(x => x); // HACK: DELETE is not compatible with GetAndFlattenPages
        }
    }
}
