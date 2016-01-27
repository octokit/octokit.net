using System;
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
    public async Task CanListLabelsForAnIssue()
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
