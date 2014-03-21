using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Helpers;
using Octokit.Tests.Integration;
using Xunit;

public class IssuesClientTests : IDisposable
{
    readonly IGitHubClient _gitHubClient;
    readonly Repository _repository;
    readonly IIssuesClient _issuesClient;

    public IssuesClientTests()
    {
        _gitHubClient = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };
        var repoName = Helper.MakeNameWithTimestamp("public-repo");
        _issuesClient = _gitHubClient.Issue;
        _repository = _gitHubClient.Repository.Create(new NewRepository { Name = repoName }).Result;
    }

    [IntegrationTest]
    public async Task CanCreateRetrieveAndCloseIssue()
    {
        string owner = _repository.Owner.Login;

        var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(owner, _repository.Name, newIssue);
        try
        {
            Assert.NotNull(issue);

            var retrieved = await _issuesClient.Get(owner, _repository.Name, issue.Number);
            var all = await _issuesClient.GetForRepository(owner, _repository.Name);
            Assert.NotNull(retrieved);
            Assert.True(all.Any(i => i.Number == retrieved.Number));
        }
        finally
        {
            var closed = _issuesClient.Update(owner, _repository.Name, issue.Number,
            new IssueUpdate { State = ItemState.Closed })
            .Result;
            Assert.NotNull(closed);
        }
    }

    [IntegrationTest]
    public async Task CanListOpenIssuesWithDefaultSort()
    {
        string owner = _repository.Owner.Login;
        var newIssue1 = new NewIssue("A test issue1") { Body = "A new unassigned issue" };
        var newIssue2 = new NewIssue("A test issue2") { Body = "A new unassigned issue" };
        var newIssue3 = new NewIssue("A test issue3") { Body = "A new unassigned issue" };
        var newIssue4 = new NewIssue("A test issue4") { Body = "A new unassigned issue" };
        await _issuesClient.Create(owner, _repository.Name, newIssue1);
        Thread.Sleep(1000); 
        await _issuesClient.Create(owner, _repository.Name, newIssue2);
        Thread.Sleep(1000); 
        await _issuesClient.Create(owner, _repository.Name, newIssue3);
        var closed = await _issuesClient.Create(owner, _repository.Name, newIssue4);
        await _issuesClient.Update(owner, _repository.Name, closed.Number,
            new IssueUpdate { State = ItemState.Closed });

        var issues = await _issuesClient.GetForRepository(owner, _repository.Name);

        Assert.Equal(3, issues.Count);
        Assert.Equal("A test issue3", issues[0].Title);
        Assert.Equal("A test issue2", issues[1].Title);
        Assert.Equal("A test issue1", issues[2].Title);
    }

    [IntegrationTest]
    public async Task CanListIssuesWithAscendingSort()
    {
        string owner = _repository.Owner.Login;

        var newIssue1 = new NewIssue("A test issue1") { Body = "A new unassigned issue" };
        var newIssue2 = new NewIssue("A test issue2") { Body = "A new unassigned issue" };
        var newIssue3 = new NewIssue("A test issue3") { Body = "A new unassigned issue" };
        var newIssue4 = new NewIssue("A test issue4") { Body = "A new unassigned issue" };
        await _issuesClient.Create(owner, _repository.Name, newIssue1);
        Thread.Sleep(1000);
        await _issuesClient.Create(owner, _repository.Name, newIssue2);
        Thread.Sleep(1000);
        await _issuesClient.Create(owner, _repository.Name, newIssue3);
        var closed = await _issuesClient.Create(owner, _repository.Name, newIssue4);
        await _issuesClient.Update(owner, _repository.Name, closed.Number,
            new IssueUpdate { State = ItemState.Closed });

        var issues = await _issuesClient.GetForRepository(owner, _repository.Name,
            new RepositoryIssueRequest {SortDirection = SortDirection.Ascending});

        Assert.Equal(3, issues.Count);
        Assert.Equal("A test issue1", issues[0].Title);
        Assert.Equal("A test issue2", issues[1].Title); 
        Assert.Equal("A test issue3", issues[2].Title);
    }

    [IntegrationTest]
    public async Task CanListClosedIssues()
    {
        string owner = _repository.Owner.Login;

        var newIssue1 = new NewIssue("A test issue1") { Body = "A new unassigned issue" };
        var newIssue2 = new NewIssue("A closed issue") { Body = "A new unassigned issue" };
        await _issuesClient.Create(owner, _repository.Name, newIssue1);
        await _issuesClient.Create(owner, _repository.Name, newIssue2);
        var closed = await _issuesClient.Create(owner, _repository.Name, newIssue2);
        await _issuesClient.Update(owner, _repository.Name, closed.Number,
            new IssueUpdate { State = ItemState.Closed });

        var issues = await _issuesClient.GetForRepository(owner, _repository.Name,
            new RepositoryIssueRequest { State = ItemState.Closed });

        Assert.Equal(1, issues.Count);
        Assert.Equal("A closed issue", issues[0].Title);
    }

    [IntegrationTest]
    public async Task CanListMilestoneIssues()
    {
        string owner = _repository.Owner.Login;
        var milestone = await _issuesClient.Milestone.Create(owner, _repository.Name, new NewMilestone("milestone"));
        var newIssue1 = new NewIssue("A test issue1") { Body = "A new unassigned issue" };
        var newIssue2 = new NewIssue("A milestone issue") { Body = "A new unassigned issue", Milestone = milestone.Number };
        await _issuesClient.Create(owner, _repository.Name, newIssue1);
        await _issuesClient.Create(owner, _repository.Name, newIssue2);

        var issues = await _issuesClient.GetForRepository(owner, _repository.Name,
            new RepositoryIssueRequest { Milestone = milestone.Number.ToString(CultureInfo.InvariantCulture) });

        Assert.Equal(1, issues.Count);
        Assert.Equal("A milestone issue", issues[0].Title);
    }

    [IntegrationTest]
    public async Task CanRetrieveClosedIssues()
    {
        string owner = _repository.Owner.Login;

        var newIssue = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var issue1 = await _issuesClient.Create(owner, _repository.Name, newIssue);
        var issue2 = await _issuesClient.Create(owner, _repository.Name, newIssue);
        await _issuesClient.Update(owner, _repository.Name, issue1.Number,
        new IssueUpdate { State = ItemState.Closed });
        await _issuesClient.Update(owner, _repository.Name, issue2.Number,
        new IssueUpdate { State = ItemState.Closed });

        var retrieved = await _issuesClient.GetForRepository(owner, _repository.Name,
            new RepositoryIssueRequest { State = ItemState.Closed });

        Assert.True(retrieved.Count >= 2);
        Assert.True(retrieved.Any(i => i.Number == issue1.Number));
        Assert.True(retrieved.Any(i => i.Number == issue2.Number));
    }

    [IntegrationTest]
    public async Task CanFilterByAssigned()
    {
        var owner = _repository.Owner.Login;
        var newIssue1 = new NewIssue("An assigned issue") { Body = "Assigning this to myself", Assignee = owner };
        var newIssue2 = new NewIssue("An unassigned issue") { Body = "A new unassigned issue" };
        await _issuesClient.Create(owner, _repository.Name, newIssue1);
        await _issuesClient.Create(owner, _repository.Name, newIssue2);

        var allIssues = await _issuesClient.GetForRepository(owner, _repository.Name,
            new RepositoryIssueRequest());

        Assert.Equal(2, allIssues.Count);

        var assignedIssues = await _issuesClient.GetForRepository(owner, _repository.Name, 
            new RepositoryIssueRequest { Assignee = owner });

        Assert.Equal(1, assignedIssues.Count);
        Assert.Equal("An assigned issue", assignedIssues[0].Title);

        var unassignedIssues = await _issuesClient.GetForRepository(owner, _repository.Name,
            new RepositoryIssueRequest { Assignee = "none" });

        Assert.Equal(1, unassignedIssues.Count);
        Assert.Equal("An unassigned issue", unassignedIssues[0].Title);
    }

    [IntegrationTest]
    public async Task CanFilterByCreator()
    {
        var owner = _repository.Owner.Login;
        var newIssue1 = new NewIssue("An issue") { Body = "words words words" };
        var newIssue2 = new NewIssue("Another issue") { Body = "some other words" };
        await _issuesClient.Create(owner, _repository.Name, newIssue1);
        await _issuesClient.Create(owner, _repository.Name, newIssue2);

        var allIssues = await _issuesClient.GetForRepository(owner, _repository.Name,
            new RepositoryIssueRequest());

        Assert.Equal(2, allIssues.Count);

        var issuesCreatedByOwner = await _issuesClient.GetForRepository(owner, _repository.Name,
            new RepositoryIssueRequest { Creator = owner });

        Assert.Equal(2, issuesCreatedByOwner.Count);

        var issuesCreatedByExternalUser = await _issuesClient.GetForRepository(owner, _repository.Name,
            new RepositoryIssueRequest { Creator = "shiftkey" });

        Assert.Equal(0, issuesCreatedByExternalUser.Count);
    }

    [IntegrationTest]
    public async Task CanFilterByMentioned()
    {
        var owner = _repository.Owner.Login;
        var newIssue1 = new NewIssue("An issue") { Body = "words words words hello there @shiftkey" };
        var newIssue2 = new NewIssue("Another issue") { Body = "some other words" };
        await _issuesClient.Create(owner, _repository.Name, newIssue1);
        await _issuesClient.Create(owner, _repository.Name, newIssue2);

        var allIssues = await _issuesClient.GetForRepository(owner, _repository.Name,
            new RepositoryIssueRequest());

        Assert.Equal(2, allIssues.Count);

        var mentionsWithShiftkey = await _issuesClient.GetForRepository(owner, _repository.Name,
            new RepositoryIssueRequest { Mentioned = "shiftkey" });

        Assert.Equal(1, mentionsWithShiftkey.Count);

        var mentionsWithHaacked = await _issuesClient.GetForRepository(owner, _repository.Name,
            new RepositoryIssueRequest { Mentioned = "haacked" });

        Assert.Equal(0, mentionsWithHaacked.Count);
    }

    [IntegrationTest]
    public async Task FilteringByInvalidAccountThrowsError()
    {
        var owner = _repository.Owner.Login;

        await AssertEx.Throws<ApiValidationException>(
            async () => await _issuesClient.GetForRepository(owner, _repository.Name,
                new RepositoryIssueRequest { Creator = "some-random-account" }));

        await AssertEx.Throws<ApiValidationException>(
            async () => await _issuesClient.GetForRepository(owner, _repository.Name,
                new RepositoryIssueRequest { Assignee = "some-random-account" }));
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}
