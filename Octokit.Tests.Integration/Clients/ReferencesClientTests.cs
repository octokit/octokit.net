using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;
using Octokit.Tests.Integration.Helpers;

public class ReferencesClientTests : IDisposable
{
    private readonly IGitHubClient _github;
    private readonly IReferencesClient _fixture;
    private readonly RepositoryContext _context;

    public ReferencesClientTests()
    {
        _github = Helper.GetAuthenticatedClient();

        _fixture = _github.Git.Reference;

        _context = _github.CreateRepositoryContext("public-repo").Result;
    }

    [IntegrationTest]
    public async Task CanGetAReference()
    {
        var @ref = await _fixture.Get("octokit", "octokit.net", "heads/master");

        // validate the top-level properties
        Assert.Equal("refs/heads/master", @ref.Ref);
        Assert.Equal("https://api.github.com/repos/octokit/octokit.net/git/refs/heads/master", @ref.Url);

        // validate the git reference
        Assert.Equal(TaggedType.Commit, @ref.Object.Type);
        Assert.False(string.IsNullOrWhiteSpace(@ref.Object.Sha));
    }

    [IntegrationTest]
    public async Task CanGetAReferenceWithRepositoryId()
    {
        var @ref = await _fixture.Get(7528679, "heads/master");

        // validate the top-level properties
        Assert.Equal("refs/heads/master", @ref.Ref);
        Assert.Equal("https://api.github.com/repos/octokit/octokit.net/git/refs/heads/master", @ref.Url);

        // validate the git reference
        Assert.Equal(TaggedType.Commit, @ref.Object.Type);
        Assert.False(string.IsNullOrWhiteSpace(@ref.Object.Sha));
    }

    [IntegrationTest]
    public async Task WhenReferenceDoesNotExistAnExceptionIsThrown()
    {
        await Assert.ThrowsAsync<NotFoundException>(
            () => _fixture.Get("octokit", "octokit.net", "heads/foofooblahblah"));
    }

    [IntegrationTest(Skip = "This is paging for a long long time")]
    public async Task CanGetListOfReferences()
    {
        var list = await _fixture.GetAll("octokit", "octokit.net");
        Assert.NotEmpty(list);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfReferencesWithStart()
    {
        var options = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1,
            StartPage = 2
        };

        var references = await _fixture.GetAll("octokit", "octokit.net", options);

        Assert.Equal(5, references.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfReferencesWithoutStart()
    {
        var options = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1
        };

        var references = await _fixture.GetAll("octokit", "octokit.net", options);

        Assert.Equal(5, references.Count);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctReferencesBasedOnStartPage()
    {
        var startOptions = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1
        };

        var skipStartOptions = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1,
            StartPage = 2
        };

        var firstRefsPage = await _fixture.GetAll("octokit", "octokit.net", startOptions);
        var secondRefsPage = await _fixture.GetAll("octokit", "octokit.net", skipStartOptions);

        Assert.False(firstRefsPage.Any(x => secondRefsPage.Contains(x)));
    }

    [IntegrationTest(Skip = "This is paging for a long long time")]
    public async Task CanGetListOfReferencesWithRepositoryId()
    {
        var list = await _fixture.GetAll(7528679);
        Assert.NotEmpty(list);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfReferencesWithRepositoryIdWithStart()
    {
        var options = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1,
            StartPage = 2
        };

        var references = await _fixture.GetAll(7528679, options);

        Assert.Equal(5, references.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfReferencesWithRepositoryIdWithoutStart()
    {
        var options = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1
        };

        var references = await _fixture.GetAll(7528679, options);

        Assert.Equal(5, references.Count);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctReferencesWithRepositoryIdBasedOnStartPage()
    {
        var startOptions = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1
        };

        var skipStartOptions = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1,
            StartPage = 2
        };

        var firstRefsPage = await _fixture.GetAll(7528679, startOptions);
        var secondRefsPage = await _fixture.GetAll(7528679, skipStartOptions);

        Assert.False(firstRefsPage.Any(x => secondRefsPage.Contains(x)));
    }

    [IntegrationTest]
    public async Task CanGetListOfReferencesInNamespace()
    {
        var list = await _fixture.GetAllForSubNamespace("octokit", "octokit.net", "heads");
        Assert.NotEmpty(list);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfReferencesInNamespaceWithStart()
    {
        var options = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1,
            StartPage = 2
        };

        var references = await _fixture.GetAllForSubNamespace("octokit", "octokit.net", "heads", options);

        Assert.Equal(5, references.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfReferencesInNamespaceWithoutStart()
    {
        var options = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1
        };

        var references = await _fixture.GetAllForSubNamespace("octokit", "octokit.net", "heads", options);

        Assert.Equal(5, references.Count);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctReferencesInNamespaceBasedOnStartPage()
    {
        var startOptions = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1
        };

        var skipStartOptions = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1,
            StartPage = 2
        };

        var firstRefsPage = await _fixture.GetAllForSubNamespace("octokit", "octokit.net", "heads", startOptions);
        var secondRefsPage = await _fixture.GetAllForSubNamespace("octokit", "octokit.net", "heads", skipStartOptions);

        Assert.False(firstRefsPage.Any(x => secondRefsPage.Contains(x)));
    }

