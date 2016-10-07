using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class IssuesEventsClientTests : IDisposable
{
    private readonly IIssuesEventsClient _issuesEventsClient;
    private readonly IIssuesClient _issuesClient;
    private readonly RepositoryContext _context;

    public IssuesEventsClientTests()
    {
        var github = Helper.GetAuthenticatedClient();

        _issuesEventsClient = github.Issue.Events;
        _issuesClient = github.Issue;

        var repoName = Helper.MakeNameWithTimestamp("public-repo");

        _context = github.CreateRepositoryContext(new NewRepository(repoName)).Result;
    }

    [IntegrationTest]
    public async Task CanListEventInfoForAnIssue()
    {
        var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

        var issueEventInfo = await _issuesEventsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        Assert.Empty(issueEventInfo);

        var closed = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new IssueUpdate { State = ItemState.Closed });
        Assert.NotNull(closed);
        issueEventInfo = await _issuesEventsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        Assert.Equal(1, issueEventInfo.Count);
        Assert.Equal(EventInfoState.Closed, issueEventInfo[0].Event);
    }

    [IntegrationTest]
    public async Task CanListEventInfoForAnIssueWithRepositoryId()
    {
        var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

        var issueEventInfo = await _issuesEventsClient.GetAllForIssue(_context.Repository.Id, issue.Number);
        Assert.Empty(issueEventInfo);

        var closed = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new IssueUpdate { State = ItemState.Closed });
        Assert.NotNull(closed);
        issueEventInfo = await _issuesEventsClient.GetAllForIssue(_context.Repository.Id, issue.Number);

        Assert.Equal(1, issueEventInfo.Count);
        Assert.Equal(EventInfoState.Closed, issueEventInfo[0].Event);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfEventInfosWithoutStart()
    {
        var newIssue = new NewIssue("issue 1") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Unlock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var eventInfos = await _issuesEventsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number, options);

        Assert.Equal(3, eventInfos.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfEventInfosWithoutStartWithRepositoryId()
    {
        var newIssue = new NewIssue("issue 1") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Unlock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var eventInfos = await _issuesEventsClient.GetAllForIssue(_context.Repository.Id, issue.Number, options);

        Assert.Equal(3, eventInfos.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfEventInfosWithStart()
    {
        var newIssue = new NewIssue("issue 1") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Unlock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        var options = new ApiOptions
        {
            PageSize = 2,
            PageCount = 1,
            StartPage = 2
        };

        var eventInfos = await _issuesEventsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number, options);

        Assert.Equal(1, eventInfos.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfEventInfosWithStartWithRepositoryId()
    {
        var newIssue = new NewIssue("issue 1") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Unlock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        var options = new ApiOptions
        {
            PageSize = 2,
            PageCount = 1,
            StartPage = 2
        };

        var eventInfos = await _issuesEventsClient.GetAllForIssue(_context.Repository.Id, issue.Number, options);

        Assert.Equal(1, eventInfos.Count);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctEventInfosBasedOnStartPage()
    {
        var newIssue = new NewIssue("issue 1") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Unlock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        var startOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1
        };

        var firstPage = await _issuesEventsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _issuesEventsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number, skipStartOptions);

        Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctEventInfosBasedOnStartPageWithRepositoryId()
    {
        var newIssue = new NewIssue("issue 1") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Unlock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        var startOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1
        };

        var firstPage = await _issuesEventsClient.GetAllForIssue(_context.Repository.Id, issue.Number, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _issuesEventsClient.GetAllForIssue(_context.Repository.Id, issue.Number, skipStartOptions);

        Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
    }

    [IntegrationTest]
    public async Task CanListIssueEventsForARepository()
    {
        // create 2 new issues
        var newIssue1 = new NewIssue("A test issue1") { Body = "Everything's coming up Millhouse" };
        var newIssue2 = new NewIssue("A test issue2") { Body = "A new unassigned issue" };

        var issue1 = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue1);
        await Task.Delay(1000);
        var issue2 = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue2);
        await Task.Delay(1000);

        // close and open issue1
        var closed1 = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue1.Number, new IssueUpdate { State = ItemState.Closed });
        Assert.NotNull(closed1);
        var reopened1 = _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue1.Number, new IssueUpdate { State = ItemState.Open });
        Assert.NotNull(reopened1);

        // close issue2
        var closed2 = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue2.Number, new IssueUpdate { State = ItemState.Closed });
        Assert.NotNull(closed2);

        var issueEvents = await _issuesEventsClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName);

        Assert.Equal(3, issueEvents.Count);
        Assert.Equal(2, issueEvents.Count(issueEvent => issueEvent.Issue.Body == "Everything's coming up Millhouse"));
    }

    [IntegrationTest]
    public async Task CanListIssueEventsForARepositoryWithRepositoryId()
    {
        // create 2 new issues
        var newIssue1 = new NewIssue("A test issue1") { Body = "Everything's coming up Millhouse" };
        var newIssue2 = new NewIssue("A test issue2") { Body = "A new unassigned issue" };

        var issue1 = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue1);
        await Task.Delay(1000);
        var issue2 = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue2);
        await Task.Delay(1000);

        // close and open issue1
        var closed1 = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue1.Number, new IssueUpdate { State = ItemState.Closed });
        Assert.NotNull(closed1);
        var reopened1 = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue1.Number, new IssueUpdate { State = ItemState.Open });
        Assert.NotNull(reopened1);

        // close issue2
        var closed2 = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue2.Number, new IssueUpdate { State = ItemState.Closed });
        Assert.NotNull(closed2);

        var issueEvents = await _issuesEventsClient.GetAllForRepository(_context.Repository.Id);

        Assert.Equal(3, issueEvents.Count);
        Assert.Equal(2, issueEvents.Count(issueEvent => issueEvent.Issue.Body == "Everything's coming up Millhouse"));
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfIssueEventsWithoutStart()
    {
        var newIssue = new NewIssue("issue 1") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Unlock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var eventInfos = await _issuesEventsClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, options);

        Assert.Equal(3, eventInfos.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfIssueEventsWithoutStartWithRepositoryId()
    {
        var newIssue = new NewIssue("issue 1") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Unlock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var eventInfos = await _issuesEventsClient.GetAllForRepository(_context.Repository.Id, options);

        Assert.Equal(3, eventInfos.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfIssueEventsWithStart()
    {
        var newIssue = new NewIssue("issue 1") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Unlock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        var options = new ApiOptions
        {
            PageSize = 2,
            PageCount = 1,
            StartPage = 2
        };

        var eventInfos = await _issuesEventsClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, options);

        Assert.Equal(1, eventInfos.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfIssueEventsWithStartWithRepositoryId()
    {
        var newIssue = new NewIssue("issue 1") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Unlock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        var options = new ApiOptions
        {
            PageSize = 2,
            PageCount = 1,
            StartPage = 2
        };

        var eventInfos = await _issuesEventsClient.GetAllForRepository(_context.Repository.Id, options);

        Assert.Equal(1, eventInfos.Count);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctIssueEventsBasedOnStartPage()
    {
        var newIssue = new NewIssue("issue 1") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Unlock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        var startOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1
        };

        var firstPage = await _issuesEventsClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _issuesEventsClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, skipStartOptions);

        Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctIssueEventsBasedOnStartPageWithRepositoryId()
    {
        var newIssue = new NewIssue("issue 1") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Unlock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        await _issuesClient.Lock(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        var startOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1
        };

        var firstPage = await _issuesEventsClient.GetAllForRepository(_context.Repository.Id, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _issuesEventsClient.GetAllForRepository(_context.Repository.Id, skipStartOptions);

        Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
    }

    [IntegrationTest]
    public async Task CanRetrieveIssueEventById()
    {
        var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        var closed = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new IssueUpdate { State = ItemState.Closed });
        Assert.NotNull(closed);
        var issueEvents = await _issuesEventsClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName);
        int issueEventId = issueEvents[0].Id;

        var issueEventLookupById = await _issuesEventsClient.Get(_context.RepositoryOwner, _context.RepositoryName, issueEventId);

        Assert.Equal(issueEventId, issueEventLookupById.Id);
        Assert.Equal(issueEvents[0].Event, issueEventLookupById.Event);
    }

    [IntegrationTest]
    public async Task CanRetrieveIssueEventByIdWithRepositoryId()
    {
        var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        var closed = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new IssueUpdate { State = ItemState.Closed });
        Assert.NotNull(closed);
        var issueEvents = await _issuesEventsClient.GetAllForRepository(_context.Repository.Id);
        int issueEventId = issueEvents[0].Id;

        var issueEventLookupById = await _issuesEventsClient.Get(_context.Repository.Id, issueEventId);

        Assert.Equal(issueEventId, issueEventLookupById.Id);
        Assert.Equal(issueEvents[0].Event, issueEventLookupById.Event);
    }

    [IntegrationTest]
    public async Task CanDeserializeUnsubscribeEvent()
    {
        var client = Helper.GetAuthenticatedClient();
        var issue = await client.Issue.Events.Get("waffleio", "waffle.io", 142230057);
        Assert.Equal(EventInfoState.Unsubscribed, issue.Event);
    }

    [IntegrationTest]
    public async Task CanDeserializeMergedEvent()
    {
        var issueEvent = await _issuesEventsClient.Get("octokit", "octokit.net", 490490630);

        Assert.NotNull(issueEvent);
        Assert.Equal(EventInfoState.Merged, issueEvent.Event);
        Assert.Equal("0bb8747a0ad1a9efff201ea017a0a6a4f69b797e", issueEvent.CommitId);
        Assert.Equal(new Uri("https://api.github.com/repos/octokit/octokit.net/commits/0bb8747a0ad1a9efff201ea017a0a6a4f69b797e"), issueEvent.CommitUrl);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
