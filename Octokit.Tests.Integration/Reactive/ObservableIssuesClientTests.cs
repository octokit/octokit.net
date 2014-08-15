using Octokit;
using Octokit.Reactive;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Tests.Integration;
using Xunit;

public class ObservableIssuesClientTests : IDisposable
{
    readonly ObservableIssuesClient _client;
    readonly string _repoName;
    readonly Repository _createdRepository;

    public ObservableIssuesClientTests()
    {
        var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };

        _client = new ObservableIssuesClient(github);
        _repoName = Helper.MakeNameWithTimestamp("public-repo");
        var result = github.Repository.Create(new NewRepository { Name = _repoName }).Result;
        _createdRepository = result;
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
        var issues = await _client.GetForRepository("libgit2", "libgit2sharp").ToList();

        Assert.NotEmpty(issues);
    }

    [IntegrationTest]
    public async Task ReturnsAllIssuesForCurrentUser()
    {
        var newIssue = new NewIssue("Integration test issue") { Assignee = _createdRepository.Owner.Login };
        await _client.Create(_createdRepository.Owner.Login, _repoName, newIssue);

        var issues = await _client.GetAllForCurrent().ToList();

        Assert.NotEmpty(issues);
    }

    [IntegrationTest]
    public async Task ReturnsAllIssuesForOwnedAndMemberRepositories()
    {
        var newIssue = new NewIssue("Integration test issue") { Assignee = _createdRepository.Owner.Login };
        await _client.Create(_createdRepository.Owner.Login, _repoName, newIssue);
        var result = await _client.GetAllForOwnedAndMemberRepositories().ToList();

        Assert.NotEmpty(result);
    }

    [IntegrationTest]
    public async Task CanCreateAndUpdateIssues()
    {
        var newIssue = new NewIssue("Integration test issue");

        var createResult = await _client.Create(_createdRepository.Owner.Login, _repoName, newIssue);
        var updateResult = await _client.Update(_createdRepository.Owner.Login, _repoName, createResult.Number, new IssueUpdate { Title = "Modified integration test issue" });

        Assert.Equal("Modified integration test issue", updateResult.Title);
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_createdRepository);
    }
}