    [IntegrationTest]
    public async Task CanGetListOfReferencesInNamespaceWithRepositoryId()
    {
        var list = await _fixture.GetAllForSubNamespace(7528679, "heads");
        Assert.NotEmpty(list);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfReferencesInNamespaceWithRepositoryIdWithStart()
    {
        var options = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1,
            StartPage = 2
        };

        var references = await _fixture.GetAllForSubNamespace(7528679, "heads", options);

        Assert.Equal(5, references.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfReferencesInNamespaceWithRepositoryIdWithoutStart()
    {
        var options = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1
        };

        var references = await _fixture.GetAllForSubNamespace(7528679, "heads", options);

        Assert.Equal(5, references.Count);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctReferencesInNamespaceWithRepositoryIdBasedOnStartPage()
    {
        var startOptions = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1
        };

        var skipStartOptions = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1,
            StartPage = 2
        };

        var firstRefsPage = await _fixture.GetAllForSubNamespace(7528679, "heads", startOptions);
        var secondRefsPage = await _fixture.GetAllForSubNamespace(7528679, "heads", skipStartOptions);

        Assert.False(firstRefsPage.Any(x => secondRefsPage.Contains(x)));
    }

    [IntegrationTest]
    public async Task CanGetErrorForInvalidNamespace()
    {
        var owner = "octokit";
        var repo = "octokit.net";
        var subNamespace = "666";

        var result = await Assert.ThrowsAsync<NotFoundException>(
            async () => { await _fixture.GetAllForSubNamespace(owner, repo, subNamespace); });
        Assert.Equal(string.Format("{0} was not found.", ApiUrls.Reference(owner, repo, subNamespace)), result.Message);
    }

