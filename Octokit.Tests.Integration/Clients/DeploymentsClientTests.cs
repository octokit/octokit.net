using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class DeploymentsClientTests : IDisposable
{
    private readonly IDeploymentsClient _deploymentsClient;
    private readonly RepositoryContext _context;
    private readonly Commit _commit;

    public DeploymentsClientTests()
    {
        var github = Helper.GetAuthenticatedClient();

        _deploymentsClient = github.Repository.Deployment;
        _context = github.CreateRepositoryContextWithAutoInit("public-repo").Result;

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

    private IEnumerable<Commit> CreateCommits(int commitCount)
    {
        var github = Helper.GetAuthenticatedClient();

        var list = new List<Commit>();

        for (int i = 0; i < commitCount; i++)
        {
            var blob = new NewBlob
            {
                Content = string.Format("Hello World {0}!", i),
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
            list.Add(commit);
        }

        return list;
    }

    [IntegrationTest]
    public async Task CanCreateDeployment()
    {
        var newDeployment = new NewDeployment(_commit.Sha) { AutoMerge = false, TransientEnvironment = true, ProductionEnvironment = true };

        var deployment = await _deploymentsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newDeployment);

        Assert.NotNull(deployment);
        Assert.Equal(newDeployment.TransientEnvironment, deployment.TransientEnvironment);
        Assert.Equal(newDeployment.ProductionEnvironment, deployment.ProductionEnvironment);
    }

    [IntegrationTest]
    public async Task CanCreateDeploymentWithRepositoryId()
    {
        var newDeployment = new NewDeployment(_commit.Sha) { AutoMerge = false };

        var deployment = await _deploymentsClient.Create(_context.Repository.Id, newDeployment);

        Assert.NotNull(deployment);
    }

    [IntegrationTest]
    public async Task ReturnsDeployments()
    {
        var newDeployment = new NewDeployment(_commit.Sha) { AutoMerge = false };
        await _deploymentsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newDeployment);

        var deployments = await _deploymentsClient.GetAll(_context.RepositoryOwner, _context.RepositoryName);

        Assert.NotEmpty(deployments);
    }

    [IntegrationTest]
    public async Task ReturnsDeploymentsWithRepositoryId()
    {
        var newDeployment = new NewDeployment(_commit.Sha) { AutoMerge = false };
        await _deploymentsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newDeployment);

        var deployments = await _deploymentsClient.GetAll(_context.Repository.Id);

        Assert.NotEmpty(deployments);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfDeploymentsWithoutStart()
    {
        var commits = CreateCommits(6);
        foreach (var commit in commits)
        {
            var newDeployment = new NewDeployment(commit.Sha) { AutoMerge = false };
            await _deploymentsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newDeployment);
        }

        var options = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1
        };

        var releases = await _deploymentsClient.GetAll(_context.RepositoryOwner, _context.RepositoryName, options);

        Assert.Equal(5, releases.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfDeploymentsWithStart()
    {
        var commits = CreateCommits(6);
        foreach (var commit in commits)
        {
            var newDeployment = new NewDeployment(commit.Sha) { AutoMerge = false };
            await _deploymentsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newDeployment);
        }

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1,
            StartPage = 2
        };

        var releases = await _deploymentsClient.GetAll(_context.RepositoryOwner, _context.RepositoryName, options);

        Assert.Equal(3, releases.Count);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctResultsBasedOnStartPage()
    {
        var commits = CreateCommits(6);
        foreach (var commit in commits)
        {
            var newDeployment = new NewDeployment(commit.Sha) { AutoMerge = false };
            await _deploymentsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newDeployment);
        }

        var startOptions = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var firstPage = await _deploymentsClient.GetAll(_context.RepositoryOwner, _context.RepositoryName, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _deploymentsClient.GetAll(_context.RepositoryOwner, _context.RepositoryName, skipStartOptions);

        Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
        Assert.NotEqual(firstPage[1].Id, secondPage[1].Id);
        Assert.NotEqual(firstPage[2].Id, secondPage[2].Id);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctResultsBasedOnStartPageWithRepositoryId()
    {
        var commits = CreateCommits(6);
        foreach (var commit in commits)
        {
            var newDeployment = new NewDeployment(commit.Sha) { AutoMerge = false };
            await _deploymentsClient.Create(_context.Repository.Id, newDeployment);
        }

        var startOptions = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var firstPage = await _deploymentsClient.GetAll(_context.Repository.Id, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _deploymentsClient.GetAll(_context.Repository.Id, skipStartOptions);

        Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
        Assert.NotEqual(firstPage[1].Id, secondPage[1].Id);
        Assert.NotEqual(firstPage[2].Id, secondPage[2].Id);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
