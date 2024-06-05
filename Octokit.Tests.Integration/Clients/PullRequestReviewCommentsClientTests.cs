using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class PullRequestReviewCommentsClientTests : IDisposable
{
    private readonly IGitHubClient _github;
    private readonly IPullRequestReviewCommentsClient _client;
    private readonly RepositoryContext _context;

    const string branchName = "new-branch";
    const string path = "CONTRIBUTING.md";

    public PullRequestReviewCommentsClientTests()
    {
        _github = Helper.GetAuthenticatedClient();

        _client = _github.PullRequest.ReviewComment;

        // We'll create a pull request that can be used by most tests
        _context = _github.CreateRepositoryContextWithAutoInit("test-repo").Result;
    }

    [IntegrationTest]
    public async Task CanCreateAndRetrieveComment()
    {
        var pullRequest = await CreatePullRequest(_context);

        const string body = "A review comment message";
        const int position = 1;

        var createdComment = await CreateComment(body, position, pullRequest.Sha, pullRequest.Number);

        var commentFromGitHub = await _client.GetComment(Helper.UserName, _context.RepositoryName, createdComment.Id);

        AssertComment(commentFromGitHub, body, position);
    }

    [IntegrationTest]
    public async Task CanCreateAndRetrieveCommentWithRepositoryId()
    {
        var pullRequest = await CreatePullRequest(_context);

        const string body = "A review comment message";
        const int position = 1;

        var createdComment = await CreateCommentWithRepositoryId(body, position, pullRequest.Sha, pullRequest.Number);

        var commentFromGitHub = await _client.GetComment(_context.Repository.Id, createdComment.Id);

        AssertComment(commentFromGitHub, body, position);
    }

    [IntegrationTest]
    public async Task CanEditComment()
    {
        var pullRequest = await CreatePullRequest(_context);

        const string body = "A new review comment message";
        const int position = 1;

        var createdComment = await CreateComment(body, position, pullRequest.Sha, pullRequest.Number);

        var edit = new PullRequestReviewCommentEdit("Edited Comment");

        var editedComment = await _client.Edit(Helper.UserName, _context.RepositoryName, createdComment.Id, edit);

        var commentFromGitHub = await _client.GetComment(Helper.UserName, _context.RepositoryName, editedComment.Id);

        AssertComment(commentFromGitHub, edit.Body, position);
    }

    [IntegrationTest]
    public async Task CanEditCommentWithRepositoryId()
    {
        var pullRequest = await CreatePullRequest(_context);

        const string body = "A new review comment message";
        const int position = 1;

        var createdComment = await CreateCommentWithRepositoryId(body, position, pullRequest.Sha, pullRequest.Number);

        var edit = new PullRequestReviewCommentEdit("Edited Comment");

        var editedComment = await _client.Edit(_context.Repository.Id, createdComment.Id, edit);

        var commentFromGitHub = await _client.GetComment(_context.Repository.Id, editedComment.Id);

        AssertComment(commentFromGitHub, edit.Body, position);
    }

    [IntegrationTest]
    public async Task TimestampsAreUpdated()
    {
        var pullRequest = await CreatePullRequest(_context);

        const string body = "A new review comment message";
        const int position = 1;

        var createdComment = await CreateComment(body, position, pullRequest.Sha, pullRequest.Number);

        Assert.Equal(createdComment.UpdatedAt, createdComment.CreatedAt);

        await Task.Delay(TimeSpan.FromSeconds(2));

        var edit = new PullRequestReviewCommentEdit("Edited Comment");

        var editedComment = await _client.Edit(Helper.UserName, _context.RepositoryName, createdComment.Id, edit);

        Assert.NotEqual(editedComment.UpdatedAt, editedComment.CreatedAt);
    }

    [IntegrationTest]
    public async Task TimestampsAreUpdatedWithRepositoryId()
    {
        var pullRequest = await CreatePullRequest(_context);

        const string body = "A new review comment message";
        const int position = 1;

        var createdComment = await CreateCommentWithRepositoryId(body, position, pullRequest.Sha, pullRequest.Number);

        Assert.Equal(createdComment.UpdatedAt, createdComment.CreatedAt);

        await Task.Delay(TimeSpan.FromSeconds(2));

        var edit = new PullRequestReviewCommentEdit("Edited Comment");

        var editedComment = await _client.Edit(_context.Repository.Id, createdComment.Id, edit);

        Assert.NotEqual(editedComment.UpdatedAt, editedComment.CreatedAt);
    }

    [IntegrationTest]
    public async Task CanDeleteComment()
    {
        var pullRequest = await CreatePullRequest(_context);

        const string body = "A new review comment message";
        const int position = 1;

        var createdComment = await CreateComment(body, position, pullRequest.Sha, pullRequest.Number);

        await _client.Delete(Helper.UserName, _context.RepositoryName, createdComment.Id);

        await Assert.ThrowsAsync<NotFoundException>(() => _client.GetComment(Helper.UserName, _context.RepositoryName, createdComment.Id));
    }

    [IntegrationTest]
    public async Task CanDeleteCommentWithRepositoryId()
    {
        var pullRequest = await CreatePullRequest(_context);

        const string body = "A new review comment message";
        const int position = 1;

        var createdComment = await CreateCommentWithRepositoryId(body, position, pullRequest.Sha, pullRequest.Number);

        await _client.Delete(_context.Repository.Id, createdComment.Id);

        await Assert.ThrowsAsync<NotFoundException>(() => _client.GetComment(_context.Repository.Id, createdComment.Id));
    }

    [IntegrationTest]
    public async Task CanCreateReply()
    {
        var pullRequest = await CreatePullRequest(_context);

        const string body = "Reply me!";
        const int position = 1;

        var createdComment = await CreateComment(body, position, pullRequest.Sha, pullRequest.Number);

        var reply = new PullRequestReviewCommentReplyCreate("Replied", createdComment.Id);
        var createdReply = await _client.CreateReply(Helper.UserName, _context.RepositoryName, pullRequest.Number, reply);
        var createdReplyFromGitHub = await _client.GetComment(Helper.UserName, _context.RepositoryName, createdReply.Id);

        AssertComment(createdReplyFromGitHub, reply.Body, position);
    }

    [IntegrationTest]
    public async Task CanCreateReplyWithRepositoryId()
    {
        var pullRequest = await CreatePullRequest(_context);

        const string body = "Reply me!";
        const int position = 1;

        var createdComment = await CreateCommentWithRepositoryId(body, position, pullRequest.Sha, pullRequest.Number);

        var reply = new PullRequestReviewCommentReplyCreate("Replied", createdComment.Id);

        var createdReply = await _client.CreateReply(_context.Repository.Id, pullRequest.Number, reply);

        var createdReplyFromGitHub = await _client.GetComment(_context.Repository.Id, createdReply.Id);

        AssertComment(createdReplyFromGitHub, reply.Body, position);
    }

    [IntegrationTest]
    public async Task CanGetForPullRequest()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var pullRequestComments = await _client.GetAll(Helper.UserName, _context.RepositoryName, pullRequest.Number);

        AssertComments(pullRequestComments, commentsToCreate, position);
    }

    [IntegrationTest]
    public async Task CanGetForPullRequestWithRepositoryId()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var pullRequestComments = await _client.GetAll(_context.Repository.Id, pullRequest.Number);

        AssertComments(pullRequestComments, commentsToCreate, position);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestReviewCommentWithoutStart()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var pullRequestComments = await _client.GetAll(Helper.UserName, _context.RepositoryName, pullRequest.Number, options);

        Assert.Equal(3, pullRequestComments.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestReviewCommentWithoutStartWithRepositoryId()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var pullRequestComments = await _client.GetAll(_context.Repository.Id, pullRequest.Number, options);

        Assert.Equal(3, pullRequestComments.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestReviewCommentWithStart()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var options = new ApiOptions
        {
            PageSize = 2,
            PageCount = 1,
            StartPage = 2
        };

        var pullRequestComments = await _client.GetAll(Helper.UserName, _context.RepositoryName, pullRequest.Number, options);

        Assert.Single(pullRequestComments);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestReviewCommentWithStartWithRepositoryId()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var options = new ApiOptions
        {
            PageSize = 2,
            PageCount = 1,
            StartPage = 2
        };

        var pullRequestComments = await _client.GetAll(_context.Repository.Id, pullRequest.Number, options);

        Assert.Single(pullRequestComments);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctResultsBasedOnStartPage()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var startOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1
        };

        var firstPage = await _client.GetAll(Helper.UserName, _context.RepositoryName, pullRequest.Number, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _client.GetAll(Helper.UserName, _context.RepositoryName, pullRequest.Number, skipStartOptions);

        Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctResultsBasedOnStartPageWithRepositoryId()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var startOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1
        };

        var firstPage = await _client.GetAll(_context.Repository.Id, pullRequest.Number, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _client.GetAll(_context.Repository.Id, pullRequest.Number, skipStartOptions);

        Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
    }

    [IntegrationTest]
    public async Task CanGetForRepository()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment One", "Comment Two" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var pullRequestComments = await _client.GetAllForRepository(Helper.UserName, _context.RepositoryName);

        AssertComments(pullRequestComments, commentsToCreate, position);
    }

    [IntegrationTest]
    public async Task CanGetForRepositoryWithRepositoryId()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment One", "Comment Two" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var pullRequestComments = await _client.GetAllForRepository(_context.Repository.Id);

        AssertComments(pullRequestComments, commentsToCreate, position);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestReviewCommentWithoutStartForRepository()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var pullRequestComments = await _client.GetAllForRepository(Helper.UserName, _context.RepositoryName, options);

        Assert.Equal(3, pullRequestComments.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestReviewCommentWithoutStartForRepositoryWithRepositoryId()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var pullRequestComments = await _client.GetAllForRepository(_context.Repository.Id, options);

        Assert.Equal(3, pullRequestComments.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestReviewCommentWithStartForRepository()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var options = new ApiOptions
        {
            PageSize = 2,
            PageCount = 1,
            StartPage = 2
        };

        var pullRequestComments = await _client.GetAllForRepository(Helper.UserName, _context.RepositoryName, options);

        Assert.Single(pullRequestComments);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestReviewCommentWithStartForRepositoryWithRepositoryId()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var options = new ApiOptions
        {
            PageSize = 2,
            PageCount = 1,
            StartPage = 2
        };

        var pullRequestComments = await _client.GetAllForRepository(_context.Repository.Id, options);

        Assert.Single(pullRequestComments);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctResultsBasedOnStartPageForRepository()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var startOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1
        };

        var firstPage = await _client.GetAllForRepository(Helper.UserName, _context.RepositoryName, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _client.GetAllForRepository(Helper.UserName, _context.RepositoryName, skipStartOptions);

        Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctResultsBasedOnStartPageForRepositoryWithRepositoryId()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var startOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1
        };

        var firstPage = await _client.GetAllForRepository(_context.Repository.Id, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _client.GetAllForRepository(_context.Repository.Id, skipStartOptions);

        Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
    }

    [IntegrationTest]
    public async Task CanGetForRepositoryAscendingSort()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new[] { "Comment One", "Comment Two", "Comment Three" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var pullRequestReviewCommentRequest = new PullRequestReviewCommentRequest { Direction = SortDirection.Ascending };

        var pullRequestComments = await _client.GetAllForRepository(Helper.UserName, _context.RepositoryName, pullRequestReviewCommentRequest);

        Assert.Equal(pullRequestComments.Select(x => x.Body), commentsToCreate);
    }

    [IntegrationTest]
    public async Task CanGetForRepositoryAscendingSortWithRepositoryId()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new[] { "Comment One", "Comment Two", "Comment Three" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var pullRequestReviewCommentRequest = new PullRequestReviewCommentRequest { Direction = SortDirection.Ascending };

        var pullRequestComments = await _client.GetAllForRepository(_context.Repository.Id, pullRequestReviewCommentRequest);

        Assert.Equal(pullRequestComments.Select(x => x.Body), commentsToCreate);
    }

    [IntegrationTest]
    public async Task CanGetForRepositoryDescendingSort()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new[] { "Comment One", "Comment Two", "Comment Three", "Comment Four" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var pullRequestReviewCommentRequest = new PullRequestReviewCommentRequest { Direction = SortDirection.Descending };

        var pullRequestComments = await _client.GetAllForRepository(Helper.UserName, _context.RepositoryName, pullRequestReviewCommentRequest);

        Assert.Equal(pullRequestComments.Select(x => x.Body), commentsToCreate.Reverse());
    }

    [IntegrationTest]
    public async Task CanGetForRepositoryDescendingSortWithRepositoryId()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new[] { "Comment One", "Comment Two", "Comment Three", "Comment Four" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var pullRequestReviewCommentRequest = new PullRequestReviewCommentRequest { Direction = SortDirection.Descending };

        var pullRequestComments = await _client.GetAllForRepository(_context.Repository.Id, pullRequestReviewCommentRequest);

        Assert.Equal(pullRequestComments.Select(x => x.Body), commentsToCreate.Reverse());
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestReviewCommentWithoutStartForRepositoryParametrized()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var pullRequestReviewCommentRequest = new PullRequestReviewCommentRequest { Direction = SortDirection.Descending };

        var pullRequestComments = await _client.GetAllForRepository(Helper.UserName, _context.RepositoryName, pullRequestReviewCommentRequest, options);

        Assert.Equal(3, pullRequestComments.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestReviewCommentWithoutStartForRepositoryParametrizedWithRepositoryId()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var pullRequestReviewCommentRequest = new PullRequestReviewCommentRequest { Direction = SortDirection.Descending };

        var pullRequestComments = await _client.GetAllForRepository(_context.Repository.Id, pullRequestReviewCommentRequest, options);

        Assert.Equal(3, pullRequestComments.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestReviewCommentWithStartForRepositoryParametrized()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var options = new ApiOptions
        {
            PageSize = 2,
            PageCount = 1,
            StartPage = 2
        };

        var pullRequestReviewCommentRequest = new PullRequestReviewCommentRequest { Direction = SortDirection.Descending };

        var pullRequestComments = await _client.GetAllForRepository(Helper.UserName, _context.RepositoryName, pullRequestReviewCommentRequest, options);

        Assert.Single(pullRequestComments);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestReviewCommentWithStartForRepositoryParametrizedWithRepositoryId()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var options = new ApiOptions
        {
            PageSize = 2,
            PageCount = 1,
            StartPage = 2
        };

        var pullRequestReviewCommentRequest = new PullRequestReviewCommentRequest { Direction = SortDirection.Descending };

        var pullRequestComments = await _client.GetAllForRepository(_context.Repository.Id, pullRequestReviewCommentRequest, options);

        Assert.Single(pullRequestComments);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctResultsBasedOnStartPageForRepositoryParametrized()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        var pullRequestReviewCommentRequest = new PullRequestReviewCommentRequest { Direction = SortDirection.Descending };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var startOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1
        };

        var firstPage = await _client.GetAllForRepository(Helper.UserName, _context.RepositoryName, pullRequestReviewCommentRequest, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _client.GetAllForRepository(Helper.UserName, _context.RepositoryName, pullRequestReviewCommentRequest, skipStartOptions);

        Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctResultsBasedOnStartPageForRepositoryParametrizedWithRepositoryId()
    {
        var pullRequest = await CreatePullRequest(_context);

        const int position = 1;
        var commentsToCreate = new List<string> { "Comment 1", "Comment 2", "Comment 3" };

        var pullRequestReviewCommentRequest = new PullRequestReviewCommentRequest { Direction = SortDirection.Descending };

        await CreateComments(commentsToCreate, position, _context.RepositoryName, pullRequest.Sha, pullRequest.Number);

        var startOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1
        };

        var firstPage = await _client.GetAllForRepository(_context.Repository.Id, pullRequestReviewCommentRequest, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _client.GetAllForRepository(Helper.UserName, _context.RepositoryName, pullRequestReviewCommentRequest, skipStartOptions);

        Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    async Task<PullRequestReviewComment> CreateComment(string body, int position, string commitId, int number)
    {
        return await CreateComment(body, position, _context.RepositoryName, commitId, number);
    }

    async Task<PullRequestReviewComment> CreateCommentWithRepositoryId(string body, int position, string commitId, int number)
    {
        return await CreateComment(body, position, _context.Repository.Id, commitId, number);
    }

    async Task<PullRequestReviewComment> CreateComment(string body, int position, string repoName, string pullRequestCommitId, int pullRequestNumber)
    {
        var comment = new PullRequestReviewCommentCreate(body, pullRequestCommitId, path, position);

        var createdComment = await _client.Create(Helper.UserName, repoName, pullRequestNumber, comment);

        AssertComment(createdComment, body, position);

        return createdComment;
    }

    async Task<PullRequestReviewComment> CreateComment(string body, int position, long repositoryId, string pullRequestCommitId, int pullRequestNumber)
    {
        var comment = new PullRequestReviewCommentCreate(body, pullRequestCommitId, path, position);

        var createdComment = await _client.Create(repositoryId, pullRequestNumber, comment);

        AssertComment(createdComment, body, position);

        return createdComment;
    }

    async Task CreateComments(IEnumerable<string> comments, int position, string repoName, string pullRequestCommitId, int pullRequestNumber)
    {
        foreach (var comment in comments)
        {
            await CreateComment(comment, position, repoName, pullRequestCommitId, pullRequestNumber);
            await Task.Delay(TimeSpan.FromSeconds(2));
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

    /// <summary>
    /// Creates the base state for testing (creates a repo, a commit in main, a branch, a commit in the branch and a pull request)
    /// </summary>
    /// <returns></returns>
    async Task<PullRequestData> CreatePullRequest(RepositoryContext context, string branch = branchName)
    {
        string branchHead = "heads/" + branch;
        string branchRef = "refs/" + branchHead;


        var repoName = context.RepositoryName;

        // Creating a commit in main

        var createdCommitInMain = await CreateCommit(repoName, "Hello World!", "README.md", "heads/main", "A main commit message");

        // Creating a branch

        var newBranch = new NewReference(branchRef, createdCommitInMain.Sha);
        await _github.Git.Reference.Create(Helper.UserName, repoName, newBranch);

        // Creating a commit in the branch

        var createdCommitInBranch = await CreateCommit(repoName, "Hello from the fork!", path, branchHead, "A branch commit message");

        // Creating a pull request

        var pullRequest = new NewPullRequest("Nice title for the pull request", branch, "main");
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

    [IntegrationTest]
    public async Task CanGetReactionPayloadForPullRequestReviews()
    {
        int numberToCreate = 2;
        using (var context = await _github.CreateRepositoryContextWithAutoInit(Helper.MakeNameWithTimestamp("PullRequestReviewCommentsReactionTests")))
        {
            var commentIds = new List<int>();

            // Create a test pull request
            var pullRequest = await CreatePullRequest(context);

            // With multiple review comments with reactions
            for (int count = 1; count <= numberToCreate; count++)
            {
                var commentId = await HelperCreatePullRequestReviewCommentWithReactions(context.RepositoryOwner, context.RepositoryName, pullRequest);
                commentIds.Add(commentId);
            }
            Assert.Equal(numberToCreate, commentIds.Count);

            // Retrieve all review comments for the pull request
            var reviewComments = await _client.GetAll(context.RepositoryOwner, context.RepositoryName, pullRequest.Number);

            // Check the reactions
            foreach (var commentId in commentIds)
            {
                var retrieved = reviewComments.FirstOrDefault(x => x.Id == commentId);

                Assert.NotNull(retrieved);
                Assert.Equal(6, retrieved.Reactions.TotalCount);
                Assert.Equal(1, retrieved.Reactions.Plus1);
                Assert.Equal(1, retrieved.Reactions.Hooray);
                Assert.Equal(1, retrieved.Reactions.Heart);
                Assert.Equal(1, retrieved.Reactions.Laugh);
                Assert.Equal(1, retrieved.Reactions.Confused);
                Assert.Equal(1, retrieved.Reactions.Minus1);
            }
        }
    }

    [IntegrationTest]
    public async Task CanGetReactionPayloadForRepositoryPullRequestReviews()
    {
        int numberToCreate = 2;
        using (var context = await _github.CreateRepositoryContextWithAutoInit(Helper.MakeNameWithTimestamp("PullRequestReviewCommentsReactionTests")))
        {
            var commentIds = new List<int>();

            // Create multiple test pull requests
            for (int count = 1; count <= numberToCreate; count++)
            {
                var pullRequest = await CreatePullRequest(context, "branch" + count);

                // Each with a review comment with reactions
                var commentId = await HelperCreatePullRequestReviewCommentWithReactions(context.RepositoryOwner, context.RepositoryName, pullRequest);
                commentIds.Add(commentId);
            }
            Assert.Equal(numberToCreate, commentIds.Count);

            // Retrieve all review comments for the repo
            var reviewComments = await _client.GetAllForRepository(context.RepositoryOwner, context.RepositoryName);

            // Check the reactions
            foreach (var commentId in commentIds)
            {
                var retrieved = reviewComments.FirstOrDefault(x => x.Id == commentId);

                Assert.NotNull(retrieved);
                Assert.Equal(6, retrieved.Reactions.TotalCount);
                Assert.Equal(1, retrieved.Reactions.Plus1);
                Assert.Equal(1, retrieved.Reactions.Hooray);
                Assert.Equal(1, retrieved.Reactions.Heart);
                Assert.Equal(1, retrieved.Reactions.Laugh);
                Assert.Equal(1, retrieved.Reactions.Confused);
                Assert.Equal(1, retrieved.Reactions.Minus1);
            }
        }
    }

    async Task<int> HelperCreatePullRequestReviewCommentWithReactions(string owner, string repo, PullRequestData pullRequest)
    {
        var github = Helper.GetAuthenticatedClient();

        const string body = "A review comment message";
        const int position = 1;

        var reviewComment = await CreateComment(body, position, repo, pullRequest.Sha, pullRequest.Number);

        foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
        {
            var newReaction = new NewReaction(reactionType);

            var reaction = await github.Reaction.PullRequestReviewComment.Create(owner, repo, reviewComment.Id, newReaction);

            Assert.IsType<Reaction>(reaction);
            Assert.Equal(reactionType, reaction.Content);
            Assert.Equal(reviewComment.User.Id, reaction.User.Id);
        }

        return reviewComment.Id;
    }
}