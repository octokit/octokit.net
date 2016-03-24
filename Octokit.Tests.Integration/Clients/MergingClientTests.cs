using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;
using Octokit.Tests.Integration.Helpers;

public class MergingClientTests : IDisposable
{
    private readonly IGitHubClient _github;
    private readonly IMergingClient _fixture;
    private readonly RepositoryContext _context;

    const string branchName = "my-branch";

    public MergingClientTests()
    {
        _github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };

        _fixture = _github.Repository.Merging;
        _context = _github.CreateRepositoryContext("public-repo").Result;
    }

    [IntegrationTest]
    public async Task CanCreateMerge()
    {
        await CreateTheWorld();

        var newMerge = new NewMerge("master", branchName) { CommitMessage = "merge commit to master from integrationtests" };

        var merge = await _fixture.Create(_context.RepositoryOwner, _context.RepositoryName, newMerge);
        Assert.NotNull(merge);
        Assert.NotNull(merge.Commit);
        Assert.Equal("merge commit to master from integrationtests", merge.Commit.Message);
    }

    async Task CreateTheWorld()
    {
        var master = await _github.Git.Reference.Get(Helper.UserName, _context.RepositoryName, "heads/master");

        // create new commit for master branch
        var newMasterTree = await CreateTree(new Dictionary<string, string> { { "README.md", "Hello World! I want to be overwritten by featurebranch!" } });
        var newMaster = await CreateCommit("baseline for merge", newMasterTree.Sha, master.Object.Sha);

        // update master
        await _github.Git.Reference.Update(Helper.UserName, _context.RepositoryName, "heads/master", new ReferenceUpdate(newMaster.Sha));

        // create new commit for feature branch
        var featureBranchTree = await CreateTree(new Dictionary<string, string> { { "README.md", "I am overwriting this blob with something new" } });
        var featureBranchCommit = await CreateCommit("this is the commit to merge", featureBranchTree.Sha, newMaster.Sha);

        // create branch
        await _github.Git.Reference.Create(Helper.UserName, _context.RepositoryName, new NewReference("refs/heads/my-branch", featureBranchCommit.Sha));
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
            var baselineBlobResult = await _github.Git.Blob.Create(Helper.UserName, _context.RepositoryName, baselineBlob);

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

        return await _github.Git.Tree.Create(Helper.UserName, _context.RepositoryName, newTree);
    }

    async Task<Commit> CreateCommit(string message, string sha, string parent)
    {
        var newCommit = new NewCommit(message, sha, parent);
        return await _github.Git.Commit.Create(Helper.UserName, _context.RepositoryName, newCommit);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
