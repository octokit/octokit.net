# GitHub Enterprise Administration

Octokit also ships with support for the administration APIs that GitHub
Enterprise includes to script tasks for administrators.

```c#
var enterprise = new Uri("https://github.myenterprise.com/");
var github = new GitHubClient("some app name", enterprise);
github.Credentials = new Credentials("some-token-here");

var stats = await github.Enterprise.AdminStats.GetStatisticsUsers();
Console.WriteLine($"Found {stats.AdminUsers} admins, {stats.TotalUsers} total users and {stats.SuspendedUsers} suspended users");
```

Some caveats on using these APIs that you should be aware of:

 - only administrators of the GitHub Enterprise instance are able to access
   these APIs
 - administrators creating OAuth tokens to use this endpoint must ensure the
   `site_admin` scope is set
 - the [Management Console API](https://developer.github.com/enterprise/2.18/v3/enterprise/management_console/)
   also require providing the password created during setup of the GitHub
   Enterprise installation to confirm the action

You can read more about this support [on the GitHub website](https://developer.github.com/enterprise/2.18/v3/enterprise-admin/).


## Management console

To view the maintenance mode status of a GitHub Enteprise installation:

```C#
var maintenance = await github.Enterprise.ManagementConsole.GetMaintenanceMode("management-console-password");
```

To put the GitHub Enterprise installation into maintenance mode immediately:

```C#
var request = new UpdateMaintenanceRequest(new UpdateMaintenanceRequestDetails(true));
var maintenance = await github.Enterprise.ManagementConsole.EditMaintenanceMode(
  request,
  "management-console-password");
```

You can also provide a human-friendly phrase based on the rules in
[`mojombo/chronic`](https://github.com/mojombo/chronic):

```C#
var request = new UpdateMaintenanceRequest(new UpdateMaintenanceRequestDetails(true, "tomorrow at 5pm"));
var maintenance = await github.Enterprise.ManagementConsole.EditMaintenanceMode(
  request,
  "management-console-password");
```

To put the GitHub Enterprise installation into maintenance mode after a period
of time:

```C#
var scheduledTime = DateTimeOffset.Now.AddMinutes(10);
var request = new UpdateMaintenanceRequest(new UpdateMaintenanceRequestDetails(true, scheduledTime));
var maintenance = await github.Enterprise.ManagementConsole.EditMaintenanceMode(
  request,
  "management-console-password");
```

To disable maintenance mode, simple pass in `false` or leave the `request`
constructor empty:

```c#
var maintenance = await github.Enterprise.ManagementConsole.EditMaintenanceMode(
  new UpdateMaintenanceRequest(), // off by default if nothing specified
  "management-console-password");
```
