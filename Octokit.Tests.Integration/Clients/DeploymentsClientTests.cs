using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;
using Octokit.Tests.Integration.Helpers;

public class DeploymentsClientTests : IDisposable
{
    private readonly IDeploymentsClient _deploymentsClient;
    private readonly RepositoryContext _context;
    private readonly Commit _commit;

    public DeploymentsClientTests()
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
        _commit = github.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, newCommit).Result;
    }

    [IntegrationTest]
    public async Task CanCreateDeployment()
    {
        var newDeployment = new NewDeployment(_commit.Sha) { AutoMerge = false };

        var deployment = await _deploymentsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newDeployment);

        Assert.NotNull(deployment);
    }

    [IntegrationTest]
    public async Task CanGetDeployments()
    {
        var newDeployment = new NewDeployment(_commit.Sha) { AutoMerge = false };
        await _deploymentsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newDeployment);

        var deployments = await _deploymentsClient.GetAll(_context.RepositoryOwner, _context.RepositoryName);

        Assert.NotEmpty(deployments);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
