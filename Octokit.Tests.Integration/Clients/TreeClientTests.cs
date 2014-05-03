using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class TreeClientTests : IDisposable
{
    readonly ITreesClient _fixture;
    readonly Repository _repository;
    readonly string _owner;
    readonly GitHubClient _client;

    public TreeClientTests()
    {
        _client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };
        _fixture = _client.GitDatabase.Tree;

        var repoName = Helper.MakeNameWithTimestamp("public-repo");
        _repository = _client.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
        _owner = _repository.Owner.Login;
    }

    [IntegrationTest]
    public async Task CanCreateATree()
    {
        var blob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };

        var createdBlob = await _client.GitDatabase.Blob.Create(_owner, _repository.Name, blob);

        var newTree = new NewTree();
        newTree.Tree.Add(new NewTreeItem
        {
            Type = TreeType.Blob,
            Path = "README.md",
            Sha = createdBlob.Sha,
            Mode = FileMode.File
        });

        var result = await _fixture.Create(_owner, _repository.Name, newTree);

        Assert.NotNull(result);
    }

    [IntegrationTest]
    public async Task CanGetATree()
    {
        var result = await _fixture.Get("octokit", "octokit.net", "master");

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

        var blobResult = await _client.GitDatabase.Blob.Create(_owner, _repository.Name, blob);

        var newTree = new NewTree();
        newTree.Tree.Add(new NewTreeItem
        {
            Type = TreeType.Blob,
            Path = "README.md",
            Sha = blobResult.Sha,
            Mode = FileMode.File
        });

        var tree = await _fixture.Create(_owner, _repository.Name, newTree);

        var result = await _fixture.Get(_owner, _repository.Name, tree.Sha);

        Assert.NotNull(result);
        Assert.Equal(1, result.Tree.Count);
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}
