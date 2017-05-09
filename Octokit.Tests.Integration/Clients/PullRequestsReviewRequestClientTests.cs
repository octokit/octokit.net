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
    private static readonly string[] _collaboratorLogins = {
        "octocat",
        "gdziadkiewicz-test",
        "ryangribble-testorg"
    };

    public class PullRequestReviewRequestClientTestsBase
    {
        internal readonly IGitHubClient Github;
        internal readonly IPullRequestReviewRequestsClient Client;
        internal readonly RepositoryContext Context;

        public PullRequestReviewRequestClientTestsBase()
        {
            Github = Helper.GetAuthenticatedClient();

            Client = Github.PullRequest.ReviewRequest;

            Context = Github.CreateRepositoryContext("test-repo").Result;

            Task.WaitAll(_collaboratorLogins.Select(AddCollaborator).ToArray());
        }

        private Task AddCollaborator(string collaborator) => Github.Repository.Collaborator.Add(Context.RepositoryOwner, Context.RepositoryName, collaborator);
    }

    public class TheGetAllMethod : PullRequestReviewRequestClientTestsBase, IDisposable
    {
        [IntegrationTest]
        public async Task CanGetAllWhenNone()
        {
            var pullRequestId = await CreateTheWorld(Github, Context);

            var reviewRequests = await Client.GetAll(Context.RepositoryOwner, Context.RepositoryName, pullRequestId);

            Assert.NotNull(reviewRequests);
            Assert.Empty(reviewRequests);
        }

        [IntegrationTest]
        public async Task CanGetAllWhenNoneWithRepositoryId()
        {
            var pullRequestId = await CreateTheWorld(Github, Context);

            var reviewRequests = await Client.GetAll(Context.RepositoryId, pullRequestId);

            Assert.NotNull(reviewRequests);
            Assert.Empty(reviewRequests);
        }

        [IntegrationTest]
        public async Task CanCreateAndThenGetAll()
        {
            var pullRequestId = await CreateTheWorld(Github, Context);
            var reviewers = _collaboratorLogins.ToList();
            var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

            await Client.Create(Context.RepositoryOwner, Context.RepositoryName, pullRequestId, reviewRequestToCreate);
            var reviewRequests = await Client.GetAll(Context.RepositoryOwner, Context.RepositoryName, pullRequestId);

            Assert.Equal(reviewers, reviewRequests.Select(rr => rr.Login));
        }

        [IntegrationTest]
        public async Task CanCreateAndThenGetAllWithRepositoryId()
        {
            var pullRequestId = await CreateTheWorld(Github, Context);
            var reviewers = _collaboratorLogins.ToList();
            var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

            await Client.Create(Context.RepositoryId,  pullRequestId, reviewRequestToCreate);
            var reviewRequests = await Client.GetAll(Context.RepositoryId, pullRequestId);

            Assert.Equal(reviewers, reviewRequests.Select(rr => rr.Login));
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReviewRequestsWithStart()
        {
            var pullRequestId = await CreateTheWorld(Github, Context);
            var reviewers = _collaboratorLogins.ToList();
            var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

            var options = new ApiOptions
            {
                PageSize = 2,
                PageCount = 1,
                StartPage = 2
            };

            await Client.Create(Context.RepositoryOwner, Context.RepositoryName, pullRequestId, reviewRequestToCreate);
            var reviewRequests = await Client.GetAll(Context.RepositoryOwner, Context.RepositoryName, pullRequestId, options);

            Assert.Equal(1, reviewRequests.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReviewRequestsWithStartWithRepositoryId()
        {
            var pullRequestId = await CreateTheWorld(Github, Context);
            var reviewers = _collaboratorLogins.ToList();
            var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 2,
                StartPage = 2
            };

            await Client.Create(Context.RepositoryId, pullRequestId, reviewRequestToCreate);
            var reviewRequests = await Client.GetAll(Context.RepositoryId, pullRequestId, options);

            Assert.Equal(2, reviewRequests.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var pullRequestId = await CreateTheWorld(Github, Context);
            var reviewers = _collaboratorLogins.ToList();
            var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

            var startOptions = new ApiOptions
            {
                PageSize = 2,
                PageCount = 1
            };

            await Client.Create(Context.RepositoryOwner, Context.RepositoryName, pullRequestId, reviewRequestToCreate);
            var firstPage = await Client.GetAll(Context.RepositoryOwner, Context.RepositoryName, pullRequestId, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 2,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await Client.GetAll(Context.RepositoryOwner, Context.RepositoryName, pullRequestId, skipStartOptions);

            Assert.Equal(2, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            Assert.NotEqual(firstPage[1].Id, secondPage[0].Id);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPageWithRepositoryId()
        {
            var pullRequestId = await CreateTheWorld(Github, Context);
            var reviewers = _collaboratorLogins.ToList();
            var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

            var startOptions = new ApiOptions
            {
                PageSize = 2,
                PageCount = 1
            };

            await Client.Create(Context.RepositoryId, pullRequestId, reviewRequestToCreate);
            var firstPage = await Client.GetAll(Context.RepositoryId, pullRequestId, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 2,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await Client.GetAll(Context.RepositoryId, pullRequestId, skipStartOptions);

            Assert.Equal(2, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            Assert.NotEqual(firstPage[1].Id, secondPage[0].Id);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }

    public class TheDeleteMethod : PullRequestReviewRequestClientTestsBase, IDisposable
    {
        [IntegrationTest]
        public async Task CanCreateAndDelete()
        {
            var pullRequestId = await CreateTheWorld(Github, Context);
            var reviewers = _collaboratorLogins.ToList();
            var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

            await Client.Create(Context.RepositoryOwner, Context.RepositoryName, pullRequestId, reviewRequestToCreate);
            var reviewRequestsAfterCreate = await Client.GetAll(Context.RepositoryOwner, Context.RepositoryName, pullRequestId);
            await Client.Delete(Context.RepositoryOwner, Context.RepositoryName, pullRequestId, reviewRequestToCreate);
            var reviewRequestsAfterDelete = await Client.GetAll(Context.RepositoryOwner, Context.RepositoryName, pullRequestId);

            Assert.Equal(reviewers, reviewRequestsAfterCreate.Select(rr => rr.Login));
            Assert.Empty(reviewRequestsAfterDelete);
        }

        [IntegrationTest]
        public async Task CanCreateAndDeleteWithRepositoryId()
        {
            var pullRequestId = await CreateTheWorld(Github, Context);
            var reviewers = _collaboratorLogins.ToList();
            var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

            await Client.Create(Context.RepositoryId, pullRequestId, reviewRequestToCreate);
            var reviewRequestsAfterCreate = await Client.GetAll(Context.RepositoryId,  pullRequestId);
            await Client.Delete(Context.RepositoryId, pullRequestId, reviewRequestToCreate);
            var reviewRequestsAfterDelete = await Client.GetAll(Context.RepositoryId,  pullRequestId);

            Assert.Equal(reviewers, reviewRequestsAfterCreate.Select(rr => rr.Login));
            Assert.Empty(reviewRequestsAfterDelete);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }

    public class TheCreateMethod : PullRequestReviewRequestClientTestsBase, IDisposable
    {
        [IntegrationTest]
        public async Task CanCreate()
        {
            var pullRequestId = await CreateTheWorld(Github, Context);
            var reviewers = _collaboratorLogins.ToList();
            var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

            var pr = await Client.Create(Context.RepositoryOwner, Context.RepositoryName, pullRequestId, reviewRequestToCreate);

            Assert.Equal(reviewers, pr.RequestedReviewers.Select(rr => rr.Login));
        }

        [IntegrationTest]
        public async Task CanCreateWithRepositoryId()
        {
            var pullRequestId = await CreateTheWorld(Github, Context);
            var reviewers = _collaboratorLogins.ToList();
            var reviewRequestToCreate = new PullRequestReviewRequest(reviewers);

            var pr = await Client.Create(Context.RepositoryId, pullRequestId, reviewRequestToCreate);

            Assert.Equal(reviewers, pr.RequestedReviewers.Select(rr => rr.Login));
        }

        public void Dispose()
        {
            Context.Dispose();
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