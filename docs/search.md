# Search

You can use Octokit to search for different sorts of data available
on the GitHub or GitHub Enterprise server:

 - issues
 - repositories
 - code
 - users

I won't go into the full details here as the [developer documentation](https://developer.github.com/)
covers all the options far better than I could.

## Search Issues

You can search for issues containing a given phrase across many repositories:

```csharp
// a search 
var request = new SearchIssuesRequest("linux");

// it is highly recommended to search in specific repositories
request.Repos = new Collection<string> {
    "aspnet/dnx",
    "aspnet/dnvm"
};

// other parameters available for tweaking the output
request.SortField = IssueSearchSort.Created;
request.Order = SortDirection.Descending;
```

By default, search will return the first page of results and use
a page size of 100 results.

```csharp
request.Page = 2;
request.PerPage = 30;
```


Once you've set the right parameters, execute the request:

```csharp
// actually perform the search
var repos = await client.Search.SearchIssues(request);

Console.WriteLine("Query has {0} matches.", repos.TotalCount);
Console.WriteLine("Response has {0} items.", repos.Items.Count);
Assert.NotEmpty();
```

## Search Repositories

## Search Code

## Search Users
