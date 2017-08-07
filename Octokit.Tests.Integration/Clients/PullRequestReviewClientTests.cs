using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class PullRequestReviewsClientTests : IDisposable
{
    private readonly IGitHubClient _github;
    private readonly IPullRequestReviewsClient _client;
    private readonly RepositoryContext _context;

    const string branchName = "new-branch";
    const string path = "CONTRIBUTING.md";

    public PullRequestReviewsClientTests()
    {
        _github = Helper.GetAuthenticatedClient();

        _client = _github.PullRequest.Review;

        // We'll create a pull request that can be used by most tests
        _context = _github.CreateRepositoryContext("test-repo").Result;
    }


    [IntegrationTest]
    public async Task CanCreateAndRetrieveReview()
    {
        var pullRequest = await CreatePullRequest(_context);

        const string body = "A review comment message";

        var createdReview = await CreateReview(pullRequest.Sha, body, PullRequestReviewRequestEvents.Approve, new List<PullRequestReviewCommentCreate>(), pullRequest.Number);

        var reviewFromGitHub = await _client.GetReview(Helper.UserName, _context.RepositoryName, pullRequest.Number, createdReview.Id);

        AssertReviewCreate(reviewFromGitHub, body, pullRequest.Sha);
    }


    async Task<PullRequestReview> CreateReview(string commitId, string body, PullRequestReviewRequestEvents evt, List<PullRequestReviewCommentCreate> comments,  int pullRequestNumber)
    {
        var comment = new PullRequestReviewCreate()
        {
            CommitId = commitId,
            Body = body,
            Event = evt,
            Comments = comments
        };

        var createdReview = await _client.Create(Helper.UserName, _context.RepositoryName, pullRequestNumber, comment);

        AssertReviewCreate(createdReview, commitId, body);

        return createdReview;
    }

    async Task<PullRequestReview> CreateReview(string commitId, string body, string evt, List<PullRequestReviewCommentCreate> comments, long repoId, int pullRequestNumber)
    {
        var comment = new PullRequestReviewCreate()
        {
            CommitId = commitId,
            Body = body,
            Event = evt,
            Comments = comments
        };

        var createdReview = await _client.Create(repoId, pullRequestNumber, comment);

        AssertReviewCreate(createdReview, commitId, body);

        return createdReview;
    }

    static void AssertReviewCreate(PullRequestReview review, string commitId, string body)
    {
        Assert.NotNull(review);
        Assert.Equal(body, review.Body);
        Assert.Equal(commitId, review.CommitId);
    }

    class PullRequestData
    {
        public int Number { get; set; }
        public string Sha { get; set; }
    }

    async Task<PullRequestData> CreatePullRequest(RepositoryContext context, string branch = branchName)
    {
        string branchHead = "heads/" + branch;
        string branchRef = "refs/" + branchHead;


        var repoName = context.RepositoryName;

        // Creating a commit in master

        var createdCommitInMaster = await CreateCommit(repoName, "Hello World!", "README.md", "heads/master", "A master commit message");

        // Creating a branch

        var newBranch = new NewReference(branchRef, createdCommitInMaster.Sha);
        await _github.Git.Reference.Create(Helper.UserName, repoName, newBranch);

        // Creating a commit in the branch

        var createdCommitInBranch = await CreateCommit(repoName, "Hello from the fork!", path, branchHead, "A branch commit message");

        // Creating a pull request

        var pullRequest = new NewPullRequest("Nice title for the pull request", branch, "master");
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


    public void Dispose()
    {
        _context.Dispose();
    }
}