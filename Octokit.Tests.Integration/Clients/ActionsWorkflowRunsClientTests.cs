using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class ActionsWorkflowRunsClientTests
{
    private const string HelloWorldWorkflow = @"
name: hello
on: [ push, workflow_dispatch ]
jobs:
  world:
    runs-on: [ ubuntu-latest ]
    steps:
      - run: echo ""Hello world.""";

    private const string BrokenWorkflow = @"
name: hello
on: [ push, workflow_dispatch ]
jobs:
  world:
    runs-on: [ ubuntu-latest ]
    steps:
      - run: exit 1";

    [IntegrationTest]
    public async Task CanListWorkflowRuns()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows.Runs;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            (var workflowFileName, _) = await CreateWorkflowAndWaitForFirstRun(github, context);

            var runs = await fixture.List(owner, name);

            Assert.NotNull(runs);
            Assert.NotEqual(0, runs.TotalCount);
            Assert.NotNull(runs.WorkflowRuns);
            Assert.NotEmpty(runs.WorkflowRuns);

            runs = await fixture.List(owner, name, new WorkflowRunsRequest() {  Branch = "main" });

            Assert.NotNull(runs);
            Assert.NotEqual(0, runs.TotalCount);
            Assert.NotNull(runs.WorkflowRuns);
            Assert.NotEmpty(runs.WorkflowRuns);

            runs = await fixture.List(owner, name, new WorkflowRunsRequest() { Branch = "not-main" });

            Assert.NotNull(runs);
            Assert.Equal(0, runs.TotalCount);
            Assert.NotNull(runs.WorkflowRuns);
            Assert.Empty(runs.WorkflowRuns);
        }
    }

    [IntegrationTest]
    public async Task CanGetWorkflowRun()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows.Runs;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            (var workflowFileName, var runId) = await CreateWorkflowAndWaitForFirstRun(github, context);

            var run = await fixture.Get(owner, name, runId);

            Assert.NotNull(run);
            Assert.Equal(runId, run.Id);
        }
    }

    [IntegrationTest]
    public async Task CanDeleteWorkflowRun()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows.Runs;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            (var workflowFileName, var runId) = await CreateWorkflowAndWaitForFirstRun(github, context, WorkflowRunStatus.Completed);

            await fixture.Delete(owner, name, runId);
            await Assert.ThrowsAsync<NotFoundException>(() => fixture.Get(owner, name, runId));
        }
    }

    [IntegrationTest]
    public async Task CanDeleteWorkflowRunLogs()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows.Runs;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            (var workflowFileName, var runId) = await CreateWorkflowAndWaitForFirstRun(github, context, WorkflowRunStatus.Completed);

            await fixture.DeleteLogs(owner, name, runId);
        }
    }

    [IntegrationTest]
    public async Task CanGetWorkflowRunReviewHistory()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows.Runs;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            (var workflowFileName, var runId) = await CreateWorkflowAndWaitForFirstRun(github, context);

            var reviews = await fixture.GetReviewHistory(owner, name, runId);
            Assert.NotNull(reviews);
        }
    }

    [IntegrationTest]
    public async Task CanGetWorkflowRunAttempt()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows.Runs;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            (var workflowFileName, var runId) = await CreateWorkflowAndWaitForFirstRun(github, context);

            var run = await fixture.GetAttempt(owner, name, runId, 1);

            Assert.NotNull(run);
            Assert.Equal(runId, run.Id);
            Assert.Equal(1, run.RunAttempt);
        }
    }

    [IntegrationTest]
    public async Task CanGetWorkflowRunAttemptLogs()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows.Runs;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            (var workflowFileName, var runId) = await CreateWorkflowAndWaitForFirstRun(github, context, WorkflowRunStatus.Completed);

            var logs = await fixture.GetAttemptLogs(owner, name, runId, 1);

            Assert.NotNull(logs);
            Assert.NotEmpty(logs);

            var tempFile = Path.GetTempFileName();

            try
            {
                File.WriteAllBytes(tempFile, logs);

                using (var archive = ZipFile.OpenRead(tempFile))
                {
                    Assert.NotEmpty(archive.Entries);
                }
            }
            finally
            {
                File.Delete(tempFile);
            }
        }
    }

    [IntegrationTest]
    public async Task CanGetWorkflowRunLogs()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows.Runs;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            (var workflowFileName, var runId) = await CreateWorkflowAndWaitForFirstRun(github, context, WorkflowRunStatus.Completed);

            var logs = await fixture.GetLogs(owner, name, runId);

            Assert.NotNull(logs);
            Assert.NotEmpty(logs);

            var tempFile = Path.GetTempFileName();

            try
            {
                File.WriteAllBytes(tempFile, logs);

                using (var archive = ZipFile.OpenRead(tempFile))
                {
                    Assert.NotEmpty(archive.Entries);
                }
            }
            finally
            {
                File.Delete(tempFile);
            }
        }
    }

    [IntegrationTest]
    public async Task CanCancelWorkflowRun()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows.Runs;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            (var workflowFileName, var runId) = await CreateWorkflowAndWaitForFirstRun(github, context);

            await fixture.Cancel(owner, name, runId);
        }
    }

    [IntegrationTest]
    public async Task CanRerunWorkflow()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows.Runs;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            (var workflowFileName, var runId) = await CreateWorkflowAndWaitForFirstRun(github, context, WorkflowRunStatus.Completed);

            await fixture.Rerun(owner, name, runId);
        }
    }

    [IntegrationTest]
    public async Task CanRerunFailedJobs()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows.Runs;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            
            (var workflowFileName, var runId) = await CreateWorkflowAndWaitForFirstRun(
                github,
                context,
                WorkflowRunStatus.Completed,
                BrokenWorkflow);

            await fixture.RerunFailedJobs(owner, name, runId);
        }
    }

    [IntegrationTest]
    public async Task CanGetWorkflowRunUsage()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows.Runs;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;

            (var workflowFileName, var runId) = await CreateWorkflowAndWaitForFirstRun(github, context, WorkflowRunStatus.Completed);

            var usage = await fixture.GetUsage(owner, name, runId);

            Assert.NotNull(usage);
            Assert.NotEqual(0, usage.RunDurationMs);
            Assert.NotNull(usage.Billable);
        }
    }

    [IntegrationTest]
    public async Task CanListWorkflowRunsByWorkflow()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows.Runs;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            (var workflowFileName, _) = await CreateWorkflowAndWaitForFirstRun(github, context);

            var runs = await fixture.ListByWorkflow(owner, name, workflowFileName);

            Assert.NotNull(runs);
            Assert.NotEqual(0, runs.TotalCount);
            Assert.NotNull(runs.WorkflowRuns);
            Assert.NotEmpty(runs.WorkflowRuns);

            runs = await fixture.ListByWorkflow(owner, name, workflowFileName, new WorkflowRunsRequest() { Branch = "main" });

            Assert.NotNull(runs);
            Assert.NotEqual(0, runs.TotalCount);
            Assert.NotNull(runs.WorkflowRuns);
            Assert.NotEmpty(runs.WorkflowRuns);

            runs = await fixture.ListByWorkflow(owner, name, workflowFileName, new WorkflowRunsRequest() { Branch = "not-main" });

            Assert.NotNull(runs);
            Assert.Equal(0, runs.TotalCount);
            Assert.NotNull(runs.WorkflowRuns);
            Assert.Empty(runs.WorkflowRuns);

            var workflowId = await GetWorkflowId(github, context, workflowFileName);

            runs = await fixture.ListByWorkflow(owner, name, workflowId);

            Assert.NotNull(runs);
            Assert.NotEqual(0, runs.TotalCount);
            Assert.NotNull(runs.WorkflowRuns);
            Assert.NotEmpty(runs.WorkflowRuns);

            runs = await fixture.ListByWorkflow(owner, name, workflowId, new WorkflowRunsRequest() { Branch = "main" });

            Assert.NotNull(runs);
            Assert.NotEqual(0, runs.TotalCount);
            Assert.NotNull(runs.WorkflowRuns);
            Assert.NotEmpty(runs.WorkflowRuns);

            runs = await fixture.ListByWorkflow(owner, name, workflowId, new WorkflowRunsRequest() { Branch = "not-main" });

            Assert.NotNull(runs);
            Assert.Equal(0, runs.TotalCount);
            Assert.NotNull(runs.WorkflowRuns);
            Assert.Empty(runs.WorkflowRuns);
        }
    }

    private static async Task<(string WorkflowFileName, long RunId)> CreateWorkflowAndWaitForFirstRun(
        IGitHubClient github,
        RepositoryContext context,
        WorkflowRunStatus? statusToWaitFor = null,
        string workflowFile = HelloWorldWorkflow)
    {
        var owner = context.Repository.Owner.Login;
        var name = context.Repository.Name;
        var workflowFileName = ".github/workflows/hello-world.yml";

        _ = await github.Repository.Content.CreateFile(
            owner,
            name,
            workflowFileName,
            new CreateFileRequest("Create test workflow", workflowFile));

        var totalTimeout = TimeSpan.FromMinutes(1);
        var loopDelay = TimeSpan.FromSeconds(1);

        using (var cts = new CancellationTokenSource(totalTimeout))
        {
            while (!cts.IsCancellationRequested)
            {
                cts.Token.ThrowIfCancellationRequested();

                var runs = await github.Actions.Workflows.Runs.List(owner, name);

                await Task.Delay(loopDelay);

                if (runs.TotalCount > 0 && (statusToWaitFor == null || runs.WorkflowRuns[0].Status == statusToWaitFor))
                {
                    return (workflowFileName, runs.WorkflowRuns[0].Id);
                }
            }
        }

        throw new InvalidOperationException("Timed out waiting for workflow run.");
    }

    private static async Task<long> GetWorkflowId(
        IGitHubClient github,
        RepositoryContext context,
        string workflowFileName)
    {
        var owner = context.Repository.Owner.Login;
        var name = context.Repository.Name;

        var workflow = await github.Actions.Workflows.Get(owner, name, workflowFileName);
        return workflow.Id;
    }
}
