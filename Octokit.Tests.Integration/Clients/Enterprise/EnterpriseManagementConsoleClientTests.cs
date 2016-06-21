using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class EnterpriseManagementConsoleClientTests
{
    readonly IGitHubClient _github;

    public EnterpriseManagementConsoleClientTests()
    {
        _github = EnterpriseHelper.GetAuthenticatedClient();
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

    [GitHubEnterpriseTest]
    public async Task CanGetAllAuthorizedKeys()
    {
        var keys = await
            _github.Enterprise.ManagementConsole.GetAllAuthorizedKeys(
                EnterpriseHelper.ManagementConsolePassword);

        Assert.NotNull(keys);
        Assert.True(keys.Count > 0);
        Assert.True(keys.All(x => !string.IsNullOrEmpty(x.Key)));
        Assert.True(keys.All(x => !string.IsNullOrEmpty(x.PrettyPrint)));
        Assert.True(keys.All(x => !string.IsNullOrEmpty(x.Comment)));
    }

    [GitHubEnterpriseTest]
    public async Task CanAddAuthorizedKey()
    {
        var keyData = "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAQEAjo4DqFKg8dOxiz/yjypmN1A4itU5QOStyYrfOFuTinesU/2zm9hqxJ5BctIhgtSHJ5foxkhsiBji0qrUg73Q25BThgNg8YFE8njr4EwjmqSqW13akx/zLV0GFFU0SdJ2F6rBldhi93lMnl0ex9swBqa3eLTY8C+HQGBI6MQUMw+BKp0oFkz87Kv+Pfp6lt/Uo32ejSxML1PT5hTH5n+fyl0ied+sRmPGZWmWoHB5Bc9mox7lB6I6A/ZgjtBqbEEn4HQ2/6vp4ojKfSgA4Mm7XMu0bZzX0itKjH1QWD9Lr5apV1cmZsj49Xf8SHucTtH+bq98hb8OOXEGFzplwsX2MQ==";

        var keys = await
            _github.Enterprise.ManagementConsole.AddAuthorizedKey(
                keyData,
                EnterpriseHelper.ManagementConsolePassword);

        Assert.NotNull(keys);
        Assert.Contains(keys, x => x.Key == keyData);
    }

    [GitHubEnterpriseTest]
    public async Task CanDeleteAuthorizedKey()
    {
        var keyData = "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAQEAjo4DqFKg8dOxiz/yjypmN1A4itU5QOStyYrfOFuTinesU/2zm9hqxJ5BctIhgtSHJ5foxkhsiBji0qrUg73Q25BThgNg8YFE8njr4EwjmqSqW13akx/zLV0GFFU0SdJ2F6rBldhi93lMnl0ex9swBqa3eLTY8C+HQGBI6MQUMw+BKp0oFkz87Kv+Pfp6lt/Uo32ejSxML1PT5hTH5n+fyl0ied+sRmPGZWmWoHB5Bc9mox7lB6I6A/ZgjtBqbEEn4HQ2/6vp4ojKfSgA4Mm7XMu0bZzX0itKjH1QWD9Lr5apV1cmZsj49Xf8SHucTtH+bq98hb8OOXEGFzplwsX2MQ==";

        await _github.Enterprise.ManagementConsole.DeleteAuthorizedKey(
                keyData,
                EnterpriseHelper.ManagementConsolePassword);

        var keys = await _github.Enterprise.ManagementConsole.GetAllAuthorizedKeys(
                EnterpriseHelper.ManagementConsolePassword);

        Assert.DoesNotContain(keys, x => x.Key == keyData);
    }
}