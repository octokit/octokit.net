using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;
using Octokit.Tests.Integration.Helpers;

public class DeploymentsClientTests : IDisposable
{
    private readonly IDeploymentsClient _deploymentsClient;
    private readonly RepositoryContext _context;
    private readonly ICollection<Commit> _commits;

    public DeploymentsClientTests()
    {
        var github = Helper.GetAuthenticatedClient();

        _deploymentsClient = github.Repository.Deployment;
        _context = github.CreateRepositoryContext("public-repo").Result;
        _commits = new List<Commit>();

        for (int i = 0; i < 6; i++)
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
            _commits.Add(commit);
        }
    }

    [IntegrationTest]
    public async Task CanCreateDeployment()
    {
        var newDeployment = new NewDeployment(_commits.First().Sha) { AutoMerge = false };

        var deployment = await _deploymentsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newDeployment);

        Assert.NotNull(deployment);
    }

    [IntegrationTest]
    public async Task ReturnsDeployments()
    {
        var newDeployment = new NewDeployment(_commits.First().Sha) { AutoMerge = false };
        await _deploymentsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newDeployment);

        var deployments = await _deploymentsClient.GetAll(_context.RepositoryOwner, _context.RepositoryName);

        Assert.NotEmpty(deployments);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfDeploymentsWithoutStart()
    {
        foreach (var commit in _commits)
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
        foreach (var commit in _commits)
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
        foreach (var commit in _commits)
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

    public void Dispose()
    {
        _context.Dispose();
    }
}
