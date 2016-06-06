using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;
using Octokit.Tests.Integration.Helpers;

public class MilestonesClientTests : IDisposable
{
    private readonly IGitHubClient _github;
    private readonly IMilestonesClient _milestonesClient;
    private readonly RepositoryContext _context;

    public MilestonesClientTests()
    {
        _github = Helper.GetAuthenticatedClient();

        _milestonesClient = _github.Issue.Milestone;
        var repoName = Helper.MakeNameWithTimestamp("public-repo");

        _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
    }

    [IntegrationTest]
    public async Task CanRetrieveOneMilestone()
    {
        var newMilestone = new NewMilestone("a milestone") { DueOn = DateTime.Now };
        var created = await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newMilestone);

        var result = await _milestonesClient.Get(_context.RepositoryOwner, _context.RepositoryName, created.Number);

        Assert.Equal("a milestone", result.Title);
    }

    [IntegrationTest]
    public async Task CanListEmptyMilestones()
    {
        var milestones = await _milestonesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName);

        Assert.Empty(milestones);
    }

    [IntegrationTest]
    public async Task CanListMilestonesWithDefaultSortByDueDateAsc()
    {
        var milestone1 = new NewMilestone("milestone 1") { DueOn = DateTime.Now };
        var milestone2 = new NewMilestone("milestone 2") { DueOn = DateTime.Now.AddDays(1) };
        var milestone3 = new NewMilestone("milestone 3") { DueOn = DateTime.Now.AddDays(3), State = ItemState.Closed };
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone1);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone2);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone3);

        var milestones = await _milestonesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName);
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
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone1);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone2);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone3);

        var milestones = await _milestonesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName,
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
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone1);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone2);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone3);

        var milestones = await _milestonesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName,
            new MilestoneRequest { State = ItemStateFilter.Closed });

        Assert.Equal(1, milestones.Count);
        Assert.Equal("milestone 3", milestones[0].Title);
    }

    [IntegrationTest]
    public async Task CanRetrieveClosedIssues()
    {
        var newIssue = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var issue1 = await _github.Issue.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        var issue2 = await _github.Issue.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        await _github.Issue.Update(_context.RepositoryOwner, _context.RepositoryName, issue1.Number,
        new IssueUpdate { State = ItemState.Closed });
        await _github.Issue.Update(_context.RepositoryOwner, _context.RepositoryName, issue2.Number,
        new IssueUpdate { State = ItemState.Closed });

        var retrieved = await _github.Issue.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName,
            new RepositoryIssueRequest { State = ItemStateFilter.Closed });

        Assert.True(retrieved.Count >= 2);
        Assert.True(retrieved.Any(i => i.Number == issue1.Number));
        Assert.True(retrieved.Any(i => i.Number == issue2.Number));
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfMilestonesWithoutStart()
    {
        var milestone1 = new NewMilestone("milestone 1") { DueOn = DateTime.Now };
        var milestone2 = new NewMilestone("milestone 2") { DueOn = DateTime.Now.AddDays(1) };
        var milestone3 = new NewMilestone("milestone 3") { DueOn = DateTime.Now.AddDays(3) };
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone1);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone2);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone3);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var milestones = await _milestonesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, options);

        Assert.Equal(3, milestones.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfMilestonesWithStart()
    {
        var milestone1 = new NewMilestone("milestone 1") { DueOn = DateTime.Now };
        var milestone2 = new NewMilestone("milestone 2") { DueOn = DateTime.Now.AddDays(1) };
        var milestone3 = new NewMilestone("milestone 3") { DueOn = DateTime.Now.AddDays(3) };
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone1);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone2);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone3);

        var options = new ApiOptions
        {
            PageSize = 2,
            PageCount = 1,
            StartPage = 2
        };

        var milestones = await _milestonesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, options);

        Assert.Equal(1, milestones.Count);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctResultsBasedOnStartPage()
    {
        var milestone1 = new NewMilestone("milestone 1") { DueOn = DateTime.Now };
        var milestone2 = new NewMilestone("milestone 2") { DueOn = DateTime.Now.AddDays(1) };
        var milestone3 = new NewMilestone("milestone 3") { DueOn = DateTime.Now.AddDays(3), State = ItemState.Closed };
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone1);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone2);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone3);
        
        var startOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1
        };

        var firstPage = await _milestonesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _milestonesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, skipStartOptions);

        Assert.NotEqual(firstPage[0].Number, secondPage[0].Number);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfMilestonesWithoutStartParametrized()
    {
        var milestone1 = new NewMilestone("milestone 1") { DueOn = DateTime.Now };
        var milestone2 = new NewMilestone("milestone 2") { DueOn = DateTime.Now.AddDays(1), State = ItemState.Closed };
        var milestone3 = new NewMilestone("milestone 3") { DueOn = DateTime.Now.AddDays(3), State = ItemState.Closed };
        var milestone4 = new NewMilestone("milestone 4") { DueOn = DateTime.Now.AddDays(4), State = ItemState.Closed };
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone1);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone2);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone3);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone4);

        var options = new ApiOptions
        {
            PageSize = 3,
            PageCount = 1
        };

        var milestones = await _milestonesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, new MilestoneRequest { State = ItemStateFilter.Closed }, options);

        Assert.Equal(3, milestones.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfMilestonesWithStartParametrized()
    {
        var milestone1 = new NewMilestone("milestone 1") { DueOn = DateTime.Now };
        var milestone2 = new NewMilestone("milestone 2") { DueOn = DateTime.Now.AddDays(1), State = ItemState.Closed };
        var milestone3 = new NewMilestone("milestone 3") { DueOn = DateTime.Now.AddDays(3), State = ItemState.Closed };
        var milestone4 = new NewMilestone("milestone 4") { DueOn = DateTime.Now.AddDays(4), State = ItemState.Closed };
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone1);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone2);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone3);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone4);

        var options = new ApiOptions
        {
            PageSize = 2,
            PageCount = 1,
            StartPage = 2
        };

        var milestones = await _milestonesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, new MilestoneRequest { State = ItemStateFilter.Closed }, options);

        Assert.Equal(1, milestones.Count);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctResultsBasedOnStartPageParametrized()
    {
        var milestone1 = new NewMilestone("milestone 1") { DueOn = DateTime.Now };
        var milestone2 = new NewMilestone("milestone 2") { DueOn = DateTime.Now.AddDays(1), State = ItemState.Closed };
        var milestone3 = new NewMilestone("milestone 3") { DueOn = DateTime.Now.AddDays(3), State = ItemState.Closed };
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone1);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone2);
        await _milestonesClient.Create(_context.RepositoryOwner, _context.RepositoryName, milestone3);

        var milestoneRequest = new MilestoneRequest { State = ItemStateFilter.Closed };

        var startOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1
        };

        var firstPage = await _milestonesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, milestoneRequest, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };
        
        var secondPage = await _milestonesClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, milestoneRequest, skipStartOptions);

        Assert.NotEqual(firstPage[0].Number, secondPage[0].Number);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
