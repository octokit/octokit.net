using Octokit;
using Octokit.Reactive;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Tests.Integration;
using Xunit;
using Octokit.Tests.Integration.Helpers;

public class ObservableIssuesClientTests : IDisposable
{
    private readonly RepositoryContext _context;
    private readonly ObservableIssuesClient _client;

    public ObservableIssuesClientTests()
    {
        var github = Helper.GetAuthenticatedClient();

        _client = new ObservableIssuesClient(github);

        var repoName = Helper.MakeNameWithTimestamp("public-repo");
        _context = github.CreateRepositoryContext(new NewRepository(repoName)).Result;
    }

    [IntegrationTest]
    public async Task ReturnsSpecifiedIssue()
    {
        var observable = _client.Get("libgit2", "libgit2sharp", 1);
        var issue = await observable;

        Assert.Equal(1, issue.Number);
        Assert.Equal("Change License ", issue.Title);
    }

    [IntegrationTest]
    public async Task ReturnsPageOfIssuesForARepository()
    {
        var options = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1
        };

        var issues = await _client.GetAllForRepository("libgit2", "libgit2sharp", options).ToList();

        Assert.Equal(5, issues.Count);
    }

    [IntegrationTest]
    public async Task ReturnsPageOfIssuesFromStartForARepository()
    {
        var first = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1
        };

        var firstPage = await _client.GetAllForRepository("libgit2", "libgit2sharp", first).ToList();

        var second = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _client.GetAllForRepository("libgit2", "libgit2sharp", second).ToList();

        Assert.Equal(5, firstPage.Count);
        Assert.Equal(5, secondPage.Count);

        Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
        Assert.NotEqual(firstPage[1].Id, secondPage[1].Id);
        Assert.NotEqual(firstPage[2].Id, secondPage[2].Id);
        Assert.NotEqual(firstPage[3].Id, secondPage[3].Id);
        Assert.NotEqual(firstPage[4].Id, secondPage[4].Id);
    }

    [IntegrationTest]
    public async Task ReturnsAllIssuesForCurrentUser()
    {
        var newIssue = new NewIssue("Integration test issue");
        newIssue.Assignees.Add(_context.RepositoryOwner);
        await _client.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

        var issues = await _client.GetAllForCurrent().ToList();

        Assert.NotEmpty(issues);
    }

    [IntegrationTest]
    public async Task ReturnsAllIssuesForOwnedAndMemberRepositories()
    {
        var newIssue = new NewIssue("Integration test issue");
        newIssue.Assignees.Add(_context.RepositoryOwner);
        await _client.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        var result = await _client.GetAllForOwnedAndMemberRepositories().ToList();

        Assert.NotEmpty(result);
    }

    [IntegrationTest]
    public async Task CanCreateAndUpdateIssues()
    {
        var newIssue = new NewIssue("Integration test issue");

        var createResult = await _client.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        var updateResult = await _client.Update(_context.RepositoryOwner, _context.RepositoryName, createResult.Number, new IssueUpdate { Title = "Modified integration test issue" });

        Assert.Equal("Modified integration test issue", updateResult.Title);
    }

    [IntegrationTest]
    public async Task CanLockAndUnlockIssues()
    {
        var newIssue = new NewIssue("Integration Test Issue");

        var createResult = await _client.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        Assert.False(createResult.Locked);

        await _client.LockUnlock.Lock(_context.RepositoryOwner, _context.RepositoryName, createResult.Number);
        var lockResult = await _client.Get(_context.RepositoryOwner, _context.RepositoryName, createResult.Number);
        Assert.True(lockResult.Locked);

        await _client.LockUnlock.Unlock(_context.RepositoryOwner, _context.RepositoryName, createResult.Number);
        var unlockIssueResult = await _client.Get(_context.RepositoryOwner, _context.RepositoryName, createResult.Number);
        Assert.False(unlockIssueResult.Locked);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

