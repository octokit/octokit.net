using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class DeploymentStatusClientTests : IDisposable
{
    readonly IDeploymentsClient _deploymentsClient;
    readonly Repository _repository;
    readonly Deployment _deployment;
    readonly string _repositoryOwner;

    public DeploymentStatusClientTests()
    {
        var gitHubClient = Helper.GetAuthenticatedClient();
        _deploymentsClient = gitHubClient.Repository.Deployment;

        var newRepository = new NewRepository(Helper.MakeNameWithTimestamp("public-repo"))
        {
            AutoInit = true
        };

        _repository = gitHubClient.Repository.Create(newRepository).Result;
        _repositoryOwner = _repository.Owner.Login;

        var blob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };

        var blobResult = gitHubClient.GitDatabase.Blob.Create(_repositoryOwner, _repository.Name, blob).Result;

        var newTree = new NewTree();
        newTree.Tree.Add(new NewTreeItem
        {
            Type = TreeType.Blob,
            Mode = FileMode.File,
            Path = "README.md",
            Sha = blobResult.Sha
        });

        var treeResult = gitHubClient.GitDatabase.Tree.Create(_repositoryOwner, _repository.Name, newTree).Result;
        var newCommit = new NewCommit("test-commit", treeResult.Sha);
        var commit = gitHubClient.GitDatabase.Commit.Create(_repositoryOwner, _repository.Name, newCommit).Result;

        var newDeployment = new NewDeployment(commit.Sha) { AutoMerge = false };
        _deployment = _deploymentsClient.Create(_repositoryOwner, _repository.Name, newDeployment).Result;
    }

    [IntegrationTest]
    public async Task CanCreateDeploymentStatus()
    {
        var newStatus = new NewDeploymentStatus(DeploymentState.Success);

        var status = await _deploymentsClient.Status.Create(_repositoryOwner, _repository.Name, _deployment.Id, newStatus);

        Assert.NotNull(status);
        Assert.Equal(DeploymentState.Success, status.State);
    }

    [IntegrationTest]
    public async Task CanReadDeploymentStatuses()
    {
        var newStatus = new NewDeploymentStatus(DeploymentState.Success);
        await _deploymentsClient.Status.Create(_repositoryOwner, _repository.Name, _deployment.Id, newStatus);

        var statuses = await _deploymentsClient.Status.GetAll(_repositoryOwner, _repository.Name, _deployment.Id);

        Assert.NotEmpty(statuses);
        Assert.Equal(DeploymentState.Success, statuses[0].State);
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}

