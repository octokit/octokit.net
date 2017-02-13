### How is the codebase organised?

The two main projects are the `Octokit` and `Octokit.Reactive` projects.

The `Octokit.Reactive` library is a thin wrapper over the `Octokit`
library - for those who want to use Reactive Extensions (Rx) instead of tasks.

The namespaces are organised so that the relevant components are easy to discover:

 - **Authentication** - everything related to authenticating requests
 - **Clients** - the logic for interacting with various parts of the GitHub API
 - **Exceptions** - types which represent exceptional behaviour from the API
 - **Helpers** - assorted extensions and helpers to keep the code neat and tidy
 - **Http** - the internal networking components which Octokit requires
 - **Models** - types which represent request/response objects

Unless you're modifying some core behaviour, the **Clients** and **Models** namespaces
are likely to be the most interesting areas.

The clients within a project are organized similarly to the endpoints in the
[GitHub API documentation](http://developer.github.com/v3/)

Some clients are "sub-clients". For example, when you navigate to the
[Issues API](http://developer.github.com/v3/issues/) you'll notice there's an
endpoint for issues. But in the right navbar, there are other APIs such as
[Assignees](http://developer.github.com/v3/issues/assignees/) and
[Milestones](http://developer.github.com/v3/issues/milestones/).

We've tried to mirror this structure. So the `IObservableMilestoneClient` isn't
a direct property of `IObservableGitHubClient`. Instead, it's a property of the
`IObservableIssuesClient`. And thus you can get to it by going to
`client.Issues.Milestones`.
