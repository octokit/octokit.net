using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Helpers;
using Octokit.Tests.Integration;
using Xunit;
using Octokit.Tests.Integration.Helpers;

public class IssuesClientTests : IDisposable
{
    private readonly RepositoryContext _context;
    private readonly IIssuesClient _issuesClient;

    public IssuesClientTests()
    {
        var github = Helper.GetAuthenticatedClient();
        var repoName = Helper.MakeNameWithTimestamp("public-repo");
        _issuesClient = github.Issue;
        _context = github.CreateRepositoryContext(new NewRepository(repoName)).Result;
    }

    [IntegrationTest]
    public async Task CanDeserializeIssue()
    {
        const string title = "a test issue";
        const string description = "A new unassigned issue";
        var newIssue = new NewIssue(title) { Body = description };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        var retrieved = await _issuesClient.Get(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        Assert.NotNull(retrieved);
        Assert.NotEqual(0, issue.Id);
        Assert.Equal(false, issue.Locked);
        Assert.Equal(title, retrieved.Title);
        Assert.Equal(description, retrieved.Body);
    }

    [IntegrationTest]
    public async Task CanCreateRetrieveAndCloseIssue()
    {
        var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        try
        {
            Assert.NotNull(issue);

            var retrieved = await _issuesClient.Get(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
            var all = await _issuesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName);
            Assert.NotNull(retrieved);
            Assert.True(all.Any(i => i.Number == retrieved.Number));
        }
        finally
        {
            var closed = _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number,
            new IssueUpdate { State = ItemState.Closed })
            .Result;
            Assert.NotNull(closed);
        }
    }

