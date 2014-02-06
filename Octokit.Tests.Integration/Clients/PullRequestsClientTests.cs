using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class PullRequestsClientTests : IDisposable
{
    readonly IGitHubClient _client;
    readonly IPullRequestsClient _pullRequestsClient;
    readonly Repository _repository;

    public PullRequestsClientTests()
    {
        _client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };

        _pullRequestsClient = _client.Repository.PullRequest;

        var repoName = Helper.MakeNameWithTimestamp("source-repo");

        _repository = _client.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
    }

    [IntegrationTest]
    public async Task CanRetrieveOnePullRequest() 
    {
        // create baseline commit for master branch
        var baselineBlob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };
        var baselineBlobResult = await _client.GitDatabase.Blob.Create(Helper.UserName, _repository.Name, baselineBlob);

        var baselineNewTree = new NewTree();
        baselineNewTree.Tree.Add(new NewTreeItem
        {
            Type = TreeType.Blob,
            Mode = FileMode.File,
            Path = "README.md",
            Sha = baselineBlobResult.Sha
        });

        var baselineTreeResult = await _client.GitDatabase.Tree.Create(Helper.UserName, _repository.Name, baselineNewTree);

        var baselineNewCommit = new NewCommit("baseline for pull request", baselineTreeResult.Sha);

        var baselineCommit = await _client.GitDatabase.Commit.Create(Helper.UserName, _repository.Name, baselineNewCommit);

        // update master
        await _client.GitDatabase.Reference.Update(Helper.UserName, _repository.Name, "heads/master", new ReferenceUpdate(baselineCommit.Sha, true));

        // create baseline commit for feature branch
        var blob = new NewBlob
        {
            Content = "I am overwriting this blob with something new",
            Encoding = EncodingType.Utf8
        };
        var blobResult = await _client.GitDatabase.Blob.Create(Helper.UserName, _repository.Name, blob);

        var newTree = new NewTree();
        newTree.Tree.Add(new NewTreeItem
        {
            Type = TreeType.Blob,
            Mode = FileMode.File,
            Path = "README.md",
            Sha = blobResult.Sha
        });

        var treeResult = await _client.GitDatabase.Tree.Create(Helper.UserName, _repository.Name, newTree);

        var newCommit = new NewCommit("this is the commit to merge into the pull request", treeResult.Sha, baselineCommit.Sha);

        var commit = await _client.GitDatabase.Commit.Create(Helper.UserName, _repository.Name, newCommit);

        // create branch
        await _client.GitDatabase.Reference.Create(Helper.UserName, _repository.Name, new NewReference("refs/heads/my-branch", commit.Sha));

        var head = Helper.UserName + ":my-branch";

        var newPullRequest = new NewPullRequest("a pull request", head, "master");
        var result = await _pullRequestsClient.Create(Helper.UserName, _repository.Name, newPullRequest);

        Assert.Equal("a pull request", result.Title);
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}
