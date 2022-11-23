using System;
using System.Threading;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class ActionsWorkflowJobsClientTests
{
    private const string HelloWorldWorkflow = @"
name: hello
on: [ push, workflow_dispatch ]
jobs:
  world:
    runs-on: [ ubuntu-latest ]
    steps:
      - run: echo ""Hello world.""";

    [IntegrationTest]
    public async Task CanListWorkflowJobs()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows.Jobs;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            (var workflowFileName, var runId) = await CreateWorkflowAndWaitForCompletion(github, context);

            var jobs = await fixture.List(owner, name, runId);

            Assert.NotNull(jobs);
            Assert.NotEqual(0, jobs.TotalCount);
            Assert.NotNull(jobs.Jobs);
            Assert.NotEmpty(jobs.Jobs);

            jobs = await fixture.List(owner, name, runId, new WorkflowRunJobsRequest() { Filter = WorkflowRunJobsFilter.All });

            Assert.NotNull(jobs);
            Assert.NotEqual(0, jobs.TotalCount);
            Assert.NotNull(jobs.Jobs);
            Assert.NotEmpty(jobs.Jobs);
        }
    }

    [IntegrationTest]
    public async Task CanGetWorkflowJob()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows.Jobs;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            (var workflowFileName, var runId) = await CreateWorkflowAndWaitForCompletion(github, context);

            var jobs = await fixture.List(owner, name, runId);

            Assert.NotNull(jobs);
            Assert.NotNull(jobs.Jobs);
            Assert.Single(jobs.Jobs);

            var job = await fixture.Get(owner, name, jobs.Jobs[0].Id);

            Assert.NotNull(job);
            Assert.Equal(jobs.Jobs[0].Id, job.Id);
        }
    }

    [IntegrationTest]
    public async Task CanListWorkflowJobsForAttempt()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows.Jobs;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            (var workflowFileName, var runId) = await CreateWorkflowAndWaitForCompletion(github, context);
            
            var jobs = await fixture.List(owner, name, runId, 1);

            Assert.NotNull(jobs);
            Assert.NotEqual(0, jobs.TotalCount);
            Assert.NotNull(jobs.Jobs);
            Assert.NotEmpty(jobs.Jobs);
        }
    }

    [IntegrationTest]
    public async Task CanGetWorkflowJobLogs()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows.Jobs;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            (var workflowFileName, var runId) = await CreateWorkflowAndWaitForCompletion(github, context);

            var jobs = await fixture.List(owner, name, runId);

            Assert.NotNull(jobs);
            Assert.NotNull(jobs.Jobs);
            var job = Assert.Single(jobs.Jobs);

            var logs = await fixture.GetLogs(owner, name, job.Id);

            Assert.NotNull(logs);
            Assert.NotEmpty(logs);
        }
    }

    [IntegrationTest]
    public async Task CanRerunJob()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows.Jobs;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            (var workflowFileName, var runId) = await CreateWorkflowAndWaitForCompletion(github, context);

            var jobs = await fixture.List(owner, name, runId);

            Assert.NotNull(jobs);
            Assert.NotNull(jobs.Jobs);
            var job = Assert.Single(jobs.Jobs);

            await fixture.Rerun(owner, name, job.Id);
        }
    }

    private static async Task<(string WorkflowFileName, long RunId)> CreateWorkflowAndWaitForCompletion(
        IGitHubClient github,
        RepositoryContext context)
    {
        var owner = context.Repository.Owner.Login;
        var name = context.Repository.Name;
        var workflowFileName = ".github/workflows/hello-world.yml";

        _ = await github.Repository.Content.CreateFile(
            owner,
            name,
            workflowFileName,
            new CreateFileRequest("Create test workflow", HelloWorldWorkflow));

        var totalTimeout = TimeSpan.FromMinutes(1);
        var loopDelay = TimeSpan.FromSeconds(2);

        using (var cts = new CancellationTokenSource(totalTimeout))
        {
            while (!cts.IsCancellationRequested)
            {
                cts.Token.ThrowIfCancellationRequested();

                var runs = await github.Actions.Workflows.Runs.List(owner, name);

                await Task.Delay(loopDelay);

                if (runs.TotalCount > 0 && runs.WorkflowRuns[0].Status == WorkflowRunStatus.Completed)
                {
                    return (workflowFileName, runs.WorkflowRuns[0].Id);
                }
            }
        }

        throw new InvalidOperationException("Timed out waiting for workflow run.");
    }
}
