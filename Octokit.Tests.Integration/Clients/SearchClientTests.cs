using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class SearchClientTests
{
    readonly IGitHubClient _gitHubClient;

    public SearchClientTests()
    {
        _gitHubClient = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };
    }

    [Fact]
    public async Task SearchForCSharpRepositories()
    {
        var request = new SearchRepositoriesRequest("csharp");
        var repos = await _gitHubClient.Search.SearchRepo(request);

        Assert.NotEmpty(repos.Items);
    }

    [Fact]
    public async Task SearchForGitHub()
    {
        var request = new SearchUsersRequest("github");
        var repos = await _gitHubClient.Search.SearchUsers(request);

        Assert.NotEmpty(repos.Items);
    }

    [Fact]
    public async Task SearchForFunctionInCode()
    {
        var request = new SearchCodeRequest("addClass");
        request.Repo = "jquery/jquery";
        var repos = await _gitHubClient.Search.SearchCode(request);

        Assert.NotEmpty(repos.Items);
    }

    [Fact]
    public async Task SearchForWordInCode()
    {
        var request = new SearchIssuesRequest("windows");
        request.SortField = IssueSearchSort.Created;
        request.Order = SortDirection.Descending;

        var repos = await _gitHubClient.Search.SearchIssues(request);

        Assert.NotEmpty(repos.Items);
    }

    [Fact]
    public async Task SearchForOpenIssues()
    {
        var request = new SearchIssuesRequest("phone");
        request.Repo = "caliburn-micro/caliburn.micro";
        request.State = ItemState.Open;

        var issues = await _gitHubClient.Search.SearchIssues(request);

        Assert.NotEmpty(issues.Items);
    }
}
