# Octokit - GitHub API Client Library for .NET 

![Build status](https://github.com/octokit/octokit.net/workflows/CI%20Build/badge.svg)
[![Build status](https://ci.appveyor.com/api/projects/status/cego2g42yw26th26/branch/master?svg=true)](https://ci.appveyor.com/project/github-windows/octokit-net/branch/master)
[![codecov](https://codecov.io/gh/octokit/octokit.net/branch/master/graph/badge.svg)](https://codecov.io/gh/octokit/octokit.net)
[![Join the chat at https://gitter.im/octokit/octokit.net](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/octokit/octokit.net?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![NuGet](http://img.shields.io/nuget/v/Octokit.svg)](https://www.nuget.org/packages/Octokit)
[![NuGet](http://img.shields.io/nuget/v/Octokit.Reactive.svg)](https://www.nuget.org/packages/Octokit.Reactive)

![logo](octokit-dotnet_2.png)

Octokit is a client library targeting .NET Framework 4.5+ and .NET Standard 1+
and above that provides an easy way to interact with the
[GitHub API](http://developer.github.com/v3/).

## Usage examples

Get public info on a specific user.

```c#
var github = new GitHubClient(new ProductHeaderValue("MyAmazingApp"));
var user = await github.User.Get("half-ogre");
Console.WriteLine(user.Followers + " folks love the half ogre!");
```

## Supported Platforms

* .NET 4.5 (Desktop / Server) or greater
* [.NET Standard 1.1](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) or greater

## Getting Started

Octokit is a GitHub API client library for .NET and is available on NuGet:

```
Install-Package Octokit
```

There is also an IObservable based GitHub API client library for .NET using Reactive Extensions:

```
Install-Package Octokit.Reactive
```


### Beta packages ###
Unstable NuGet packages that track the master branch of this repository are available at
[https://ci.appveyor.com/nuget/octokit-net](https://ci.appveyor.com/nuget/octokit-net)

In Xamarin Studio you can find this option under the project's context menu: **Add | Add Packages...***.

## Documentation

Documentation is available at http://octokitnet.readthedocs.io/en/latest/.

## Build

Octokit is a single assembly designed to be easy to deploy anywhere.

To clone and build it locally click the "Clone in Desktop" button above or run the 
following git commands.

```
git clone git@github.com:octokit/Octokit.net.git Octokit
cd Octokit
```

To build the libraries, run the following command:

Windows: `.\build.ps1`

Linux/OSX: `./build.sh`

## Contribute

Visit the [Contributor Guidelines](https://github.com/octokit/octokit.net/blob/master/CONTRIBUTING.md)
for more details. All contributors are expected to follow our
[Code of Conduct](https://github.com/octokit/octokit.net/blob/master/CODE_OF_CONDUCT.md).

## Problems?

If you find an issue with our library, please visit the [issue tracker](https://github.com/octokit/octokit.net/issues)
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

 - [ScriptCs.OctoKit](https://github.com/hnrkndrssn/ScriptCs.OctoKit) - a [script pack](https://github.com/scriptcs/scriptcs/wiki/Script-Packs) to use Octokit in scriptcs 
 - [ScriptCs.OctokitLibrary](https://github.com/ryanrousseau/ScriptCs.OctokitLibrary) - a [script library](https://github.com/scriptcs/scriptcs/wiki/Script-Libraries) to use Octokit in scriptcs

## Copyright and License

Copyright 2017 GitHub, Inc.

Licensed under the [MIT License](https://github.com/octokit/octokit.net/blob/master/LICENSE.txt)
