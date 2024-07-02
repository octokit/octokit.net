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
    const string labelName = "my-label";

    public PullRequestsClientTests()
    {
        _github = Helper.GetAuthenticatedClient();

        _fixture = _github.Repository.PullRequest;
        _repositoryCommentsClient = _github.Repository.Comment;

        _context = _github.CreateRepositoryContextWithAutoInit("source-repo").Result;
    }

    [IntegrationTest]
    public async Task CanCreate()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);
        Assert.Equal("a pull request", result.Title);
    }

    [IntegrationTest]
    public async Task CanCreateDraft()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a draft pull request", branchName, "main") { Draft = true };
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);
        Assert.Equal("a draft pull request", result.Title);
        Assert.True(result.Draft);
    }

    [IntegrationTest]
    public async Task CanCreateFromIssue()
    {
        await CreateTheWorld();

        var newIssue = await _github.Issue.Create(Helper.UserName, _context.RepositoryName, new NewIssue("an issue"));
        var newPullRequest = new NewPullRequest(newIssue.Number, branchName, "main");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);
        Assert.Equal(newIssue.Number, result.Number);
        Assert.Equal(newIssue.Title, result.Title);
    }

    [IntegrationTest]
    public async Task CanCreateWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var result = await _fixture.Create(_context.Repository.Id, newPullRequest);
        Assert.Equal("a pull request", result.Title);
    }

    [IntegrationTest]
    public async Task CanCreateDraftWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a draft pull request", branchName, "main") { Draft = true };
        var result = await _fixture.Create(_context.Repository.Id, newPullRequest);
        Assert.Equal("a draft pull request", result.Title);
        Assert.True(result.Draft);
    }

    [IntegrationTest]
    public async Task CanCreateFromIssueWithRepositoryId()
    {
        await CreateTheWorld();

        var newIssue = await _github.Issue.Create(_context.RepositoryId, new NewIssue("an issue"));
        var newPullRequest = new NewPullRequest(newIssue.Number, branchName, "main");
        var result = await _fixture.Create(_context.Repository.Id, newPullRequest);
        Assert.Equal(newIssue.Number, result.Number);
        Assert.Equal(newIssue.Title, result.Title);
    }

    [IntegrationTest]
    public async Task CanGetForRepository()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName);

        Assert.Single(pullRequests);
        Assert.Equal(result.Title, pullRequests[0].Title);
        Assert.True(pullRequests[0].Id > 0);
    }

    [IntegrationTest]
    public async Task CanGetDraftForRepository()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a draft pull request", branchName, "main") { Draft = true };
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName);

        Assert.Single(pullRequests);
        Assert.Equal(result.Title, pullRequests[0].Title);
        Assert.Equal(result.Draft, pullRequests[0].Draft);
        Assert.True(pullRequests[0].Id > 0);
    }

    [IntegrationTest]
    public async Task CanGetForRepositoryWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var pullRequests = await _fixture.GetAllForRepository(_context.Repository.Id);

        Assert.Single(pullRequests);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task CanGetDraftForRepositoryWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a draft pull request", branchName, "main") { Draft = true };
        var result = await _fixture.Create(_context.Repository.Id, newPullRequest);

        var pullRequests = await _fixture.GetAllForRepository(_context.Repository.Id);

        Assert.Single(pullRequests);
        Assert.Equal(result.Title, pullRequests[0].Title);
        Assert.Equal(result.Draft, pullRequests[0].Draft);
        Assert.True(pullRequests[0].Id > 0);
    }

    [IntegrationTest]
    public async Task CanGetWithAssigneesForRepository()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        // Add an assignee
        var issueUpdate = new IssueUpdate();
        issueUpdate.AddAssignee(Helper.UserName);
        await _github.Issue.Update(Helper.UserName, _context.RepositoryName, result.Number, issueUpdate);

        // Retrieve the Pull Requests
        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName);

        Assert.Single(pullRequests);
        Assert.Equal(result.Title, pullRequests[0].Title);
        Assert.Equal(Helper.UserName, pullRequests[0].Assignee.Login);
        Assert.Single(pullRequests[0].Assignees);
        Assert.Contains(pullRequests[0].Assignees, x => x.Login == Helper.UserName);
    }

    [IntegrationTest]
    public async Task CanGetWithAssigneesForRepositoryWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var result = await _fixture.Create(_context.Repository.Id, newPullRequest);

        // Add an assignee
        var issueUpdate = new IssueUpdate();
        issueUpdate.AddAssignee(Helper.UserName);
        await _github.Issue.Update(_context.Repository.Id, result.Number, issueUpdate);

        // Retrieve the Pull Requests
        var pullRequests = await _fixture.GetAllForRepository(_context.Repository.Id);

        Assert.Single(pullRequests);
        Assert.Equal(result.Title, pullRequests[0].Title);
        Assert.Equal(Helper.UserName, pullRequests[0].Assignee.Login);
        Assert.Single(pullRequests[0].Assignees);
        Assert.Contains(pullRequests[0].Assignees, x => x.Login == Helper.UserName);
    }

    [IntegrationTest]
    public async Task CanGetWithLabelsForRepository()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        // Add a label
        var issueUpdate = new IssueUpdate();
        issueUpdate.AddLabel(labelName);
        await _github.Issue.Update(Helper.UserName, _context.RepositoryName, result.Number, issueUpdate);

        // Retrieve the Pull Requests
        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName);

        Assert.Single(pullRequests);
        Assert.Equal(result.Title, pullRequests[0].Title);
        Assert.Equal(Helper.UserName, pullRequests[0].Assignee.Login);
        Assert.Single(pullRequests[0].Labels);
        Assert.Contains(pullRequests[0].Labels, x => x.Name == labelName);
    }

    [IntegrationTest]
    public async Task CanGetWithLabelsForRepositoryWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var result = await _fixture.Create(_context.Repository.Id, newPullRequest);

        // Add a label
        var issueUpdate = new IssueUpdate();
        issueUpdate.AddLabel(labelName);
        await _github.Issue.Update(_context.Repository.Id, result.Number, issueUpdate);

        // Retrieve the Pull Requests
        var pullRequests = await _fixture.GetAllForRepository(_context.Repository.Id);

        Assert.Single(pullRequests);
        Assert.Equal(result.Title, pullRequests[0].Title);
        Assert.Equal(Helper.UserName, pullRequests[0].Assignee.Login);
        Assert.Single(pullRequests[0].Labels);
        Assert.Contains(pullRequests[0].Labels, x => x.Name == labelName);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestsWithoutStart()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, options);

        Assert.Single(pullRequests);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestsWithoutStartWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var pullRequests = await _fixture.GetAllForRepository(_context.Repository.Id, options);

        Assert.Single(pullRequests);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestsWithStart()
    {
        await CreateTheWorld();

        var newPullRequest1 = new NewPullRequest("a pull request 1", branchName, "main");
        var newPullRequest2 = new NewPullRequest("a pull request 2", otherBranchName, "main");
        await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest1);
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest2);

        var options = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 1
        };

        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, options);

        Assert.Single(pullRequests);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestsWithStartWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest1 = new NewPullRequest("a pull request 1", branchName, "main");
        var newPullRequest2 = new NewPullRequest("a pull request 2", otherBranchName, "main");
        await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest1);
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest2);

        var options = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 1
        };

        var pullRequests = await _fixture.GetAllForRepository(_context.Repository.Id, options);

        Assert.Single(pullRequests);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctPullRequestsBasedOnStartPage()
    {
        await CreateTheWorld();

        var newPullRequest1 = new NewPullRequest("a pull request 1", branchName, "main");
        var newPullRequest2 = new NewPullRequest("a pull request 2", otherBranchName, "main");
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

        var newPullRequest1 = new NewPullRequest("a pull request 1", branchName, "main");
        var newPullRequest2 = new NewPullRequest("a pull request 2", otherBranchName, "main");
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

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var openPullRequests = new PullRequestRequest { State = ItemStateFilter.Open };
        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, openPullRequests);

        Assert.Single(pullRequests);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task CanGetOpenPullRequestWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var openPullRequests = new PullRequestRequest { State = ItemStateFilter.Open };
        var pullRequests = await _fixture.GetAllForRepository(_context.Repository.Id, openPullRequests);

        Assert.Single(pullRequests);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestsWithoutStartParameterized()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var openPullRequests = new PullRequestRequest { State = ItemStateFilter.Open };
        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, openPullRequests, options);

        Assert.Single(pullRequests);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestsWithoutStartParameterizedWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var result = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var openPullRequests = new PullRequestRequest { State = ItemStateFilter.Open };
        var pullRequests = await _fixture.GetAllForRepository(_context.Repository.Id, openPullRequests, options);

        Assert.Single(pullRequests);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestsWithStartParameterized()
    {
        await CreateTheWorld();

        var newPullRequest1 = new NewPullRequest("a pull request 1", branchName, "main");
        var newPullRequest2 = new NewPullRequest("a pull request 2", otherBranchName, "main");
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

        Assert.Single(pullRequests);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfPullRequestsWithStartParameterizedWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest1 = new NewPullRequest("a pull request 1", branchName, "main");
        var newPullRequest2 = new NewPullRequest("a pull request 2", otherBranchName, "main");
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

        Assert.Single(pullRequests);
        Assert.Equal(result.Title, pullRequests[0].Title);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctPullRequestsBasedOnStartPageParameterized()
    {
        await CreateTheWorld();

        var newPullRequest1 = new NewPullRequest("a pull request 1", branchName, "main");
        var newPullRequest2 = new NewPullRequest("a pull request 2", otherBranchName, "main");
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

        var newPullRequest1 = new NewPullRequest("a pull request 1", branchName, "main");
        var newPullRequest2 = new NewPullRequest("a pull request 2", otherBranchName, "main");
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

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var openPullRequests = new PullRequestRequest { State = ItemStateFilter.Closed };
        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, openPullRequests);

        Assert.Empty(pullRequests);
    }

    [IntegrationTest]
    public async Task IgnoresOpenPullRequestWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var openPullRequests = new PullRequestRequest { State = ItemStateFilter.Closed };
        var pullRequests = await _fixture.GetAllForRepository(_context.Repository.Id, openPullRequests);

        Assert.Empty(pullRequests);
    }

    [IntegrationTest]
    public async Task CanUpdate()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
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

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
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

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var updatePullRequest = new PullRequestUpdate { State = ItemState.Closed };
        var result = await _fixture.Update(Helper.UserName, _context.RepositoryName, pullRequest.Number, updatePullRequest);

        Assert.Equal(ItemState.Closed, result.State);
        Assert.Equal(pullRequest.Title, result.Title);
        Assert.Equal(pullRequest.Body, result.Body);
    }

    [IntegrationTest]
    public async Task CanLockAndUnlock()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request to lock", branchName, "main");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        await _fixture.LockUnlock.Lock(Helper.UserName, _context.RepositoryName, pullRequest.Number, LockReason.OffTopic);
        var retrived = await _fixture.Get(Helper.UserName, _context.RepositoryName, pullRequest.Number);

        Assert.Equal(retrived.ActiveLockReason, LockReason.OffTopic);

        await _fixture.LockUnlock.Unlock(Helper.UserName, _context.RepositoryName, pullRequest.Number);
        var unlocked = await _fixture.Get(Helper.UserName, _context.RepositoryName, pullRequest.Number);

        Assert.Null(unlocked.ActiveLockReason);
    }

    [IntegrationTest]
    public async Task CanFindClosedPullRequest()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var updatePullRequest = new PullRequestUpdate { State = ItemState.Closed };
        await _fixture.Update(Helper.UserName, _context.RepositoryName, pullRequest.Number, updatePullRequest);

        var closedPullRequests = new PullRequestRequest { State = ItemStateFilter.Closed };
        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, closedPullRequests);

        Assert.Single(pullRequests);
    }

    [IntegrationTest]
    public async Task CanSortPullRequests()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var newPullRequest2 = new NewPullRequest("another pull request", otherBranchName, "main");
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

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var newPullRequest2 = new NewPullRequest("another pull request", otherBranchName, "main");
        var anotherPullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest2);

        var pullRequests = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, new PullRequestRequest { SortDirection = SortDirection.Ascending });
        Assert.Equal(pullRequest.Title, pullRequests[0].Title);

        var pullRequestsDescending = await _fixture.GetAllForRepository(Helper.UserName, _context.RepositoryName, new PullRequestRequest { SortDirection = SortDirection.Descending });
        Assert.Equal(anotherPullRequest.Title, pullRequestsDescending[0].Title);
    }

    [IntegrationTest]
    public async Task IsNotMergedInitially()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var result = await _fixture.Merged(Helper.UserName, _context.RepositoryName, pullRequest.Number);

        Assert.False(result);
    }

    [IntegrationTest]
    public async Task IsNotMergedInitiallyWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var result = await _fixture.Merged(_context.Repository.Id, pullRequest.Number);

        Assert.False(result);
    }

    [IntegrationTest]
    public async Task CanBeMerged()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var merge = new MergePullRequest { CommitMessage = "thing the thing" };
        var result = await _fixture.Merge(Helper.UserName, _context.RepositoryName, pullRequest.Number, merge);

        Assert.True(result.Merged);
    }

    [IntegrationTest]
    public async Task CanBeMergedWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var merge = new MergePullRequest { CommitMessage = "thing the thing" };
        var result = await _fixture.Merge(_context.Repository.Id, pullRequest.Number, merge);

        Assert.True(result.Merged);
    }

    [IntegrationTest]
    public async Task CanBeMergedWithNoOptionalInput()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var merge = new MergePullRequest();
        var result = await _fixture.Merge(Helper.UserName, _context.RepositoryName, pullRequest.Number, merge);

        Assert.True(result.Merged);
    }

    [IntegrationTest]
    public async Task CanBeMergedWithShaSpecified()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var merge = new MergePullRequest { CommitMessage = "thing the thing", Sha = pullRequest.Head.Sha };
        var result = await _fixture.Merge(Helper.UserName, _context.RepositoryName, pullRequest.Number, merge);

        Assert.True(result.Merged);
    }

    [IntegrationTest]
    public async Task CanBeMergedWithMergeMethod()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("squash commit pull request", branchName, "main");
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

        var newPullRequest = new NewPullRequest("squash commit pull request", branchName, "main");
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

        var newPullRequest = new NewPullRequest("squash commit pull request", branchName, "main");
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

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var merge = new MergePullRequest { Sha = fakeSha };
        var ex = await Assert.ThrowsAsync<PullRequestMismatchException>(() => _fixture.Merge(Helper.UserName, _context.RepositoryName, pullRequest.Number, merge));

        Assert.StartsWith("Head branch was modified", ex.Message);
    }

    [IntegrationTest]
    public async Task CannotBeMergedDueNotInMergeableState()
    {
        await CreateTheWorld();

        var main = await _github.Git.Reference.Get(Helper.UserName, _context.RepositoryName, "heads/main");
        var newMainTree = await CreateTree(new Dictionary<string, string> { { "README.md", "Hello World, we meet again!" } });
        var mainCommit = await CreateCommit("Commit in main", newMainTree.Sha, main.Object.Sha);
        await _github.Git.Reference.Update(Helper.UserName, _context.RepositoryName, "heads/main", new ReferenceUpdate(mainCommit.Sha));

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        await Task.Delay(TimeSpan.FromSeconds(5));

        var updatedPullRequest = await _fixture.Get(Helper.UserName, _context.RepositoryName, pullRequest.Number);

        Assert.False(updatedPullRequest.Mergeable);
        Assert.Equal(MergeableState.Dirty, updatedPullRequest.MergeableState);

        var merge = new MergePullRequest { Sha = pullRequest.Head.Sha };
        var ex = await Assert.ThrowsAsync<PullRequestNotMergeableException>(() => _fixture.Merge(Helper.UserName, _context.RepositoryName, pullRequest.Number, merge));

        Assert.Equal("Pull Request is not mergeable", ex.Message);
    }

    [IntegrationTest]
    public async Task UpdatesMain()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var merge = new MergePullRequest { CommitMessage = "thing the thing" };
        var result = await _fixture.Merge(Helper.UserName, _context.RepositoryName, pullRequest.Number, merge);

        var main = await _github.Git.Reference.Get(Helper.UserName, _context.RepositoryName, "heads/main");

        Assert.Equal(result.Sha, main.Object.Sha);
    }

    [IntegrationTest]
    public async Task CanBrowseCommits()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var result = await _fixture.Commits(Helper.UserName, _context.RepositoryName, pullRequest.Number);

        Assert.Equal(2, result.Count);
        Assert.Equal("this is the commit to merge into the pull request", result[0].Commit.Message);
    }

    [IntegrationTest]
    public async Task CanBrowseCommitsWithRepositoryId()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
        var pullRequest = await _fixture.Create(Helper.UserName, _context.RepositoryName, newPullRequest);

        var result = await _fixture.Commits(_context.Repository.Id, pullRequest.Number);

        Assert.Equal(2, result.Count);
        Assert.Equal("this is the commit to merge into the pull request", result[0].Commit.Message);
    }

    [IntegrationTest]
    public async Task CanGetCommitsAndCommentCount()
    {
        await CreateTheWorld();

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
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

        var newPullRequest = new NewPullRequest("a pull request", branchName, "main");
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
            new PullRequestFile(null, "Octokit.Tests.Integration/Clients/ReferencesClientTests.cs", null, 8, 3, 11, null, null, null, null, null),
            new PullRequestFile(null, "Octokit/Clients/ApiPagination.cs", null, 21, 6, 27, null, null, null, null, null),
            new PullRequestFile(null, "Octokit/Helpers/IApiPagination.cs", null, 1, 1, 2, null, null, null, null, null),
            new PullRequestFile(null, "Octokit/Http/ApiConnection.cs", null, 1, 1, 2, null, null, null, null, null)
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
            new PullRequestFile(null, "Octokit.Tests.Integration/Clients/ReferencesClientTests.cs", null, 8, 3, 11, null, null, null, null, null),
            new PullRequestFile(null, "Octokit/Clients/ApiPagination.cs", null, 21, 6, 27, null, null, null, null, null),
            new PullRequestFile(null, "Octokit/Helpers/IApiPagination.cs", null, 1, 1, 2, null, null, null, null, null),
            new PullRequestFile(null, "Octokit/Http/ApiConnection.cs", null, 1, 1, 2, null, null, null, null, null)
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
        var main = await _github.Git.Reference.Get(Helper.UserName, _context.RepositoryName, "heads/main");

        // create new commit for main branch
        var newMainTree = await CreateTree(new Dictionary<string, string> { { "README.md", "Hello World!" } });
        var newMain = await CreateCommit("baseline for pull request", newMainTree.Sha, main.Object.Sha);

        // update main
        await _github.Git.Reference.Update(Helper.UserName, _context.RepositoryName, "heads/main", new ReferenceUpdate(newMain.Sha));

        // create new commit for feature branch
        var featureBranchTree = await CreateTree(new Dictionary<string, string> { { "README.md", "I am overwriting this blob with something new" } });
        var featureBranchCommit = await CreateCommit("this is the commit to merge into the pull request", featureBranchTree.Sha, newMain.Sha);

        var featureBranchTree2 = await CreateTree(new Dictionary<string, string> { { "README.md", "I am overwriting this blob with something new a 2nd time" } });
        var featureBranchCommit2 = await CreateCommit("this is a 2nd commit to merge into the pull request", featureBranchTree2.Sha, featureBranchCommit.Sha);

        // create branch
        await _github.Git.Reference.Create(Helper.UserName, _context.RepositoryName, new NewReference("refs/heads/my-branch", featureBranchCommit2.Sha));

        var otherFeatureBranchTree = await CreateTree(new Dictionary<string, string> { { "README.md", "I am overwriting this blob with something else" } });
        var otherFeatureBranchCommit = await CreateCommit("this is the other commit to merge into the other pull request", otherFeatureBranchTree.Sha, newMain.Sha);

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
