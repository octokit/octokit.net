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
    public async Task ReturnsAllIssuesForARepository()
    {
        var issues = await _client.GetAllForRepository("libgit2", "libgit2sharp").ToList();

        Assert.NotEmpty(issues);
    }

    [IntegrationTest]
    public async Task ReturnsAllIssuesForCurrentUser()
    {
        var newIssue = new NewIssue("Integration test issue") { Assignee = _context.RepositoryOwner };
        await _client.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

        var issues = await _client.GetAllForCurrent().ToList();

        Assert.NotEmpty(issues);
    }

    [IntegrationTest]
    public async Task ReturnsAllIssuesForOwnedAndMemberRepositories()
    {
        var newIssue = new NewIssue("Integration test issue") { Assignee = _context.RepositoryOwner };
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

    public void Dispose()
    {
        _context.Dispose();
    }
}

