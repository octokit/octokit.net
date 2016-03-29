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

    [Fact]
    public async Task SearchForAllIssues()
    {
        var request = new SearchIssuesRequest("phone");
        request.Repos.Add("caliburn-micro", "caliburn.micro");

        var issues = await _gitHubClient.Search.SearchIssues(request);

        Assert.NotEmpty(issues.Items);
    }

    [Fact]
    public async Task SearchForAllIssuesWithoutUsingTerm()
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

    [Fact]
    public async Task SearchForMergedPullRequests()
    {
        var allRequest = new SearchIssuesRequest();
        allRequest.Repos.Add("octokit", "octokit.net");
        allRequest.Type = IssueTypeQualifier.PR;

        var mergedRequest = new SearchIssuesRequest();
        mergedRequest.Repos.Add("octokit", "octokit.net");
        mergedRequest.Is = new List<IssueIsQualifier> { IssueIsQualifier.PullRequest, IssueIsQualifier.Merged };

        var allPullRequests = await _gitHubClient.Search.SearchIssues(allRequest);
        var mergedPullRequests = await _gitHubClient.Search.SearchIssues(mergedRequest);

        Assert.NotEmpty(allPullRequests.Items);
        Assert.NotEmpty(mergedPullRequests.Items);
        Assert.NotEqual(allPullRequests.TotalCount, mergedPullRequests.TotalCount);
    }

    [Fact]
    public async Task SearchForLabelsAndExcludedLabels()
    {
        var labelRequest = new SearchIssuesRequest();
        labelRequest.Repos.Add("octokit", "octokit.net");
        labelRequest.Labels = new List<string> { "up-for-grabs" };

        var notLabelRequest = new SearchIssuesRequest();
        notLabelRequest.Repos.Add("octokit", "octokit.net");
        notLabelRequest.NotLabels = new List<string> { "up-for-grabs" };

        var upForGrabs = await _gitHubClient.Search.SearchIssues(labelRequest);
        var notUpForGrabs = await _gitHubClient.Search.SearchIssues(notLabelRequest);

        Assert.NotEmpty(upForGrabs.Items);
        Assert.NotEmpty(notUpForGrabs.Items);
        Assert.NotEqual(upForGrabs.TotalCount, notUpForGrabs.TotalCount);

        Assert.False(upForGrabs.Items.Any(x1 => notUpForGrabs.Items.Any(x2 => x2.Number == x1.Number)));
    }

    [Fact]
    public async Task SearchForMissingMetadata()
    {
        var allRequest = new SearchIssuesRequest();
        allRequest.Repos.Add("octokit", "octokit.net");

        var noAssigneeRequest = new SearchIssuesRequest();
        noAssigneeRequest.Repos.Add("octokit", "octokit.net");
        noAssigneeRequest.No = IssueNoMetadataQualifier.Assignee;

        var allIssues = await _gitHubClient.Search.SearchIssues(allRequest);
        var noAssigneeIssues = await _gitHubClient.Search.SearchIssues(noAssigneeRequest);

        Assert.NotEmpty(allIssues.Items);
        Assert.NotEmpty(noAssigneeIssues.Items);
        Assert.NotEqual(allIssues.TotalCount, noAssigneeIssues.TotalCount);
    }
}
