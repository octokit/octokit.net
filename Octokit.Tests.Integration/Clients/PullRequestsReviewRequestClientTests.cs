using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class PullRequestsReviewRequestClientTests
{
    const string collaboratorLogin = "octocat";

    public class TheGetAllMethod : IDisposable
    {
        private readonly IGitHubClient _github;
        private readonly IPullRequestReviewRequestsClient _client;
        private readonly RepositoryContext _context;

        public TheGetAllMethod()
        {
            _github = Helper.GetAuthenticatedClient();

            _client = _github.PullRequest.ReviewRequest;

            _context = _github.CreateRepositoryContext("test-repo").Result;
            _github.Repository.Collaborator.Add(_context.RepositoryOwner, _context.RepositoryName, collaboratorLogin);
        }

        [IntegrationTest]
        public async Task CanGetAllWhenNone()
        {
            var pullRequestId = await CreateTheWorld(_github, _context);

            var reviewRequests = await _client.GetAll(_context.RepositoryOwner, _context.RepositoryName, pullRequestId);

            Assert.NotNull(reviewRequests);
            Assert.Empty(reviewRequests);
        }

        [IntegrationTest]
        public async Task CanGetAllWhenNoneWithRepositoryId()
        {
            var pullRequestId = await CreateTheWorld(_github, _context);

            var reviewRequests = await _client.GetAll(_context.RepositoryId, pullRequestId);

            Assert.NotNull(reviewRequests);
            Assert.Empty(reviewRequests);
        }

        [IntegrationTest]
        public async Task CanCreateAndThenGetAll()
        {
            var pullRequestId = await CreateTheWorld(_github, _context);
            var reviewers = new List<string> { collaboratorLogin };
            var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

            await _client.Create(_context.RepositoryOwner, _context.RepositoryName, pullRequestId, reviewRequestToCreate);
            var reviewRequests = await _client.GetAll(_context.RepositoryOwner, _context.RepositoryName, pullRequestId);

            Assert.Equal(reviewers, reviewRequests.Select(rr => rr.Login));
        }

        [IntegrationTest]
        public async Task CanCreateAndThenGetAllWithRepositoryId()
        {
            var pullRequestId = await CreateTheWorld(_github, _context);
            var reviewers = new List<string> { collaboratorLogin };
            var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

            await _client.Create(_context.RepositoryId,  pullRequestId, reviewRequestToCreate);
            var reviewRequests = await _client.GetAll(_context.RepositoryId, pullRequestId);

            Assert.Equal(reviewers, reviewRequests.Select(rr => rr.Login));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public class TheDeleteMethod : IDisposable
    {
        private readonly IGitHubClient _github;
        private readonly IPullRequestReviewRequestsClient _client;
        private readonly RepositoryContext _context;

        public TheDeleteMethod()
        {
            _github = Helper.GetAuthenticatedClient();

            _client = _github.PullRequest.ReviewRequest;

            _context = _github.CreateRepositoryContext("test-repo").Result;
            _github.Repository.Collaborator.Add(_context.RepositoryOwner, _context.RepositoryName, collaboratorLogin);
        }

        [IntegrationTest]
        public async Task CanCreateAndDelete()
        {
            var pullRequestId = await CreateTheWorld(_github, _context);
            var reviewers = new List<string> { collaboratorLogin };
            var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

            await _client.Create(_context.RepositoryOwner, _context.RepositoryName, pullRequestId, reviewRequestToCreate);
            var reviewRequestsAfterCreate = await _client.GetAll(_context.RepositoryOwner, _context.RepositoryName, pullRequestId);
            await _client.Delete(_context.RepositoryOwner, _context.RepositoryName, pullRequestId, reviewRequestToCreate);
            var reviewRequestsAfterDelete = await _client.GetAll(_context.RepositoryOwner, _context.RepositoryName, pullRequestId);

            Assert.Equal(reviewers, reviewRequestsAfterCreate.Select(rr => rr.Login));
            Assert.Empty(reviewRequestsAfterDelete);
        }

        [IntegrationTest]
        public async Task CanCreateAndDeleteWithRepositoryId()
        {
            var pullRequestId = await CreateTheWorld(_github, _context);
            var reviewers = new List<string> { collaboratorLogin };
            var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

            await _client.Create(_context.RepositoryId, pullRequestId, reviewRequestToCreate);
            var reviewRequestsAfterCreate = await _client.GetAll(_context.RepositoryId,  pullRequestId);
            await _client.Delete(_context.RepositoryId, pullRequestId, reviewRequestToCreate);
            var reviewRequestsAfterDelete = await _client.GetAll(_context.RepositoryId,  pullRequestId);

            Assert.Equal(reviewers, reviewRequestsAfterCreate.Select(rr => rr.Login));
            Assert.Empty(reviewRequestsAfterDelete);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public class TheCreateMethod : IDisposable
    {
        private readonly IGitHubClient _github;
        private readonly IPullRequestReviewRequestsClient _client;
        private readonly RepositoryContext _context;

        public TheCreateMethod()
        {
            _github = Helper.GetAuthenticatedClient();

            _client = _github.PullRequest.ReviewRequest;

            _context = _github.CreateRepositoryContext("test-repo").Result;
            _github.Repository.Collaborator.Add(_context.RepositoryOwner, _context.RepositoryName, collaboratorLogin);
        }

        [IntegrationTest]
        public async Task CanCreate()
        {
            var pullRequestId = await CreateTheWorld(_github, _context);
            var reviewers = new List<string> { collaboratorLogin };
            var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

            var pr = await _client.Create(_context.RepositoryOwner, _context.RepositoryName, pullRequestId, reviewRequestToCreate);

            Assert.Equal(reviewers, pr.RequestedReviewers.Select(rr => rr.Login));
        }

        [IntegrationTest]
        public async Task CanCreateWithRepositoryId()
        {
            var pullRequestId = await CreateTheWorld(_github, _context);
            var reviewers = new List<string> { collaboratorLogin };
            var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

            var pr = await _client.Create(_context.RepositoryId, pullRequestId, reviewRequestToCreate);

            Assert.Equal(reviewers, pr.RequestedReviewers.Select(rr => rr.Login));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    static async Task<int> CreateTheWorld(IGitHubClient github, RepositoryContext context)
    {
        var master = await github.Git.Reference.Get(context.RepositoryOwner, context.RepositoryName, "heads/master");

        // create new commit for master branch
        var newMasterTree = await CreateTree(github, context, new Dictionary<string, string> { { "README.md", "Hello World!" } });
        var newMaster = await CreateCommit(github, context, "baseline for pull request", newMasterTree.Sha, master.Object.Sha);

        // update master
        await github.Git.Reference.Update(context.RepositoryOwner, context.RepositoryName, "heads/master", new ReferenceUpdate(newMaster.Sha));

        // create new commit for feature branch
        var featureBranchTree = await CreateTree(github, context, new Dictionary<string, string> { { "README.md", "I am overwriting this blob with something new" } });
        var featureBranchCommit = await CreateCommit(github, context, "this is the commit to merge into the pull request", featureBranchTree.Sha, newMaster.Sha);

        var featureBranchTree2 = await CreateTree(github, context, new Dictionary<string, string> { { "README.md", "I am overwriting this blob with something new a 2nd time" } });
        var featureBranchCommit2 = await CreateCommit(github, context, "this is a 2nd commit to merge into the pull request", featureBranchTree2.Sha, featureBranchCommit.Sha);

        // create branch
        await github.Git.Reference.Create(context.RepositoryOwner, context.RepositoryName, new NewReference("refs/heads/my-branch", featureBranchCommit2.Sha));

        // Creating a pull request
        var pullRequest = new NewPullRequest("Nice title for the pull request", "my-branch", "master");
        var createdPullRequest = await github.PullRequest.Create(context.RepositoryOwner, context.RepositoryName, pullRequest);

        return createdPullRequest.Number;
    }

    static async Task<TreeResponse> CreateTree(IGitHubClient github, RepositoryContext context, IEnumerable<KeyValuePair<string, string>> treeContents)
    {
        var collection = new List<NewTreeItem>();

        foreach (var c in treeContents)
        {
            var baselineBlob = new NewBlob
            {
                Content = c.Value,
                Encoding = EncodingType.Utf8
            };
            var baselineBlobResult = await github.Git.Blob.Create(context.RepositoryOwner, context.RepositoryName, baselineBlob);

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

        return await github.Git.Tree.Create(context.RepositoryOwner, context.RepositoryName, newTree);
    }

    static async Task<Commit> CreateCommit(IGitHubClient github, RepositoryContext context, string message, string sha, string parent)
    {
        var newCommit = new NewCommit(message, sha, parent);
        return await github.Git.Commit.Create(context.RepositoryOwner, context.RepositoryName, newCommit);
    }
}