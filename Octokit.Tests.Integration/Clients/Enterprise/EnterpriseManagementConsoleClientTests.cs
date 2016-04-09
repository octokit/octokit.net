using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class EnterpriseManagementConsoleClientTests
{
    readonly IGitHubClient _github;

    public EnterpriseManagementConsoleClientTests()
    {
        _github = EnterpriseHelper.GetAuthenticatedManagementConsoleClient();
    }

    [GitHubEnterpriseTest]
    public async Task CanGetMaintenanceMode()
    {
        var maintenance = await _github.Enterprise.ManagementConsole.GetMaintenanceMode("Password01");

        Assert.NotNull(maintenance);
    }

    [GitHubEnterpriseTest]
    public async Task CanSetMaintenanceModeOff()
    {
        // Set maintenance mode OFF now
        var maintenance = await
            _github.Enterprise.ManagementConsole.EditMaintenanceMode(
                new UpdateMaintenanceRequest(),
                EnterpriseHelper.ManagementConsolePassword);

        Assert.NotNull(maintenance);
        Assert.Equal(maintenance.Status, MaintenanceModeStatus.Off);
    }

    [GitHubEnterpriseTest]
    public async Task CanSetMaintenanceModeOnNow()
    {
        // Set maintenance mode ON now
        var maintenance = await
            _github.Enterprise.ManagementConsole.EditMaintenanceMode(
                new UpdateMaintenanceRequest(true, MaintenanceDate.Now()),
                EnterpriseHelper.ManagementConsolePassword);

        Assert.NotNull(maintenance);
        Assert.Equal(maintenance.Status, MaintenanceModeStatus.On);

        // Ensure maintenance mode is OFF
        await _github.Enterprise.ManagementConsole.EditMaintenanceMode(
            new UpdateMaintenanceRequest(),
            EnterpriseHelper.ManagementConsolePassword);
    }

    [GitHubEnterpriseTest]
    public async Task CanScheduleMaintenanceModeOnWithDateTime()
    {
        // Schedule maintenance mode ON in 5 minutes
        var scheduledTime = DateTimeOffset.Now.AddMinutes(5);
        var maintenance = await 
            _github.Enterprise.ManagementConsole.EditMaintenanceMode(
                new UpdateMaintenanceRequest(true, MaintenanceDate.FromDateTimeOffset(scheduledTime)),
                EnterpriseHelper.ManagementConsolePassword);

        Assert.NotNull(maintenance);
        Assert.Equal(maintenance.Status, MaintenanceModeStatus.Scheduled);

        // Ensure maintenance mode is OFF
        await _github.Enterprise.ManagementConsole.EditMaintenanceMode(
            new UpdateMaintenanceRequest(),
            EnterpriseHelper.ManagementConsolePassword);
    }

    [GitHubEnterpriseTest]
    public async Task CanScheduleMaintenanceModeOnWithPhrase()
    {
        // Schedule maintenance mode ON
        var maintenance = await
            _github.Enterprise.ManagementConsole.EditMaintenanceMode(
                new UpdateMaintenanceRequest(true, MaintenanceDate.FromChronicValue("tomorrow at 5pm")),
                EnterpriseHelper.ManagementConsolePassword);

        Assert.NotNull(maintenance);
        Assert.Equal(maintenance.Status, MaintenanceModeStatus.Scheduled);

        // Ensure maintenance mode is OFF
        await _github.Enterprise.ManagementConsole.EditMaintenanceMode(
            new UpdateMaintenanceRequest(),
            EnterpriseHelper.ManagementConsolePassword);
    }
}