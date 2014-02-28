using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class AssigneesClientTests
{
    readonly IGitHubClient _gitHubClient;
    readonly Repository _repository;
    readonly string _owner;

    public AssigneesClientTests()
    {
        _gitHubClient = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };
        var repoName = Helper.MakeNameWithTimestamp("public-repo");

        _repository = _gitHubClient.Repository.Create(new NewRepository { Name = repoName }).Result;
        _owner = _repository.Owner.Login;
    }

    [IntegrationTest]
    public async Task CanCheckAssignees()
    {
        var isAssigned = await
            _gitHubClient.Issue.Assignee.CheckAssignee(_owner, _repository.Name, "FakeHaacked");
        Assert.False(isAssigned);
        
        // Repository owner is always an assignee
        isAssigned = await
            _gitHubClient.Issue.Assignee.CheckAssignee(_owner, _repository.Name, _owner);
        Assert.True(isAssigned);
    }

    [IntegrationTest]
    public async Task CanListAssignees()
    {
        // Repository owner is always an assignee
        var assignees = await _gitHubClient.Issue.Assignee.GetForRepository(_owner, _repository.Name);
        Assert.True(assignees.Any(u => u.Login == Helper.UserName));
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}
