using Octokit;
using Octokit.Tests.Integration;
using System;
using System.Threading.Tasks;
using Xunit;

public class DeploymentStatusClientTests : IDisposable
{
    IGitHubClient _gitHubClient;
    IDeploymentsClient _deploymentsClient;
    Repository _repository;
    Commit _commit;
    Deployment _deployment;
    string _repositoryOwner;

    public DeploymentStatusClientTests()
    {
        _gitHubClient = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };

        _deploymentsClient = _gitHubClient.Repository.Deployment;

        var newRepository = new NewRepository
        {
            Name = Helper.MakeNameWithTimestamp("public-repo"),
            AutoInit = true
        };

        _repository = _gitHubClient.Repository.Create(newRepository).Result;
        _repositoryOwner = _repository.Owner.Login;

        var blob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };

        var blobResult = _gitHubClient.GitDatabase.Blob.Create(_repositoryOwner, _repository.Name, blob).Result;

        var newTree = new NewTree();
        newTree.Tree.Add(new NewTreeItem
        {
            Type = TreeType.Blob,
            Mode = FileMode.File,
            Path = "README.md",
            Sha = blobResult.Sha
        });

        var treeResult = _gitHubClient.GitDatabase.Tree.Create(_repositoryOwner, _repository.Name, newTree).Result;
        var newCommit = new NewCommit("test-commit", treeResult.Sha);
        _commit = _gitHubClient.GitDatabase.Commit.Create(_repositoryOwner, _repository.Name, newCommit).Result;

        var newDeployment = new NewDeployment { Ref = _commit.Sha };
        _deployment = _deploymentsClient.Create(_repositoryOwner, _repository.Name, newDeployment).Result;
    }

    [IntegrationTest]
    public async Task CanCreateDeploymentStatus()
    {
        var newStatus = new NewDeploymentStatus { State = DeploymentState.Success };

        var status = await _deploymentsClient.Status.Create(_repositoryOwner, _repository.Name, _deployment.Id, newStatus);

        Assert.NotNull(status);
        Assert.Equal(DeploymentState.Success, status.State);
    }

    [IntegrationTest]
    public async Task CanReadDeploymentStatuses()
    {
        var newStatus = new NewDeploymentStatus { State = DeploymentState.Success };
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

