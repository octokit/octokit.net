using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class PullRequestsClientTests : IDisposable
{
    readonly IGitHubClient _client;
    readonly IPullRequestsClient _fixture;
    readonly Repository _repository;

    public PullRequestsClientTests()
    {
        _client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };

        _fixture = _client.Repository.PullRequest;

        var repoName = Helper.MakeNameWithTimestamp("source-repo");

        _repository = _client.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
    }

    [IntegrationTest]
    public async Task CanCreate()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", "my-branch", "master");
        var result = await _fixture.Create(Helper.UserName, _repository.Name, newPullRequest);

        Assert.Equal("a pull request", result.Title);
        Assert.False(result.Merged);
    }

    [IntegrationTest]
    public async Task CanGetForRepository()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", "my-branch", "master");
        var result = await _fixture.Create(Helper.UserName, _repository.Name, newPullRequest);

        var pullRequests = await _fixture.GetForRepository(Helper.UserName, _repository.Name);

        Assert.Equal(1, pullRequests.Count);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task CanGetOpenPullRequest()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", "my-branch", "master");
        var result = await _fixture.Create(Helper.UserName, _repository.Name, newPullRequest);

        var openPullRequests = new PullRequestRequest { State = ItemState.Open };
        var pullRequests = await _fixture.GetForRepository(Helper.UserName, _repository.Name, openPullRequests);

        Assert.Equal(1, pullRequests.Count);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task IgnoresOpenPullRequest()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", "my-branch", "master");
        await _fixture.Create(Helper.UserName, _repository.Name, newPullRequest);

        var openPullRequests = new PullRequestRequest { State = ItemState.Closed };
        var pullRequests = await _fixture.GetForRepository(Helper.UserName, _repository.Name, openPullRequests);

        Assert.Empty(pullRequests);
    }

    [IntegrationTest]
    public async Task CanUpdate()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", "my-branch", "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _repository.Name, newPullRequest);

        var updatePullRequest = new PullRequestUpdate { Title = "updated title", Body = "Hello New Body" };
        var result = await _fixture.Update(Helper.UserName, _repository.Name, pullRequest.Number, updatePullRequest);

        Assert.Equal(updatePullRequest.Title, result.Title);
        Assert.Equal(updatePullRequest.Body, result.Body);
    }

    [IntegrationTest]
    public async Task CanClose()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", "my-branch", "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _repository.Name, newPullRequest);

        var updatePullRequest = new PullRequestUpdate { State = ItemState.Closed };
        var result = await _fixture.Update(Helper.UserName, _repository.Name, pullRequest.Number, updatePullRequest);

        Assert.Equal(ItemState.Closed, result.State);
        Assert.Equal(pullRequest.Title, result.Title);
        Assert.Equal(pullRequest.Body, result.Body);
    }

    [IntegrationTest]
    public async Task CanFindClosedPullRequest()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", "my-branch", "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _repository.Name, newPullRequest);

        var updatePullRequest = new PullRequestUpdate { State = ItemState.Closed };
        await _fixture.Update(Helper.UserName, _repository.Name, pullRequest.Number, updatePullRequest);

        var closedPullRequests = new PullRequestRequest { State = ItemState.Closed };
        var pullRequests = await _fixture.GetForRepository(Helper.UserName, _repository.Name, closedPullRequests);

        Assert.Equal(1, pullRequests.Count);
    }

    [IntegrationTest]
    public async Task IsNotMergedInitially()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", "my-branch", "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _repository.Name, newPullRequest);

        var result = await _fixture.Merged(Helper.UserName, _repository.Name, pullRequest.Number);

        Assert.False(result);
    }

    [IntegrationTest]
    public async Task CanBeMerged()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", "my-branch", "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _repository.Name, newPullRequest);

        var merge = new MergePullRequest("thing the thing");
        var result = await _fixture.Merge(Helper.UserName, _repository.Name, pullRequest.Number, merge);

        Assert.True(result.Merged);
    }

    [IntegrationTest]
    public async Task UpdatesMaster()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", "my-branch", "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _repository.Name, newPullRequest);

        var merge = new MergePullRequest("thing the thing");
        var result = await _fixture.Merge(Helper.UserName, _repository.Name, pullRequest.Number, merge);

        var master = await _client.GitDatabase.Reference.Get(Helper.UserName, _repository.Name, "heads/master");

        Assert.Equal(result.Sha, master.Object.Sha);
    }

    [IntegrationTest]
    public async Task CanBrowseCommits()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", "my-branch", "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _repository.Name, newPullRequest);

        var result = await _fixture.Commits(Helper.UserName, _repository.Name, pullRequest.Number);

        Assert.Equal(1, result.Count);
        Assert.Equal("this is the commit to merge into the pull request", result[0].Commit.Message);
    }

    async Task CreateTheWorld()
    {
        var master = await _client.GitDatabase.Reference.Get(Helper.UserName, _repository.Name, "heads/master");

        // create new commit for master branch
        var newMasterTree = await CreateTree(new Dictionary<string, string> { { "README.md", "Hello World!" } });
        var newMaster = await CreateCommit("baseline for pull request", newMasterTree.Sha, master.Object.Sha);

        // update master
        await _client.GitDatabase.Reference.Update(Helper.UserName, _repository.Name, "heads/master", new ReferenceUpdate(newMaster.Sha));

        // create new commit for feature branch
        var featureBranchTree = await CreateTree(new Dictionary<string, string> { { "README.md", "I am overwriting this blob with something new" } });
        var newFeature = await CreateCommit("this is the commit to merge into the pull request", featureBranchTree.Sha, newMaster.Sha);

        // create branch
        await _client.GitDatabase.Reference.Create(Helper.UserName, _repository.Name, new NewReference("refs/heads/my-branch", newFeature.Sha));
    }

    async Task<TreeResponse> CreateTree(IDictionary<string,string> treeContents)
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

        var newTree = new NewTree { Tree = collection };

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
