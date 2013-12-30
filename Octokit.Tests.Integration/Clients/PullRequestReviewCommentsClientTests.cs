using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class PullRequestReviewCommentsClientTests : IDisposable
{
    IGitHubClient _gitHubClient;
    Repository _repository;
    IPullRequestReviewCommentsClient _client;

    string _ownerName;
    string _repoName;
    string _branchName;

    int _pullRequestNumber;
    string _pullRequestCommitId;
    string _path;

    public PullRequestReviewCommentsClientTests()
    {
        _gitHubClient = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };

        // Creating a repository

        _repoName = Helper.MakeNameWithTimestamp("public-repo");
        _repository = _gitHubClient.Repository.Create(new NewRepository { Name = _repoName }).Result;
        _ownerName = _repository.Owner.Name;

        // Creating a blob

        var blob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };

        var createdBlob = _gitHubClient.GitDatabase.Blob.Create(_ownerName, _repoName, blob).Result;

        // Creating a tree

        var treeSha = createdBlob.Sha;

        var newTree = new NewTree();
        newTree.Tree.Add(new NewTreeItem
        {
            Type = TreeType.Blob,
            Mode = FileMode.File,
            Path = "README.md",
            Sha = treeSha,
        });

        var createdTree = _gitHubClient.GitDatabase.Tree.Create(_ownerName, _repoName, newTree).Result;

        // Creating a commit

        var commit = new NewCommit("A nice commit message", treeSha, Enumerable.Empty<string>());

        var createdCommit = _gitHubClient.GitDatabase.Commit.Create(_ownerName, _repoName, commit).Result;

        // Creating a branch

        var newBranch = new NewReference("refs/heads/new-branch", createdCommit.Sha);
        var reference = _gitHubClient.GitDatabase.Reference.Create(_ownerName, _repoName, newBranch).Result;
        _branchName = reference.Ref;

        // Creating a blob in the branch

        var pullRequestBlob = new NewBlob
        {
            Content = "Hello from the fork!",
            Encoding = EncodingType.Utf8
        };

        var createdPullRequestBlob = _gitHubClient.GitDatabase.Blob.Create(_ownerName, _branchName, pullRequestBlob).Result;

        // Creating a tree in the branch

        var pullRequestTreeSha = createdPullRequestBlob.Sha;
        _path = "CONTRIBUTING.md";

        var pullRequestTree = new NewTree();
        pullRequestTree.Tree.Add(new NewTreeItem
        {
            Type = TreeType.Blob,
            Mode = FileMode.File,
            Path = _path,
            Sha = pullRequestTreeSha,
        });

        var createdPullRequestTree = _gitHubClient.GitDatabase.Tree.Create(_ownerName, _branchName, pullRequestTree).Result;

        // Creating a commit in the branch

        var pullRequestcommit = new NewCommit("A pull request commit message", pullRequestTreeSha, Enumerable.Empty<string>());

        var createdPullRequestCommit = _gitHubClient.GitDatabase.Commit.Create(_ownerName, _branchName, pullRequestcommit).Result;
        _pullRequestCommitId = createdPullRequestCommit.Sha;
        
        // Creating a pull request

        // TODO Not Implemented: http://developer.github.com/v3/pulls/#create-a-pull-request
        _pullRequestNumber = 0;

        _client = _gitHubClient.PullRequest.Comment;
    }

    [IntegrationTest(Skip = "Requires Pull Request Api implementation")]
    public async Task CanCreateAndRetrieveReviewComment()
    {
        var pullRequestReviewComment = new PullRequestReviewCommentCreate("A review comment message", _pullRequestCommitId, _path, 1);

        var createdComment = await _client.Create(_ownerName, _repoName, _pullRequestNumber, pullRequestReviewComment);

        Assert.NotNull(createdComment);
        Assert.Equal(pullRequestReviewComment.Body, createdComment.Body);

        var commentFromGitHub = await _client.GetComment(_ownerName, _repoName, createdComment.Id);

        Assert.NotNull(commentFromGitHub);
        Assert.Equal(pullRequestReviewComment.Body, commentFromGitHub.Body);
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
        // TODO Ensure that it deletes the branch too
    }
}
