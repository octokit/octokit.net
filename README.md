# Octokit - GitHub API Client Library for .NET [![Build Status](https://ci.appveyor.com/api/projects/status/github/octokit/octokit.net?branch=master)](https://ci.appveyor.com/project/Haacked15676/octokit-net)

![logo](octokit-dotnet_2.png)


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
or an IObservable based GitHub API client library for .NET using Reactive Extensions

```
Install-Package Octokit.Reactive
```

In Xamarin Studio you can find this option under the project's context menu: **Add | Add Packages...***.

## Documentation

Documentation is available at http://octokitnet.readthedocs.org/en/latest/.

## Build

Octokit is a single assembly designed to be easy to deploy anywhere. If you
prefer to compile it yourself, you’ll need:

* Visual Studio 2013, or Xamarin Studio
* Windows 8 or higher to build and test the WinRT projects

To clone it locally click the "Clone in Windows" button above or run the
following git commands.

```
git clone git@github.com:octokit/Octokit.net.git Octokit
cd Octokit
.\build.cmd
```

## Contribute

Visit the [Contributor Guidelines](https://github.com/octokit/octokit.net/blob/master/CONTRIBUTING.md)
for more details.

## Build Server

The builds and tests for Octokit.net are run on [AppVeyor](http://www.appveyor.com). This enables us to build and test incoming pull requests: https://ci.appveyor.com/project/Haacked15676/octokit-net

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

## Related Projects

 - [ScriptCs.OctoKit](https://github.com/alfhenrik/ScriptCs.OctoKit) - a script pack to use Octokit in scriptcs

## Copyright and License

Copyright 2013 GitHub, Inc.

Licensed under the [MIT License](https://github.com/octokit/octokit.net/blob/master/LICENSE.txt)
