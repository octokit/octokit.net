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
    readonly IRepositoryCommentsClient _repositoryCommentsClient;

    const string branchName = "my-branch";

    public PullRequestsClientTests()
    {
        _client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };

        _fixture = _client.Repository.PullRequest;
        _repositoryCommentsClient = _client.Repository.RepositoryComments;

        var repoName = Helper.MakeNameWithTimestamp("source-repo");

        _repository = _client.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
    }

    [IntegrationTest]
    public async Task CanCreate()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var result = await _fixture.Create(Helper.UserName, _repository.Name, newPullRequest);

        Assert.Equal("a pull request", result.Title);
        Assert.False(result.Merged);
    }

    [IntegrationTest]
    public async Task CanGetForRepository()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var result = await _fixture.Create(Helper.UserName, _repository.Name, newPullRequest);

        var pullRequests = await _fixture.GetForRepository(Helper.UserName, _repository.Name);

        Assert.Equal(1, pullRequests.Count);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task CanGetOpenPullRequest()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
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

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        await _fixture.Create(Helper.UserName, _repository.Name, newPullRequest);

        var openPullRequests = new PullRequestRequest { State = ItemState.Closed };
        var pullRequests = await _fixture.GetForRepository(Helper.UserName, _repository.Name, openPullRequests);

        Assert.Empty(pullRequests);
    }

    [IntegrationTest]
    public async Task CanUpdate()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
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

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
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

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
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

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _repository.Name, newPullRequest);

        var result = await _fixture.Merged(Helper.UserName, _repository.Name, pullRequest.Number);

        Assert.False(result);
    }

    [IntegrationTest]
    public async Task CanBeMerged()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _repository.Name, newPullRequest);

        var merge = new MergePullRequest("thing the thing");
        var result = await _fixture.Merge(Helper.UserName, _repository.Name, pullRequest.Number, merge);

        Assert.True(result.Merged);
    }

    [IntegrationTest]
    public async Task UpdatesMaster()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
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

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _repository.Name, newPullRequest);

        var result = await _fixture.Commits(Helper.UserName, _repository.Name, pullRequest.Number);

        Assert.Equal(1, result.Count);
        Assert.Equal("this is the commit to merge into the pull request", result[0].Commit.Message);
    }

    [IntegrationTest]
    public async Task CanGetCommitsAndCommentCount()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _repository.Name, newPullRequest);

        // create new commit for branch

        const string commitMessage = "Another commit in branch";
        
        var branch = await _client.GitDatabase.Reference.Get(Helper.UserName, _repository.Name, "heads/" + branchName);

        var newTree = await CreateTree(new Dictionary<string, string> { { "README.md", "Hello World!" } });
        var newCommit = await CreateCommit(commitMessage, newTree.Sha, branch.Object.Sha);
        await _client.GitDatabase.Reference.Update(Helper.UserName, _repository.Name, "heads/" + branchName, new ReferenceUpdate(newCommit.Sha));
        
        await _repositoryCommentsClient.Create(Helper.UserName, _repository.Name, newCommit.Sha, new NewCommitComment("I am a nice comment") { Path = "README.md", Position = 1 });

        // don't try this at home
        await Task.Delay(TimeSpan.FromSeconds(5));

        var result = await _fixture.Commits(Helper.UserName, _repository.Name, pullRequest.Number);

        Assert.Equal(2, result.Count);
        Assert.Equal("this is the commit to merge into the pull request", result[0].Commit.Message);
        Assert.Equal(0, result[0].Commit.CommentCount);
        Assert.Equal(commitMessage, result[1].Commit.Message);
        Assert.Equal(1, result[1].Commit.CommentCount);
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
        var featureBranchCommit = await CreateCommit("this is the commit to merge into the pull request", featureBranchTree.Sha, newMaster.Sha);

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
