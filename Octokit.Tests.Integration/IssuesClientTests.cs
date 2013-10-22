using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class IssuesClientTests : IDisposable
{
    readonly IGitHubClient _gitHubClient;
    readonly Repository _repository;

    public IssuesClientTests()
    {
        _gitHubClient = new GitHubClient("Test Runner User Agent")
        {
            Credentials = Helper.Credentials
        };
        var repoName = Helper.MakeNameWithTimestamp("public-repo");

        _repository = _gitHubClient.Repository.Create(new NewRepository { Name = repoName }).Result;
    }

    [IntegrationTest]
    public async Task CanCreateRetrieveAndDeleteIssue()
    {
        string owner = _repository.Owner.Login;

        var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
        var issue = await _gitHubClient.Issue.Create(owner, _repository.Name, newIssue);
        try
        {
            Assert.NotNull(issue);

            var retrieved = await _gitHubClient.Issue.Get(owner, _repository.Name, issue.Number);
            var all = await _gitHubClient.Issue.GetForRepository(owner, _repository.Name);
            Assert.NotNull(retrieved);
            Assert.True(all.Any(i => i.Number == retrieved.Number));
        }
        finally
        {
            var closed = _gitHubClient.Issue.Update(owner, _repository.Name, issue.Number,
            new IssueUpdate { State = ItemState.Closed })
            .Result;
            Assert.NotNull(closed);
        }
    }

    [IntegrationTest]
    public async Task CanRetrieveClosedIssues()
    {
        string owner = _repository.Owner.Login;

        var newIssue = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var issue1 = await _gitHubClient.Issue.Create(owner, _repository.Name, newIssue);
        var issue2 = await _gitHubClient.Issue.Create(owner, _repository.Name, newIssue);
        await _gitHubClient.Issue.Update(owner, _repository.Name, issue1.Number,
        new IssueUpdate { State = ItemState.Closed });
        await _gitHubClient.Issue.Update(owner, _repository.Name, issue2.Number,
        new IssueUpdate { State = ItemState.Closed });

        var retrieved = await _gitHubClient.Issue.GetForRepository(owner, _repository.Name,
            new RepositoryIssueRequest { State = ItemState.Closed });

        Assert.True(retrieved.Count >= 2);
        Assert.True(retrieved.Any(i => i.Number == issue1.Number));
        Assert.True(retrieved.Any(i => i.Number == issue2.Number));
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}
