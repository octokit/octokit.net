# Search

You can use Octokit to search for different sorts of data available
on the GitHub or GitHub Enterprise server:

 - issues
 - repositories
 - code
 - users

## Search Issues

A common scenario is to search for issues to triage:

```csharp
// you can also specify a search term here
var request = new SearchIssuesRequest();

// you can add individual repos to focus your search
request.Repos.Add("aspnet/dnx");
request.Repos.Add("aspnet", "dnvm");

// or use a series of repositories
request.Repos = new RepositoryCollection {
    "aspnet/dnx",
    "aspnet/dnvm"
};

request.Repos = new RepositoryCollection {
    { "aspnet", "dnx" },
    { "aspnet", "dnvm" }
};
```

There's many other options available here to tweak
your search criteria:

```csharp
// if you're searching for a specific term, you can
// focus your search on specific criteria
request.In = new[] {
    IssueInQualifier.Title,
    IssueInQualifier.Body
};

// you can restrict your search to issues or pull requests
request.Type = IssueTypeQualifier.Issue;

// you can filter on when the issue was created or updated
var aWeekAgo = DateTime.Now.Subtract(TimeSpan.FromDays(7));
request.Created = new DateRange(aWeekAgo, SearchQualifierOperator.GreaterThan)

// you can search for issues created by, assigned to
// or mentioning a specific user
request.Author = "davidfowl";
request.Assignee = "damianedwards";
request.Mentions = "shiftkey";
request.Commenter = "haacked";

// rather than setting all these, you can use this to find
// all the above for a specific user with this one-liner
request.Involves = "davidfowl";

// by default this will search on open issues, set this if
// you want to get all issues
request.State = ItemState.All;
// or to just search closed issues
request.State = ItemState.Closed;
```

There's other options available to control how the results are returned:

```csharp
request.SortField = IssueSearchSort.Created;
request.Order = SortDirection.Descending;

// 100 results per page as default
request.PerPage = 30;

// set this when you want to fetch subsequent pages
request.Page = 2;
```

Once you've set the right parameters, execute the request:

```csharp
var repos = await client.Search.SearchIssues(request);

Console.WriteLine("Query has {0} matches.", repos.TotalCount);
Console.WriteLine("Response has {0} items.", repos.Items.Count);
```

## Search Pull Requests

Another scenario to consider is how to search broadly:

```csharp
var threeMonthsAgoIsh = DateTime.Now.Subtract(TimeSpan.FromDays(90));

// search for a specific term
var request = new SearchIssuesRequest("linux")
{
    // only search pull requests
    Type = IssueTypeQualifier.PR,

    // search across open and closed PRs
    State = ItemState.All,

    // search repositories which contain code
    // matching a given language
    Language = Language.CSharp,

    // focus on pull requests updated recently
    Updated = new DateRange(threeMonthsAgoIsh, SearchQualifierOperator.GreaterThan)
};
```



## Search Repositories

**TODO**

## Search Code

**TODO**

## Search Users

**TODO**
