using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;
using System.Collections.Generic;

public class AssigneesClientTests
{
    readonly IGitHubClient _github;
    readonly RepositoryContext _context;
    private readonly IIssuesClient _issuesClient;

    public AssigneesClientTests()
    {
        _github = Helper.GetAuthenticatedClient();
        var repoName = Helper.MakeNameWithTimestamp("public-repo");
        _issuesClient = _github.Issue;

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
    public async Task CanListAssignees()
    {
        // Repository owner is always an assignee
        var assignees = await _github.Issue.Assignee.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName);
        Assert.True(assignees.Any(u => u.Login == Helper.UserName));
    }

    [IntegrationTest]
    public async Task CanAddAssignees()
    {        
        var newAssignees = new AssigneesUpdate(new List<string>() { _context.RepositoryOwner });
        var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };

        var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

        Assert.NotNull(issue);

        var addAssignees = await _github.Issue.Assignee.AddAssignees(_context.RepositoryOwner, _context.RepositoryName, issue.Id, newAssignees);

        Assert.IsType<Issue>(addAssignees);

        //Check if assignee was added to issue
        Assert.True(addAssignees.Assignees.Where(x => x.Name == _context.RepositoryOwner).Any() == true);
    }
}