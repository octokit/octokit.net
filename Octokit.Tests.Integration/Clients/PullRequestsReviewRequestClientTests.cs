using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class PullRequestsReviewRequestClientTests : IDisposable
{
    private readonly IGitHubClient _github;
    private readonly IPullRequestReviewRequestsClient _client;
    private readonly RepositoryContext _context;

    const string branchName = "new-branch";
    const string collaboratorLogin = "m-zuber-octokit-integration-tests";
    const string path = "CONTRIBUTING.md";

    public PullRequestsReviewRequestClientTests()
    {
        _github = Helper.GetAuthenticatedClient();

        _client = _github.PullRequest.ReviewRequest;

        _context = _github.CreateRepositoryContext("test-repo").Result;
        _github.Repository.Collaborator.Add(Helper.UserName, _context.RepositoryName, collaboratorLogin);
    }

    [IntegrationTest]
    public async Task CanGetAllWhenNone()
    {
        var pullRequest = await CreatePullRequest(_context);

        var reviewRequests = await _client.GetAll(Helper.UserName, _context.RepositoryName, pullRequest.Number);

        Assert.NotNull(reviewRequests);
        Assert.Empty(reviewRequests);
    }

    [IntegrationTest]
    public async Task CanCreateAndThenGetAll()
    {
        var pullRequest = await CreatePullRequest(_context);
        var reviewers = new List<string> { collaboratorLogin };
        var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

        await _client.Create(Helper.UserName, _context.RepositoryName, pullRequest.Number, reviewRequestToCreate);
        var reviewRequests = await _client.GetAll(Helper.UserName, _context.RepositoryName, pullRequest.Number);

        Assert.Equal(reviewers, reviewRequests.Select(rr => rr.Login));
    }

    [IntegrationTest]
    public async Task CanCreate()
    {
        var pullRequest = await CreatePullRequest(_context);
        var reviewers = new List<string> { collaboratorLogin };
        var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

        var pr = await _client.Create(Helper.UserName, _context.RepositoryName, pullRequest.Number, reviewRequestToCreate);

        Assert.Equal(reviewers, pr.RequestedReviewers.Select(rr => rr.Login));
    }

    [IntegrationTest]
    public async Task CanCreateAndDelete()
    {
        var pullRequest = await CreatePullRequest(_context);
        var reviewers = new List<string> { collaboratorLogin };
        var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

        await _client.Create(Helper.UserName, _context.RepositoryName, pullRequest.Number, reviewRequestToCreate);
        var reviewRequestsAfterCreate = await _client.GetAll(Helper.UserName, _context.RepositoryName, pullRequest.Number);
        await _client.Delete(Helper.UserName, _context.RepositoryName, pullRequest.Number, reviewRequestToCreate);
        var reviewRequestsAfterDelete = await _client.GetAll(Helper.UserName, _context.RepositoryName, pullRequest.Number);

        Assert.Equal(reviewers, reviewRequestsAfterCreate.Select(rr => rr.Login));
        Assert.Empty(reviewRequestsAfterDelete);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Creates the base state for testing (creates a repo, a commit in master, a branch, a commit in the branch and a pull request)
    /// </summary>
    /// <returns></returns>
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

    class PullRequestData
    {
        public int Number { get; set; }
        public string Sha { get; set; }
    }
}