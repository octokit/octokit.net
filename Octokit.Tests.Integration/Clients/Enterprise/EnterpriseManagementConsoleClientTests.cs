using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class EnterpriseManagementConsoleClientTests
{
    readonly IGitHubClient _github;

    public EnterpriseManagementConsoleClientTests()
    {
        _github = EnterpriseHelper.GetAuthenticatedClient();
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

            Assert.Equal(maintenance.Status, MaintenanceModeStatus.Off);
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
                new UpdateMaintenanceRequest(true, MaintenanceDate.Now()),
                EnterpriseHelper.ManagementConsolePassword);

            Assert.Equal(maintenance.Status, MaintenanceModeStatus.On);
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
                    new UpdateMaintenanceRequest(true, MaintenanceDate.FromDateTimeOffset(scheduledTime)),
                    EnterpriseHelper.ManagementConsolePassword);

            Assert.Equal(maintenance.Status, MaintenanceModeStatus.Scheduled);
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
                new UpdateMaintenanceRequest(true, MaintenanceDate.FromChronicValue("tomorrow at 5pm")),
                EnterpriseHelper.ManagementConsolePassword);

            Assert.Equal(maintenance.Status, MaintenanceModeStatus.Scheduled);
        }
    }
}