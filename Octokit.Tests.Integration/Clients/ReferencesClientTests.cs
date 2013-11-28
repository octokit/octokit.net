using System;
using System.Linq;
using System.Net.Http.Headers;
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

    [IntegrationTest(Skip = "See https://github.com/octokit/octokit.net/issues/242 and https://github.com/octokit/octokit.net/issues/238 for the relevant issues we need to address")]
    public async Task CanGetErrorForInvalidNamespace()
    {
        await AssertEx.Throws<Exception>(
            async () => { await _fixture.GetAllForSubNamespace("octokit", "octokit.net", "666"); });
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

        var newCommit = new NewCommit("This is a new commit", treeResult.Sha, Enumerable.Empty<string>());

        var commitResult = await _client.GitDatabase.Commit.Create(_owner, _repository.Name, newCommit);

        var newReference = new NewReference("heads/develop", commitResult.Sha);
        var result = await _fixture.Create(_owner, _repository.Name, newReference);

        Assert.NotNull(result);
    }

    [IntegrationTest(Skip="TODO")]
    public async Task CanUpdateAReference()
    {
        // TODO: create a blob
        // TODO: create a tree
        // TODO: create a commit
        // TODO: use the SHA to create a reference

        var newReference = new NewReference("heads/develop", "sha");
        await _fixture.Create("owner", "repo", newReference);

        var referenceUpdate = new ReferenceUpdate("sha");

        var result = await _fixture.Update("owner", "repo", "heads/develop", referenceUpdate);

        Assert.NotNull(result);
    }

    [IntegrationTest(Skip="TODO")]
    public async Task CanDeleteAReference()
    {
        // TODO: create a blob
        // TODO: create a tree
        // TODO: create a commit
        // TODO: use the SHA to create a reference

        // TODO: can we validate the response here?
        // TODO: otherwise, fire off a GetAll and validate it's not in the list

        await _fixture.Delete("owner", "repo", "heads/develop");
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}
