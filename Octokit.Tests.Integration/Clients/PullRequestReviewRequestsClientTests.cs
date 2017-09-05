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

    public abstract class PullRequestReviewRequestClientFixtureBase : IAsyncLifetime, IDisposable
    {
        public IGitHubClient GitHub { get; private set; }
        public IPullRequestReviewRequestsClient Client { get; private set; }
        public RepositoryContext Context { get; private set; }
        public int Number { get; private set; }

        public virtual async Task InitializeAsync()
        {
            GitHub = Helper.GetAuthenticatedClient();
            Client = GitHub.PullRequest.ReviewRequest;
            Context = await GitHub.CreateRepositoryContext("test-repo");
            await Task.WhenAll(_collaboratorLogins.Select(AddCollaborator).ToArray());
            Number = await CreatePullRequest();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        public void Dispose() => Context.Dispose();

        protected abstract Task<int> CreatePullRequest();

        private Task AddCollaborator(string collaborator) => GitHub.Repository.Collaborator.Add(Context.RepositoryOwner, Context.RepositoryName, collaborator);

        protected async Task<int> CreateTheWorld(bool createReviewRequests = true)
        {
            var master = await GitHub.Git.Reference.Get(Context.RepositoryOwner, Context.RepositoryName, "heads/master");

            // create new commit for master branch
            var newMasterTree = await CreateTree(new Dictionary<string, string> { { "README.md", "Hello World!" } });
            var newMaster = await CreateCommit("baseline for pull request", newMasterTree.Sha, master.Object.Sha);

            // update master
            await GitHub.Git.Reference.Update(Context.RepositoryOwner, Context.RepositoryName, "heads/master", new ReferenceUpdate(newMaster.Sha));

            // create new commit for feature branch
            var featureBranchTree = await CreateTree(new Dictionary<string, string> { { "README.md", "I am overwriting this blob with something new" } });
            var featureBranchCommit = await CreateCommit("this is the commit to merge into the pull request", featureBranchTree.Sha, newMaster.Sha);

            var featureBranchTree2 = await CreateTree(new Dictionary<string, string> { { "README.md", "I am overwriting this blob with something new a 2nd time" } });
            var featureBranchCommit2 = await CreateCommit("this is a 2nd commit to merge into the pull request", featureBranchTree2.Sha, featureBranchCommit.Sha);

            // create branch
            await GitHub.Git.Reference.Create(Context.RepositoryOwner, Context.RepositoryName, new NewReference("refs/heads/my-branch", featureBranchCommit2.Sha));

            // create a pull request
            var pullRequest = new NewPullRequest("Nice title for the pull request", "my-branch", "master");
            var createdPullRequest = await GitHub.PullRequest.Create(Context.RepositoryOwner, Context.RepositoryName, pullRequest);

            // Create review requests (optional)
            if (createReviewRequests)
            {
                var reviewRequest = new PullRequestReviewRequest(_collaboratorLogins);
                await GitHub.PullRequest.ReviewRequest.Create(Context.RepositoryOwner, Context.RepositoryName, createdPullRequest.Number, reviewRequest);
            }

            return createdPullRequest.Number;
        }

        private async Task<TreeResponse> CreateTree(IEnumerable<KeyValuePair<string, string>> treeContents)
        {
            var collection = new List<NewTreeItem>();

            foreach (var c in treeContents)
            {
                var baselineBlob = new NewBlob
                {
                    Content = c.Value,
                    Encoding = EncodingType.Utf8
                };
                var baselineBlobResult = await GitHub.Git.Blob.Create(Context.RepositoryOwner, Context.RepositoryName, baselineBlob);

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

            return await GitHub.Git.Tree.Create(Context.RepositoryOwner, Context.RepositoryName, newTree);
        }

        private Task<Commit> CreateCommit(string message, string sha, string parent)
        {
            var newCommit = new NewCommit(message, sha, parent);
            return GitHub.Git.Commit.Create(Context.RepositoryOwner, Context.RepositoryName, newCommit);
        }
    }

    public class WhenNoRequestsExistFixture : PullRequestReviewRequestClientFixtureBase
    {
        protected override Task<int> CreatePullRequest() =>
            CreateTheWorld(createReviewRequests: false);
    }

    public class WhenNoRequestsExistTheGetAllMethod : IClassFixture<WhenNoRequestsExistFixture>
    {
        private readonly WhenNoRequestsExistFixture _fixture;

        public WhenNoRequestsExistTheGetAllMethod(WhenNoRequestsExistFixture fixture)
        {
            _fixture = fixture;
        }

        [IntegrationTest]
        public async Task GetsNoRequestsWhenNoneExist()
        {
            var reviewRequests = await _fixture.Client.GetAll(
                _fixture.Context.RepositoryOwner,
                _fixture.Context.RepositoryName,
                _fixture.Number);

            Assert.NotNull(reviewRequests);
            Assert.Empty(reviewRequests);
        }

        [IntegrationTest]
        public async Task GetsNoRequestsWhenNoneExistWithRepositoryId()
        {
            var reviewRequests = await _fixture.Client.GetAll(
                _fixture.Context.RepositoryId,
                _fixture.Number);

            Assert.NotNull(reviewRequests);
            Assert.Empty(reviewRequests);
        }
    }

    public class WhenRequestsExistFixture : PullRequestReviewRequestClientFixtureBase
    {
        protected override Task<int> CreatePullRequest() =>
            CreateTheWorld();
    }

    public class WhenRequestsExistTheGetAllMethod : IClassFixture<WhenRequestsExistFixture>
    {
        private readonly WhenRequestsExistFixture _fixture;

        public WhenRequestsExistTheGetAllMethod(WhenRequestsExistFixture fixture)
        {
            _fixture = fixture;
        }

        [IntegrationTest]
        public async Task GetsRequests()
        {
            var reviewRequests = await _fixture.Client.GetAll(
                _fixture.Context.RepositoryOwner,
                _fixture.Context.RepositoryName,
                _fixture.Number);

            Assert.Equal(_collaboratorLogins, reviewRequests.Select(rr => rr.Login));
        }

        [IntegrationTest]
        public async Task GetsRequestsWithRepositoryId()
        {
            var reviewRequests = await _fixture.Client.GetAll(_fixture.Context.RepositoryId, _fixture.Number);

            Assert.Equal(_collaboratorLogins, reviewRequests.Select(rr => rr.Login));
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReviewRequestsWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var reviewRequests = await _fixture.Client.GetAll(_fixture.Context.RepositoryOwner, _fixture.Context.RepositoryName, _fixture.Number, options);

            Assert.Equal(1, reviewRequests.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReviewRequestsWithStartWithRepositoryId()
        {
            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var reviewRequests = await _fixture.Client.GetAll(_fixture.Context.RepositoryId, _fixture.Number, options);

            Assert.Equal(1, reviewRequests.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var firstPage = await _fixture.Client.GetAll(
                _fixture.Context.RepositoryOwner,
                _fixture.Context.RepositoryName,
                _fixture.Number,
                startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _fixture.Client.GetAll(
                _fixture.Context.RepositoryOwner,
                _fixture.Context.RepositoryName,
                _fixture.Number,
                skipStartOptions);

            Assert.Equal(1, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPageWithRepositoryId()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var firstPage = await _fixture.Client.GetAll(
                _fixture.Context.RepositoryId,
                _fixture.Number,
                startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _fixture.Client.GetAll(
                _fixture.Context.RepositoryId,
                _fixture.Number,
                skipStartOptions);

            Assert.Equal(1, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
        }
    }

    public class TheDeleteMethod : WhenRequestsExistFixture
    {
        [IntegrationTest]
        public async Task DeletesRequests()
        {
            var reviewRequestsBeforeDelete = await Client.GetAll(Context.RepositoryOwner, Context.RepositoryName, Number);
            var reviewRequestToCreate = new PullRequestReviewRequest(_collaboratorLogins);
            await Client.Delete(Context.RepositoryOwner, Context.RepositoryName, Number, reviewRequestToCreate);
            var reviewRequestsAfterDelete = await Client.GetAll(Context.RepositoryOwner, Context.RepositoryName, Number);

            Assert.NotEmpty(reviewRequestsBeforeDelete);
            Assert.Empty(reviewRequestsAfterDelete);
        }

        [IntegrationTest]
        public async Task DeletesRequestsWithRepositoryId()
        {
            var reviewRequestsBeforeDelete = await Client.GetAll(Context.RepositoryId, Number);
            var reviewRequestToCreate = new PullRequestReviewRequest(_collaboratorLogins);
            await Client.Delete(Context.RepositoryId, Number, reviewRequestToCreate);
            var reviewRequestsAfterDelete = await Client.GetAll(Context.RepositoryId, Number);

            Assert.NotEmpty(reviewRequestsBeforeDelete);
            Assert.Empty(reviewRequestsAfterDelete);
        }
    }

    public class TheCreateMethod : WhenRequestsExistFixture
    {
        [IntegrationTest]
        public async Task CreatesRequests()
        {
            var reviewRequestToCreate = new PullRequestReviewRequest(_collaboratorLogins);

            var pr = await Client.Create(Context.RepositoryOwner, Context.RepositoryName, Number, reviewRequestToCreate);

            Assert.Equal(_collaboratorLogins.ToList(), pr.RequestedReviewers.Select(rr => rr.Login));
        }

        [IntegrationTest]
        public async Task CreatesRequestsWithRepositoryId()
        {
            var reviewRequestToCreate = new PullRequestReviewRequest(_collaboratorLogins);

            var pr = await Client.Create(Context.RepositoryId, Number, reviewRequestToCreate);

            Assert.Equal(_collaboratorLogins.ToList(), pr.RequestedReviewers.Select(rr => rr.Login));
        }
    }
}