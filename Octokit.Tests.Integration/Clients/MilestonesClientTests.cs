using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class MilestonesClientTests : IDisposable
{
    readonly IGitHubClient _gitHubClient;
    readonly IMilestonesClient _milestonesClient;
    readonly Repository _repository;
    readonly string _repositoryOwner;
    readonly string _repositoryName;

    public MilestonesClientTests()
    {
        _gitHubClient = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };
        _milestonesClient = _gitHubClient.Issue.Milestone;
        var repoName = Helper.MakeNameWithTimestamp("public-repo");

        _repository = _gitHubClient.Repository.Create(new NewRepository { Name = repoName }).Result;
        _repositoryOwner = _repository.Owner.Login;
        _repositoryName = _repository.Name;
    }

    [IntegrationTest]
    public async Task CanRetrieveOneMilestone()
    {
        var newMilestone = new NewMilestone("a milestone") { DueOn = DateTime.Now };
        var created = await _milestonesClient.Create(_repositoryOwner, _repositoryName, newMilestone);

        var result = await _milestonesClient.Get(_repositoryOwner, _repositoryName, created.Number);

        Assert.Equal("a milestone", result.Title);
    }

    [IntegrationTest]
    public async Task CanListEmptyMilestones()
    {
        var milestones = await _milestonesClient.GetForRepository(_repositoryOwner, _repositoryName);
        
        Assert.Empty(milestones);
    }

    [IntegrationTest]
    public async Task CanListMilestonesWithDefaultSortByDueDateAsc()
    {
        var milestone1 = new NewMilestone("milestone 1") { DueOn = DateTime.Now };
        var milestone2 = new NewMilestone("milestone 2") { DueOn = DateTime.Now.AddDays(1) };
        var milestone3 = new NewMilestone("milestone 3") { DueOn = DateTime.Now.AddDays(3), State = ItemState.Closed };
        await _milestonesClient.Create(_repositoryOwner, _repositoryName, milestone1);
        await _milestonesClient.Create(_repositoryOwner, _repositoryName, milestone2);
        await _milestonesClient.Create(_repositoryOwner, _repositoryName, milestone3);

        var milestones = await _milestonesClient.GetForRepository(_repositoryOwner, _repositoryName);
        Assert.Equal(2, milestones.Count);
        Assert.Equal("milestone 1", milestones[0].Title);
        Assert.Equal("milestone 2", milestones[1].Title);
    }

    [IntegrationTest]
    public async Task CanListMilestonesWithSortByDueDateDesc()
    {
        var milestone1 = new NewMilestone("milestone 1") { DueOn = DateTime.Now };
        var milestone2 = new NewMilestone("milestone 2") { DueOn = DateTime.Now.AddDays(1) };
        var milestone3 = new NewMilestone("milestone 3") { DueOn = DateTime.Now.AddDays(3), State = ItemState.Closed };
        await _milestonesClient.Create(_repositoryOwner, _repositoryName, milestone1);
        await _milestonesClient.Create(_repositoryOwner, _repositoryName, milestone2);
        await _milestonesClient.Create(_repositoryOwner, _repositoryName, milestone3);

        var milestones = await _milestonesClient.GetForRepository(_repositoryOwner, _repositoryName,
            new MilestoneRequest { SortDirection = SortDirection.Descending });
        Assert.Equal(2, milestones.Count);
        Assert.Equal("milestone 2", milestones[0].Title);
        Assert.Equal("milestone 1", milestones[1].Title);
    }

    [IntegrationTest]
    public async Task CanListClosedMilestones()
    {
        var milestone1 = new NewMilestone("milestone 1") { DueOn = DateTime.Now };
        var milestone2 = new NewMilestone("milestone 2") { DueOn = DateTime.Now.AddDays(1) };
        var milestone3 = new NewMilestone("milestone 3") { DueOn = DateTime.Now.AddDays(3), State = ItemState.Closed };
        await _milestonesClient.Create(_repositoryOwner, _repositoryName, milestone1);
        await _milestonesClient.Create(_repositoryOwner, _repositoryName, milestone2);
        await _milestonesClient.Create(_repositoryOwner, _repositoryName, milestone3);

        var milestones = await _milestonesClient.GetForRepository(_repositoryOwner, _repositoryName,
            new MilestoneRequest { State = ItemState.Closed });

        Assert.Equal(1, milestones.Count);
        Assert.Equal("milestone 3", milestones[0].Title);
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
