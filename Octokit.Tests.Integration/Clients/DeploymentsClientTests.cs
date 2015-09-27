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
    private readonly string _repositoryOwner;

    public DeploymentsClientTests()
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
    }
  
    [IntegrationTest]
    public async Task CanCreateDeployment()
    {
        var newDeployment = new NewDeployment { Ref = _commit.Sha, AutoMerge = false };

        var deployment = await _deploymentsClient.Create(_repositoryOwner, _context.Repository.Name, newDeployment);

        Assert.NotNull(deployment);
    }

    [IntegrationTest]
    public async Task CanGetDeployments()
    {
        var newDeployment = new NewDeployment { Ref = _commit.Sha, AutoMerge = false };
        await _deploymentsClient.Create(_repositoryOwner, _context.Repository.Name, newDeployment);

        var deployments = await _deploymentsClient.GetAll(_repositoryOwner, _context.Repository.Name);

        Assert.NotEmpty(deployments);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
