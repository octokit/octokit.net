﻿using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

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

        Assert.True(issue.Id > 0);
        Assert.False(issue.Locked);
        Assert.Equal(title, issue.Title);
        Assert.Equal(description, issue.Body);

        var retrieved = await _issuesClient.Get(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        Assert.True(retrieved.Id > 0);
        Assert.False(retrieved.Locked);
        Assert.Equal(title, retrieved.Title);
        Assert.Equal(description, retrieved.Body);
    }

    [IntegrationTest]
    public async Task ReturnsPageOfIssuesForARepository()
    {
        var options = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1
        };

        var issues = await _issuesClient.GetAllForRepository("libgit2", "libgit2sharp", options);

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

        var firstPage = await _issuesClient.GetAllForRepository("libgit2", "libgit2sharp", first);

        var second = new ApiOptions
        {
            PageSize = 5,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _issuesClient.GetAllForRepository("libgit2", "libgit2sharp", second);

        Assert.Equal(5, firstPage.Count);
        Assert.Equal(5, secondPage.Count);

        Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
        Assert.NotEqual(firstPage[1].Id, secondPage[1].Id);
        Assert.NotEqual(firstPage[2].Id, secondPage[2].Id);
        Assert.NotEqual(firstPage[3].Id, secondPage[3].Id);
        Assert.NotEqual(firstPage[4].Id, secondPage[4].Id);
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
            var closed = _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new IssueUpdate { State = ItemState.Closed }).Result;
            Assert.NotNull(closed);
        }
    }

    [IntegrationTest]
    public async Task CanLockAndUnlockIssue()
    {
        var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        Assert.False(issue.Locked);

        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        var retrieved = await _issuesClient.Get(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        Assert.NotNull(retrieved);
        Assert.True(retrieved.Locked);

        await _issuesClient.Unlock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        retrieved = await _issuesClient.Get(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        Assert.NotNull(retrieved);
        Assert.False(retrieved.Locked);
    }

    [IntegrationTest]
    public async Task CanListOpenIssuesWithDefaultSort()
    {
        var newIssue1 = new NewIssue("A test issue1") { Body = "A new unassigned issue" };
        var newIssue2 = new NewIssue("A test issue2") { Body = "A new unassigned issue" };
        var newIssue3 = new NewIssue("A test issue3") { Body = "A new unassigned issue" };
        var newIssue4 = new NewIssue("A test issue4") { Body = "A new unassigned issue" };
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue1);

        await Task.Delay(1000);
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue2);
        await Task.Delay(1000);
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
        await Task.Delay(1000);
        await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue2);
        await Task.Delay(1000);
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
            new RepositoryIssueRequest { State = ItemStateFilter.Closed });

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

    [IntegrationTest]
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
        await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue4.Number, new IssueUpdate { State = ItemState.Closed });

        var request = new RepositoryIssueRequest { State = ItemStateFilter.All };

        var retrieved = await _issuesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, request);

        Assert.Equal(4, retrieved.Count);
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
            Body = "A new unassigned issue"
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
            Body = "A new unassigned issue"
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
            Body = "A new unassigned issue"
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
        var expectedUri = "https://api.github.com/repos/{0}/{1}/issues/{2}/{3}";

        var newIssue = new NewIssue("A test issue")
        {
            Body = "A new unassigned issue"
        };

        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

        Assert.NotNull(issue.CommentsUrl);
        Assert.Equal(new Uri(string.Format(expectedUri, _context.RepositoryOwner, _context.RepositoryName, issue.Number, "comments")), issue.CommentsUrl);
        Assert.NotNull(issue.EventsUrl);
        Assert.Equal(new Uri(string.Format(expectedUri, _context.RepositoryOwner, _context.RepositoryName, issue.Number, "events")), issue.EventsUrl);
    }

    [IntegrationTest]
    public async Task GetAllForCurrentContainsRepositoryData()
    {
        var issuesForCurrentUser = await _issuesClient.GetAllForCurrent();

        foreach (var issue in issuesForCurrentUser)
        {
            Assert.Equal(Helper.UserName, issue.User.Login);
            Assert.NotNull(issue.Repository);
        }
    }

    [IntegrationTest]
    public async Task GetAllForOwnedAndMemberRepositoriesContainsRepositoryData()
    {
        var issuesForOwnedAndMemberRepositories = await _issuesClient.GetAllForOwnedAndMemberRepositories();

        foreach (var issue in issuesForOwnedAndMemberRepositories)
        {
            Assert.NotNull(issue.Repository);
        }
    }

    [IntegrationTest]
    public async Task GetAllForOrganizationContainsRepositoryData()
    {
        var issuesForOrganization = await _issuesClient.GetAllForOrganization(Helper.Organization);

        foreach (var issue in issuesForOrganization)
        {
            Assert.NotNull(issue.Repository);
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
