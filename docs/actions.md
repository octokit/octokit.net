# Workflow runs

Allow you to view workflow runs. A workflow run is an instance of your workflow that runs when the pre-configured event occurs.

### List for a repository

If you want to lists all workflow runs for a repository you belong to
use this methods:

```csharp
var workflowRuns = await client.Action.Run.GetAllForRepository("octokit", "octokit.net");
```

```csharp
var workflowRuns = await client.Action.Run.GetAllForRepository(7528679);
```

### Filtering

Each of these methods has an overload which takes a parameter to filter results.

The simplest request is `WorkflowRunsRequest` which has these options:

 - `Actor` - returns someone's workflow runs. Use the login for the user who created the push associated with the check suite or workflow run.
 - `Branch` - returns workflow runs associated with a branch. Use the name of the branch of the push.
 - `Event` - returns workflow run triggered by the event you specify. For example, push, pull_request or issue.
 - `Status` - returns workflow runs associated with the check run status or conclusion you specify. For example, a conclusion can be success or a status can be completed.

For example, this is how you could find all the completed runs:

```csharp
var request = new WorkflowRunsRequest
{
    Status = "completed"
};
var workflowRuns = await client.Action.Run.GetAllForRepository("octokit", "octokit.net", request);
```
