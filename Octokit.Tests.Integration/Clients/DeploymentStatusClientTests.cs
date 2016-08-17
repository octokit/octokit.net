using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;
using Octokit.Tests.Integration.Helpers;

public class DeploymentStatusClientTests : IDisposable
{
    private readonly IDeploymentsClient _deploymentsClient;
    private readonly RepositoryContext _context;
    private readonly Deployment _deployment;

    public DeploymentStatusClientTests()
    {
        var github = Helper.GetAuthenticatedClient();

        _deploymentsClient = github.Repository.Deployment;
        _context = github.CreateRepositoryContext("public-repo").Result;

        var blob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };

        var blobResult = github.Git.Blob.Create(_context.RepositoryOwner, _context.RepositoryName, blob).Result;

        var newTree = new NewTree();
        newTree.Tree.Add(new NewTreeItem
        {
            Type = TreeType.Blob,
            Mode = FileMode.File,
            Path = "README.md",
            Sha = blobResult.Sha
        });

        var treeResult = github.Git.Tree.Create(_context.RepositoryOwner, _context.RepositoryName, newTree).Result;
        var newCommit = new NewCommit("test-commit", treeResult.Sha);

        var commit = github.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, newCommit).Result;

        var newDeployment = new NewDeployment(commit.Sha) { AutoMerge = false };
        _deployment = _deploymentsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newDeployment).Result;
    }

    [IntegrationTest]
    public async Task CanCreateDeploymentStatus()
    {
        var newStatus = new NewDeploymentStatus(DeploymentState.Success);

        var status = await _deploymentsClient.Status.Create(_context.RepositoryOwner, _context.RepositoryName, _deployment.Id, newStatus);

        Assert.NotNull(status);
        Assert.Equal(DeploymentState.Success, status.State);
    }

    [IntegrationTest]
    public async Task CanCreateDeploymentStatusWithRepositoryId()
    {
        var newStatus = new NewDeploymentStatus(DeploymentState.Success);

        var status = await _deploymentsClient.Status.Create(_context.Repository.Id, _deployment.Id, newStatus);

        Assert.NotNull(status);
        Assert.Equal(DeploymentState.Success, status.State);
    }

    [IntegrationTest]
    public async Task CanReadDeploymentStatuses()
    {
        var newStatus = new NewDeploymentStatus(DeploymentState.Success) { LogUrl = "http://test.com/log", EnvironmentUrl = "http:test.com/staging" };
        await _deploymentsClient.Status.Create(_context.RepositoryOwner, _context.RepositoryName, _deployment.Id, newStatus);

        var statuses = await _deploymentsClient.Status.GetAll(_context.RepositoryOwner, _context.RepositoryName, _deployment.Id);

        Assert.NotEmpty(statuses);
        Assert.Equal(DeploymentState.Success, statuses[0].State);
        Assert.Equal(newStatus.LogUrl, statuses[0].LogUrl);
        Assert.Equal(newStatus.EnvironmentUrl, statuses[0].EnvironmentUrl);
    }

    [IntegrationTest]
    public async Task CanReadDeploymentStatusesWithRepositoryId()
    {
        var newStatus = new NewDeploymentStatus(DeploymentState.Success);
        await _deploymentsClient.Status.Create(_context.Repository.Id, _deployment.Id, newStatus);

        var statuses = await _deploymentsClient.Status.GetAll(_context.Repository.Id, _deployment.Id);

        Assert.NotEmpty(statuses);
        Assert.Equal(DeploymentState.Success, statuses[0].State);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfDeploymentStatusesWithoutStart()
    {
        var newStatus1 = new NewDeploymentStatus(DeploymentState.Success);
        var newStatus2 = new NewDeploymentStatus(DeploymentState.Success);
        var newStatus3 = new NewDeploymentStatus(DeploymentState.Success);
        await _deploymentsClient.Status.Create(_context.RepositoryOwner, _context.RepositoryName, _deployment.Id, newStatus1);
        await _deploymentsClient.Status.Create(_context.RepositoryOwner, _context.RepositoryName, _deployment.Id, newStatus2);
        await _deploymentsClient.Status.Create(_context.RepositoryOwner, _context.RepositoryName, _deployment.Id, newStatus3);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var deploymentStatuses = await _deploymentsClient.Status.GetAll(_context.RepositoryOwner, _context.RepositoryName, _deployment.Id, options);

        Assert.Equal(3, deploymentStatuses.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfDeploymentStatusesWithoutStartWithRepositoryId()
    {
        var newStatus1 = new NewDeploymentStatus(DeploymentState.Success);
        var newStatus2 = new NewDeploymentStatus(DeploymentState.Success);
        var newStatus3 = new NewDeploymentStatus(DeploymentState.Success);
        await _deploymentsClient.Status.Create(_context.Repository.Id, _deployment.Id, newStatus1);
        await _deploymentsClient.Status.Create(_context.Repository.Id, _deployment.Id, newStatus2);
        await _deploymentsClient.Status.Create(_context.Repository.Id, _deployment.Id, newStatus3);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var deploymentStatuses = await _deploymentsClient.Status.GetAll(_context.Repository.Id, _deployment.Id, options);

        Assert.Equal(3, deploymentStatuses.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfDeploymentStatusesWithStart()
    {
        var newStatus1 = new NewDeploymentStatus(DeploymentState.Success);
        var newStatus2 = new NewDeploymentStatus(DeploymentState.Success);
        var newStatus3 = new NewDeploymentStatus(DeploymentState.Success);
        await _deploymentsClient.Status.Create(_context.RepositoryOwner, _context.RepositoryName, _deployment.Id, newStatus1);
        await _deploymentsClient.Status.Create(_context.RepositoryOwner, _context.RepositoryName, _deployment.Id, newStatus2);
        await _deploymentsClient.Status.Create(_context.RepositoryOwner, _context.RepositoryName, _deployment.Id, newStatus3);

        var options = new ApiOptions
        {
            PageSize = 2,
            PageCount = 1,
            StartPage = 2
        };

        var deploymentStatuses = await _deploymentsClient.Status.GetAll(_context.RepositoryOwner, _context.RepositoryName, _deployment.Id, options);

        Assert.Equal(1, deploymentStatuses.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfDeploymentStatusesWithStartWithRepositoryId()
    {
        var newStatus1 = new NewDeploymentStatus(DeploymentState.Success);
        var newStatus2 = new NewDeploymentStatus(DeploymentState.Success);
        var newStatus3 = new NewDeploymentStatus(DeploymentState.Success);
        await _deploymentsClient.Status.Create(_context.Repository.Id,_deployment.Id, newStatus1);
        await _deploymentsClient.Status.Create(_context.Repository.Id, _deployment.Id, newStatus2);
        await _deploymentsClient.Status.Create(_context.Repository.Id, _deployment.Id, newStatus3);

        var options = new ApiOptions
        {
            PageSize = 2,
            PageCount = 1,
            StartPage = 2
        };

        var deploymentStatuses = await _deploymentsClient.Status.GetAll(_context.Repository.Id, _deployment.Id, options);

        Assert.Equal(1, deploymentStatuses.Count);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctDeploymentStatusesBasedOnStartPage()
    {
        var newStatus1 = new NewDeploymentStatus(DeploymentState.Success);
        var newStatus2 = new NewDeploymentStatus(DeploymentState.Success);
        var newStatus3 = new NewDeploymentStatus(DeploymentState.Success);
        await _deploymentsClient.Status.Create(_context.RepositoryOwner, _context.RepositoryName, _deployment.Id, newStatus1);
        await _deploymentsClient.Status.Create(_context.RepositoryOwner, _context.RepositoryName, _deployment.Id, newStatus2);
        await _deploymentsClient.Status.Create(_context.RepositoryOwner, _context.RepositoryName, _deployment.Id, newStatus3);

        var startOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1
        };

        var firstPage = await _deploymentsClient.Status.GetAll(_context.RepositoryOwner, _context.RepositoryName, _deployment.Id, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _deploymentsClient.Status.GetAll(_context.RepositoryOwner, _context.RepositoryName, _deployment.Id, skipStartOptions);

        Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctDeploymentStatusesBasedOnStartPageWithRepositoryId()
    {
        var newStatus1 = new NewDeploymentStatus(DeploymentState.Success);
        var newStatus2 = new NewDeploymentStatus(DeploymentState.Success);
        var newStatus3 = new NewDeploymentStatus(DeploymentState.Success);
        await _deploymentsClient.Status.Create(_context.Repository.Id, _deployment.Id, newStatus1);
        await _deploymentsClient.Status.Create(_context.Repository.Id, _deployment.Id, newStatus2);
        await _deploymentsClient.Status.Create(_context.Repository.Id, _deployment.Id, newStatus3);

        var startOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1
        };

        var firstPage = await _deploymentsClient.Status.GetAll(_context.Repository.Id, _deployment.Id, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _deploymentsClient.Status.GetAll(_context.Repository.Id, _deployment.Id, skipStartOptions);

        Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

