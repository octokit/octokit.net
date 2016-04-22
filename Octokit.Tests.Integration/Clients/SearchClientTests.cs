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

    [IntegrationTest]
    public async Task SearchForCSharpRepositories()
    {
        var request = new SearchRepositoriesRequest("csharp");
        var repos = await _gitHubClient.Search.SearchRepo(request);

        Assert.NotEmpty(repos.Items);
    }

    [IntegrationTest]
    public async Task SearchForGitHub()
    {
        var request = new SearchUsersRequest("github");
        var repos = await _gitHubClient.Search.SearchUsers(request);

        Assert.NotEmpty(repos.Items);
    }

    [IntegrationTest]
    public async Task SearchForFunctionInCode()
    {
        var request = new SearchCodeRequest("addClass", "jquery", "jquery");

        var repos = await _gitHubClient.Search.SearchCode(request);

        Assert.NotEmpty(repos.Items);
    }

    [IntegrationTest]
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

    [IntegrationTest]
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

    [IntegrationTest]
    public async Task SearchForOpenIssues()
    {
        var request = new SearchIssuesRequest("phone");
        request.Repos.Add("caliburn-micro", "caliburn.micro");
        request.State = ItemState.Open;

        var issues = await _gitHubClient.Search.SearchIssues(request);

        Assert.NotEmpty(issues.Items);
    }

    [IntegrationTest]
    public async Task SearchForAllIssues()
    {
        var request = new SearchIssuesRequest("phone");
        request.Repos.Add("caliburn-micro", "caliburn.micro");

        var issues = await _gitHubClient.Search.SearchIssues(request);

        Assert.NotEmpty(issues.Items);
    }

    [IntegrationTest]
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

    [IntegrationTest]
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

    [IntegrationTest]
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

    [IntegrationTest]
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

    [IntegrationTest]
    public async Task SearchForExcludedAuthor()
    {
        var author = "shiftkey";

        // Search for issues by include filter
        var request = new SearchIssuesRequest();
        request.Repos.Add("octokit", "octokit.net");
        request.Author = author;

        var issues = await _gitHubClient.Search.SearchIssues(request);

        // Ensure we found issues
        Assert.NotEmpty(issues.Items);

        // Search for issues by exclude filter
        var excludeRequest = new SearchIssuesRequest();
        excludeRequest.Repos.Add("octokit", "octokit.net");
        excludeRequest.Exclusions = new SearchIssuesRequestExclusions
        {
            Author = author
        };

        var otherIssues = await _gitHubClient.Search.SearchIssues(excludeRequest);

        // Ensure we found issues
        Assert.NotEmpty(otherIssues.Items);

        // Ensure no items from the first search are in the results for the second
        Assert.DoesNotContain(issues.Items, x1 => otherIssues.Items.Any(x2 => x2.Id == x1.Id));
    }

    [IntegrationTest]
    public async Task SearchForExcludedAssignee()
    {
        var assignee = "shiftkey";

        // Search for issues by include filter
        var request = new SearchIssuesRequest();
        request.Repos.Add("octokit", "octokit.net");
        request.Assignee = assignee;

        var issues = await _gitHubClient.Search.SearchIssues(request);

        // Ensure we found issues
        Assert.NotEmpty(issues.Items);

        // Search for issues by exclude filter
        var excludeRequest = new SearchIssuesRequest();
        excludeRequest.Repos.Add("octokit", "octokit.net");
        excludeRequest.Exclusions = new SearchIssuesRequestExclusions
        {
            Assignee = assignee
        };

        var otherIssues = await _gitHubClient.Search.SearchIssues(excludeRequest);

        // Ensure we found issues
        Assert.NotEmpty(otherIssues.Items);

        // Ensure no items from the first search are in the results for the second
        Assert.DoesNotContain(issues.Items, x1 => otherIssues.Items.Any(x2 => x2.Id == x1.Id));
    }

    [IntegrationTest]
    public async Task SearchForExcludedMentions()
    {
        var mentioned = "shiftkey";

        // Search for issues by include filter
        var request = new SearchIssuesRequest();
        request.Repos.Add("octokit", "octokit.net");
        request.Mentions = mentioned;

        var issues = await _gitHubClient.Search.SearchIssues(request);

        // Ensure we found issues
        Assert.NotEmpty(issues.Items);

        // Search for issues by exclude filter
        var excludeRequest = new SearchIssuesRequest();
        excludeRequest.Repos.Add("octokit", "octokit.net");
        excludeRequest.Exclusions = new SearchIssuesRequestExclusions
        {
            Mentions = mentioned
        };

        var otherIssues = await _gitHubClient.Search.SearchIssues(excludeRequest);

        // Ensure we found issues
        Assert.NotEmpty(otherIssues.Items);

        // Ensure no items from the first search are in the results for the second
        Assert.DoesNotContain(issues.Items, x1 => otherIssues.Items.Any(x2 => x2.Id == x1.Id));
    }

    [IntegrationTest]
    public async Task SearchForExcludedCommenter()
    {
        var commenter = "shiftkey";

        // Search for issues by include filter
        var request = new SearchIssuesRequest();
        request.Repos.Add("octokit", "octokit.net");
        request.Commenter = commenter;

        var issues = await _gitHubClient.Search.SearchIssues(request);

        // Ensure we found issues
        Assert.NotEmpty(issues.Items);

        // Search for issues by exclude filter
        var excludeRequest = new SearchIssuesRequest();
        excludeRequest.Repos.Add("octokit", "octokit.net");
        excludeRequest.Exclusions = new SearchIssuesRequestExclusions
        {
            Commenter = commenter
        };

        var otherIssues = await _gitHubClient.Search.SearchIssues(excludeRequest);

        // Ensure we found issues
        Assert.NotEmpty(otherIssues.Items);

        // Ensure no items from the first search are in the results for the second
        Assert.DoesNotContain(issues.Items, x1 => otherIssues.Items.Any(x2 => x2.Id == x1.Id));
    }

    [IntegrationTest]
    public async Task SearchForExcludedInvolves()
    {
        var involves = "shiftkey";

        // Search for issues by include filter
        var request = new SearchIssuesRequest();
        request.Repos.Add("octokit", "octokit.net");
        request.Involves = involves;

        var issues = await _gitHubClient.Search.SearchIssues(request);

        // Ensure we found issues
        Assert.NotEmpty(issues.Items);

        // Search for issues by exclude filter
        var excludeRequest = new SearchIssuesRequest();
        excludeRequest.Repos.Add("octokit", "octokit.net");
        excludeRequest.Exclusions = new SearchIssuesRequestExclusions
        {
            Involves = involves
        };

        var otherIssues = await _gitHubClient.Search.SearchIssues(excludeRequest);

        // Ensure we found issues
        Assert.NotEmpty(otherIssues.Items);

        // Ensure no items from the first search are in the results for the second
        Assert.DoesNotContain(issues.Items, x1 => otherIssues.Items.Any(x2 => x2.Id == x1.Id));
    }

    [IntegrationTest]
    public async Task SearchForExcludedState()
    {
        var state = ItemState.Open;

        // Search for issues by include filter
        var request = new SearchIssuesRequest();
        request.Repos.Add("octokit", "octokit.net");
        request.State = state;

        var issues = await _gitHubClient.Search.SearchIssues(request);

        // Ensure we found issues
        Assert.NotEmpty(issues.Items);

        // Search for issues by exclude filter
        var excludeRequest = new SearchIssuesRequest();
        excludeRequest.Repos.Add("octokit", "octokit.net");
        excludeRequest.Exclusions = new SearchIssuesRequestExclusions
        {
            State = state
        };

        var otherIssues = await _gitHubClient.Search.SearchIssues(excludeRequest);

        // Ensure we found issues
        Assert.NotEmpty(otherIssues.Items);

        // Ensure no items from the first search are in the results for the second
        Assert.DoesNotContain(issues.Items, x1 => otherIssues.Items.Any(x2 => x2.Id == x1.Id));
    }

    [IntegrationTest]
    public async Task SearchForExcludedLabels()
    {
        var label1 = "up-for-grabs";
        var label2 = "feature";

        // Search for issues by include filter
        var request = new SearchIssuesRequest();
        request.Repos.Add("octokit", "octokit.net");
        request.Labels = new[] { label1, label2 };

        var issues = await _gitHubClient.Search.SearchIssues(request);

        // Ensure we found issues
        Assert.NotEmpty(issues.Items);

        // Search for issues by exclude filter
        var excludeRequest = new SearchIssuesRequest();
        excludeRequest.Repos.Add("octokit", "octokit.net");
        excludeRequest.Exclusions = new SearchIssuesRequestExclusions
        {
            Labels = new[] { label1, label2 }
        };

        var otherIssues = await _gitHubClient.Search.SearchIssues(excludeRequest);

        // Ensure we found issues
        Assert.NotEmpty(otherIssues.Items);

        // Ensure no items from the first search are in the results for the second
        Assert.DoesNotContain(issues.Items, x1 => otherIssues.Items.Any(x2 => x2.Id == x1.Id));
    }

    [IntegrationTest]
    public async Task SearchForExcludedLanguage()
    {
        var language = Language.CSharp;

        // Search for issues by include filter
        var request = new SearchIssuesRequest("octokit");
        request.Language = language;

        var issues = await _gitHubClient.Search.SearchIssues(request);

        // Ensure we found issues
        Assert.NotEmpty(issues.Items);

        // Search for issues by exclude filter
        var excludeRequest = new SearchIssuesRequest("octokit");
        excludeRequest.Exclusions = new SearchIssuesRequestExclusions
        {
            Language = language
        };

        var otherIssues = await _gitHubClient.Search.SearchIssues(excludeRequest);

        // Ensure we found issues
        Assert.NotEmpty(otherIssues.Items);

        // Ensure no items from the first search are in the results for the second
        Assert.DoesNotContain(issues.Items, x1 => otherIssues.Items.Any(x2 => x2.Id == x1.Id));
    }

    [IntegrationTest]
    public async Task SearchForExcludedStatus()
    {
        var status = CommitState.Success;

        // Search for issues by include filter
        var request = new SearchIssuesRequest();
        request.Repos.Add("octokit", "octokit.net");
        request.Status = status;

        var issues = await _gitHubClient.Search.SearchIssues(request);

        // Ensure we found issues
        Assert.NotEmpty(issues.Items);

        // Search for issues by exclude filter
        var excludeRequest = new SearchIssuesRequest();
        excludeRequest.Repos.Add("octokit", "octokit.net");
        excludeRequest.Exclusions = new SearchIssuesRequestExclusions
        {
            Status = status
        };

        var otherIssues = await _gitHubClient.Search.SearchIssues(excludeRequest);

        // Ensure we found issues
        Assert.NotEmpty(otherIssues.Items);

        // Ensure no items from the first search are in the results for the second
        Assert.DoesNotContain(issues.Items, x1 => otherIssues.Items.Any(x2 => x2.Id == x1.Id));
    }

    [IntegrationTest]
    public async Task SearchForExcludedHead()
    {
        var branch = "search-issues";

        // Search for issues by source branch
        var request = new SearchIssuesRequest();
        request.Repos.Add("octokit", "octokit.net");
        request.Head = branch;

        var issues = await _gitHubClient.Search.SearchIssues(request);

        // Ensure we found issues
        Assert.NotEmpty(issues.Items);

        // Search for issues excluding source branch
        var excludeRequest = new SearchIssuesRequest();
        excludeRequest.Repos.Add("octokit", "octokit.net");
        excludeRequest.Exclusions = new SearchIssuesRequestExclusions
        {
            Head = branch
        };

        var otherIssues = await _gitHubClient.Search.SearchIssues(excludeRequest);

        // Ensure we found issues
        Assert.NotEmpty(otherIssues.Items);

        // Ensure no items from the first search are in the results for the second
        Assert.DoesNotContain(issues.Items, x1 => otherIssues.Items.Any(x2 => x2.Id == x1.Id));
    }

    [IntegrationTest]
    public async Task SearchForExcludedBase()
    {
        var branch = "master";

        // Search for issues by target branch
        var request = new SearchIssuesRequest();
        request.Repos.Add("octokit", "octokit.net");
        request.Base = branch;

        var issues = await _gitHubClient.Search.SearchIssues(request);

        // Ensure we found issues
        Assert.NotEmpty(issues.Items);

        // Search for issues excluding target branch
        var excludeRequest = new SearchIssuesRequest();
        excludeRequest.Repos.Add("octokit", "octokit.net");
        excludeRequest.Exclusions = new SearchIssuesRequestExclusions
        {
            Base = branch
        };

        var otherIssues = await _gitHubClient.Search.SearchIssues(excludeRequest);

        // Ensure we found issues
        Assert.NotEmpty(otherIssues.Items);

        // Ensure no items from the first search are in the results for the second
        Assert.DoesNotContain(issues.Items, x1 => otherIssues.Items.Any(x2 => x2.Id == x1.Id));
    }
}
