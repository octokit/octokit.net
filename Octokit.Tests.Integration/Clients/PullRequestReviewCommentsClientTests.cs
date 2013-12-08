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

    string _repoName;
    string _ownerName;

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

        // You can't for a repository that doesn't have any commit

        // Creating a blob

        // TODO Not Implemented: http://developer.github.com/v3/git/blobs/#create-a-blob

        // Creating a tree

        // TODO Not Implemented: http://developer.github.com/v3/git/trees/#create-a-tree
        var treeSha = String.Empty;

        // Creating a commit

        var commit = new NewCommit("A nice commit message", treeSha, Enumerable.Empty<string>());

        var createdCommit = _gitHubClient.GitDatabase.Commit.Create(_ownerName, _repoName, commit).Result;

        // We can't fork our own repository so we need a second test user
        // Using the second test user

        _gitHubClient = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };

        // Creating a fork

        // TODO Not Implemented: http://developer.github.com/v3/repos/forks/#create-a-fork
        
        // Creating a blob

        // TODO Not Implemented: http://developer.github.com/v3/git/blobs/#create-a-blob

        // Creating a tree

        // TODO Not Implemented: http://developer.github.com/v3/git/trees/#create-a-tree
        var pullRequestTreeSha = String.Empty;
        _path = "";

        // Creating a commit

        var pullRequestcommit = new NewCommit("A pull request commit message", pullRequestTreeSha, Enumerable.Empty<string>());

        var createdPullRequestCommit = _gitHubClient.GitDatabase.Commit.Create("second user name", _repoName, pullRequestcommit).Result;
        _pullRequestCommitId = createdPullRequestCommit.Sha;
        
        // Creating a pull request

        // TODO Not Implemented: http://developer.github.com/v3/pulls/#create-a-pull-request
        _pullRequestNumber = 0;

        // Using the first user again

        _gitHubClient = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };

        _client = _gitHubClient.PullRequest.Comment;
    }

    [IntegrationTest(Skip = "Requires Blob, Tree, Fork and Pull Request Api implementation")]
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
        // TODO Ensure that it deletes the forks too
    }
}
