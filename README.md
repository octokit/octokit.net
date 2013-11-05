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

Octokit is a single assembly designed to be easy to deploy anywhere. If you prefer
to compile it yourself, you’ll need:

* Visual Studio 2012 or later, or Xamarin Studio
* Windows 8 or higher to build and test the WinRT projects

To clone it locally click the "Clone in Windows" button above or run the 
following git commands.

```
git clone git@github.com:github/Octokit.net.git Octokit
cd Octokit
.\build.cmd
```

## Integration Tests

Octokit has integration tests that access the GitHub API, but they must be configured before they will be executed. 
To configure the tests, create a test GitHub account (i.e., **don't use your real GitHub account**) and then set
the following two environment variables:

- `OCTOKIT_GITHUBUSERNAME` (set this to the test account's username)
- `OCTOKIT_GITHUBPASSWORD` (set this to the test account's password)

Once both of these are set, the integration tests will be executed both when running the `FullBuild` MSBuild target,
and when running the `Octokit.Tests.Integration` assembly through an xUnit.net-friendly test runner.

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

## Copyright and License

Copyright 2013 GitHub, Inc.

Licensed under the [MIT License](https://github.com/octokit/octokit.net/blob/master/LICENSE.txt)

## Nonsense to test CI

More nonsense
Even more nonsense
Again!
Yet again
