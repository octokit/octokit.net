using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class PullRequestsClientTests : IDisposable
{
    private readonly IGitHubClient _github;
    private readonly IPullRequestsClient _fixture;
    private readonly RepositoryContext _context;
    private readonly IRepositoryCommentsClient _repositoryCommentsClient;

    const string branchName = "my-branch";
    const string otherBranchName = "my-other-branch";

    public PullRequestsClientTests()
    {
        _github = Helper.GetAuthenticatedClient();

        _fixture = _github.Repository.PullRequest;
        _repositoryCommentsClient = _github.Repository.Comment;

        _context = _github.CreateRepositoryContext("source-repo").Result;
    }

    [IntegrationTest]
    public async Task CanCreate()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);
        Assert.Equal("a pull request", result.Title);
    }

    [IntegrationTest]
    public async Task CanCreateWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var result = await _fixture.Create(_context.Repository.Id, newPullRequest);
        Assert.Equal("a pull request", result.Title);
    }

    [IntegrationTest]
    public async Task CanGetForRepository()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName);

        Assert.Equal(1, pullRequests.Count);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task CanGetForRepositoryWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var pullRequests = await _fixture.GetAllForRepository(_context.Repository.Id);

        Assert.Equal(1, pullRequests.Count);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestsWithoutStart()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, options);

        Assert.Equal(1, pullRequests.Count);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestsWithoutStartWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var pullRequests = await _fixture.GetAllForRepository(_context.Repository.Id, options);

        Assert.Equal(1, pullRequests.Count);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestsWithStart()
    {
        await CreateTheWorld();

        var newPullRequest1 = new NewPullRequest("a pull request 1", branchName, "master");
        var newPullRequest2 = new NewPullRequest("a pull request 2", otherBranchName, "master");
        await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest1);
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest2);

        var options = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 1
        };

        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, options);

        Assert.Equal(1, pullRequests.Count);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestsWithStartWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest1 = new NewPullRequest("a pull request 1", branchName, "master");
        var newPullRequest2 = new NewPullRequest("a pull request 2", otherBranchName, "master");
        await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest1);
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest2);

        var options = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 1
        };

        var pullRequests = await _fixture.GetAllForRepository(_context.Repository.Id, options);

        Assert.Equal(1, pullRequests.Count);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctPullRequestsBasedOnStartPage()
    {
        await CreateTheWorld();

        var newPullRequest1 = new NewPullRequest("a pull request 1", branchName, "master");
        var newPullRequest2 = new NewPullRequest("a pull request 2", otherBranchName, "master");
        await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest1);
        await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest2);

        var startOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1
        };

        var firstPage = await _fixture.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _fixture.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, skipStartOptions);

        Assert.NotEqual(firstPage[0].Title, secondPage[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctPullRequestsBasedOnStartPageWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest1 = new NewPullRequest("a pull request 1", branchName, "master");
        var newPullRequest2 = new NewPullRequest("a pull request 2", otherBranchName, "master");
        await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest1);
        await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest2);

        var startOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1
        };

        var firstPage = await _fixture.GetAllForRepository(_context.Repository.Id, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _fixture.GetAllForRepository(_context.Repository.Id, skipStartOptions);

        Assert.NotEqual(firstPage[0].Title, secondPage[0].Title);
    }

    [IntegrationTest]
    public async Task CanGetOpenPullRequest()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var openPullRequests = new PullRequestRequest { State = ItemStateFilter.Open };
        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, openPullRequests);

        Assert.Equal(1, pullRequests.Count);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task CanGetOpenPullRequestWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var openPullRequests = new PullRequestRequest { State = ItemStateFilter.Open };
        var pullRequests = await _fixture.GetAllForRepository(_context.Repository.Id, openPullRequests);

        Assert.Equal(1, pullRequests.Count);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestsWithoutStartParameterized()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var openPullRequests = new PullRequestRequest { State = ItemStateFilter.Open };
        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, openPullRequests, options);

        Assert.Equal(1, pullRequests.Count);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestsWithoutStartParameterizedWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var openPullRequests = new PullRequestRequest { State = ItemStateFilter.Open };
        var pullRequests = await _fixture.GetAllForRepository(_context.Repository.Id, openPullRequests, options);

        Assert.Equal(1, pullRequests.Count);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestsWithStartParameterized()
    {
        await CreateTheWorld();

        var newPullRequest1 = new NewPullRequest("a pull request 1", branchName, "master");
        var newPullRequest2 = new NewPullRequest("a pull request 2", otherBranchName, "master");
        await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest1);
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest2);

        var options = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 1
        };

        var openPullRequests = new PullRequestRequest { State = ItemStateFilter.Open };
        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, openPullRequests, options);

        Assert.Equal(1, pullRequests.Count);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestsWithStartParameterizedWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest1 = new NewPullRequest("a pull request 1", branchName, "master");
        var newPullRequest2 = new NewPullRequest("a pull request 2", otherBranchName, "master");
        await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest1);
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest2);

        var options = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 1
        };

        var openPullRequests = new PullRequestRequest { State = ItemStateFilter.Open };
        var pullRequests = await _fixture.GetAllForRepository(_context.Repository.Id, openPullRequests, options);

        Assert.Equal(1, pullRequests.Count);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctPullRequestsBasedOnStartPageParameterized()
    {
        await CreateTheWorld();

        var newPullRequest1 = new NewPullRequest("a pull request 1", branchName, "master");
        var newPullRequest2 = new NewPullRequest("a pull request 2", otherBranchName, "master");
        await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest1);
        await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest2);

        var openPullRequests = new PullRequestRequest { State = ItemStateFilter.Open };

        var startOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1
        };

        var firstPage = await _fixture.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, openPullRequests, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _fixture.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, openPullRequests, skipStartOptions);

        Assert.NotEqual(firstPage[0].Title, secondPage[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctPullRequestsBasedOnStartPageParameterizedWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest1 = new NewPullRequest("a pull request 1", branchName, "master");
        var newPullRequest2 = new NewPullRequest("a pull request 2", otherBranchName, "master");
        await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest1);
        await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest2);

        var openPullRequests = new PullRequestRequest { State = ItemStateFilter.Open };

        var startOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1
        };

        var firstPage = await _fixture.GetAllForRepository(_context.Repository.Id, openPullRequests, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _fixture.GetAllForRepository(_context.Repository.Id, openPullRequests, skipStartOptions);

        Assert.NotEqual(firstPage[0].Title, secondPage[0].Title);
    }

    [IntegrationTest]
    public async Task IgnoresOpenPullRequest()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var openPullRequests = new PullRequestRequest { State = ItemStateFilter.Closed };
        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, openPullRequests);

        Assert.Empty(pullRequests);
    }

    [IntegrationTest]
    public async Task IgnoresOpenPullRequestWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var openPullRequests = new PullRequestRequest { State = ItemStateFilter.Closed };
        var pullRequests = await _fixture.GetAllForRepository(_context.Repository.Id, openPullRequests);

        Assert.Empty(pullRequests);
    }

    [IntegrationTest]
    public async Task CanUpdate()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var updatePullRequest = new PullRequestUpdate { Title = "updated title", Body = "Hello New Body", Base = "my-other-branch" };
        var result = await _fixture.Update(Helper.UserName, _context.RepositoryName, pullRequest.Number, updatePullRequest);

        Assert.Equal(updatePullRequest.Title, result.Title);
        Assert.Equal(updatePullRequest.Body, result.Body);
        Assert.Equal(updatePullRequest.Base, result.Base.Ref);
    }

    [IntegrationTest]
    public async Task CanUpdateWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var updatePullRequest = new PullRequestUpdate { Title = "updated title", Body = "Hello New Body", Base = "my-other-branch" };
        var result = await _fixture.Update(_context.Repository.Id, pullRequest.Number, updatePullRequest);

        Assert.Equal(updatePullRequest.Title, result.Title);
        Assert.Equal(updatePullRequest.Body, result.Body);
        Assert.Equal(updatePullRequest.Base, result.Base.Ref);
    }

    [IntegrationTest]
    public async Task CanClose()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var updatePullRequest = new PullRequestUpdate { State = ItemState.Closed };
        var result = await _fixture.Update(Helper.UserName, _context.RepositoryName, pullRequest.Number, updatePullRequest);

        Assert.Equal(ItemState.Closed, result.State);
        Assert.Equal(pullRequest.Title, result.Title);
        Assert.Equal(pullRequest.Body, result.Body);
    }

    [IntegrationTest]
    public async Task CanFindClosedPullRequest()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var updatePullRequest = new PullRequestUpdate { State = ItemState.Closed };
        await _fixture.Update(Helper.UserName, _context.RepositoryName, pullRequest.Number, updatePullRequest);

        var closedPullRequests = new PullRequestRequest { State = ItemStateFilter.Closed };
        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, closedPullRequests);

        Assert.Equal(1, pullRequests.Count);
    }

    [IntegrationTest]
    public async Task CanSortPullRequests()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var newPullRequest2 = new NewPullRequest("another pull request", otherBranchName, "master");
        var anotherPullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest2);

        var updatePullRequest = new PullRequestUpdate { Body = "This is the body" };
        await _fixture.Update(Helper.UserName, _context.RepositoryName, pullRequest.Number, updatePullRequest);

        var sortPullRequestsByUpdated = new PullRequestRequest { SortProperty = PullRequestSort.Updated, SortDirection = SortDirection.Ascending };
        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, sortPullRequestsByUpdated);
        Assert.Equal(anotherPullRequest.Title, pullRequests[0].Title);

        var sortPullRequestsByLongRunning = new PullRequestRequest { SortProperty = PullRequestSort.LongRunning };
        var pullRequestsByLongRunning = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, sortPullRequestsByLongRunning);
        Assert.Equal(pullRequest.Title, pullRequestsByLongRunning[0].Title);
    }

    [IntegrationTest]
    public async Task CanSpecifyDirectionOfSort()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var newPullRequest2 = new NewPullRequest("another pull request", otherBranchName, "master");
        var anotherPullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest2);

        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, new PullRequestRequest { SortDirection = SortDirection.Ascending });
        Assert.Equal(pullRequest.Title, pullRequests[0].Title);

        var pullRequestsDescending = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, new PullRequestRequest());
        Assert.Equal(anotherPullRequest.Title, pullRequestsDescending[0].Title);
    }

    [IntegrationTest]
    public async Task IsNotMergedInitially()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var result = await _fixture.Merged(Helper.UserName, _context.RepositoryName, pullRequest.Number);

        Assert.False(result);
    }

    [IntegrationTest]
    public async Task IsNotMergedInitiallyWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var result = await _fixture.Merged(_context.Repository.Id, pullRequest.Number);

        Assert.False(result);
    }

    [IntegrationTest]
    public async Task CanBeMerged()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var merge = new MergePullRequest { CommitMessage = "thing the thing" };
        var result = await _fixture.Merge(Helper.UserName, _context.RepositoryName, pullRequest.Number, merge);

        Assert.True(result.Merged);
    }

    [IntegrationTest]
    public async Task CanBeMergedWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var merge = new MergePullRequest { CommitMessage = "thing the thing" };
        var result = await _fixture.Merge(_context.Repository.Id, pullRequest.Number, merge);

        Assert.True(result.Merged);
    }

    [IntegrationTest]
    public async Task CanBeMergedWithNoOptionalInput()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var merge = new MergePullRequest();
        var result = await _fixture.Merge(Helper.UserName, _context.RepositoryName, pullRequest.Number, merge);

        Assert.True(result.Merged);
    }

    [IntegrationTest]
    public async Task CanBeMergedWithShaSpecified()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var merge = new MergePullRequest { CommitMessage = "thing the thing", Sha = pullRequest.Head.Sha };
        var result = await _fixture.Merge(Helper.UserName, _context.RepositoryName, pullRequest.Number, merge);

        Assert.True(result.Merged);
    }

    [IntegrationTest]
    public async Task CanBeMergedWithSquashCommit()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("squash commit pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var merge = new MergePullRequest { CommitMessage = "fake commit message", CommitTitle = "fake title", Squash = true };
        var result = await _fixture.Merge(Helper.UserName, _context.RepositoryName, pullRequest.Number, merge);
        var commit = await _github.Repository.Commit.Get(_context.RepositoryOwner, _context.RepositoryName, result.Sha);

        Assert.True(result.Merged);
        Assert.Equal("fake title\n\nfake commit message", commit.Commit.Message);
    }

    [IntegrationTest]
    public async Task CanBeMergedWithMergeMethod()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("squash commit pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var merge = new MergePullRequest { CommitMessage = "fake commit message", CommitTitle = "fake title", MergeMethod = PullRequestMergeMethod.Merge };
        var result = await _fixture.Merge(Helper.UserName, _context.RepositoryName, pullRequest.Number, merge);
        var commit = await _github.Repository.Commit.Get(_context.RepositoryOwner, _context.RepositoryName, result.Sha);

        Assert.True(result.Merged);
        Assert.Equal("fake title\n\nfake commit message", commit.Commit.Message);
    }

    [IntegrationTest]
    public async Task CanBeMergedWithSquashMethod()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("squash commit pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var merge = new MergePullRequest { CommitMessage = "fake commit message", CommitTitle = "fake title", MergeMethod = PullRequestMergeMethod.Squash };
        var result = await _fixture.Merge(Helper.UserName, _context.RepositoryName, pullRequest.Number, merge);
        var commit = await _github.Repository.Commit.Get(_context.RepositoryOwner, _context.RepositoryName, result.Sha);

        Assert.True(result.Merged);
        Assert.Equal("fake title\n\nfake commit message", commit.Commit.Message);
    }

    [IntegrationTest]
    public async Task CanBeMergedWithRebaseMethod()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("squash commit pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var merge = new MergePullRequest { CommitMessage = "fake commit message", CommitTitle = "fake title", MergeMethod = PullRequestMergeMethod.Rebase };
        var result = await _fixture.Merge(Helper.UserName, _context.RepositoryName, pullRequest.Number, merge);
        var commit = await _github.Repository.Commit.Get(_context.RepositoryOwner, _context.RepositoryName, result.Sha);

        Assert.True(result.Merged);
        Assert.Equal("this is a 2nd commit to merge into the pull request", commit.Commit.Message);
    }

    [IntegrationTest]
    public async Task CannotBeMergedDueMismatchConflict()
    {
        await CreateTheWorld();
        var fakeSha = new string('f', 40);

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var merge = new MergePullRequest { Sha = fakeSha };
        var ex = await Assert.ThrowsAsync<PullRequestMismatchException>(() => _fixture.Merge(Helper.UserName, _context.RepositoryName, pullRequest.Number, merge));

        Assert.True(ex.Message.StartsWith("Head branch was modified"));
    }

    [IntegrationTest(Skip = "this PR is actually mergeable - rewrite the test")]
    public async Task CannotBeMergedDueNotInMergeableState()
    {
        await CreateTheWorld();

        var master = await _github.Git.Reference.Get(Helper.UserName, _context.RepositoryName, "heads/master");
        var newMasterTree = await CreateTree(new Dictionary<string, string> { { "README.md", "Hello World, we meet again!" } });
        var masterCommit = await CreateCommit("Commit in master", newMasterTree.Sha, master.Object.Sha);
        await _github.Git.Reference.Update(Helper.UserName, _context.RepositoryName, "heads/master", new ReferenceUpdate(masterCommit.Sha));

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        await Task.Delay(TimeSpan.FromSeconds(5));

        var updatedPullRequest = await _fixture.Get(Helper.UserName, _context.RepositoryName, pullRequest.Number);

        Assert.False(updatedPullRequest.Mergeable);

        var merge = new MergePullRequest { Sha = pullRequest.Head.Sha };
        var ex = await Assert.ThrowsAsync<PullRequestNotMergeableException>(() => _fixture.Merge(Helper.UserName, _context.RepositoryName, pullRequest.Number, merge));

        Assert.True(ex.Message.Equals("Pull Request is not mergeable"));
    }

    [IntegrationTest]
    public async Task UpdatesMaster()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var merge = new MergePullRequest { CommitMessage = "thing the thing" };
        var result = await _fixture.Merge(Helper.UserName, _context.RepositoryName, pullRequest.Number, merge);

        var master = await _github.Git.Reference.Get(Helper.UserName, _context.RepositoryName, "heads/master");

        Assert.Equal(result.Sha, master.Object.Sha);
    }

    [IntegrationTest]
    public async Task CanBrowseCommits()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var result = await _fixture.Commits(Helper.UserName, _context.RepositoryName, pullRequest.Number);

        Assert.Equal(2, result.Count);
        Assert.Equal("this is the commit to merge into the pull request", result[0].Commit.Message);
    }

    [IntegrationTest]
    public async Task CanBrowseCommitsWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var result = await _fixture.Commits(_context.Repository.Id, pullRequest.Number);

        Assert.Equal(2, result.Count);
        Assert.Equal("this is the commit to merge into the pull request", result[0].Commit.Message);
    }

    [IntegrationTest]
    public async Task CanGetCommitsAndCommentCount()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        // create new commit for branch

        const string commitMessage = "Another commit in branch";

        var branch = await _github.Git.Reference.Get(Helper.UserName, _context.RepositoryName, "heads/" + branchName);

        var newTree = await CreateTree(new Dictionary<string, string> { { "README.md", "Hello World!" } });
        var newCommit = await CreateCommit(commitMessage, newTree.Sha, branch.Object.Sha);
        await _github.Git.Reference.Update(Helper.UserName, _context.RepositoryName, "heads/" + branchName, new ReferenceUpdate(newCommit.Sha));

        await _repositoryCommentsClient.Create(Helper.UserName, _context.RepositoryName, newCommit.Sha, new NewCommitComment("I am a nice comment") { Path = "README.md", Position = 1 });

        // don't try this at home
        await Task.Delay(TimeSpan.FromSeconds(5));

        var result = await _fixture.Commits(Helper.UserName, _context.RepositoryName, pullRequest.Number);

        Assert.Equal(3, result.Count);
        Assert.Equal("this is the commit to merge into the pull request", result[0].Commit.Message);
        Assert.Equal(0, result[0].Commit.CommentCount);
        Assert.Equal(commitMessage, result[2].Commit.Message);
        Assert.Equal(1, result[2].Commit.CommentCount);
    }

    [IntegrationTest]
    public async Task CanGetCommitsAndCommentCountWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "master");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        // create new commit for branch

        const string commitMessage = "Another commit in branch";

        var branch = await _github.Git.Reference.Get(Helper.UserName, _context.RepositoryName, "heads/" + branchName);

        var newTree = await CreateTree(new Dictionary<string, string> { { "README.md", "Hello World!" } });
        var newCommit = await CreateCommit(commitMessage, newTree.Sha, branch.Object.Sha);
        await _github.Git.Reference.Update(Helper.UserName, _context.RepositoryName, "heads/" + branchName, new ReferenceUpdate(newCommit.Sha));

        await _repositoryCommentsClient.Create(Helper.UserName, _context.RepositoryName, newCommit.Sha, new NewCommitComment("I am a nice comment") { Path = "README.md", Position = 1 });

        // don't try this at home
        await Task.Delay(TimeSpan.FromSeconds(5));

        var result = await _fixture.Commits(_context.Repository.Id, pullRequest.Number);

        Assert.Equal(3, result.Count);
        Assert.Equal("this is the commit to merge into the pull request", result[0].Commit.Message);
        Assert.Equal(0, result[0].Commit.CommentCount);
        Assert.Equal(commitMessage, result[2].Commit.Message);
        Assert.Equal(1, result[2].Commit.CommentCount);
    }

    [IntegrationTest]
    public async Task CanBrowseFiles()
    {
        var expectedFiles = new List<PullRequestFile>
        {
            new PullRequestFile(null, "Octokit.Tests.Integration/Clients/ReferencesClientTests.cs", null, 8, 3, 11, null, null, null, null),
            new PullRequestFile(null, "Octokit/Clients/ApiPagination.cs", null, 21, 6, 27, null, null, null, null),
            new PullRequestFile(null, "Octokit/Helpers/IApiPagination.cs", null, 1, 1, 2, null, null, null, null),
            new PullRequestFile(null, "Octokit/Http/ApiConnection.cs", null, 1, 1, 2, null, null, null, null)
        };

        var result = await _fixture.Files("octokit", "octokit.net", 288);

        Assert.Equal(4, result.Count);
        Assert.True(expectedFiles.All(expectedFile => result.Any(file => file.FileName.Equals(expectedFile.FileName))));
        foreach (var file in result)
        {
            var expectedFile = expectedFiles.Find(prf => file.FileName.Equals(prf.FileName));
            Assert.Equal(expectedFile.Changes, file.Changes);
            Assert.Equal(expectedFile.Additions, file.Additions);
            Assert.Equal(expectedFile.Deletions, file.Deletions);
        }
    }

    [IntegrationTest]
    public async Task CanBrowseFilesWithRepositoryId()
    {
        var expectedFiles = new List<PullRequestFile>
        {
            new PullRequestFile(null, "Octokit.Tests.Integration/Clients/ReferencesClientTests.cs", null, 8, 3, 11, null, null, null, null),
            new PullRequestFile(null, "Octokit/Clients/ApiPagination.cs", null, 21, 6, 27, null, null, null, null),
            new PullRequestFile(null, "Octokit/Helpers/IApiPagination.cs", null, 1, 1, 2, null, null, null, null),
            new PullRequestFile(null, "Octokit/Http/ApiConnection.cs", null, 1, 1, 2, null, null, null, null)
        };

        var result = await _fixture.Files(7528679, 288);

        Assert.Equal(4, result.Count);
        Assert.True(expectedFiles.All(expectedFile => result.Any(file => file.FileName.Equals(expectedFile.FileName))));
        foreach (var file in result)
        {
            var expectedFile = expectedFiles.Find(prf => file.FileName.Equals(prf.FileName));
            Assert.Equal(expectedFile.Changes, file.Changes);
            Assert.Equal(expectedFile.Additions, file.Additions);
            Assert.Equal(expectedFile.Deletions, file.Deletions);
        }
    }

    async Task CreateTheWorld()
    {
        var master = await _github.Git.Reference.Get(Helper.UserName, _context.RepositoryName, "heads/master");

        // create new commit for master branch
        var newMasterTree = await CreateTree(new Dictionary<string, string> { { "README.md", "Hello World!" } });
        var newMaster = await CreateCommit("baseline for pull request", newMasterTree.Sha, master.Object.Sha);

        // update master
        await _github.Git.Reference.Update(Helper.UserName, _context.RepositoryName, "heads/master", new ReferenceUpdate(newMaster.Sha));

        // create new commit for feature branch
        var featureBranchTree = await CreateTree(new Dictionary<string, string> { { "README.md", "I am overwriting this blob with something new" } });
        var featureBranchCommit = await CreateCommit("this is the commit to merge into the pull request", featureBranchTree.Sha, newMaster.Sha);

        var featureBranchTree2 = await CreateTree(new Dictionary<string, string> { { "README.md", "I am overwriting this blob with something new a 2nd time" } });
        var featureBranchCommit2 = await CreateCommit("this is a 2nd commit to merge into the pull request", featureBranchTree2.Sha, featureBranchCommit.Sha);

        // create branch
        await _github.Git.Reference.Create(Helper.UserName, _context.RepositoryName, new NewReference("refs/heads/my-branch", featureBranchCommit2.Sha));

        var otherFeatureBranchTree = await CreateTree(new Dictionary<string, string> { { "README.md", "I am overwriting this blob with something else" } });
        var otherFeatureBranchCommit = await CreateCommit("this is the other commit to merge into the other pull request", otherFeatureBranchTree.Sha, newMaster.Sha);

        await _github.Git.Reference.Create(Helper.UserName, _context.RepositoryName, new NewReference("refs/heads/my-other-branch", otherFeatureBranchCommit.Sha));
    }

    async Task<TreeResponse> CreateTree(IEnumerable<KeyValuePair<string, string>> treeContents)
    {
        var collection = new List<NewTreeItem>();

        foreach (var c in treeContents)
        {
            var baselineBlob = new NewBlob
            {
                Content = c.Value,
                Encoding = EncodingType.Utf8
            };
            var baselineBlobResult = await _github.Git.Blob.Create(Helper.UserName, _context.RepositoryName, baselineBlob);

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

        return await _github.Git.Tree.Create(Helper.UserName, _context.RepositoryName, newTree);
    }

    async Task<Commit> CreateCommit(string message, string sha, string parent)
    {
        var newCommit = new NewCommit(message, sha, parent);
        return await _github.Git.Commit.Create(Helper.UserName, _context.RepositoryName, newCommit);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
