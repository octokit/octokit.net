# Octokit - GitHub API Client Library for .NET

Octokit is a client library targeting .NET 4.5 and above that provides an easy
way to interact with the [GitHub API](http://developer.github.com/v3/).

## Usage examples

Get public info on a specific user.

```c#
var github = new GitHubClient(new ProductHeaderValue("MyAmazingApp"));
var user = await github.User.Get("half-ogre");
Console.WriteLine(user.Followers + " folks love the half ogre!");
```

## Supported Platforms

* .NET 4.5 (Desktop / Server)
* Xamarin.iOS / Xamarin.Android / Xamarin.Mac
* Mono 3.x
* Windows 8 / 8.1 Store Apps

## Getting Started

Octokit is available on NuGet.

```
Install-Package Octokit
```

## Build

Octokit is a single assembly designed to be easy to deploy anywhere. If you 
prefer to compile it yourself, you’ll need:

* Visual Studio 2012 or later, or Xamarin Studio
* Windows 8 or higher to build and test the WinRT projects

To clone it locally click the "Clone in Windows" button above or run the 
following git commands.

```
git clone git@github.com:octokit/Octokit.net.git Octokit
cd Octokit
.\build.cmd
```

## Integration Tests

Octokit has integration tests that access the GitHub API, but they must be
configured before they will be executed. To configure the tests, create a test
GitHub account (i.e., **don't use your real GitHub account**) and then set the
following two environment variables:

- `OCTOKIT_GITHUBUSERNAME` (set this to the test account's username)
- `OCTOKIT_GITHUBPASSWORD` (set this to the test account's password)

Once both of these are set, the integration tests will be executed both when
running the `FullBuild` MSBuild target, and when running the 
`Octokit.Tests.Integration` assembly through an xUnit.net-friendly test runner.

## Build Server

The builds and tests for Octokit.net are run on [qed](https://github.com/half-ogre/qed/). This enables us to build and test incoming pull requests: http://half-ogre-qed.cloudapp.net/octokit/octokit.net

## Problems?

Octokit is 100% certified to be bug free. If you find an issue with our 
certification, please visit the [issue tracker](https://github.com/octokit/octokit.net/issues) 
and report the issue. 

Please be kind and search to see if the issue is already logged before creating
a new one. If you're pressed for time, log it anyways.

When creating an issue, clearly explain

* What you were trying to do.
* What you expected to happen.
* What actually happened.
* Steps to reproduce the problem.

Also include any other information you think is relevant to reproduce the 
problem.

## Contribute

Visit the [Contributor Guidelines](https://github.com/octokit/octokit.net/blob/master/CONTRIBUTING.md) 
for more details.

### A Note about project structure

There are two primary projects in the solution: `Octokit.csproj` and
`Octokit.Reactive.csproj`.

The first is the task-based library. The second is a wrapper that provides an
Reactive Extensions (Rx) based library.

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

## Copyright and License

Copyright 2013 GitHub, Inc.

Licensed under the [MIT License](https://github.com/octokit/octokit.net/blob/master/LICENSE.txt)

## Deploying a new release

When we're ready to deploy a new release, we need to do the following steps.

1. Create a branch named `release`.
2. Update [`ReleaseNotes.md`](ReleaseNotes.md). Note that the format is
important as we parse the version out and use that for the NuGet packages.
3. Push the branch to GitHub and create a pull request. This will kick off the
MyGet build of the NuGet package with this new version.
4. Test!
5. When you're satisfied with this release, push the package 
[from MyGet](https://www.myget.org/feed/Packages/octokit) to NuGet.
6. Create a tag `git tag v#.#.#`. For example, to create a tag for 1.0.0 
`git tag v1.0.0`
7. Push the tag to the server. `git push --tags`
8. Accept the pull request.
9. Create a [new release](https://github.com/octokit/octokit.net/releases/new)
using the tag you just created and pasting in the release notes you just wrote up
