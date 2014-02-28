using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class CommitsClientTests : IDisposable
{
    readonly IGitHubClient _client;
    readonly Repository _repository;
    readonly ICommitsClient _fixture;
    readonly string _owner;

    public CommitsClientTests()
    {
        _client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };

        var repoName = Helper.MakeNameWithTimestamp("public-repo");
        _fixture = _client.GitDatabase.Commit;
        _repository = _client.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
        _owner = _repository.Owner.Login;
    }

    [IntegrationTest]
    public async Task CanCreateAndRetrieveCommit()
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
            Mode = FileMode.File,
            Path = "README.md",
            Sha = blobResult.Sha
        });

        var treeResult = await _client.GitDatabase.Tree.Create(_owner, _repository.Name, newTree);

        var newCommit = new NewCommit("test-commit", treeResult.Sha);

        var commit = await _fixture.Create(_owner, _repository.Name, newCommit);
        Assert.NotNull(commit);

        var retrieved = await _fixture.Get(_owner, _repository.Name, commit.Sha);
        Assert.NotNull(retrieved);
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}
