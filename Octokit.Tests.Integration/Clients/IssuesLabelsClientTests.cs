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

        Assert.Single(issueLabelsInfo);
        Assert.Equal(newLabel.Color, issueLabelsInfo[0].Color);
        Assert.Equal(newLabel.Description, issueLabelsInfo[0].Description);
    }

    [IntegrationTest]
    public async Task CanListIssueLabelsForAnIssueWithRepositoryId()
    {
        var newIssue = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var newLabel = new NewLabel("test label", "FFFFFF");

        var label = await _issuesLabelsClient.Create(_context.Repository.Id, newLabel);
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

        var issueLabelsInfo = await _issuesLabelsClient.GetAllForIssue(_context.Repository.Id, issue.Number);
        Assert.Empty(issueLabelsInfo);

        var issueUpdate = new IssueUpdate();
        issueUpdate.AddLabel(label.Name);
        var updated = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);
        Assert.NotNull(updated);

        issueLabelsInfo = await _issuesLabelsClient.GetAllForIssue(_context.Repository.Id, issue.Number);

        Assert.Single(issueLabelsInfo);
        Assert.Equal(newLabel.Color, issueLabelsInfo[0].Color);
        Assert.Equal(newLabel.Description, issueLabelsInfo[0].Description);
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

        Assert.Single(issueLabelsInfo);
        Assert.Equal(newLabel.Color, issueLabelsInfo[0].Color);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfIssueLabelsWithoutStartForAnIssueWithRepositoryId()
    {
        var newIssue = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var newLabel = new NewLabel("test label", "FFFFFF");

        var label = await _issuesLabelsClient.Create(_context.Repository.Id, newLabel);
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

        var issueLabelsInfo = await _issuesLabelsClient.GetAllForIssue(_context.Repository.Id, issue.Number);
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

        issueLabelsInfo = await _issuesLabelsClient.GetAllForIssue(_context.Repository.Id, issue.Number, options);

        Assert.Single(issueLabelsInfo);
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
            var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, new NewLabel("test label " + (i + 1), "FFFFF" + (i + 1)));
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

        Assert.Single(issueLabelsInfo);
        Assert.Equal(labels.Last().Color, issueLabelsInfo.First().Color);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfIssueLabelsWithStartForAnIssueWithRepositoryId()
    {
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, new NewIssue("A test issue") { Body = "A new unassigned issue" });
        var issueUpdate = new IssueUpdate();

        var labels = new List<Label>();

        for (int i = 0; i < 2; i++)
        {
            var label = await _issuesLabelsClient.Create(_context.Repository.Id, new NewLabel("test label " + (i + 1), "FFFFF" + (i + 1)));
            labels.Add(label);
            issueUpdate.AddLabel(label.Name);
        }

        var issueLabelsInfo = await _issuesLabelsClient.GetAllForIssue(_context.Repository.Id, issue.Number);
        Assert.Empty(issueLabelsInfo);

        var updated = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);
        Assert.NotNull(updated);

        var options = new ApiOptions
        {
            PageCount = 1,
            PageSize = 1,
            StartPage = 2
        };

        issueLabelsInfo = await _issuesLabelsClient.GetAllForIssue(_context.Repository.Id, issue.Number, options);

        Assert.Single(issueLabelsInfo);
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

        Assert.Single(firstPage);
        Assert.Single(secondPage);
        Assert.NotEqual(firstPage.First().Color, secondPage.First().Color);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctIssueLabelsBasedOnStartPageForAnIssueWithRepositoryId()
    {
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, new NewIssue("A test issue") { Body = "A new unassigned issue" });
        var issueUpdate = new IssueUpdate();

        for (int i = 0; i < 2; i++)
        {
            var label = await _issuesLabelsClient.Create(_context.Repository.Id, new NewLabel("test label " + (i + 1), "FFFFF" + (i + 1)));
            issueUpdate.AddLabel(label.Name);
        }

        var issueLabelsInfo = await _issuesLabelsClient.GetAllForIssue(_context.Repository.Id, issue.Number);
        Assert.Empty(issueLabelsInfo);

        var updated = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);
        Assert.NotNull(updated);

        var startOptions = new ApiOptions
        {
            PageCount = 1,
            PageSize = 1,
            StartPage = 1
        };

        var firstPage = await _issuesLabelsClient.GetAllForIssue(_context.Repository.Id, issue.Number, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _issuesLabelsClient.GetAllForIssue(_context.Repository.Id, issue.Number, skipStartOptions);

        Assert.Single(firstPage);
        Assert.Single(secondPage);
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
    public async Task CanListIssueLabelsForARepositoryWithRepositoryId()
    {
        var newLabel1 = new NewLabel("test label 1", "FFFFFF");
        var newLabel2 = new NewLabel("test label 2", "FFFFFF");

        var originalIssueLabels = await _issuesLabelsClient.GetAllForRepository(_context.Repository.Id);

        await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel1);
        await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel2);

        var issueLabels = await _issuesLabelsClient.GetAllForRepository(_context.Repository.Id);

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

        Assert.Single(issueLabelsInfo);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfIssueLabelsWithoutStartForARepositoryWithRepositoryId()
    {
        for (int i = 0; i < 2; i++)
        {
            int k = i + 1;
            var newIssue = new NewIssue("A test issue " + k) { Body = "A new unassigned issue " + k };
            var newLabel = new NewLabel("test label " + k, "FFFFF" + k);

            var label = await _issuesLabelsClient.Create(_context.Repository.Id, newLabel);
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

        var issueLabelsInfo = await _issuesLabelsClient.GetAllForRepository(_context.Repository.Id, options);

        Assert.Single(issueLabelsInfo);
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

        Assert.Single(issueLabelsInfo);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfIssueLabelsWithStartForARepositoryWithRepositoryId()
    {
        for (int i = 0; i < 2; i++)
        {
            int k = i + 1;
            var newIssue = new NewIssue("A test issue " + k) { Body = "A new unassigned issue " + k };
            var newLabel = new NewLabel("test label " + k, "FFFFF" + k);

            var label = await _issuesLabelsClient.Create(_context.Repository.Id, newLabel);
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

        var issueLabelsInfo = await _issuesLabelsClient.GetAllForRepository(_context.Repository.Id, options);

        Assert.Single(issueLabelsInfo);
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

        Assert.Single(firstPage);
        Assert.Single(secondPage);
        Assert.NotEqual(firstPage.First().Color, secondPage.First().Color);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctIssueLabelsBasedOnStartPageForARepositoryWithRepositoryId()
    {
        for (int i = 0; i < 2; i++)
        {
            int k = i + 1;
            var newIssue = new NewIssue("A test issue " + k) { Body = "A new unassigned issue " + k };
            var newLabel = new NewLabel("test label " + k, "FFFFF" + k);

            var label = await _issuesLabelsClient.Create(_context.Repository.Id, newLabel);
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

        var firstPage = await _issuesLabelsClient.GetAllForRepository(_context.Repository.Id, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _issuesLabelsClient.GetAllForRepository(_context.Repository.Id, skipStartOptions);

        Assert.Single(firstPage);
        Assert.Single(secondPage);
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

        Assert.Single(issueLabelsInfo);
        Assert.Equal(label.Color, issueLabelsInfo[0].Color);
    }

    [IntegrationTest]
    public async Task CanListLabelsForAMilestoneWithRepositoryId()
    {
        var newIssue = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var newLabel = new NewLabel("test label", "FFFFFF");
        var newMilestone = new NewMilestone("New Milestone");

        var label = await _issuesLabelsClient.Create(_context.Repository.Id, newLabel);
        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        var milestone = await _issuesClient.Milestone.Create(_context.RepositoryOwner, _context.RepositoryName, newMilestone);

        var issueLabelsInfo = await _issuesLabelsClient.GetAllForMilestone(_context.Repository.Id, milestone.Number);
        Assert.Empty(issueLabelsInfo);

        var issueUpdate = new IssueUpdate { Milestone = milestone.Number };
        issueUpdate.AddLabel(label.Name);

        var updated = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, issueUpdate);
        Assert.NotNull(updated);

        issueLabelsInfo = await _issuesLabelsClient.GetAllForMilestone(_context.Repository.Id, milestone.Number);

        Assert.Single(issueLabelsInfo);
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

        Assert.Single(issueLabelsInfo);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfIssueLabelsWithoutStartForAMilestoneWithRepositoryId()
    {
        var newMilestone = new NewMilestone("New Milestone");
        var milestone = await _issuesClient.Milestone.Create(_context.RepositoryOwner, _context.RepositoryName, newMilestone);

        for (int i = 0; i < 2; i++)
        {
            int k = i + 1;
            var newIssue = new NewIssue("A test issue " + k) { Body = "A new unassigned issue " + k };
            var newLabel = new NewLabel("test label " + k, "FFFFF" + k);

            var label = await _issuesLabelsClient.Create(_context.Repository.Id, newLabel);
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

        var issueLabelsInfo = await _issuesLabelsClient.GetAllForMilestone(_context.Repository.Id, milestone.Number, options);

        Assert.Single(issueLabelsInfo);
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

        Assert.Single(issueLabelsInfo);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfIssueLabelsWithStartForAMilestoneWithRepositoryId()
    {
        var newMilestone = new NewMilestone("New Milestone");
        var milestone = await _issuesClient.Milestone.Create(_context.RepositoryOwner, _context.RepositoryName, newMilestone);

        for (int i = 0; i < 2; i++)
        {
            int k = i + 1;
            var newIssue = new NewIssue("A test issue " + k) { Body = "A new unassigned issue " + k };
            var newLabel = new NewLabel("test label " + k, "FFFFF" + k);

            var label = await _issuesLabelsClient.Create(_context.Repository.Id, newLabel);
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

        var issueLabelsInfo = await _issuesLabelsClient.GetAllForMilestone(_context.Repository.Id, milestone.Number, options);

        Assert.Single(issueLabelsInfo);
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

        Assert.Single(firstPage);
        Assert.Single(secondPage);
        Assert.NotEqual(firstPage.First().Color, secondPage.First().Color);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctIssueLabelsBasedOnStartPageForAMilestoneWithRepositoryId()
    {
        var newMilestone = new NewMilestone("New Milestone");
        var milestone = await _issuesClient.Milestone.Create(_context.RepositoryOwner, _context.RepositoryName, newMilestone);

        for (int i = 0; i < 2; i++)
        {
            int k = i + 1;
            var newIssue = new NewIssue("A test issue " + k) { Body = "A new unassigned issue " + k };
            var newLabel = new NewLabel("test label " + k, "FFFFF" + k);

            var label = await _issuesLabelsClient.Create(_context.Repository.Id, newLabel);
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

        var firstPage = await _issuesLabelsClient.GetAllForMilestone(_context.Repository.Id, milestone.Number, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageSize = 1,
            PageCount = 1,
            StartPage = 2
        };

        var secondPage = await _issuesLabelsClient.GetAllForMilestone(_context.Repository.Id, milestone.Number, skipStartOptions);

        Assert.Single(firstPage);
        Assert.Single(secondPage);
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
        Assert.Equal(label.Description, issueLabelLookupByName.Description);
    }

    [IntegrationTest]
    public async Task CanRetrieveIssueLabelByNameWithRepositoryId()
    {
        var newLabel = new NewLabel("test label 1b", "FFFFFF");
        var label = await _issuesLabelsClient.Create(_context.Repository.Id, newLabel);
        Assert.NotNull(label);

        var issueLabelLookupByName = await _issuesLabelsClient.Get(_context.Repository.Id, label.Name);

        Assert.Equal(label.Name, issueLabelLookupByName.Name);
        Assert.Equal(label.Color, issueLabelLookupByName.Color);
    }

    [IntegrationTest]
    public async Task CanCreateIssueLabel()
    {
        var newLabel = new NewLabel("test label", "FFFFFF");
        var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel);
        Assert.NotNull(label);

        var issueLabelLookupByName = await _issuesLabelsClient.Get(_context.RepositoryOwner, _context.RepositoryName, label.Name);

        Assert.Equal(label.Name, issueLabelLookupByName.Name);
        Assert.Equal(label.Color, issueLabelLookupByName.Color);
    }

    [IntegrationTest]
    public async Task CanCreateIssueLabelWithDescription()
    {
        var newLabel = new NewLabel("test label", "FFFFFF") { Description = "Test label description." };
        var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel);
        Assert.NotNull(label);

        var issueLabelLookupByName = await _issuesLabelsClient.Get(_context.RepositoryOwner, _context.RepositoryName, label.Name);

        Assert.Equal(label.Name, issueLabelLookupByName.Name);
        Assert.Equal(label.Color, issueLabelLookupByName.Color);
        Assert.Equal(label.Description, issueLabelLookupByName.Description);
    }

    [IntegrationTest]
    public async Task CanUpdateIssueLabel()
    {
        var newLabel = new NewLabel("test label", "FFFFFF") { Description = "Test label description." };
        var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel);
        Assert.NotNull(label);

        var labelUpdate = new LabelUpdate("test label", "000000") { Description = "Updated label description." };
        label = await _issuesLabelsClient.Update(_context.RepositoryOwner, _context.RepositoryName, labelUpdate.Name, labelUpdate);
        Assert.NotNull(label);

        var issueLabelLookupByName = await _issuesLabelsClient.Get(_context.RepositoryOwner, _context.RepositoryName, label.Name);

        Assert.Equal(labelUpdate.Name, issueLabelLookupByName.Name);
        Assert.Equal(labelUpdate.Color, issueLabelLookupByName.Color);
        Assert.Equal(labelUpdate.Description, issueLabelLookupByName.Description);
    }

    [IntegrationTest]
    public async Task CanDeleteIssueLabelByName()
    {
        var newLabel = new NewLabel("test label 1b", "FFFFFF");
        var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel);
        Assert.NotNull(label);

        var issueLabelLookupByName = await _issuesLabelsClient.Get(_context.RepositoryOwner, _context.RepositoryName, label.Name);

        Assert.Equal(label.Name, issueLabelLookupByName.Name);
        Assert.Equal(label.Color, issueLabelLookupByName.Color);

        await _issuesLabelsClient.Delete(_context.RepositoryOwner, _context.RepositoryName, label.Name);

        await Assert.ThrowsAsync<NotFoundException>(() => _issuesLabelsClient.Get(_context.RepositoryOwner, _context.RepositoryName, label.Name));
    }

    [IntegrationTest]
    public async Task CanDeleteIssueLabelByNameWithRepositoryId()
    {
        var newLabel = new NewLabel("test label 1b", "FFFFFF");
        var label = await _issuesLabelsClient.Create(_context.Repository.Id, newLabel);
        Assert.NotNull(label);

        var issueLabelLookupByName = await _issuesLabelsClient.Get(_context.Repository.Id, label.Name);

        Assert.Equal(label.Name, issueLabelLookupByName.Name);
        Assert.Equal(label.Color, issueLabelLookupByName.Color);

        await _issuesLabelsClient.Delete(_context.RepositoryOwner, _context.RepositoryName, label.Name);

        await Assert.ThrowsAsync<NotFoundException>(() => _issuesLabelsClient.Get(_context.RepositoryOwner, _context.RepositoryName, label.Name));
    }

    [IntegrationTest]
    public async Task CanAddToIssue()
    {
        var newIssue = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var newLabel = new NewLabel("test label 1b", "FFFFFF");

        var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel);
        Assert.NotNull(label);

        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        Assert.NotNull(issue);

        await _issuesLabelsClient.AddToIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new[] { label.Name });

        var labels = await _issuesLabelsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        Assert.NotEmpty(labels);
        Assert.Equal(label.Name, labels[0].Name);
        Assert.Equal(label.Color, labels[0].Color);
    }

    [IntegrationTest]
    public async Task CanAddToIssueWithRepositoryId()
    {
        var newIssue = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var newLabel = new NewLabel("test label 1b", "FFFFFF");

        var label = await _issuesLabelsClient.Create(_context.Repository.Id, newLabel);
        Assert.NotNull(label);

        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        Assert.NotNull(issue);

        await _issuesLabelsClient.AddToIssue(_context.Repository.Id, issue.Number, new[] { label.Name });

        var labels = await _issuesLabelsClient.GetAllForIssue(_context.Repository.Id, issue.Number);

        Assert.NotEmpty(labels);
        Assert.Equal(label.Name, labels[0].Name);
        Assert.Equal(label.Color, labels[0].Color);
    }

    [IntegrationTest]
    public async Task CanRemoveAllFromIssue()
    {
        var newIssue = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var newLabel = new NewLabel("test label 1b", "FFFFFF");

        var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel);
        Assert.NotNull(label);

        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        Assert.NotNull(issue);

        await _issuesLabelsClient.AddToIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new[] { label.Name });
        await _issuesLabelsClient.RemoveAllFromIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        var labels = await _issuesLabelsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        Assert.Empty(labels);
    }

    [IntegrationTest]
    public async Task CanRemoveAllFromIssueWithRepositoryId()
    {
        var newIssue = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var newLabel = new NewLabel("test label 1b", "FFFFFF");

        var label = await _issuesLabelsClient.Create(_context.Repository.Id, newLabel);
        Assert.NotNull(label);

        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        Assert.NotNull(issue);

        await _issuesLabelsClient.AddToIssue(_context.Repository.Id, issue.Number, new[] { label.Name });
        await _issuesLabelsClient.RemoveAllFromIssue(_context.Repository.Id, issue.Number);

        var labels = await _issuesLabelsClient.GetAllForIssue(_context.Repository.Id, issue.Number);

        Assert.Empty(labels);
    }

    [IntegrationTest]
    public async Task CanRemoveFromIssue()
    {
        var newIssue = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var newLabel = new NewLabel("test label 1b", "FFFFFF");

        var label = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel);
        Assert.NotNull(label);

        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        Assert.NotNull(issue);

        await _issuesLabelsClient.AddToIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new[] { label.Name });
        await _issuesLabelsClient.RemoveFromIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number, label.Name);

        var labels = await _issuesLabelsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        Assert.Empty(labels);
    }

    [IntegrationTest]
    public async Task CanRemoveFromIssueWithRepositoryId()
    {
        var newIssue = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var newLabel = new NewLabel("test label 1b", "FFFFFF");

        var label = await _issuesLabelsClient.Create(_context.Repository.Id, newLabel);
        Assert.NotNull(label);

        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);
        Assert.NotNull(issue);

        await _issuesLabelsClient.AddToIssue(_context.Repository.Id, issue.Number, new[] { label.Name });
        await _issuesLabelsClient.RemoveFromIssue(_context.Repository.Id, issue.Number, label.Name);

        var labels = await _issuesLabelsClient.GetAllForIssue(_context.Repository.Id, issue.Number);

        Assert.Empty(labels);
    }

    [IntegrationTest]
    public async Task CanReplaceAllForIssue()
    {
        var newIssue1 = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var newLabel1 = new NewLabel("test label 1b", "FFFFFF");
        var newLabel2 = new NewLabel("test label 1a", "FFFFFF");

        var label1 = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel1);
        Assert.NotNull(label1);
        var label2 = await _issuesLabelsClient.Create(_context.RepositoryOwner, _context.RepositoryName, newLabel2);
        Assert.NotNull(label2);

        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue1);
        Assert.NotNull(issue);

        await _issuesLabelsClient.AddToIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new[] { label1.Name });
        await _issuesLabelsClient.ReplaceAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new[] { label2.Name });

        var labels = await _issuesLabelsClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

        Assert.NotEmpty(labels);
        Assert.Equal(label2.Name, labels[0].Name);
        Assert.Equal(label2.Color, labels[0].Color);
    }

    [IntegrationTest]
    public async Task CanReplaceAllForIssueWithRepositoryId()
    {
        var newIssue1 = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var newLabel1 = new NewLabel("test label 1b", "FFFFFF");
        var newLabel2 = new NewLabel("test label 1a", "FFFFFF");

        var label1 = await _issuesLabelsClient.Create(_context.Repository.Id, newLabel1);
        Assert.NotNull(label1);
        var label2 = await _issuesLabelsClient.Create(_context.Repository.Id, newLabel2);
        Assert.NotNull(label2);

        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue1);
        Assert.NotNull(issue);

        await _issuesLabelsClient.AddToIssue(_context.Repository.Id, issue.Number, new[] { label1.Name });
        await _issuesLabelsClient.ReplaceAllForIssue(_context.Repository.Id, issue.Number, new[] { label2.Name });

        var labels = await _issuesLabelsClient.GetAllForIssue(_context.Repository.Id, issue.Number);

        Assert.NotEmpty(labels);
        Assert.Equal(label2.Name, labels[0].Name);
        Assert.Equal(label2.Color, labels[0].Color);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
