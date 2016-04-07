# How to Contribute

Contributions take many forms from submitting issues, writing docs, to making
code changes - we welcome it all!

## Getting Started

If you don't have a GitHub account, you can [sign up](https://github.com/signup/free)
as it will help you to participate with the project.

If you are looking to contribute to the codebase, please ensure you have Visual
Studio 2015 installed - you can download the Community edition from
[here](https://www.visualstudio.com/en-us/downloads/download-visual-studio-vs.aspx)

If you are running GitHub Desktop, you can clone this repository locally from
GitHub using the "Clone in Desktop" button from the Octokit.net project page,
or run this command in your Git-enabled shell:

`git clone https://github.com/octokit/Octokit.net.git Octokit`

If you want to make contributions to the project,
[forking the project](https://help.github.com/articles/fork-a-repo) is the
easiest way to do this. You can then clone down your fork instead:

`git clone https://github.com/MY-USERNAME-HERE/Octokit.net.git Octokit`

After doing that, run the `.\build` script at the root of the repository
to ensure everything builds and the tests pass.

## How can I get involved?

We have an [`up-for-grabs`](https://github.com/octokit/octokit.net/issues/labels/up-for-grabs)
tag on our issue tracker to indicate tasks which contributors can pick up.

If you've found something you'd like to contribute to, leave a comment in the issue
so everyone is aware.

For v1 of the Octokit.net API, we're looking to support everything that v3 of the
GitHub API supports. As this is currently a pre-1.0 library, we're always looking
for ways to improve the API to make it easy to work with the GitHub API.

## Making Changes

When you're ready to make a change, create a branch off the `master` branch:

```
git checkout master
git pull origin master
git checkout -b SOME-BRANCH-NAME
```

We use `master` as the default branch for the repository, and it holds the most
recent contributions. By working in a branch away from `master` you can handle
potential conflicts that may occur in the future.

If you make focused commits (instead of one monolithic commit) and have descriptive
commit messages, this will help speed up the review process.

### Adding New files

To ensure new files are available in the various projects, we have a helper script
to synchronize these changes across all the projects in the solution.

If you need to create new files:

  - add the file to the main `Octokit` project
  - build the project (to ensure the `csproj` change is saved)
  - run this command: `.\build FixProjects`

At any time you can build the project with `.\build BuildApp` - this will also
run FxCop analysis.

### Running Tests

Octokit.net also has a suite of tests which you can run to ensure existing
behaviour is not affected. If you're adding new features, please add some
tests alongside so the maintainers can sleep at night, knowing their
safety blanket is nice and green!

The test suite is arranged into fast and slow tests.

#### Fast Tests

**Unit Tests:** `.\build UnitTests`

These tests verify specific behaviour while being isolated from the rest of the
library. If you are not familiar with unit testing, have a look at the existing
examples - they should be easy to apply to your work.

**Convention Tests:** `.\build ConventionTests`

These tests verify conventions and structure across the entire codebase -
ensuring everything is consistent and predictable. When writing new features,
these tests may fail and should help indicate where the changes have violated
the conventions, so feel free to run them locally while you're working on new
features.

#### Slow Tests

**Integration Tests**

Octokit has integration tests that access the GitHub API, but they require a
bit of setup to run. The tests make use of a set of test accounts accessed via
credentials stored in environment variables.

Run the following interactive script to set the necessary environment
variables:

`.\script\configure-integration-tests.ps1`

After running this, ensure any existing instances of Visual Studio are restarted
so they pick up the new environment variables are detected.

With these variables set, you can run the integration tests locally using
`.\build IntegrationTests` or by running the `Octokit.Tests.Integration`
assembly in the Visual Studio test runner.

**Note:** as the integration tests rely on using the actual GitHub API, you may
encounter issues if running the tests too frequently. Please use a test account
so that you're not impacted in the unlikely scenario of your account being
flagged as a spammer.

### Testing Documentation

If you are making changes to the documentation for Octokit, you can test these
changes locally using the [guide](https://github.com/shiftkey/octokit.net/blob/master/docs/contributing.md)
under the `docs` folder.

### Submitting Changes

You can publish your branch from GitHub for Windows, or run this command from
the Git Shell:

`git push origin MY-BRANCH-NAME`

Once your changes are ready to be reviewed, publish the branch to GitHub and
[open a pull request](https://help.github.com/articles/using-pull-requests)
against it.

A few suggestions when opening a pull request:

 - if you are addressing a particular issue, reference it like this:

>   Fixes #1145

 - prefix the title with `[WIP]` to indicate this is a work-in-progress. It's
   always good to get feedback early, so don't be afraid to open the PR before
   it's "done".
 - use [checklists](https://github.com/blog/1375-task-lists-in-gfm-issues-pulls-comments)
   to indicate the tasks which need to be done, so everyone knows how close you
   are to done.
 - add comments to the PR about things that are unclear or you would like
   suggestions on

Some things that will increase the chance that your pull request is accepted:

* Follow existing code conventions. Most of what we do follows [standard .NET
  conventions](https://github.com/dotnet/corefx/blob/master/Documentation/coding-guidelines/coding-style.md) except in a few places. We include a ReSharper team settings file.
* Include unit tests that would otherwise fail without your code, but pass with
  it.
* Update the documentation, the surrounding one, examples elsewhere, guides,
  whatever is affected by your contribution

# Additional Resources

* [Octokit Codebase Overview](https://github.com/octokit/octokit.net/blob/master/OVERVIEW.md)
* [General GitHub documentation](http://help.github.com/)
