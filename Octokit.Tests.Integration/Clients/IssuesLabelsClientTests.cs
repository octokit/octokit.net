using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;
using Octokit.Tests.Integration.Helpers;

public class IssuesLabelsClientTests : IDisposable
{
    private readonly IIssuesLabelsClient _issuesLabelsClient;
    private readonly IIssuesClient _issuesClient;
    private readonly RepositoryContext _context;

    public IssuesLabelsClientTests()
    {
        var github = Helper.GetAuthenticatedClient();

        _issuesLabelsClient = github.Issue.Labels;
        _issuesClient = github.Issue;

        var repoName = Helper.MakeNameWithTimestamp("public-repo");

        _context = github.CreateRepositoryContext(new NewRepository(repoName)).Result;
    }

    [IntegrationTest]
    public async Task CanListIssueLabelsForAnIssue()
    {
        var newIssue = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var newLabel = new NewLabel("test label", "FFFFFF");

        var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel);
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

        var issueLabelsInfo = await _issuesLabelsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        Assert.Empty(issueLabelsInfo);

        var issueUpdate = new IssueUpdate();
        issueUpdate.AddLabel(label.Name);
        var updated = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);
        Assert.NotNull(updated);

        issueLabelsInfo = await _issuesLabelsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        Assert.Equal(1, issueLabelsInfo.Count);
        Assert.Equal(newLabel.Color, issueLabelsInfo[0].Color);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfIssueLabelsWithoutStartForAnIssue()
    {
        var newIssue = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var newLabel = new NewLabel("test label", "FFFFFF");

        var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel);
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

        var issueLabelsInfo = await _issuesLabelsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        Assert.Empty(issueLabelsInfo);

        var issueUpdate = new IssueUpdate();
        issueUpdate.AddLabel(label.Name);
        var updated = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);
        Assert.NotNull(updated);

        var options = new ApiOptions
        {
            PageCount = 1,
            PageSize = 1
        };

        issueLabelsInfo = await _issuesLabelsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number, options);

        Assert.Equal(1, issueLabelsInfo.Count);
        Assert.Equal(newLabel.Color, issueLabelsInfo[0].Color);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfIssueLabelsWithStartForAnIssue()
    {
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, new NewIssue("A test issue") { Body = "A new unassigned issue" });
        var issueUpdate = new IssueUpdate();

        var labels = new List<Label>();

        for (int i = 0; i < 2; i++)
        {
            var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, new NewLabel("test label " + (i + 1), "FFFFF" + (i+1)));
            labels.Add(label);
            issueUpdate.AddLabel(label.Name);
        }

        var issueLabelsInfo = await _issuesLabelsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        Assert.Empty(issueLabelsInfo);
        
        var updated = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);
        Assert.NotNull(updated);

        var options = new ApiOptions
        {
            PageCount = 1,
            PageSize = 1,
            StartPage = 2
        };

        issueLabelsInfo = await _issuesLabelsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number, options);

        Assert.Equal(1, issueLabelsInfo.Count);
        Assert.Equal(labels.Last().Color, issueLabelsInfo.First().Color);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctIssueLabelsBasedOnStartPageForAnIssue()
    {
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, new NewIssue("A test issue") { Body = "A new unassigned issue" });
        var issueUpdate = new IssueUpdate();
        
        for (int i = 0; i < 2; i++)
        {
            var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, new NewLabel("test label " + (i + 1), "FFFFF" + (i + 1)));
            issueUpdate.AddLabel(label.Name);
        }

        var issueLabelsInfo = await _issuesLabelsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
        Assert.Empty(issueLabelsInfo);

        var updated = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);
        Assert.NotNull(updated);

        var startOptions = new ApiOptions
        {
            PageCount = 1,
            PageSize = 1,
            StartPage = 1
        };

        var firstPage = await _issuesLabelsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _issuesLabelsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number, skipStartOptions);

        Assert.Equal(1, firstPage.Count);
        Assert.Equal(1, secondPage.Count);
        Assert.NotEqual(firstPage.First().Color, secondPage.First().Color);
    }

    [IntegrationTest]
    public async Task CanListIssueLabelsForARepository()
    {
        var newLabel1 = new NewLabel("test label 1", "FFFFFF");
        var newLabel2 = new NewLabel("test label 2", "FFFFFF");

        var originalIssueLabels = await _issuesLabelsClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName);

        await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel1);
        await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel2);

        var issueLabels = await _issuesLabelsClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName);

        Assert.Equal(originalIssueLabels.Count + 2, issueLabels.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfIssueLabelsWithoutStartForARepository()
    {
        for (int i = 0; i < 2; i++)
        {
            int k = i + 1;
            var newIssue = new NewIssue("A test issue " + k) { Body = "A new unassigned issue " + k };
            var newLabel = new NewLabel("test label " + k, "FFFFF" + k);

            var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel);
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            var issueUpdate = new IssueUpdate();
            issueUpdate.AddLabel(label.Name);
            var updated = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);
            Assert.NotNull(updated);
        }

        var options = new ApiOptions
        {
            PageCount = 1,
            PageSize = 1
        };

        var issueLabelsInfo = await _issuesLabelsClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, options);

        Assert.Equal(1, issueLabelsInfo.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfIssueLabelsWithStartForARepository()
    {
        for (int i = 0; i < 2; i++)
        {
            int k = i + 1;
            var newIssue = new NewIssue("A test issue " + k) { Body = "A new unassigned issue " + k };
            var newLabel = new NewLabel("test label " + k, "FFFFF" + k);

            var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel);
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            var issueUpdate = new IssueUpdate();
            issueUpdate.AddLabel(label.Name);
            var updated = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);
            Assert.NotNull(updated);
        }

        var options = new ApiOptions
        {
            PageCount = 1,
            PageSize = 1,
            StartPage = 2
        };

        var issueLabelsInfo = await _issuesLabelsClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, options);

        Assert.Equal(1, issueLabelsInfo.Count);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctIssueLabelsBasedOnStartPageForARepository()
    {
        for (int i = 0; i < 2; i++)
        {
            int k = i + 1;
            var newIssue = new NewIssue("A test issue " + k) { Body = "A new unassigned issue " + k };
            var newLabel = new NewLabel("test label " + k, "FFFFF" + k);

            var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel);
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            var issueUpdate = new IssueUpdate();
            issueUpdate.AddLabel(label.Name);
            var updated = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);
            Assert.NotNull(updated);
        }

        var startOptions = new ApiOptions
        {
            PageCount = 1,
            PageSize = 1,
            StartPage = 1
        };

        var firstPage = await _issuesLabelsClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _issuesLabelsClient.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, skipStartOptions);

        Assert.Equal(1, firstPage.Count);
        Assert.Equal(1, secondPage.Count);
        Assert.NotEqual(firstPage.First().Color, secondPage.First().Color);
    }

    [IntegrationTest]
    public async Task CanListLabelsForAnMilestone()
    {
        var newIssue = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var newLabel = new NewLabel("test label", "FFFFFF");
        var newMilestone = new NewMilestone("New Milestone");

        var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel);
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        var milestone = await _issuesClient.Milestone.Create(_context.RepositoryOwner, _context.RepositoryName, newMilestone);
        
        var issueLabelsInfo = await _issuesLabelsClient.GetAllForMilestone(_context.RepositoryOwner, _context.RepositoryName, milestone.Number);
        Assert.Empty(issueLabelsInfo);

        var issueUpdate = new IssueUpdate { Milestone = milestone.Number };
        issueUpdate.AddLabel(label.Name);

        var updated = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);
        Assert.NotNull(updated);

        issueLabelsInfo = await _issuesLabelsClient.GetAllForMilestone(_context.RepositoryOwner, _context.RepositoryName, milestone.Number);

        Assert.Equal(1, issueLabelsInfo.Count);
        Assert.Equal(label.Color, issueLabelsInfo[0].Color);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfIssueLabelsWithoutStartForAMilestone()
    {
        var newMilestone = new NewMilestone("New Milestone");
        var milestone = await _issuesClient.Milestone.Create(_context.RepositoryOwner, _context.RepositoryName, newMilestone);

        for (int i = 0; i < 2; i++)
        {
            int k = i + 1;
            var newIssue = new NewIssue("A test issue " + k) { Body = "A new unassigned issue " + k };
            var newLabel = new NewLabel("test label " + k, "FFFFF" + k);

            var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel);
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            var issueUpdate = new IssueUpdate { Milestone = milestone.Number };
            issueUpdate.AddLabel(label.Name);
            var updated = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);
            Assert.NotNull(updated);
        }

        var options = new ApiOptions
        {
            PageCount = 1,
            PageSize = 1
        };

        var issueLabelsInfo = await _issuesLabelsClient.GetAllForMilestone(_context.RepositoryOwner, _context.RepositoryName, milestone.Number, options);

        Assert.Equal(1, issueLabelsInfo.Count);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfIssueLabelsWithStartForAMilestone()
    {
        var newMilestone = new NewMilestone("New Milestone");
        var milestone = await _issuesClient.Milestone.Create(_context.RepositoryOwner, _context.RepositoryName, newMilestone);

        for (int i = 0; i < 2; i++)
        {
            int k = i + 1;
            var newIssue = new NewIssue("A test issue " + k) { Body = "A new unassigned issue " + k };
            var newLabel = new NewLabel("test label " + k, "FFFFF" + k);

            var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel);
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            var issueUpdate = new IssueUpdate { Milestone = milestone.Number };
            issueUpdate.AddLabel(label.Name);
            var updated = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);
            Assert.NotNull(updated);
        }

        var options = new ApiOptions
        {
            PageCount = 1,
            PageSize = 1,
            StartPage = 2
        };

        var issueLabelsInfo = await _issuesLabelsClient.GetAllForMilestone(_context.RepositoryOwner, _context.RepositoryName, milestone.Number, options);

        Assert.Equal(1, issueLabelsInfo.Count);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctIssueLabelsBasedOnStartPageForAMilestone()
    {
        var newMilestone = new NewMilestone("New Milestone");
        var milestone = await _issuesClient.Milestone.Create(_context.RepositoryOwner, _context.RepositoryName, newMilestone);

        for (int i = 0; i < 2; i++)
        {
            int k = i + 1;
            var newIssue = new NewIssue("A test issue " + k) { Body = "A new unassigned issue " + k };
            var newLabel = new NewLabel("test label " + k, "FFFFF" + k);

            var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel);
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            var issueUpdate = new IssueUpdate { Milestone = milestone.Number };
            issueUpdate.AddLabel(label.Name);
            var updated = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);
            Assert.NotNull(updated);
        }

        var startOptions = new ApiOptions
        {
            PageCount = 1,
            PageSize = 1,
            StartPage = 1
        };

        var firstPage = await _issuesLabelsClient.GetAllForMilestone(_context.RepositoryOwner, _context.RepositoryName, milestone.Number, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _issuesLabelsClient.GetAllForMilestone(_context.RepositoryOwner, _context.RepositoryName, milestone.Number, skipStartOptions);

        Assert.Equal(1, firstPage.Count);
        Assert.Equal(1, secondPage.Count);
        Assert.NotEqual(firstPage.First().Color, secondPage.First().Color);
    }

    [IntegrationTest]
    public async Task CanRetrieveIssueLabelByName()
    {
        var newLabel = new NewLabel("test label 1b", "FFFFFF");
        var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel);
        Assert.NotNull(label);

        var issueLabelLookupByName = await _issuesLabelsClient.Get(_context.RepositoryOwner, _context.RepositoryName, label.Name);

        Assert.Equal(label.Name, issueLabelLookupByName.Name);
        Assert.Equal(label.Color, issueLabelLookupByName.Color);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
