using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class TreeClientTests : IDisposable
{
    private readonly IGitHubClient _github;
    private readonly ITreesClient _fixture;
    private readonly RepositoryContext _context;

    public TreeClientTests()
    {
        _github = Helper.GetAuthenticatedClient();

        _fixture = _github.Git.Tree;

        _context = _github.CreateRepositoryContextWithAutoInit("public-repo").Result;
    }

    [IntegrationTest]
    public async Task CanCreateATree()
    {
        var blob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };

        var createdBlob = await _github.Git.Blob.Create(_context.RepositoryOwner, _context.RepositoryName, blob);

        var newTree = new NewTree();
        newTree.Tree.Add(new NewTreeItem
        {
            Type = TreeType.Blob,
            Path = "README.md",
            Sha = createdBlob.Sha,
            Mode = FileMode.File
        });

        var result = await _fixture.Create(_context.RepositoryOwner, _context.RepositoryName, newTree);

        Assert.NotNull(result);
    }

    [IntegrationTest]
    public async Task CanCreateATreeWithRepositoryId()
    {
        var blob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };

        var createdBlob = await _github.Git.Blob.Create(_context.RepositoryOwner, _context.RepositoryName, blob);

        var newTree = new NewTree();
        newTree.Tree.Add(new NewTreeItem
        {
            Type = TreeType.Blob,
            Path = "README.md",
            Sha = createdBlob.Sha,
            Mode = FileMode.File
        });

        var result = await _fixture.Create(_context.Repository.Id, newTree);

        Assert.NotNull(result);
    }

    [IntegrationTest]
    public async Task CanGetATree()
    {
        var result = await _fixture.Get("octokit", "octokit.net", "main");

        Assert.NotNull(result);
        Assert.NotEmpty(result.Tree);
    }

    [IntegrationTest]
    public async Task CanGetATreeWithRepositoryId()
    {
        var result = await _fixture.Get(1, "main");

        Assert.NotNull(result);
        Assert.NotEmpty(result.Tree);
    }

    [IntegrationTest]
    public async Task CanGetACreatedTree()
    {
        var blob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };

        var blobResult = await _github.Git.Blob.Create(_context.RepositoryOwner, _context.RepositoryName, blob);

        var newTree = new NewTree();
        newTree.Tree.Add(new NewTreeItem
        {
            Type = TreeType.Blob,
            Path = "README.md",
            Sha = blobResult.Sha,
            Mode = FileMode.File
        });

        var tree = await _fixture.Create(_context.RepositoryOwner, _context.RepositoryName, newTree);

        var result = await _fixture.Get(_context.RepositoryOwner, _context.RepositoryName, tree.Sha);

        Assert.NotNull(result);
        Assert.Single(result.Tree);
    }

    [IntegrationTest]
    public async Task CanGetACreatedTreeWithRepositoryId()
    {
        var blob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };

        var blobResult = await _github.Git.Blob.Create(_context.RepositoryOwner, _context.RepositoryName, blob);

        var newTree = new NewTree();
        newTree.Tree.Add(new NewTreeItem
        {
            Type = TreeType.Blob,
            Path = "README.md",
            Sha = blobResult.Sha,
            Mode = FileMode.File
        });

        var tree = await _fixture.Create(_context.Repository.Id, newTree);

        var result = await _fixture.Get(_context.Repository.Id, tree.Sha);

        Assert.NotNull(result);
        Assert.Single(result.Tree);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
