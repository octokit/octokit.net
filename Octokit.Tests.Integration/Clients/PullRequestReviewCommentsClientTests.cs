using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class PullRequestReviewCommentsClientTests : IDisposable
{
    readonly IGitHubClient _gitHubClient;
    readonly List<Repository> _repositoriesToDelete = new List<Repository>();
    readonly IPullRequestReviewCommentsClient _client;

    const string branchName = "heads/new-branch";
    const string path = "CONTRIBUTING.md";

    readonly string _repoName;
    readonly int _pullRequestNumber;
    readonly string _pullRequestCommitId;

    public PullRequestReviewCommentsClientTests()
    {
        _gitHubClient = new GitHubClient(new Octokit.ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };

        _client = _gitHubClient.PullRequest.Comment;

        // We'll create a pull request that can be used by most tests

        var pullRequestData = CreatePullRequest().Result;

        _repositoriesToDelete.Add(pullRequestData.Repository);

        _repoName = pullRequestData.RepoName;
        _pullRequestNumber = pullRequestData.PullRequestNumber;
        _pullRequestCommitId = pullRequestData.PullRequestCommitId;
    }

    [IntegrationTest]
    public async Task CanCreateAndRetrieveComment()
    {
        const string body = "A review comment message";
        const int position = 1;

        var createdCommentId = await CreateComment(body, position);

        var commentFromGitHub = await _client.GetComment(Helper.UserName, _repoName, createdCommentId);

        AssertComment(commentFromGitHub, body, position);
    }

    [IntegrationTest]
    public async Task CanEditComment()
    {
        const string body = "A new review comment message";
        const int position = 1;

        var createdCommentId = await CreateComment(body, position);

        var edit = new PullRequestReviewCommentEdit("Edited Comment");

        var editedComment = await _client.Edit(Helper.UserName, _repoName, createdCommentId, edit);

        var commentFromGitHub = await _client.GetComment(Helper.UserName, _repoName, editedComment.Id);

        AssertComment(commentFromGitHub, edit.Body, position);
    }

    [IntegrationTest]
    public async Task CanDeleteComment()
    {
        const string body = "A new review comment message";
        const int position = 1;

        var createdCommentId = await CreateComment(body, position);

        Assert.DoesNotThrow(async () => { await _client.Delete(Helper.UserName, _repoName, createdCommentId); });
    }

    [IntegrationTest]
    public async Task CanCreateReply()
    {
        const string body = "Reply me!";
        const int position = 1;

        var createdCommentId = await CreateComment(body, position);

        var reply = new PullRequestReviewCommentReplyCreate("Replied", createdCommentId);
        var createdReply = await _client.CreateReply(Helper.UserName, _repoName, _pullRequestNumber, reply);
        var createdReplyFromGitHub = await _client.GetComment(Helper.UserName, _repoName, createdReply.Id);

        AssertComment(createdReplyFromGitHub, reply.Body, position);
    }

    [IntegrationTest]
    public async Task CanGetForPullRequest()
    {
        var pullRequest = await CreatePullRequest();
        _repositoriesToDelete.Add(pullRequest.Repository);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, pullRequest.RepoName, pullRequest.PullRequestCommitId, pullRequest.PullRequestNumber);

        var pullRequestComments = await _client.GetForPullRequest(Helper.UserName, pullRequest.RepoName, pullRequest.PullRequestNumber);

        AssertComments(pullRequestComments, commentsToCreate, position);
    }

    [IntegrationTest]
    public async Task CanGetForRepository()
    {
        var pullRequest = await CreatePullRequest();
        _repositoriesToDelete.Add(pullRequest.Repository);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment One", "Comment Two" };

        await CreateComments(commentsToCreate, position, pullRequest.RepoName, pullRequest.PullRequestCommitId, pullRequest.PullRequestNumber);

        var pullRequestComments = await _client.GetForRepository(Helper.UserName, pullRequest.RepoName);

        AssertComments(pullRequestComments, commentsToCreate, position);
    }

    [IntegrationTest]
    public async Task CanGetForRepositoryAscendingSort()
    {
        var pullRequest = await CreatePullRequest();
        _repositoriesToDelete.Add(pullRequest.Repository);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment One", "Comment Two", "Comment Three", "Comment Four" };

        await CreateComments(commentsToCreate, position, pullRequest.RepoName, pullRequest.PullRequestCommitId, pullRequest.PullRequestNumber);

        var pullRequestComments = await _client.GetForRepository(Helper.UserName, pullRequest.RepoName, new PullRequestReviewCommentRequest { Direction = SortDirection.Ascending });

        AssertComments(pullRequestComments, commentsToCreate, position);
    }

    [IntegrationTest]
    public async Task CanGetForRepositoryDescendingSort()
    {
        var pullRequest = await CreatePullRequest();
        _repositoriesToDelete.Add(pullRequest.Repository);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment One", "Comment Two", "Comment Three", "Comment Four" };

        await CreateComments(commentsToCreate, position, pullRequest.RepoName, pullRequest.PullRequestCommitId, pullRequest.PullRequestNumber);

        var pullRequestComments = await _client.GetForRepository(Helper.UserName, pullRequest.RepoName, new PullRequestReviewCommentRequest { Direction = SortDirection.Descending });

        commentsToCreate.Reverse();
        AssertComments(pullRequestComments, commentsToCreate, position);
    }

    public void Dispose()
    {
        foreach (var repo in _repositoriesToDelete)
        {
            Helper.DeleteRepo(repo);
        }
    }

    async Task<int> CreateComment(string body, int position)
    {
        return await CreateComment(body, position, _repoName, _pullRequestCommitId, _pullRequestNumber);
    }

    async Task<int> CreateComment(string body, int position, string repoName, string pullRequestCommitId, int pullRequestNumber)
    {
        var comment = new PullRequestReviewCommentCreate(body, pullRequestCommitId, path, position);

        var createdComment = await _client.Create(Helper.UserName, repoName, pullRequestNumber, comment);

        AssertComment(createdComment, body, position);

        return createdComment.Id;
    }

    async Task CreateComments(IEnumerable<string> comments, int position, string repoName, string pullRequestCommitId, int pullRequestNumber)
    {
        foreach (var comment in comments)
        {
            await CreateComment(comment, position, repoName, pullRequestCommitId, pullRequestNumber);
        }
    }

    static void AssertComment(PullRequestReviewComment comment, string body, int position)
    {
        Assert.NotNull(comment);
        Assert.Equal(body, comment.Body);
        Assert.Equal(position, comment.Position);
    }

    static void AssertComments(IReadOnlyList<PullRequestReviewComment> comments, List<string> bodies, int position)
    {
        Assert.Equal(bodies.Count, comments.Count);

        for (var i = 0; i < bodies.Count; i = i + 1)
        {
            AssertComment(comments[i], bodies[i], position);
        }
    }

    async Task<Repository> CreateRepository(string repoName)
    {
        return await _gitHubClient.Repository.Create(new NewRepository { Name = repoName, AutoInit = true });
    }

    /// <summary>
    /// Creates the base state for testing (creates a repo, a commit in master, a branch, a commit in the branch and a pull request)
    /// </summary>
    /// <returns></returns>
    async Task<PullRequestData> CreatePullRequest()
    {
        var repoName = Helper.MakeNameWithTimestamp("test-repo");
        var repository = await CreateRepository(repoName);

        // Creating a commit in master

        var createdCommitInMaster = await CreateCommit(repoName, "Hello World!", "README.md", "heads/master", "A master commit message");

        // Creating a branch

        var newBranch = new NewReference("refs/" + branchName, createdCommitInMaster.Sha);
        await _gitHubClient.GitDatabase.Reference.Create(Helper.UserName, repoName, newBranch);

        // Creating a commit in the branch

        var createdCommitInBranch = await CreateCommit(repoName, "Hello from the fork!", path, branchName, "A branch commit message");

        // Creating a pull request

        var pullRequest = new NewPullRequest("Nice title for the pull request", branchName, "master");
        var createdPullRequest = await _gitHubClient.PullRequest.Create(Helper.UserName, repoName, pullRequest);

        var data = new PullRequestData
        {
            PullRequestCommitId = createdCommitInBranch.Sha,
            PullRequestNumber = createdPullRequest.Number,
            RepoName = repoName,
            Repository = repository,
        };

        return data;
    }

    async Task<Commit> CreateCommit(string repoName, string blobContent, string treePath, string reference, string commitMessage)
    {
        // Creating a blob
        var blob = new NewBlob
        {
            Content = blobContent,
            Encoding = EncodingType.Utf8
        };

        var createdBlob = await _gitHubClient.GitDatabase.Blob.Create(Helper.UserName, repoName, blob);

        // Creating a tree
        var newTree = new NewTree();
        newTree.Tree.Add(new NewTreeItem
        {
            Type = TreeType.Blob,
            Mode = FileMode.File,
            Path = treePath,
            Sha = createdBlob.Sha,
        });

        var createdTree = await _gitHubClient.GitDatabase.Tree.Create(Helper.UserName, repoName, newTree);
        var treeSha = createdTree.Sha;

        // Creating a commit
        var parent = await _gitHubClient.GitDatabase.Reference.Get(Helper.UserName, repoName, reference);
        var commit = new NewCommit(commitMessage, treeSha, parent.Object.Sha);

        var createdCommit = await _gitHubClient.GitDatabase.Commit.Create(Helper.UserName, repoName, commit);
        await _gitHubClient.GitDatabase.Reference.Update(Helper.UserName, repoName, reference, new ReferenceUpdate(createdCommit.Sha));

        return createdCommit;
    }
}

class PullRequestData
{
    public Repository Repository { get; set; }
    public int PullRequestNumber { get; set; }
    public string PullRequestCommitId { get; set; }
    public string RepoName { get; set; }
}