    [IntegrationTest]
    public async Task CanCreateAReference()
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
            Mode = FileMode.File,
            Type = TreeType.Blob,
            Path = "README.md",
            Sha = blobResult.Sha
        });

        var treeResult = await _github.Git.Tree.Create(_context.RepositoryOwner, _context.RepositoryName, newTree);

        var newCommit = new NewCommit("This is a new commit", treeResult.Sha);

        var commitResult = await _github.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, newCommit);

        var newReference = new NewReference("heads/develop", commitResult.Sha);
        var result = await _fixture.Create(_context.RepositoryOwner, _context.RepositoryName, newReference);

        Assert.Equal(commitResult.Sha, result.Object.Sha);
    }

    [IntegrationTest]
    public async Task CanCreateAReferenceWithRepositoryId()
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
            Mode = FileMode.File,
            Type = TreeType.Blob,
            Path = "README.md",
            Sha = blobResult.Sha
        });

        var treeResult = await _github.Git.Tree.Create(_context.RepositoryOwner, _context.RepositoryName, newTree);

        var newCommit = new NewCommit("This is a new commit", treeResult.Sha);

        var commitResult = await _github.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, newCommit);

        var newReference = new NewReference("heads/develop", commitResult.Sha);
        var result = await _fixture.Create(_context.Repository.Id, newReference);

        Assert.Equal(commitResult.Sha, result.Object.Sha);
    }

    [IntegrationTest]
    public async Task CanUpdateAReference()
    {
        var firstBlob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };
        var firstBlobResult = await _github.Git.Blob.Create(_context.RepositoryOwner, _context.RepositoryName, firstBlob);
        var secondBlob = new NewBlob
        {
            Content = "This is a test!",
            Encoding = EncodingType.Utf8
        };
        var secondBlobResult = await _github.Git.Blob.Create(_context.RepositoryOwner, _context.RepositoryName, secondBlob);

        var firstTree = new NewTree();
        firstTree.Tree.Add(new NewTreeItem
        {
            Mode = FileMode.File,
            Type = TreeType.Blob,
            Path = "README.md",
            Sha = firstBlobResult.Sha
        });

        var firstTreeResult = await _github.Git.Tree.Create(_context.RepositoryOwner, _context.RepositoryName, firstTree);
        var firstCommit = new NewCommit("This is a new commit", firstTreeResult.Sha);
        var firstCommitResult = await _github.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, firstCommit);

        var newReference = new NewReference("heads/develop", firstCommitResult.Sha);
        await _fixture.Create(_context.RepositoryOwner, _context.RepositoryName, newReference);

        var secondTree = new NewTree();
        secondTree.Tree.Add(new NewTreeItem
        {
            Mode = FileMode.File,
            Type = TreeType.Blob,
            Path = "README.md",
            Sha = secondBlobResult.Sha
        });

        var secondTreeResult = await _github.Git.Tree.Create(_context.RepositoryOwner, _context.RepositoryName, secondTree);

        var secondCommit = new NewCommit("This is a new commit", secondTreeResult.Sha, firstCommitResult.Sha);
        var secondCommitResult = await _github.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, secondCommit);

        var referenceUpdate = new ReferenceUpdate(secondCommitResult.Sha);

        var result = await _fixture.Update(_context.RepositoryOwner, _context.RepositoryName, "heads/develop", referenceUpdate);

        Assert.Equal(secondCommitResult.Sha, result.Object.Sha);
    }

    [IntegrationTest]
    public async Task CanUpdateAReferenceWithRepositoryId()
    {
        var firstBlob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };
        var firstBlobResult = await _github.Git.Blob.Create(_context.RepositoryOwner, _context.RepositoryName, firstBlob);
        var secondBlob = new NewBlob
        {
            Content = "This is a test!",
            Encoding = EncodingType.Utf8
        };
        var secondBlobResult = await _github.Git.Blob.Create(_context.RepositoryOwner, _context.RepositoryName, secondBlob);

        var firstTree = new NewTree();
        firstTree.Tree.Add(new NewTreeItem
        {
            Mode = FileMode.File,
            Type = TreeType.Blob,
            Path = "README.md",
            Sha = firstBlobResult.Sha
        });

        var firstTreeResult = await _github.Git.Tree.Create(_context.RepositoryOwner, _context.RepositoryName, firstTree);
        var firstCommit = new NewCommit("This is a new commit", firstTreeResult.Sha);
        var firstCommitResult = await _github.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, firstCommit);

        var newReference = new NewReference("heads/develop", firstCommitResult.Sha);
        await _fixture.Create(_context.Repository.Id, newReference);

        var secondTree = new NewTree();
        secondTree.Tree.Add(new NewTreeItem
        {
            Mode = FileMode.File,
            Type = TreeType.Blob,
            Path = "README.md",
            Sha = secondBlobResult.Sha
        });

        var secondTreeResult = await _github.Git.Tree.Create(_context.RepositoryOwner, _context.RepositoryName, secondTree);

        var secondCommit = new NewCommit("This is a new commit", secondTreeResult.Sha, firstCommitResult.Sha);
        var secondCommitResult = await _github.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, secondCommit);

        var referenceUpdate = new ReferenceUpdate(secondCommitResult.Sha);

        var result = await _fixture.Update(_context.Repository.Id, "heads/develop", referenceUpdate);

        Assert.Equal(secondCommitResult.Sha, result.Object.Sha);
    }

    [IntegrationTest]
    public async Task CanDeleteAReference()
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
            Mode = FileMode.File,
            Type = TreeType.Blob,
            Path = "README.md",
            Sha = blobResult.Sha
        });

        var treeResult = await _github.Git.Tree.Create(_context.RepositoryOwner, _context.RepositoryName, newTree);

        var newCommit = new NewCommit("This is a new commit", treeResult.Sha);

        var commitResult = await _github.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, newCommit);

        var newReference = new NewReference("heads/develop", commitResult.Sha);

        await _fixture.Create(_context.RepositoryOwner, _context.RepositoryName, newReference);
        await _fixture.Delete(_context.RepositoryOwner, _context.RepositoryName, "heads/develop");

        var all = await _fixture.GetAll(_context.RepositoryOwner, _context.RepositoryName);

        Assert.Empty(all.Where(r => r.Ref == "heads/develop"));
    }

    [IntegrationTest]
    public async Task CanDeleteAReferenceWithRepositoryId()
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
            Mode = FileMode.File,
            Type = TreeType.Blob,
            Path = "README.md",
            Sha = blobResult.Sha
        });

        var treeResult = await _github.Git.Tree.Create(_context.RepositoryOwner, _context.RepositoryName, newTree);

        var newCommit = new NewCommit("This is a new commit", treeResult.Sha);

        var commitResult = await _github.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, newCommit);

        var newReference = new NewReference("heads/develop", commitResult.Sha);

        await _fixture.Create(_context.Repository.Id, newReference);
        await _fixture.Delete(_context.Repository.Id, "heads/develop");

        var all = await _fixture.GetAll(_context.Repository.Id);

        Assert.Empty(all.Where(r => r.Ref == "heads/develop"));
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
