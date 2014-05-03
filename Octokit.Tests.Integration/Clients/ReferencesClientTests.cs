using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Helpers;
using Octokit.Tests.Integration;
using Xunit;

public class ReferencesClientTests : IDisposable
{
    readonly IReferencesClient _fixture;
    readonly Repository _repository;
    readonly GitHubClient _client;
    readonly string _owner;

    public ReferencesClientTests()
    {
        _client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };
        _fixture = _client.GitDatabase.Reference;

        var repoName = Helper.MakeNameWithTimestamp("public-repo");
        _repository = _client.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
        _owner = _repository.Owner.Login;
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
        Assert.False(String.IsNullOrWhiteSpace(@ref.Object.Sha));
    }

    [IntegrationTest]
    public async Task WhenReferenceDoesNotExistAnExeptionIsThrown()
    {
        await AssertEx.Throws<NotFoundException>(
            () => _fixture.Get("octokit", "octokit.net", "heads/foofooblahblah"));
    }

    [IntegrationTest]
    public async Task CanGetListOfReferences()
    {
        var list = await _fixture.GetAll("octokit", "octokit.net");
        Assert.NotEmpty(list);
    }

    [IntegrationTest]
    public async Task CanGetListOfReferencesInNamespace()
    {
        var list = await _fixture.GetAllForSubNamespace("octokit", "octokit.net", "heads");
        Assert.NotEmpty(list);
    }

    [IntegrationTest]
    public async Task CanGetErrorForInvalidNamespace()
    {
        var owner = "octokit";
        var repo = "octokit.net";
        var subNamespace = "666";

        var result = await AssertEx.Throws<NotFoundException>(
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
        var blobResult = await _client.GitDatabase.Blob.Create(_owner, _repository.Name, blob);

        var newTree = new NewTree();
        newTree.Tree.Add(new NewTreeItem
        {
            Mode = FileMode.File,
            Type = TreeType.Blob,
            Path = "README.md",
            Sha = blobResult.Sha
        });

        var treeResult = await _client.GitDatabase.Tree.Create(_owner, _repository.Name, newTree);

        var newCommit = new NewCommit("This is a new commit", treeResult.Sha);

        var commitResult = await _client.GitDatabase.Commit.Create(_owner, _repository.Name, newCommit);

        var newReference = new NewReference("heads/develop", commitResult.Sha);
        var result = await _fixture.Create(_owner, _repository.Name, newReference);

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
        var firstBlobResult = await _client.GitDatabase.Blob.Create(_owner, _repository.Name, firstBlob);
        var secondBlob = new NewBlob
        {
            Content = "This is a test!",
            Encoding = EncodingType.Utf8
        };
        var secondBlobResult = await _client.GitDatabase.Blob.Create(_owner, _repository.Name, secondBlob);

        var firstTree = new NewTree();
        firstTree.Tree.Add(new NewTreeItem
        {
            Mode = FileMode.File,
            Type = TreeType.Blob,
            Path = "README.md",
            Sha = firstBlobResult.Sha
        });

        var firstTreeResult = await _client.GitDatabase.Tree.Create(_owner, _repository.Name, firstTree);
        var firstCommit = new NewCommit("This is a new commit", firstTreeResult.Sha);
        var firstCommitResult = await _client.GitDatabase.Commit.Create(_owner, _repository.Name, firstCommit);

        var newReference = new NewReference("heads/develop", firstCommitResult.Sha);
        await _fixture.Create(_owner, _repository.Name, newReference);

        var secondTree = new NewTree();
        secondTree.Tree.Add(new NewTreeItem
        {
            Mode = FileMode.File,
            Type = TreeType.Blob,
            Path = "README.md",
            Sha = secondBlobResult.Sha
        });

        var secondTreeResult = await _client.GitDatabase.Tree.Create(_owner, _repository.Name, secondTree);

        var secondCommit = new NewCommit("This is a new commit", secondTreeResult.Sha, firstCommitResult.Sha);
        var secondCommitResult = await _client.GitDatabase.Commit.Create(_owner, _repository.Name, secondCommit);

        var referenceUpdate = new ReferenceUpdate(secondCommitResult.Sha);

        var result = await _fixture.Update(_owner, _repository.Name, "heads/develop", referenceUpdate);

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
        var blobResult = await _client.GitDatabase.Blob.Create(_owner, _repository.Name, blob);

        var newTree = new NewTree();
        newTree.Tree.Add(new NewTreeItem
        {
            Mode = FileMode.File,
            Type = TreeType.Blob,
            Path = "README.md",
            Sha = blobResult.Sha
        });

        var treeResult = await _client.GitDatabase.Tree.Create(_owner, _repository.Name, newTree);

        var newCommit = new NewCommit("This is a new commit", treeResult.Sha);

        var commitResult = await _client.GitDatabase.Commit.Create(_owner, _repository.Name, newCommit);

        var newReference = new NewReference("heads/develop", commitResult.Sha);

        await _fixture.Create(_owner, _repository.Name, newReference);
        await _fixture.Delete(_owner, _repository.Name, "heads/develop");

        var all = await _fixture.GetAll(_owner, _repository.Name);

        Assert.Empty(all.Where(r => r.Ref == "heads/develop"));
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}
