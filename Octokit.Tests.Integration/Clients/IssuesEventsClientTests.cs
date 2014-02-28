using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class IssuesEventsClientTests : IDisposable
{
    readonly IGitHubClient _gitHubClient;
    readonly IIssuesEventsClient _issuesEventsClientClient;
    readonly IIssuesClient _issuesClient;
    readonly Repository _repository;
    readonly string _repositoryOwner;
    readonly string _repositoryName;

    public IssuesEventsClientTests()
    {
        _gitHubClient = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };
        _issuesEventsClientClient = _gitHubClient.Issue.Events;
        _issuesClient = _gitHubClient.Issue;
        var repoName = Helper.MakeNameWithTimestamp("public-repo");

        _repository = _gitHubClient.Repository.Create(new NewRepository { Name = repoName }).Result;
        _repositoryOwner = _repository.Owner.Login;
        _repositoryName = _repository.Name;
    }

    [IntegrationTest]
    public async Task CanListEventInfoForAnIssue()
    {
        var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_repositoryOwner, _repositoryName, newIssue);
        
        var issueEventInfo = await _issuesEventsClientClient.GetForIssue(_repositoryOwner, _repositoryName, issue.Number);
        Assert.Empty(issueEventInfo);

        var closed = _issuesClient.Update(_repositoryOwner, _repository.Name, issue.Number, new IssueUpdate { State = ItemState.Closed })
            .Result;
        Assert.NotNull(closed);
        issueEventInfo = await _issuesEventsClientClient.GetForIssue(_repositoryOwner, _repositoryName, issue.Number);
        
        Assert.Equal(1, issueEventInfo.Count);
        Assert.Equal(EventInfoState.Closed, issueEventInfo[0].InfoState);
    }

    [IntegrationTest]
    public async Task CanListIssueEventsForARepository()
    {
        // create 2 new issues
        var newIssue1 = new NewIssue("A test issue1") { Body = "Everything's coming up Millhouse" };
        var newIssue2 = new NewIssue("A test issue2") { Body = "A new unassigned issue" };
        
        var issue1 = await _issuesClient.Create(_repositoryOwner, _repository.Name, newIssue1);
        Thread.Sleep(1000);
        var issue2 = await _issuesClient.Create(_repositoryOwner, _repository.Name, newIssue2);
        Thread.Sleep(1000);
        
        // close and open issue1
        var closed1 = _issuesClient.Update(_repositoryOwner, _repository.Name, issue1.Number,new IssueUpdate { State = ItemState.Closed })
            .Result;
        Assert.NotNull(closed1);
        var reopened1 = _issuesClient.Update(_repositoryOwner, _repository.Name, issue1.Number, new IssueUpdate { State = ItemState.Open })
            .Result;
        Assert.NotNull(reopened1);

        // close issue2
        var closed2 = _issuesClient.Update(_repositoryOwner, _repository.Name, issue2.Number, new IssueUpdate { State = ItemState.Closed })
            .Result;
        Assert.NotNull(closed2);
        
        var issueEvents = await _issuesEventsClientClient.GetForRepository(_repositoryOwner, _repositoryName);

        Assert.Equal(3, issueEvents.Count);
        Assert.Equal(2, issueEvents.Count(issueEvent => issueEvent.Issue.Body == "Everything's coming up Millhouse"));
    }

    [IntegrationTest]
    public async Task CanRetrieveIssueEventById()
    {
        var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
        var issue = await _issuesClient.Create(_repositoryOwner, _repositoryName, newIssue);
        var closed = _issuesClient.Update(_repositoryOwner, _repository.Name, issue.Number, new IssueUpdate { State = ItemState.Closed })
            .Result;
        Assert.NotNull(closed);
        var issueEvents = await _issuesEventsClientClient.GetForRepository(_repositoryOwner, _repositoryName);
        int issueEventId = issueEvents[0].Id;

        var issueEventLookupById = await _issuesEventsClientClient.Get(_repositoryOwner, _repositoryName, issueEventId);

        Assert.Equal(issueEventId, issueEventLookupById.Id);
        Assert.Equal(issueEvents[0].InfoState, issueEventLookupById.InfoState);
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}
