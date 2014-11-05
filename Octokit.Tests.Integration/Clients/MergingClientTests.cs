using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class MergingClientTests : IDisposable
{
    readonly IGitHubClient _client;
    readonly Repository _repository;
    readonly string _owner;
    readonly IMergingClient _fixture;
    readonly ICommitsClient _commitsClient;

    public MergingClientTests()
    {
        _client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };

        var repoName = Helper.MakeNameWithTimestamp("public-repo");
        _fixture = _client.GitDatabase.Merging;
        _commitsClient = _client.GitDatabase.Commit;
        _repository = _client.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
        _owner = _repository.Owner.Login;
    }

    [IntegrationTest]
    public async Task CanCreateMerge()
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

        var commit = await _commitsClient.Create(_owner, _repository.Name, newCommit);
        Assert.NotNull(commit);

        var newMerge = new NewMerge("master", commit.Sha, "merge commit to master from integrationtests");

        var merge = await _fixture.Create(_owner, _repository.Name, newMerge);
        Assert.NotNull(merge);
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}
