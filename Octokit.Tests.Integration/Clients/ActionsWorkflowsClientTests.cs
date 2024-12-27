using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class ActionsWorkflowsClientTests
{
    private static readonly string HelloWorldWorkflow = @"
name: hello
on: [ push, workflow_dispatch ]
jobs:
  world:
    runs-on: [ ubuntu-latest ]
    steps:
      - run: echo ""Hello world.""";

    [IntegrationTest]
    public async Task CanGetWorkflow()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            var workflowFileName = await CreateWorkflow(github, context);

            var workflowByName = await fixture.Get(owner, name, workflowFileName);
            Assert.NotNull(workflowByName);
            Assert.Equal(workflowFileName, workflowByName.Path);

            var workflowById = await fixture.Get(owner, name, workflowByName.Id);
            Assert.NotNull(workflowById);
            Assert.Equal(workflowByName.Id, workflowById.Id);
        }
    }

    [IntegrationTest]
    public async Task CanGetWorkflowUsage()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            var workflowFileName = await CreateWorkflow(github, context);

            var usage = await fixture.GetUsage(owner, name, workflowFileName);
            Assert.NotNull(usage);
            Assert.NotNull(usage.Billable);

            var workflowId = await GetWorkflowId(github, context, workflowFileName);

            usage = await fixture.GetUsage(owner, name, workflowId);
            Assert.NotNull(usage);
            Assert.NotNull(usage.Billable);
        }
    }

    [IntegrationTest]
    public async Task CanListWorkflows()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;

            var workflows = await fixture.List(owner, name);

            Assert.NotNull(workflows);
            Assert.Equal(0, workflows.TotalCount);
            Assert.NotNull(workflows.Workflows);
            Assert.Empty(workflows.Workflows);

            var workflowFileName = await CreateWorkflow(github, context);

            workflows = await fixture.List(owner, name);

            Assert.NotNull(workflows);
            Assert.Equal(1, workflows.TotalCount);
            Assert.NotNull(workflows.Workflows);

            var workflow = Assert.Single(workflows.Workflows);

            Assert.NotNull(workflow);
            Assert.Equal(workflowFileName, workflow.Path);
        }
    }

    [IntegrationTest]
    public async Task CanDispatchWorkflow()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;
            var repoId = context.Repository.Id;
            var workflowFileName = await CreateWorkflow(github, context);
            var reference = "main";

            await fixture.CreateDispatch(owner, name, workflowFileName, new CreateWorkflowDispatch(reference));
            await fixture.CreateDispatch(repoId, workflowFileName, new CreateWorkflowDispatch(reference));

            var workflowId = await GetWorkflowId(github, context, workflowFileName);

            await fixture.CreateDispatch(owner, name, workflowId, new CreateWorkflowDispatch(reference));
            await fixture.CreateDispatch(repoId, workflowId, new CreateWorkflowDispatch(reference));
        }
    }

    [IntegrationTest]
    public async Task CanEnableAndDisableWorkflow()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Actions.Workflows;

        using (var context = await github.CreateRepositoryContextWithAutoInit())
        {
            var owner = context.Repository.Owner.Login;
            var name = context.Repository.Name;

            var workflowFileName = await CreateWorkflow(github, context);
            var workflowId = await AssertWorkflowState(github, context, workflowFileName, "active");

            await fixture.Disable(owner, name, workflowId);

            await AssertWorkflowState(github, context, workflowFileName, "disabled_manually");

            await fixture.Enable(owner, name, workflowId);

            await AssertWorkflowState(github, context, workflowFileName, "active");

            await fixture.Disable(owner, name, workflowFileName);

            await AssertWorkflowState(github, context, workflowFileName, "disabled_manually");

            await fixture.Enable(owner, name, workflowFileName);

            await AssertWorkflowState(github, context, workflowFileName, "active");
        }
    }

    private static async Task<string> CreateWorkflow(IGitHubClient github, RepositoryContext context)
    {
        var owner = context.Repository.Owner.Login;
        var name = context.Repository.Name;
        var workflowFileName = ".github/workflows/hello-world.yml";

        _ = await github.Repository.Content.CreateFile(
            owner,
            name,
            workflowFileName,
            new CreateFileRequest("Create test workflow", HelloWorldWorkflow));

        await Task.Delay(TimeSpan.FromSeconds(1.5));

        return workflowFileName;
    }

    private static async Task<long> AssertWorkflowState(
        IGitHubClient github,
        RepositoryContext context,
        string workflowFileName,
        string expected)
    {
        var owner = context.Repository.Owner.Login;
        var name = context.Repository.Name;

        var workflow = await github.Actions.Workflows.Get(owner, name, workflowFileName);
        Assert.NotNull(workflow);
        Assert.Equal(expected, workflow.State);

        return workflow.Id;
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
