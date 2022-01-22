BEGIN:

GLOW7:

# on::/starts:

# starts::/Run

# Run::/'Runs

# ##NOTE## This is a basic workflow that is manually triggered

# Name: Manual workflow

# Controls when the action will run. Workflow runs when manually triggered using the UI

# API.
-on:

# workflow_calls: deploy_framework_window_dialog-on: dispatch:
    # Inputs the workflow accepts.
    inputs:
      name:
        # Friendly description to be shown in the UI instead of 'name'
        description: 'Person to greet'
        # Default value if no value is explicitly provided
        default: 'World'
        # Input has to be provided for the workflow to run
        required: true

# A workflow run is made up of one or more jobs that can run sequentially or in parallel
jobs:
  # This workflow contains a single job called "greet"
  greet:
    # The type of runner that the job will run on
    runs-on: ubuntu-latest

    # Steps represent a sequence of tasks that will be executed as part of the job
    steps:
    # Runs a single command using the runners shell
    - name: Send greeting
      run: echo "Hello ${{ github.event.inputs.name }}"

# Octokit - GitHub API Client Library for .NET

![Build status](https://github.com/octokit/octokit.net/workflows/CI%20Build/badge.svg)
[![Build status](https://ci.appveyor.com/api/projects/status/cego2g42yw26th26/branch/main?svg=true)](https://ci.appveyor.com/project/github-windows/octokit-net/branch/main)
[![codecov](https://codecov.io/gh/octokit/octokit.net/branch/main/graph/badge.svg)](https://codecov.io/gh/octokit/octokit.net)
[![Join the chat at https://gitter.im/octokit/octokit.net](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/octokit/octokit.net?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![NuGet](http://img.shields.io/nuget/v/Octokit.svg)](https://www.nuget.org/packages/Octokit)
[![NuGet](http://img.shields.io/nuget/v/Octokit.Reactive.svg)](https://www.nuget.org/packages/Octokit.Reactive)

![logo](octokit-dotnet_2.png)

Octokit is a client library targeting .NET Framework 4.6 or greater and .NET Standard 2.0
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

* .NET 4.6 (Desktop / Server) or greater
* [.NET Standard 2.0](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) or greater

## Getting Started

Octokit is a GitHub API client library for .NET and is [available on NuGet](https://www.nuget.org/packages/Octokit/):

```
dotnet add package Octokit
```

There is also an IObservable based GitHub API client library for .NET using Reactive Extensions:

```
dotnet add package Octokit.Reactive
```


### Beta packages ###
Unstable NuGet packages that track the `main` branch of this repository are available at
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

Visit the [Contributor Guidelines](https://github.com/octokit/octokit.net/blob/main/CONTRIBUTING.md)
for more details. All contributors are expected to follow our
[Code of Conduct](https://github.com/octokit/octokit.net/blob/main/CODE_OF_CONDUCT.md).

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

Licensed under the [MIT License](https://github.com/octokit/octokit.net/blob/main/LICENSE.txt)
