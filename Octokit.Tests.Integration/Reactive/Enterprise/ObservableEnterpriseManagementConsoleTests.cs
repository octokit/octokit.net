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
    }
}