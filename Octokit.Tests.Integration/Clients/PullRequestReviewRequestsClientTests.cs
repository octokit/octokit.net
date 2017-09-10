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
    public abstract class PullRequestReviewRequestClientFixtureBase : IAsyncLifetime, IDisposable
    {
        private IGitHubClient _githubTwo;

        public IGitHubClient GitHub { get; private set; }
        public IPullRequestReviewRequestsClient Client { get; private set; }
        public RepositoryContext Context { get; private set; }
        public int Number { get; private set; }
        public string CollaboratorLogin { get; private set; }

        public IReadOnlyList<string> CollaboratorLoginAsList =>
            new[] { CollaboratorLogin }.ToList().AsReadOnly();

        public virtual async Task InitializeAsync()
        {
            _githubTwo = Helper.GetAuthenticatedClient(useSecondUser: true);
            GitHub = Helper.GetAuthenticatedClient();
            Client = GitHub.PullRequest.ReviewRequest;
            Context = await GitHub.CreateRepositoryContext("test-repo");

            CollaboratorLogin = Helper.CredentialsSecondUser.Login;
            await AddCollaborator(CollaboratorLogin);

            Number = await CreatePullRequest();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        public void Dispose() => Context.Dispose();

        protected abstract Task<int> CreatePullRequest();

        private async Task AddCollaborator(string collaborator)
        {
            await GitHub.Repository.Collaborator.Add(Context.RepositoryOwner, Context.RepositoryName, collaborator);

            var invitations = await _githubTwo.Repository.Invitation.GetAllForCurrent();
            Assert.Single(invitations);

            await _githubTwo.Repository.Invitation.Accept(invitations[0].Id);
        }

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
                var reviewRequest = new PullRequestReviewRequest(CollaboratorLoginAsList);
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

    public class WhenNoRequestExists : PullRequestReviewRequestClientFixtureBase
    {
        protected override Task<int> CreatePullRequest() =>
            CreateTheWorld(createReviewRequests: false);
    }

    public class WhenNoRequestExistsTheGetAllMethod : IClassFixture<WhenNoRequestExists>
    {
        private readonly WhenNoRequestExists _fixture;

        public WhenNoRequestExistsTheGetAllMethod(WhenNoRequestExists fixture)
        {
            _fixture = fixture;
        }

        [DualAccountTest]
        public async Task GetsNoRequestsWhenNoneExist()
        {
            var reviewRequests = await _fixture.Client.GetAll(
                _fixture.Context.RepositoryOwner,
                _fixture.Context.RepositoryName,
                _fixture.Number);

            Assert.NotNull(reviewRequests);
            Assert.Empty(reviewRequests);
        }

        [DualAccountTest]
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

    public class WhenRequestsExistTheGetAllMethod
    {
        private const string _repositoryName = "pr-review-request";
        private const string _repositoryOwner = "octokitnet-test";
        private const long _repositoryId = 102994692;
        private const int _pullRequestNumber = 1;

        private static readonly IEnumerable<string> _expectedCollaborators = new[] { "ryangribble-test2", "octokitnet-test2" };
        private readonly IPullRequestReviewRequestsClient _client;

        public WhenRequestsExistTheGetAllMethod()
        {
            _client = Helper.GetAuthenticatedClient().PullRequest.ReviewRequest;
        }

        [Fact]
        public async Task GetsRequests()
        {
            var reviewRequests = await _client.GetAll(
                _repositoryOwner,
                _repositoryName,
                _pullRequestNumber);

            Assert.Equal(_expectedCollaborators, reviewRequests.Select(rr => rr.Login));
        }

        [Fact]
        public async Task GetsRequestsWithRepositoryId()
        {
            var reviewRequests = await _client.GetAll(_repositoryId, _pullRequestNumber);

            Assert.Equal(_expectedCollaborators, reviewRequests.Select(rr => rr.Login));
        }

        [Fact]
        public async Task ReturnsCorrectCountOfReviewRequestsWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var reviewRequests = await _client.GetAll(_repositoryOwner, _repositoryName, _pullRequestNumber, options);

            Assert.Equal(1, reviewRequests.Count);
        }

        [Fact]
        public async Task ReturnsCorrectCountOfReviewRequestsWithStartWithRepositoryId()
        {
            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var reviewRequests = await _client.GetAll(_repositoryId, _pullRequestNumber, options);

            Assert.Equal(1, reviewRequests.Count);
        }

        [Fact]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var firstPage = await _client.GetAll(
                _repositoryOwner,
                _repositoryName,
                _pullRequestNumber,
                startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _client.GetAll(
                _repositoryOwner,
                _repositoryName,
                _pullRequestNumber,
                skipStartOptions);

            Assert.Equal(1, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
        }

        [Fact]
        public async Task ReturnsDistinctResultsBasedOnStartPageWithRepositoryId()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var firstPage = await _client.GetAll(
                _repositoryId,
                _pullRequestNumber,
                startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _client.GetAll(
                _repositoryId,
                _pullRequestNumber,
                skipStartOptions);

            Assert.Equal(1, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
        }
    }

    public class TheDeleteMethod : WhenRequestsExistFixture
    {
        [DualAccountTest]
        public async Task DeletesRequests()
        {
            var reviewRequestsBeforeDelete = await Client.GetAll(Context.RepositoryOwner, Context.RepositoryName, Number);
            var reviewRequestToCreate = new PullRequestReviewRequest(CollaboratorLoginAsList);
            await Client.Delete(Context.RepositoryOwner, Context.RepositoryName, Number, reviewRequestToCreate);
            var reviewRequestsAfterDelete = await Client.GetAll(Context.RepositoryOwner, Context.RepositoryName, Number);

            Assert.NotEmpty(reviewRequestsBeforeDelete);
            Assert.Empty(reviewRequestsAfterDelete);
        }

        [DualAccountTest]
        public async Task DeletesRequestsWithRepositoryId()
        {
            var reviewRequestsBeforeDelete = await Client.GetAll(Context.RepositoryId, Number);
            var reviewRequestToCreate = new PullRequestReviewRequest(CollaboratorLoginAsList);
            await Client.Delete(Context.RepositoryId, Number, reviewRequestToCreate);
            var reviewRequestsAfterDelete = await Client.GetAll(Context.RepositoryId, Number);

            Assert.NotEmpty(reviewRequestsBeforeDelete);
            Assert.Empty(reviewRequestsAfterDelete);
        }
    }

    public class TheCreateMethod : WhenRequestsExistFixture
    {
        [DualAccountTest]
        public async Task CreatesRequests()
        {
            var reviewRequestToCreate = new PullRequestReviewRequest(CollaboratorLoginAsList);

            var pr = await Client.Create(Context.RepositoryOwner, Context.RepositoryName, Number, reviewRequestToCreate);

            Assert.Equal(CollaboratorLoginAsList, pr.RequestedReviewers.Select(rr => rr.Login));
        }

        [DualAccountTest]
        public async Task CreatesRequestsWithRepositoryId()
        {
            var reviewRequestToCreate = new PullRequestReviewRequest(CollaboratorLoginAsList);

            var pr = await Client.Create(Context.RepositoryId, Number, reviewRequestToCreate);

            Assert.Equal(CollaboratorLoginAsList, pr.RequestedReviewers.Select(rr => rr.Login));
        }
    }
}