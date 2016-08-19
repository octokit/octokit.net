using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class ObservableEnterpriseManagementConsoleTests
    {
        readonly IObservableGitHubClient _github;

        public ObservableEnterpriseManagementConsoleTests()
        {
            _github = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());
        }

        [GitHubEnterpriseManagementConsoleTest]
        public async Task CanGetMaintenanceMode()
        {
            var maintenance = await _github.Enterprise.ManagementConsole.GetMaintenanceMode(EnterpriseHelper.ManagementConsolePassword);

            Assert.NotNull(maintenance);
        }

        [GitHubEnterpriseManagementConsoleTest]
        public async Task CanSetMaintenanceModeOff()
        {
            using (_github.CreateMaintenanceModeContext(true))
            {
                // Set maintenance mode OFF now
                var maintenance = await
                    _github.Enterprise.ManagementConsole.EditMaintenanceMode(
                        new UpdateMaintenanceRequest(),
                        EnterpriseHelper.ManagementConsolePassword);

                Assert.Equal(MaintenanceModeStatus.Off, maintenance.Status);
            }
        }

        [GitHubEnterpriseManagementConsoleTest]
        public async Task CanSetMaintenanceModeOnNow()
        {
            using (_github.CreateMaintenanceModeContext(false))
            {
                // Set maintenance mode ON now
                var maintenance = await
                _github.Enterprise.ManagementConsole.EditMaintenanceMode(
                    new UpdateMaintenanceRequest(
                        new UpdateMaintenanceRequestDetails(true)),
                    EnterpriseHelper.ManagementConsolePassword);

                Assert.Equal(MaintenanceModeStatus.On, maintenance.Status);
            }
        }

        [GitHubEnterpriseManagementConsoleTest]
        public async Task CanScheduleMaintenanceModeOnWithDateTime()
        {
            using (_github.CreateMaintenanceModeContext(false))
            {
                // Schedule maintenance mode ON in 5 minutes
                var scheduledTime = DateTimeOffset.Now.AddMinutes(5);
                var maintenance = await
                    _github.Enterprise.ManagementConsole.EditMaintenanceMode(
                        new UpdateMaintenanceRequest(
                            new UpdateMaintenanceRequestDetails(true, scheduledTime)),
                        EnterpriseHelper.ManagementConsolePassword);

                Assert.Equal(MaintenanceModeStatus.Scheduled, maintenance.Status);
            }
        }

        [GitHubEnterpriseManagementConsoleTest]
        public async Task CanScheduleMaintenanceModeOnWithPhrase()
        {
            using (_github.CreateMaintenanceModeContext(false))
            {
                // Schedule maintenance mode ON with phrase
                var maintenance = await
                _github.Enterprise.ManagementConsole.EditMaintenanceMode(
                    new UpdateMaintenanceRequest(
                        new UpdateMaintenanceRequestDetails(true, "tomorrow at 5pm")),
                    EnterpriseHelper.ManagementConsolePassword);

                Assert.Equal(MaintenanceModeStatus.Scheduled, maintenance.Status);
            }
        }

        private async Task DeleteManagementAuthorizedKeyIfExists(string keyData)
        {
            // Check if key already exists
            var keys = await _github.Enterprise.ManagementConsole.GetAllAuthorizedKeys(
                EnterpriseHelper.ManagementConsolePassword).ToList();

            // Delete it if so
            if (keys.Any(x => x.Key == keyData))
            {
                await _github.Enterprise.ManagementConsole.DeleteAuthorizedKey(
                    new AuthorizedKeyRequest(keyData),
                    EnterpriseHelper.ManagementConsolePassword).ToList();
            }
        }

        [GitHubEnterpriseManagementConsoleTest]
        public async Task CanGetAllAuthorizedKeys()
        {
            var keys = await _github.Enterprise.ManagementConsole.GetAllAuthorizedKeys(
                EnterpriseHelper.ManagementConsolePassword).ToList();

            Assert.NotNull(keys);
            Assert.True(keys.Count > 0);
            Assert.True(keys.All(x => !string.IsNullOrEmpty(x.Key)));
            Assert.True(keys.All(x => !string.IsNullOrEmpty(x.PrettyPrint)));
            Assert.True(keys.All(x => !string.IsNullOrEmpty(x.Comment)));
        }

        [GitHubEnterpriseManagementConsoleTest]
        public async Task CanAddAndDeleteAuthorizedKey()
        {
            var keyData = "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAQEAjo4DqFKg8dOxiz/yjypmN1A4itU5QOStyYrfOFuTinesU/2zm9hqxJ5BctIhgtSHJ5foxkhsiBji0qrUg73Q25BThgNg8YFE8njr4EwjmqSqW13akx/zLV0GFFU0SdJ2F6rBldhi93lMnl0ex9swBqa3eLTY8C+HQGBI6MQUMw+BKp0oFkz87Kv+Pfp6lt/Uo32ejSxML1PT5hTH5n+fyl0ied+sRmPGZWmWoHB5Bc9mox7lB6I6A/ZgjtBqbEEn4HQ2/6vp4ojKfSgA4Mm7XMu0bZzX0itKjH1QWD9Lr5apV1cmZsj49Xf8SHucTtH+bq98hb8OOXEGFzplwsX2MQ== my-test-key";

            // Ensure key doesn't already exist
            await DeleteManagementAuthorizedKeyIfExists(keyData).ConfigureAwait(false);

            // Add key
            var keys = await _github.Enterprise.ManagementConsole.AddAuthorizedKey(
                new AuthorizedKeyRequest(keyData),
                EnterpriseHelper.ManagementConsolePassword).ToList();

            // Ensure key was added
            Assert.NotNull(keys);
            Assert.Contains(keys, x => x.Key == keyData);

            // Now delete it
            var remainingKeys = await _github.Enterprise.ManagementConsole.DeleteAuthorizedKey(
                new AuthorizedKeyRequest(keyData),
                EnterpriseHelper.ManagementConsolePassword).ToList();

            // Ensure key no longer exists
            Assert.DoesNotContain(remainingKeys, x => x.Key == keyData);
        }
    }
}