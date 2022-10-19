using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class PullRequestReviewRequestsClientTests
{
    private static readonly string[] _collaboratorLogins = {
        "octokitnet-test1",
        "octokitnet-test2"
    };

    public class PullRequestReviewRequestClientTestsBase : IDisposable
    {
        internal readonly IGitHubClient _github;
        internal readonly IPullRequestReviewRequestsClient _client;
        internal readonly RepositoryContext _context;

        public PullRequestReviewRequestClientTestsBase()
        {
            _github = Helper.GetAuthenticatedClient();

            _client = _github.PullRequest.ReviewRequest;

            _context = _github.CreateRepositoryContextWithAutoInit("test-repo").Result;

            Task.WaitAll(_collaboratorLogins.Select(AddCollaborator).ToArray());
        }

        private Task AddCollaborator(string collaborator) => _github.Repository.Collaborator.Add(_context.RepositoryOwner, _context.RepositoryName, collaborator);

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public class TheGetAllMethod : PullRequestReviewRequestClientTestsBase
    {
        [IntegrationTest]
        public async Task GetsNoRequestsWhenNoneExist()
        {
            var number = await CreateTheWorld(_github, _context, createReviewRequests: false);

            var reviewRequests = await _client.Get(_context.RepositoryOwner, _context.RepositoryName, number);

            Assert.NotNull(reviewRequests);
            Assert.Empty(reviewRequests.Users);
            Assert.Empty(reviewRequests.Teams);
        }

        [IntegrationTest]
        public async Task GetsNoRequestsWhenNoneExistWithRepositoryId()
        {
            var number = await CreateTheWorld(_github, _context, createReviewRequests: false);

            var reviewRequests = await _client.Get(_context.RepositoryId, number);

            Assert.NotNull(reviewRequests);
            Assert.Empty(reviewRequests.Users);
            Assert.Empty(reviewRequests.Teams);
        }

        [IntegrationTest]
        public async Task GetsRequests()
        {
            var number = await CreateTheWorld(_github, _context);

            var reviewRequests = await _client.Get(_context.RepositoryOwner, _context.RepositoryName, number);

            Assert.Equal(_collaboratorLogins, reviewRequests.Users.Select(rr => rr.Login));
        }

        [IntegrationTest]
        public async Task GetsRequestsWithRepositoryId()
        {
            var number = await CreateTheWorld(_github, _context);

            var reviewRequests = await _client.Get(_context.RepositoryId, number);

            Assert.Equal(_collaboratorLogins, reviewRequests.Users.Select(rr => rr.Login));
        }
    }

    public class TheDeleteMethod : PullRequestReviewRequestClientTestsBase
    {
        [IntegrationTest]
        public async Task DeletesRequests()
        {
            var number = await CreateTheWorld(_github, _context);

            var reviewRequestsBeforeDelete = await _client.Get(_context.RepositoryOwner, _context.RepositoryName, number);
            var reviewRequestToCreate = PullRequestReviewRequest.ForReviewers(_collaboratorLogins);
            await _client.Delete(_context.RepositoryOwner, _context.RepositoryName, number, reviewRequestToCreate);
            var reviewRequestsAfterDelete = await _client.Get(_context.RepositoryOwner, _context.RepositoryName, number);

            Assert.NotEmpty(reviewRequestsBeforeDelete.Users);
            Assert.Empty(reviewRequestsAfterDelete.Users);
        }

        [IntegrationTest]
        public async Task DeletesRequestsWithRepositoryId()
        {
            var number = await CreateTheWorld(_github, _context);

            var reviewRequestsBeforeDelete = await _client.Get(_context.RepositoryId, number);
            var reviewRequestToCreate = PullRequestReviewRequest.ForReviewers(_collaboratorLogins);
            await _client.Delete(_context.RepositoryId, number, reviewRequestToCreate);
            var reviewRequestsAfterDelete = await _client.Get(_context.RepositoryId, number);

            Assert.NotEmpty(reviewRequestsBeforeDelete.Users);
            Assert.Empty(reviewRequestsAfterDelete.Users);
        }
    }

    public class TheCreateMethod : PullRequestReviewRequestClientTestsBase, IDisposable
    {
        [IntegrationTest]
        public async Task CreatesRequests()
        {
            var number = await CreateTheWorld(_github, _context, createReviewRequests: false);
            var reviewRequestToCreate = PullRequestReviewRequest.ForReviewers(_collaboratorLogins);

            var pr = await _client.Create(_context.RepositoryOwner, _context.RepositoryName, number, reviewRequestToCreate);

            Assert.Equal(_collaboratorLogins.ToList(), pr.RequestedReviewers.Select(rr => rr.Login));
        }

        [IntegrationTest]
        public async Task CreatesRequestsWithRepositoryId()
        {
            var number = await CreateTheWorld(_github, _context, createReviewRequests: false);
            var reviewRequestToCreate = PullRequestReviewRequest.ForReviewers(_collaboratorLogins);

            var pr = await _client.Create(_context.RepositoryId, number, reviewRequestToCreate);

            Assert.Equal(_collaboratorLogins.ToList(), pr.RequestedReviewers.Select(rr => rr.Login));
        }
    }

    static async Task<int> CreateTheWorld(IGitHubClient github, RepositoryContext context, bool createReviewRequests = true)
    {
        var main = await github.Git.Reference.Get(context.RepositoryOwner, context.RepositoryName, "heads/main");

        // create new commit for main branch
        var newMainTree = await CreateTree(github, context, new Dictionary<string, string> { { "README.md", "Hello World!" } });
        var newMain = await CreateCommit(github, context, "baseline for pull request", newMainTree.Sha, main.Object.Sha);

        // update main
        await github.Git.Reference.Update(context.RepositoryOwner, context.RepositoryName, "heads/main", new ReferenceUpdate(newMain.Sha));

        // create new commit for feature branch
        var featureBranchTree = await CreateTree(github, context, new Dictionary<string, string> { { "README.md", "I am overwriting this blob with something new" } });
        var featureBranchCommit = await CreateCommit(github, context, "this is the commit to merge into the pull request", featureBranchTree.Sha, newMain.Sha);

        var featureBranchTree2 = await CreateTree(github, context, new Dictionary<string, string> { { "README.md", "I am overwriting this blob with something new a 2nd time" } });
        var featureBranchCommit2 = await CreateCommit(github, context, "this is a 2nd commit to merge into the pull request", featureBranchTree2.Sha, featureBranchCommit.Sha);

        // create branch
        await github.Git.Reference.Create(context.RepositoryOwner, context.RepositoryName, new NewReference("refs/heads/my-branch", featureBranchCommit2.Sha));

        // create a pull request
        var pullRequest = new NewPullRequest("Nice title for the pull request", "my-branch", "main");
        var createdPullRequest = await github.PullRequest.Create(context.RepositoryOwner, context.RepositoryName, pullRequest);

        // Create review requests (optional)
        if (createReviewRequests)
        {
            var reviewRequest = PullRequestReviewRequest.ForReviewers(_collaboratorLogins);
            await github.PullRequest.ReviewRequest.Create(context.RepositoryOwner, context.RepositoryName, createdPullRequest.Number, reviewRequest);
        }

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
