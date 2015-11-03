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

To search for repositories using Octokit you need to create a `SearchRepositoriesRequest` and populate it with the search criteria.

```csharp
// Initialize a new instance of the SearchRepositoriesRequest class
var request = new SearchRepositoriesRequest();

// you can also specify a search term here
var request = new SearchRepositoriesRequest("bootstrap");

var result = await githubClient.Search.SearchRepo(request);
```

Now we can further filter our search.

```csharp
var request = new SearchRepositoriesRequest("mvc client side framework")
{
    // lets find a library with over 1k stars
    Stars = Range.GreaterThan(1000),

    // we can specify how big we want the repo to be in kilobytes
    Size = Range.GreaterThan(1),

    // maybe we want the library to have over 50 forks?
    Forks = Range.GreaterThan(50),

    // we may want to include or exclude the forks too
    Fork = ForkQualifier.IncludeForks,

    // how about we restrict the language the library is written in?
    Language = Language.JavaScript,

    // maybe we want to include searching in the read me?
    In = new[] { InQualifier.Readme },

    // or go all out and search the readme, name or description?
    In = new[] { InQualifier.Readme, InQualifier.Description, InQualifier.Name },

    // how about searching for libraries created after a given date?
    Created = DateRange.GreaterThan(new DateTime(2015, 1, 1)),

    // or maybe check for repos that have been updated between a given date range?
    Updated = DateRange.Between(new DateTime(2012, 4, 30), new DateTime(2012, 7, 4)),

    // we can also restrict the owner of the repo if we so wish
    User = "dhh"
};
```

We can also sort our results, the default sort direction is descending

```csharp
var request = new SearchRepositoriesRequest("mvc client side framework")
{
    // sort by the number of stars
    SortField = RepoSearchSort.Stars,

    // or by forks?
    SortField = RepoSearchSort.Forks,

    // how about changing that sort direction?
    Order = SortDirection.Ascending
}
```

## Search Code

To search for code using Octokit you need to create a `SearchCodeRequest` and populate it with the search criteria.

```csharp
// Initialize a new instance of the SearchCodeRequest class with a search term
var request = new SearchCodeRequest("auth");

// Or we can restrict the search to a specific repo
var request = new SearchCodeRequest("auth", "dhh", "rails");

var result = await githubClient.Search.SearchCode(request);
```

Now we can further filter our search.

```csharp
var request = new SearchCodeRequest("auth")
{
    // we can restrict search to the file, path or search both
    In = new[] { CodeInQualifier.File, CodeInQualifier.Path },
    
    // how about we find a file based on a certain language
    Language = Language.JavaScript,
    
    // do we want to search forks too?
    Forks = true,

    // find files that are above 1000 bytes
    Size = Range.GreaterThan(1000),

    // we may want to restrict the search to the path of a file
    Path = "app/assets",
    
    // we may want to restrict the file based on file extension
    Extension = "json",
    
    // restrict search to a specific file name
    FileName = "app.json",
    
    // search within a users or orgs repo
    User = "dhh"
};
```

We can also sort our results by indexed or leave as null for best match.

```csharp
var request = new SearchCodeRequest("dhh")
{
    // sort by last indexed
    SortField = CodeSearchSort.Indexed
}
```

## Search Users

To search for users using Octokit you need to create a `SearchUsersRequest` and populate it with the search criteria.

```csharp
// Initialize a new instance of the SearchUsersRequest class with a search term
var request = new SearchUsersRequest("dhh");

var result = await githubClient.Search.SearchUsers(request);
```

Now we can further filter our search.

```csharp
var request = new SearchUsersRequest("dhh")
{
    // lets find user with over 1k followers
    Followers = Range.GreaterThan(1000),

    // find a user created after the date
    Created = DateRange.GreaterThan(new DateTime(2015, 1, 1)),

    // we can search the location of a user, found a martian anyone?
    Location = "Mars",

    // find a user that has over 100 repos
    Repositories = Range.GreaterThan(100),

    // how about we find users that have a repo that match a certain language
    Language = Language.JavaScript,

    // we may want to restrict to orgs or users only
    AccountType = AccountSearchType.Org,

    // maybe we want to peek the username only?
    In = new[] { UserInQualifier.Username },

    // or go all out and search username, email and fullname?
    In = new[] { UserInQualifier.Username, UserInQualifier.Email, UserInQualifier.Fullname },
};
```

We can also sort our results, by Followers, Repositories, Joined or leave as null for best match.

```csharp
var request = new SearchUsersRequest("dhh")
{
    // sort by the number of followers
    SortField = UsersSearchSort.Followers
}
```
