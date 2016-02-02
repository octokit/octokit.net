using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class SearchClientTests
{
    readonly IGitHubClient _gitHubClient;

    public SearchClientTests()
    {
        _gitHubClient = Helper.GetAuthenticatedClient();
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
        var request = new SearchCodeRequest("addClass", "jquery", "jquery");

        var repos = await _gitHubClient.Search.SearchCode(request);

        Assert.NotEmpty(repos.Items);
    }

    [Fact]
    public async Task SearchForFileNameInCode()
    {
        var request = new SearchCodeRequest("GitHub")
        {
            FileName = "readme.md",
            Repos = new RepositoryCollection { "octokit/octokit.net" }
        };

        var repos = await _gitHubClient.Search.SearchCode(request);

        Assert.NotEmpty(repos.Items);
    }

    [Fact]
    public async Task SearchForWordInCode()
    {
        var request = new SearchIssuesRequest("windows");
        request.Repos = new RepositoryCollection {
            { "aspnet", "dnx" },
            { "aspnet", "dnvm" }
        };

        request.SortField = IssueSearchSort.Created;
        request.Order = SortDirection.Descending;

        var repos = await _gitHubClient.Search.SearchIssues(request);

        Assert.NotEmpty(repos.Items);
    }

    [Fact]
    public async Task SearchForOpenIssues()
    {
        var request = new SearchIssuesRequest("phone");
        request.Repos.Add("caliburn-micro", "caliburn.micro");
        request.State = ItemState.Open;

        var issues = await _gitHubClient.Search.SearchIssues(request);

        Assert.NotEmpty(issues.Items);
    }

    [Fact(Skip = "see https://github.com/octokit/octokit.net/issues/1082 for investigating this failing test")]
    public async Task SearchForAllIssues()
    {
        var request = new SearchIssuesRequest("phone");
        request.Repos.Add("caliburn-micro", "caliburn.micro");
        request.State = ItemState.All;

        var issues = await _gitHubClient.Search.SearchIssues(request);

        Assert.NotEmpty(issues.Items);
    }

    [Fact]
    public async Task SearchForAllIssuesWithouTaskUsingTerm()
    {
        var request = new SearchIssuesRequest();
        request.Repos.Add("caliburn-micro/caliburn.micro");

        var issues = await _gitHubClient.Search.SearchIssues(request);

        var closedIssues = issues.Items.Where(x => x.State == ItemState.Closed);
        var openedIssues = issues.Items.Where(x => x.State == ItemState.Open);

        Assert.NotEmpty(closedIssues);
        Assert.NotEmpty(openedIssues);
    }

    [Fact]
    public async Task SearchForAllIssuesUsingTerm()
    {
        var request = new SearchIssuesRequest("phone");
        request.Repos.Add("caliburn-micro", "caliburn.micro");

        var issues = await _gitHubClient.Search.SearchIssues(request);

        var closedIssues = issues.Items.Where(x => x.State == ItemState.Closed);
        var openedIssues = issues.Items.Where(x => x.State == ItemState.Open);

        Assert.NotEmpty(closedIssues);
        Assert.NotEmpty(openedIssues);
    }
}
