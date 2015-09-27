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
    private readonly Commit _commit;
    private readonly Deployment _deployment;
    private readonly string _repositoryOwner;

    public DeploymentStatusClientTests()
    {
        var github = Helper.GetAuthenticatedClient();

        _deploymentsClient = github.Repository.Deployment;
        _context = github.CreateRepositoryContext("public-repo").Result;
        _repositoryOwner = _context.Repository.Owner.Login;

        var blob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };

        var blobResult = github.GitDatabase.Blob.Create(_repositoryOwner, _context.Repository.Name, blob).Result;

        var newTree = new NewTree();
        newTree.Tree.Add(new NewTreeItem
        {
            Type = TreeType.Blob,
            Mode = FileMode.File,
            Path = "README.md",
            Sha = blobResult.Sha
        });

        var treeResult = github.GitDatabase.Tree.Create(_repositoryOwner, _context.Repository.Name, newTree).Result;
        var newCommit = new NewCommit("test-commit", treeResult.Sha);
        _commit = github.GitDatabase.Commit.Create(_repositoryOwner, _context.Repository.Name, newCommit).Result;

        var newDeployment = new NewDeployment { Ref = _commit.Sha, AutoMerge = false };
        _deployment = _deploymentsClient.Create(_repositoryOwner, _context.Repository.Name, newDeployment).Result;
    }

    [IntegrationTest]
    public async Task CanCreateDeploymentStatus()
    {
        var newStatus = new NewDeploymentStatus { State = DeploymentState.Success };

        var status = await _deploymentsClient.Status.Create(_repositoryOwner, _context.Repository.Name, _deployment.Id, newStatus);

        Assert.NotNull(status);
        Assert.Equal(DeploymentState.Success, status.State);
    }

    [IntegrationTest]
    public async Task CanReadDeploymentStatuses()
    {
        var newStatus = new NewDeploymentStatus { State = DeploymentState.Success };
        await _deploymentsClient.Status.Create(_repositoryOwner, _context.Repository.Name, _deployment.Id, newStatus);

        var statuses = await _deploymentsClient.Status.GetAll(_repositoryOwner, _context.Repository.Name, _deployment.Id);

        Assert.NotEmpty(statuses);
        Assert.Equal(DeploymentState.Success, statuses[0].State);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

