# How to Contribute

We love Pull Requests! Your contributions help make Octokit great.

## Getting Started

So you want to contribute to Octokit. Great! Contributions take many forms 
from submitting issues, writing docs, to making code changes. We welcome 
it all. Don't forget to sign up for a [GitHub account](https://github.com/signup/free), 
if you haven't already.

## Submitting Issues

We track all issues and outstanding work 
[here](https://github.com/octokit/octokit.net/issues). If you've found a 
bug or issue, check that someone else hasn't already reported it.

When submitting an issue, including some of the below will help to identify 
and resolve the issue quicker:

 * the version of Octokit.net you are using
 * the expected behaviour, compared to the actual behaviour
 * the steps to recreate the issue
 * a code sample
 * HTTP request/response details (be careful to not post anything sensitive!)

## Getting Started

You can clone this repository locally from GitHub using the "Clone in Desktop" 
button from the main project site, or run this command in the Git Shell:

`git clone git@github.com:octokit/Octokit.net.git Octokit`

If you want to make contributions to the project, 
[forking the project](https://help.github.com/articles/fork-a-repo) is the 
easiest way to do this. You can then clone down your fork instead:

`git clone git@github.com:MY-USERNAME-HERE/Octokit.net.git Octokit`

After doing that, run the `.\build.cmd` script at the root of the repository 
to ensure all the tests pass.

## How is the codebase organised?

The two main projects are the `Octokit` and `Octokit.Reactive` projects.

The namespaces are organised so that the relevant components are easy to discover:

 - **Authentication** - everything related to authenticating requests 
 - **Clients** - the logic for interacting with various parts of the GitHub API
 - **Exceptions** - types which represent exceptional behaviour from the API 
 - **Helpers** - assorted extensions and helpers to keep the code neat and tidy
 - **Http** - the internal networking components which Octokit requires
 - **Models** - types which represent request/response objects

Unless you're modifying some core behaviour, the **Clients** and **Models** namespaces
are likely to be the most interesting areas.

## What needs to be done?

We have a [`easy-fix`](https://github.com/octokit/octokit.net/issues?labels=easy-fix&state=open)
tag on our issue tracker to indicate tasks which contributors can pick up.

For v1 of the Octokit.net API, we're looking to support everything that v3 of the
GitHub API supports. As this is currently a pre-1.0 library, we're always looking
for ways to improve the API to make it easy to work with the GitHub API.

## Making Changes

When you're ready to make a change, 
[create a branch](https://help.github.com/articles/fork-a-repo#create-branches) 
off the `master` branch. We use `master` as the default branch for the 
repository, and it holds the most recent contributions, so any changes you make
in master might cause conflicts down the track.

If you make focused commits (instead of one monolithic commit) and have descriptive
commit messages, this will help speed up the review process.

If you're adding new files to the Octokit project, we have a helper script to
synchronize these changes with the Mono* projects in the solution. 

Just run this command: `.\build FixProjects`

Octokit.net also has a suite of tests which you can run to ensure existing
behaviour is unchanged. If you're adding new features, please add some 
tests alongside so the maintainers can sleep at night, knowing their 
safety blanket is nice and green!

Run this command to confirm all the tests pass: `.\build`

## Submitting Changes

Once your changes are ready to be reviewed, publish the branch to GitHub and
[open a pull request](https://help.github.com/articles/using-pull-requests) 
against it.

Don't forget to mention in the pull request description which issue/issues are 
being addressed.

Some things that will increase the chance that your pull request is accepted.

* Follow existing code conventions. Most of what we do follows standard .NET
  conventions except in a few places. We include a ReSharper team settings file.
* Include unit tests that would otherwise fail without your code, but pass with 
  it.
* Update the documentation, the surrounding one, examples elsewhere, guides, 
  whatever is affected by your contribution

# Additional Resources

* [General GitHub documentation](http://help.github.com/)
