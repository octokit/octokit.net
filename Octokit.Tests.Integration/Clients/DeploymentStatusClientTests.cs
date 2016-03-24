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
    public async Task CanReadDeploymentStatuses()
    {
        var newStatus = new NewDeploymentStatus(DeploymentState.Success);
        await _deploymentsClient.Status.Create(_context.RepositoryOwner, _context.RepositoryName, _deployment.Id, newStatus);

        var statuses = await _deploymentsClient.Status.GetAll(_context.RepositoryOwner, _context.RepositoryName, _deployment.Id);

        Assert.NotEmpty(statuses);
        Assert.Equal(DeploymentState.Success, statuses[0].State);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

