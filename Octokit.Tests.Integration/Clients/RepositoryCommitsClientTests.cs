using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class RepositoryCommitsClientTests : IDisposable
{
    readonly IGitHubClient _client;
    IRepositoryCommitsClient _fixture;
    Repository _repository;

    public RepositoryCommitsClientTests()
    {
        _client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };

        _fixture = _client.Repository.Commits;

        var repoName = Helper.MakeNameWithTimestamp("source-repo");

        _repository = _client.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
    }

    [IntegrationTest]
    public async Task CanCompareReferences()
    {
        await CreateTheWorld();

        var result = await _fixture.Compare(Helper.UserName, _repository.Name, "master", "my-branch");

        Assert.Equal(1, result.TotalCommits);
        Assert.Equal(1, result.Commits.Count);
        Assert.Equal(1, result.AheadBy);
        Assert.Equal(0, result.BehindBy);
    }

    [IntegrationTest]
    public async Task CanCompareReferencesOtherWayRound()
    {
        await CreateTheWorld();

        var result = await _fixture.Compare(Helper.UserName, _repository.Name, "my-branch", "master");

        Assert.Equal(0, result.TotalCommits);
        Assert.Equal(0, result.Commits.Count);
        Assert.Equal(0, result.AheadBy);
        Assert.Equal(1, result.BehindBy);
    }

    [IntegrationTest]
    public async Task ReturnsUrlsToResources()
    {
        await CreateTheWorld();

        var result = await _fixture.Compare(Helper.UserName, _repository.Name, "my-branch", "master");

        Assert.NotNull(result.DiffUrl);
        Assert.NotNull(result.HtmlUrl);
        Assert.NotNull(result.PatchUrl);
        Assert.NotNull(result.PermalinkUrl);
    }

    [IntegrationTest]
    public async Task CanCompareUsingSha()
    {
        await CreateTheWorld();

        var master = await _client.GitDatabase.Reference.Get(Helper.UserName, _repository.Name, "heads/master");
        var branch = await _client.GitDatabase.Reference.Get(Helper.UserName, _repository.Name, "heads/my-branch");

        var result = await _fixture.Compare(Helper.UserName, _repository.Name, master.Object.Sha, branch.Object.Sha);

        Assert.Equal(1, result.Commits.Count);
        Assert.Equal(1, result.AheadBy);
        Assert.Equal(0, result.BehindBy);
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

    async Task<TreeResponse> CreateTree(IDictionary<string, string> treeContents)
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
        _client.Repository.Delete(_repository.Owner.Login, _repository.Name);
    }
}
