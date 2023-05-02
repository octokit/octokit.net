# Working with Self-hosted runners

## Create a client

```csharp
var client = new GitHubClient(....); // More on GitHubClient can be found in "Getting Started"
```

## List Runners

### List self-hosted runners for an enterprise

```csharp
var runners = await client.Actions.SelfHostedRunners.ListAllRunnersForEnterprise("enterprise");
```

### List self-hosted runners for an organization

```csharp
var runners = await client.Actions.SelfHostedRunners.ListAllRunnersForOrganization("octokit");
```

### List self-hosted runners for a repository

```csharp
var runners = await client.Actions.SelfHostedRunners.ListAllRunnersForRepository("octokit", "octokit.net");
```

## List Runner Applications

### List runner applications for an enterprise

```csharp
var runnerApplications = await client.Actions.SelfHostedRunners.ListAllRunnerApplicationsForEnterprise("enterprise");
```

### List runner applications for an organization

```csharp
var runnerApplications = await client.Actions.SelfHostedRunners.ListAllRunnerApplicationsForOrganization("octokit");
```

### List runner applications for a repository

```csharp
var runnerApplications = await client.Actions.SelfHostedRunners.ListAllRunnerApplicationsForRepository("octokit", "octokit.net");
```

## Create Registration Tokens

### Create a registration token for an enterprise

```csharp
var token = await client.Actions.SelfHostedRunners.CreateEnterpriseRegistrationToken("enterprise");
```

### Create a registration token for an organization

```csharp
var token = await client.Actions.SelfHostedRunners.CreateOrganizationRegistrationToken("octokit");
```

### Create a registration token for a repository

```csharp
var token = await client.Actions.SelfHostedRunners.CreateRepositoryRegistrationToken("octokit", "octokit.net");
```

## Delete

### Delete a self-hosted runner from an enterprise

```csharp
await client.Actions.SelfHostedRunners.DeleteEnterpriseRunner("enterprise", runnerId);
```

### Delete a self-hosted runner from an organization

```csharp
await client.Actions.SelfHostedRunners.DeleteOrganizationRunner("octokit", runnerId);
```

### Delete a self-hosted runner from a repository

```csharp
await client.Actions.SelfHostedRunners.DeleteRepositoryRunner("octokit", "octokit.net", runnerId);
```
