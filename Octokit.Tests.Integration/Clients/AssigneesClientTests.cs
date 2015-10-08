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
    public async Task CanListAssignees()
    {
        // Repository owner is always an assignee
        var assignees = await _github.Issue.Assignee.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName);
        Assert.True(assignees.Any(u => u.Login == Helper.UserName));
    }
}