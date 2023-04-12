# Working with Self-hosted runner groups

## Create a client

```csharp
var client = new GitHubClient(....); // More on GitHubClient can be found in "Getting Started"
```

## List Runner Groups

### List self-hosted runner groups for an enterprise

```csharp
var runnerGroups = await client.Actions.SelfHostedRunnerGroups.ListAllRunnerGroupsForEnterprise("enterprise");
```

### List self-hosted runner groups for an organization

```csharp
var runnerGroups = await client.Actions.SelfHostedRunnerGroups.ListAllRunnerGroupsForOrganization("octokit");
```

## List Runners in a Runner Group

### List self-hosted runners in a runner group for an enterprise

```csharp
var runners = await client.Actions.SelfHostedRunners.ListAllRunnersForEnterpriseRunnerGroup("enterprise", groupId);
```

### List self-hosted runners in a runner group for an organization

```csharp
var runners = await client.Actions.SelfHostedRunners.ListAllRunnersForOrganizationRunnerGroup("octokit", groupId);
```

## List Runner Group Access

### List organization access to a self-hosted runner group in an enterprise

```csharp
var orgs = await client.Actions.SelfHostedRunnerGroups.ListAllRunnerGroupOrganizationsForEnterprise("enterprise", groupId);
```

### List repository access to a self-hosted runner group in an organization

```csharp
var repos = await client.Actions.SelfHostedRunnerGroups.ListAllRunnerGroupRepositoriesForOrganization("octokit", groupId);
```
