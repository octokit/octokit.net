using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class AssigneesClientTests
{
    readonly IGitHubClient _github;
    readonly RepositoryContext _context;

    public AssigneesClientTests()
    {
        _github = Helper.GetAuthenticatedClient();
        var repoName = Helper.MakeNameWithTimestamp("public-repo");

        _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
    }

    [IntegrationTest]
    public async Task CanCheckAssignees()
    {
        var isAssigned = await
            _github.Issue.Assignee.CheckAssignee(_context.RepositoryOwner, _context.RepositoryName, "FakeHaacked");
        Assert.False(isAssigned);

        // Repository owner is always an assignee
        isAssigned = await
            _github.Issue.Assignee.CheckAssignee(_context.RepositoryOwner, _context.RepositoryName, _context.RepositoryOwner);
        Assert.True(isAssigned);
    }

    [IntegrationTest]
    public async Task CanCheckAssigneesWithRepositoryId()
    {
        var isAssigned = await
            _github.Issue.Assignee.CheckAssignee(_context.Repository.Id, "FakeHaacked");
        Assert.False(isAssigned);

        // Repository owner is always an assignee
        isAssigned = await
            _github.Issue.Assignee.CheckAssignee(_context.Repository.Id, _context.RepositoryOwner);
        Assert.True(isAssigned);
    }

    [IntegrationTest]
    public async Task CanListAssignees()
    {
        // Repository owner is always an assignee
        var assignees = await _github.Issue.Assignee.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName);
        Assert.Contains(assignees, u => u.Login == Helper.UserName);
    }

    [IntegrationTest]
    public async Task CanAddAndRemoveAssignees()
    {
        var newAssignees = new AssigneesUpdate(new List<string>() { _context.RepositoryOwner });
        var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
        var issuesClient = _github.Issue;

        var issue = await issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

        Assert.NotNull(issue);

        var addAssignees = await _github.Issue.Assignee.AddAssignees(_context.RepositoryOwner, _context.RepositoryName, issue.Number, newAssignees);

        Assert.IsType<Issue>(addAssignees);

        //Check if assignee was added to issue
        Assert.Contains(addAssignees.Assignees, x => x.Login == _context.RepositoryOwner);

        //Test to remove assignees
        var removeAssignees = await _github.Issue.Assignee.RemoveAssignees(_context.RepositoryOwner, _context.RepositoryName, issue.Number, newAssignees);

        //Check if assignee was removed
        Assert.DoesNotContain(removeAssignees.Assignees, x => x.Login == _context.RepositoryOwner);
    }

    [IntegrationTest]
    public async Task CanListAssigneesWithRepositoryId()
    {
        // Repository owner is always an assignee
        var assignees = await _github.Issue.Assignee.GetAllForRepository(_context.Repository.Id);
        Assert.Contains(assignees, u => u.Login == Helper.UserName);
    }
}