using System;
using System.Collections.Generic;
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
    
    const string branchName = "my-branch";
    
    public MergingClientTests()
    {
        _client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };

        var repoName = Helper.MakeNameWithTimestamp("public-repo");
        _fixture = _client.Repository.Merging;
        _repository = _client.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
        _owner = _repository.Owner.Login;
    }

    [IntegrationTest]
    public async Task CanCreateMerge()
    {
        await CreateTheWorld();

        var newMerge = new NewMerge("master", branchName) { CommitMessage = "merge commit to master from integrationtests" };

        var merge = await _fixture.Create(_owner, _repository.Name, newMerge);
        Assert.NotNull(merge);
        Assert.NotNull(merge.Commit);
        Assert.Equal("merge commit to master from integrationtests", merge.Commit.Message);
    }

    async Task CreateTheWorld()
    {
        var master = await _client.GitDatabase.Reference.Get(Helper.UserName, _repository.Name, "heads/master");

        // create new commit for master branch
        var newMasterTree = await CreateTree(new Dictionary<string, string> { { "README.md", "Hello World! I want to be overwritten by featurebranch!" } });
        var newMaster = await CreateCommit("baseline for merge", newMasterTree.Sha, master.Object.Sha);

        // update master
        await _client.GitDatabase.Reference.Update(Helper.UserName, _repository.Name, "heads/master", new ReferenceUpdate(newMaster.Sha));

        // create new commit for feature branch
        var featureBranchTree = await CreateTree(new Dictionary<string, string> { { "README.md", "I am overwriting this blob with something new" } });
        var featureBranchCommit = await CreateCommit("this is the commit to merge", featureBranchTree.Sha, newMaster.Sha);

        // create branch
        await _client.GitDatabase.Reference.Create(Helper.UserName, _repository.Name, new NewReference("refs/heads/my-branch", featureBranchCommit.Sha));
    }

    async Task<TreeResponse> CreateTree(IEnumerable<KeyValuePair<string, string>> treeContents)
    {
        var collection = new List<NewTreeItem>();

        foreach (var c in treeContents)
        {
            var baselineBlob = new NewBlob
            {
                Content = c.Value,
                Encoding = EncodingType.Utf8
            };
            var baselineBlobResult = await _client.GitDatabase.Blob.Create(Helper.UserName, _repository.Name, baselineBlob);

            collection.Add(new NewTreeItem
            {
                Type = TreeType.Blob,
                Mode = FileMode.File,
                Path = c.Key,
                Sha = baselineBlobResult.Sha
            });
        }

        var newTree = new NewTree();
        foreach (var item in collection)
        {
            newTree.Tree.Add(item);
        }

        return await _client.GitDatabase.Tree.Create(Helper.UserName, _repository.Name, newTree);
    }

    async Task<Commit> CreateCommit(string message, string sha, string parent)
    {
        var newCommit = new NewCommit(message, sha, parent);
        return await _client.GitDatabase.Commit.Create(Helper.UserName, _repository.Name, newCommit);
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}
