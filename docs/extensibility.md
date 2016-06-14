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

If `PageSize` is not set the default page size is 30.

Examples:

```csharp
// fetch all items, 100 at a time
var batchPagination = new ApiOptions
{
    PageSize = 100
};

// return first 100 items
var firstOneHundred = new ApiOptions
{
    PageSize = 100,
    PageCount = 1
};

// return 100 items after first page
var skipFirstHundred = new ApiOptions
{
    PageSize = 100,
    StartPage = 2, // 1-indexed value
    PageCount = 1
};
```
