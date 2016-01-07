# Working with Issues

There's three typical operations you have available when working
with issues - viewing, creating or editing issues.

### Get All

If you want to view all assigned, open issues against repositories you belong to
(either you own them, or you belong to a team or organization), use this
method:

```
var issues = await client.Issue.GetAllForCurrent();
```

If you want to skip organization repositories, you can instead use this
rather verbose method:

```
var issues = await client.Issue.GetAllForOwnedAndMemberRepositories();
```

If you know the specific repository, just invoke that:

```
var issuesForOctokit = await client.Issue.GetForRepository("octokit", "octokit.net");
```

### Filtering

Each of these methods has an overload which takes a parameter to filter results.

The simplest request is `IssueRequest` which has these options:

 - `Filter` - specify which issues to display - by default it will display issues assigned to you
 - `State` - by default it will display open issues, you can specify closed or all issues
 - `Labels` - specify a set of labels to include
 - `SortProperty` - sort by when the issue was created, when it was updated, or comment count
 - `SortDirection` - whether to sort in ascending or descending fashion
 - `Since` - ignore issues before a specific date

For example, this is how you could find all issues updated in the past two weeks:

```
var recently = new IssueRequest
{
    Filter = IssueFilter.All,
    State = ItemState.All,
    Since = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(14))
};
var issues = await client.Issue.GetAllForCurrent(recently);
```

`RepositoryIssueRequest` extends `IssueRequest` and adds these options:

 - `Milestone` - use `*` for any issue in a milestone, "none" for issues not assigned to a milestone
 - `Assignee` - specify the GitHub username, or "none" for unassigned issues
 - `Creator` - specify the GitHub username
 - `Mentioned` - specify the GitHub username

For example, to find all issues which need to be prioritized:

```
var shouldPrioritize = new RepositoryIssueRequest
{
    Assignee = "none",
    Milestone = "none",
    Filter = IssueFilter.All
};
var issues = await client.Issue.GetForRepository("octokit", "octokit.net", shouldPrioritize);
```

### Create

At a minimum, you need to specify the title:

```
var client = new GitHubClient(....); // More on GitHubClient can be found in "Getting Started"
var createIssue = new NewIssue("this thing doesn't work");
var issue = await client.Issue.Create("octokit", "octokit.net", createIssue);
```

`Create` returns a `Task<Issue>` which represents the created issue.

There's also a number of additional fields:

 - `Body` - details about the issue (Markdown)
 - `Assignee` - the GitHub user to associate with the issue
 - `Milestone` - the milestone id to assign the issue to
 - `Labels` - a collection of labels to assign to the issue

Note that `Milestones` and `Labels` need to exist in the repository before
creating the issue. Refer to the [Milestones](https://github.com/octokit/octokit.net/blob/master/docs/milestones.md)
and [Labels](https://github.com/octokit/octokit.net/blob/master/docs/labels.md)
sections for more details.

### Update

You can either hold the new issue in memory, or use the id to fetch the issue
later:

```
var issue = await client.Issue.Get("octokit", "octokit.net", 405);
```

With this issue, you can transform it into an `IssueUpdate` using the extension method:

```
var update = issue.ToUpdate();
```

This creates an `IssueUpdate` which lets you specify the neccessary changes.
Label changes probably requires some explanation:

 - by default, no labels are set in an `IssueUpdate` - this is to indicate
   to the server that no change is necessary when doing the update
 - to set a new label as part of the update, call `AddLabel()` specifying
   the name of the new label
 - to remove all labels as part of the update, call `ClearLabels()`

If you're trying to populate the `Labels` collection by hand, you might hit
some exceptional behaviour due to these rules.
