﻿using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using System;
using System.Threading.Tasks;
using Xunit;

public class PullRequestReviewCommentReactionsClientTests : IDisposable
{
    private readonly IGitHubClient _github;
    private readonly IPullRequestReviewCommentsClient _client;
    private readonly RepositoryContext _context;

    const string branchName = "new-branch";
    const string branchHead = "heads/" + branchName;
    const string branchRef = "refs/" + branchHead;
    const string path = "CONTRIBUTING.md";

    public PullRequestReviewCommentReactionsClientTests()
    {
        _github = Helper.GetAuthenticatedClient();

        _client = _github.PullRequest.Comment;

        // We'll create a pull request that can be used by most tests
        _context = _github.CreateRepositoryContext("test-repo").Result;
    }

    [IntegrationTest]
    public async Task CanCreateReaction()
    {
        var pullRequest = await CreatePullRequest(_context);

        const string body = "A review comment message";
        const int position = 1;

        var createdComment = await CreateComment(body, position, pullRequest.Sha, pullRequest.Number);

        var commentFromGitHub = await _client.GetComment(Helper.UserName, _context.RepositoryName, createdComment.Id);

        AssertComment(commentFromGitHub, body, position);

        var pullRequestReviewCommentReaction = await _github.Reaction.PullRequestReviewComment.Create(_context.RepositoryOwner, _context.RepositoryName, commentFromGitHub.Id, new NewReaction(ReactionType.Heart));

        Assert.NotNull(pullRequestReviewCommentReaction);

        Assert.IsType<Reaction>(pullRequestReviewCommentReaction);

        Assert.Equal(ReactionType.Heart, pullRequestReviewCommentReaction.Content);

        Assert.Equal(commentFromGitHub.User.Id, pullRequestReviewCommentReaction.User.Id);
    }

    /// <summary>
    /// Creates the base state for testing (creates a repo, a commit in master, a branch, a commit in the branch and a pull request)
    /// </summary>
    /// <returns></returns>
    async Task<PullRequestData> CreatePullRequest(RepositoryContext context)
    {
        var repoName = context.RepositoryName;

        // Creating a commit in master

        var createdCommitInMaster = await CreateCommit(repoName, "Hello World!", "README.md", "heads/master", "A master commit message");

        // Creating a branch

        var newBranch = new NewReference(branchRef, createdCommitInMaster.Sha);
        await _github.Git.Reference.Create(Helper.UserName, repoName, newBranch);

        // Creating a commit in the branch

        var createdCommitInBranch = await CreateCommit(repoName, "Hello from the fork!", path, branchHead, "A branch commit message");

        // Creating a pull request

        var pullRequest = new NewPullRequest("Nice title for the pull request", branchName, "master");
        var createdPullRequest = await _github.PullRequest.Create(Helper.UserName, repoName, pullRequest);

        var data = new PullRequestData
        {
            Sha = createdCommitInBranch.Sha,
            Number = createdPullRequest.Number
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

        var createdBlob = await _github.Git.Blob.Create(Helper.UserName, repoName, blob);

        // Creating a tree
        var newTree = new NewTree();
        newTree.Tree.Add(new NewTreeItem
        {
            Type = TreeType.Blob,
            Mode = FileMode.File,
            Path = treePath,
            Sha = createdBlob.Sha
        });

        var createdTree = await _github.Git.Tree.Create(Helper.UserName, repoName, newTree);
        var treeSha = createdTree.Sha;

        // Creating a commit
        var parent = await _github.Git.Reference.Get(Helper.UserName, repoName, reference);
        var commit = new NewCommit(commitMessage, treeSha, parent.Object.Sha);

        var createdCommit = await _github.Git.Commit.Create(Helper.UserName, repoName, commit);
        await _github.Git.Reference.Update(Helper.UserName, repoName, reference, new ReferenceUpdate(createdCommit.Sha));

        return createdCommit;
    }

    async Task<PullRequestReviewComment> CreateComment(string body, int position, string commitId, int number)
    {
        return await CreateComment(body, position, _context.RepositoryName, commitId, number);
    }

    async Task<PullRequestReviewComment> CreateComment(string body, int position, string repoName, string pullRequestCommitId, int pullRequestNumber)
    {
        var comment = new PullRequestReviewCommentCreate(body, pullRequestCommitId, path, position);

        var createdComment = await _client.Create(Helper.UserName, repoName, pullRequestNumber, comment);

        AssertComment(createdComment, body, position);

        return createdComment;
    }

    static void AssertComment(PullRequestReviewComment comment, string body, int position)
    {
        Assert.NotNull(comment);
        Assert.Equal(body, comment.Body);
        Assert.Equal(position, comment.Position);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    class PullRequestData
    {
        public int Number { get; set; }
        public string Sha { get; set; }
    }
}