    [IntegrationTest]
    public async Task CanListOpenIssuesWithDefaultSort()
    {
        var newIssue1 = new NewIssue("A test issue1") { Body = "A new unassigned issue" };
        var newIssue2 = new NewIssue("A test issue2") { Body = "A new unassigned issue" };
        var newIssue3 = new NewIssue("A test issue3") { Body = "A new unassigned issue" };
        var newIssue4 = new NewIssue("A test issue4") { Body = "A new unassigned issue" };
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue1);
        Thread.Sleep(1000);
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue2);
        Thread.Sleep(1000);
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue3);
        var closed = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue4);
        await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, closed.Number,
            new IssueUpdate { State = ItemState.Closed });

        var issues = await _issuesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName);

        Assert.Equal(3, issues.Count);
        Assert.Equal("A test issue3", issues[0].Title);
        Assert.Equal("A test issue2", issues[1].Title);
        Assert.Equal("A test issue1", issues[2].Title);
    }

    [IntegrationTest]
    public async Task CanListIssuesWithAscendingSort()
    {
        var newIssue1 = new NewIssue("A test issue1") { Body = "A new unassigned issue" };
        var newIssue2 = new NewIssue("A test issue2") { Body = "A new unassigned issue" };
        var newIssue3 = new NewIssue("A test issue3") { Body = "A new unassigned issue" };
        var newIssue4 = new NewIssue("A test issue4") { Body = "A new unassigned issue" };
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue1);
        Thread.Sleep(1000);
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue2);
        Thread.Sleep(1000);
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue3);
        var closed = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue4);
        await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, closed.Number,
            new IssueUpdate { State = ItemState.Closed });

        var issues = await _issuesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName,
            new RepositoryIssueRequest { SortDirection = SortDirection.Ascending });

        Assert.Equal(3, issues.Count);
        Assert.Equal("A test issue1", issues[0].Title);
        Assert.Equal("A test issue2", issues[1].Title);
        Assert.Equal("A test issue3", issues[2].Title);
    }

    [IntegrationTest]
    public async Task CanListClosedIssues()
    {
        var newIssue1 = new NewIssue("A test issue1") { Body = "A new unassigned issue" };
        var newIssue2 = new NewIssue("A closed issue") { Body = "A new unassigned issue" };
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue1);
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue2);
        var closed = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue2);
        await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, closed.Number,
            new IssueUpdate { State = ItemState.Closed });

        var issues = await _issuesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName,
            new RepositoryIssueRequest { State = ItemState.Closed });

        Assert.Equal(1, issues.Count);
        Assert.Equal("A closed issue", issues[0].Title);
    }

    [IntegrationTest]
    public async Task CanListMilestoneIssues()
    {
        var milestone = await _issuesClient.Milestone.Create(_context.RepositoryOwner, _context.RepositoryName, new NewMilestone("milestone"));
        var newIssue1 = new NewIssue("A test issue1") { Body = "A new unassigned issue" };
        var newIssue2 = new NewIssue("A milestone issue") { Body = "A new unassigned issue", Milestone = milestone.Number };
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue1);
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue2);

        var issues = await _issuesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName,
            new RepositoryIssueRequest { Milestone = milestone.Number.ToString(CultureInfo.InvariantCulture) });

        Assert.Equal(1, issues.Count);
        Assert.Equal("A milestone issue", issues[0].Title);
    }

    [IntegrationTest(Skip = "This is paging for a long long time")]
    public async Task CanRetrieveAllIssues()
    {
        var newIssue1 = new NewIssue("A test issue1") { Body = "A new unassigned issue" };
        var newIssue2 = new NewIssue("A test issue2") { Body = "A new unassigned issue" };
        var newIssue3 = new NewIssue("A test issue3") { Body = "A new unassigned issue" };
        var newIssue4 = new NewIssue("A test issue4") { Body = "A new unassigned issue" };
        var issue1 = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue1);
        var issue2 = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue2);
        var issue3 = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue3);
        var issue4 = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue4);
        await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue4.Number,
        new IssueUpdate { State = ItemState.Closed });

        var retrieved = await _issuesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName,
            new RepositoryIssueRequest { State = ItemState.All });

        Assert.True(retrieved.Count >= 4);
        Assert.True(retrieved.Any(i => i.Number == issue1.Number));
        Assert.True(retrieved.Any(i => i.Number == issue2.Number));
        Assert.True(retrieved.Any(i => i.Number == issue3.Number));
        Assert.True(retrieved.Any(i => i.Number == issue4.Number));
    }

    [IntegrationTest]
    public async Task CanFilterByAssigned()
    {
        var newIssue1 = new NewIssue("An assigned issue") { Body = "Assigning this to myself", Assignee = _context.RepositoryOwner };
        var newIssue2 = new NewIssue("An unassigned issue") { Body = "A new unassigned issue" };
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue1);
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue2);

        var allIssues = await _issuesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName,
            new RepositoryIssueRequest());

        Assert.Equal(2, allIssues.Count);

        var assignedIssues = await _issuesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName,
            new RepositoryIssueRequest { Assignee = _context.RepositoryOwner });

        Assert.Equal(1, assignedIssues.Count);
        Assert.Equal("An assigned issue", assignedIssues[0].Title);

        var unassignedIssues = await _issuesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName,
            new RepositoryIssueRequest { Assignee = "none" });

        Assert.Equal(1, unassignedIssues.Count);
        Assert.Equal("An unassigned issue", unassignedIssues[0].Title);
    }

    [IntegrationTest]
    public async Task CanFilterByCreator()
    {
        var newIssue1 = new NewIssue("An issue") { Body = "words words words" };
        var newIssue2 = new NewIssue("Another issue") { Body = "some other words" };
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue1);
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue2);

        var allIssues = await _issuesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName,
            new RepositoryIssueRequest());

        Assert.Equal(2, allIssues.Count);

        var issuesCreatedByOwner = await _issuesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName,
            new RepositoryIssueRequest { Creator = _context.RepositoryOwner });

        Assert.Equal(2, issuesCreatedByOwner.Count);

        var issuesCreatedByExternalUser = await _issuesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName,
            new RepositoryIssueRequest { Creator = "shiftkey" });

        Assert.Equal(0, issuesCreatedByExternalUser.Count);
    }

    [IntegrationTest]
    public async Task CanFilterByMentioned()
    {
        var newIssue1 = new NewIssue("An issue") { Body = "words words words hello there @shiftkey" };
        var newIssue2 = new NewIssue("Another issue") { Body = "some other words" };
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue1);
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue2);

        var allIssues = await _issuesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName,
            new RepositoryIssueRequest());

        Assert.Equal(2, allIssues.Count);

        var mentionsWithShiftkey = await _issuesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName,
            new RepositoryIssueRequest { Mentioned = "shiftkey" });

        Assert.Equal(1, mentionsWithShiftkey.Count);

        var mentionsWithHaacked = await _issuesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName,
            new RepositoryIssueRequest { Mentioned = "haacked" });

        Assert.Equal(0, mentionsWithHaacked.Count);
    }

    [IntegrationTest]
    public async Task FilteringByInvalidAccountThrowsError()
    {
        await Assert.ThrowsAsync<ApiValidationException>(
            () => _issuesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName,
                new RepositoryIssueRequest { Assignee = "some-random-account" }));
    }

    [IntegrationTest]
    public async Task CanAssignAndUnassignMilestone()
    {
        var newMilestone = new NewMilestone("a milestone");
        var milestone = await _issuesClient.Milestone.Create(_context.RepositoryOwner, _context.RepositoryName, newMilestone);

        var newIssue1 = new NewIssue("A test issue1")
        {
            Body = "A new unassigned issue",
            Milestone = milestone.Number
        };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue1);

        Assert.NotNull(issue.Milestone);

        var issueUpdate = issue.ToUpdate();
        issueUpdate.Milestone = null;

        var updatedIssue = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);

        Assert.Null(updatedIssue.Milestone);
    }

    [IntegrationTest]
    public async Task DoesNotChangeLabelsByDefault()
    {
        await _issuesClient.Labels.Create(_context.RepositoryOwner, _context.RepositoryName, new NewLabel("something", "FF0000"));

        var newIssue = new NewIssue("A test issue1")
        {
            Body = "A new unassigned issue",
        };
        newIssue.Labels.Add("something");

        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

        var issueUpdate = issue.ToUpdate();

        var updatedIssue = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);

        Assert.Equal(1, updatedIssue.Labels.Count);
    }

    [IntegrationTest]
    public async Task CanUpdateLabelForAnIssue()
    {
        // create some labels
        await _issuesClient.Labels.Create(_context.RepositoryOwner, _context.RepositoryName, new NewLabel("something", "FF0000"));
        await _issuesClient.Labels.Create(_context.RepositoryOwner, _context.RepositoryName, new NewLabel("another thing", "0000FF"));

        // setup us the issue
        var newIssue = new NewIssue("A test issue1")
        {
            Body = "A new unassigned issue",
        };
        newIssue.Labels.Add("something");

        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

        // update the issue
        var issueUpdate = issue.ToUpdate();
        issueUpdate.AddLabel("another thing");

        var updatedIssue = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);

        Assert.Equal("another thing", updatedIssue.Labels[0].Name);
    }

    [IntegrationTest]
    public async Task CanClearLabelsForAnIssue()
    {
        // create some labels
        await _issuesClient.Labels.Create(_context.RepositoryOwner, _context.RepositoryName, new NewLabel("something", "FF0000"));
        await _issuesClient.Labels.Create(_context.RepositoryOwner, _context.RepositoryName, new NewLabel("another thing", "0000FF"));

        // setup us the issue
        var newIssue = new NewIssue("A test issue1")
        {
            Body = "A new unassigned issue",
        };
        newIssue.Labels.Add("something");
        newIssue.Labels.Add("another thing");

        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        Assert.Equal(2, issue.Labels.Count);

        // update the issue
        var issueUpdate = issue.ToUpdate();
        issueUpdate.ClearLabels();

        var updatedIssue = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);

        Assert.Empty(updatedIssue.Labels);
    }

    [IntegrationTest]
    public async Task CanAccessUrls()
    {
        var expctedUri = "https://api.github.com/repos/{0}/{1}/issues/{2}/{3}";

        var newIssue = new NewIssue("A test issue")
        {
            Body = "A new unassigned issue",
        };

        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

        Assert.NotNull(issue.CommentsUrl);
        Assert.Equal(new Uri(string.Format(expctedUri, _context.RepositoryOwner, _context.RepositoryName, issue.Number, "comments")), issue.CommentsUrl);
        Assert.NotNull(issue.EventsUrl);
        Assert.Equal(new Uri(string.Format(expctedUri, _context.RepositoryOwner, _context.RepositoryName, issue.Number, "events")), issue.EventsUrl);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
