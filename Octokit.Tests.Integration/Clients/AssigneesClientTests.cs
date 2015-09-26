using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class AssigneesClientTests
{
    readonly IGitHubClient _gitHubClient;
    readonly RepositoryContext _context;
    readonly string _owner;

    public AssigneesClientTests()
    {
        _gitHubClient = Helper.GetAuthenticatedClient();
        var repoName = Helper.MakeNameWithTimestamp("public-repo");

        _context = _gitHubClient.CreateRepositoryContext(new NewRepository(repoName)).Result;
        _owner = _context.Repository.Owner.Login;
    }

    [IntegrationTest]
    public async Task CanCheckAssignees()
    {
        var isAssigned = await
            _gitHubClient.Issue.Assignee.CheckAssignee(_owner, _context.Repository.Name, "FakeHaacked");
        Assert.False(isAssigned);

        // Repository owner is always an assignee
        isAssigned = await
            _gitHubClient.Issue.Assignee.CheckAssignee(_owner, _context.Repository.Name, _owner);
        Assert.True(isAssigned);
    }

    [IntegrationTest]
    public async Task CanListAssignees()
    {
        // Repository owner is always an assignee
        var assignees = await _gitHubClient.Issue.Assignee.GetAllForRepository(_owner, _context.Repository.Name);
        Assert.True(assignees.Any(u => u.Login == Helper.UserName));
    }
}