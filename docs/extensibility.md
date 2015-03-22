# Extensibility

Octokit.net has been designed to be easy to get started, but there are options
available to tweak the default behaviour once you know the basics.

## Pagination

The GitHub API supports paging results whenever collections are returned.

By default, Octokit.net will fetch the entire set of data. Any method prefixed with
`GetAll*` now has an overload which accepts an `ApiOptions` parameter.

```csharp
var options = new ApiOptions();
var repositories = await client.Repository.GetAllForCurrent(options);
```

`ApiOptions` has a number of properties:

 - `PageCount` - return a set number of pages
 - `PageSize` - change the number of results to return per page
 - `StartPage` - start results from a given page

These parameters can be used in any sort of group. If you don't specify a
`PageSize` the default page size is 30.

## Proposal: Cancellation

A future version of `ApiOptions` could specify a `CancellationToken` which
would be used for aborting long-running queries.

```csharp
var ct = new CancellationToken();
var options = new ApiOptions();
options.CancellationToken = ct;
var task = client.Repository.GetAllForCurrent(options);

...

ct.Cancel();
// TODO: finish this example to show what you *should* do here
```

I'm still working out the scenarios that aren't covered by default pagination -
if you have suggestions please leave a comment in this issue:

https://github.com/octokit/octokit.net/issues/744

## Proposal: Handling Different Media Types

The GitHub API has various endpoints which support returning different content
in the response - for example, issue comments could be raw text, or rendered HTML.

It would be nice to have the ability for the user to control the response content,
without changing much of Octokit's internals.

One suggestion is to use `ApiOptions` to override the `Accepts` header on-the-fly.

For example:

```csharp
var options = new ApiOptions
{
    Accepts = AcceptsHeader.HtmlPlusJson,
    PageSize = 100
};
var issues = await client.Issue.GetForRepository("octokit", "octokit.net", options);
```

This proposal is not as mature, as there are endpoints which do not use `ApiOptions`
which should also support this behaviour.

```csharp
var issue = await client.Issue.Get("octokit", "octokit.net", 593);
```

If you have suggestions please leave a comment in this issue:

https://github.com/octokit/octokit.net/issues/593

