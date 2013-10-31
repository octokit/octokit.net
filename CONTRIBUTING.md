# How to Contribute

We love Pull Requests! Your contributions help make Octokit great.

## Getting Started

So you want to contribute to Octokit. Great! Contributions take many forms from 
submitting issues, writing docs, to making code changes. We welcome it all.

But first things first...

* Make sure you have a [GitHub account](https://github.com/signup/free)
* Submit a ticket for your issue, assuming one does not already exist.
  * Clearly describe the issue including steps to reproduce when it is a bug.
  * Make sure you fill in the earliest version that you know has the issue.
* Fork the repository on GitHub by clicking on the "Clone in Windows" button or 
run the following command in a git shell.
```
git clone git@github.com:github/Octokit.net.git Octokit
```
* Make sure the project builds and all tests pass on your machine by running 
the `build.cmd` script (this calls a PowerShell script, `Build-Solution.ps1`).

## Making Changes

* Create a topic branch off master (don't work directly on master).
* Make commits of logical units.
* Provide descriptive commit messages in the proper format (GitHub for Windows 
  helps get the format correct).
* Make sure you have added the necessary tests for your changes.
* If you add a new file, update **all the csproj files**. If you can, make sure
  that your changes build on every platform but if that's a pain, don't worry
  about it.
* Run _all_ the tests to assure nothing else was accidentally broken.

## Submitting Changes

* Push your changes to a topic branch in your fork of the repository.
* Submit a pull request. Note what issue/issues your patch fixes.

Some things that will increase the chance that your pull request is accepted.

* Follow existing code conventions. Most of what we do follows standard .NET
  conventions except in a few places. We include a ReSharper team settings file.
* Include unit tests that would otherwise fail without your code, but pass with 
  it.
* Update the documentation, the surrounding one, examples elsewhere, guides, 
  whatever is affected by your contribution


# Additional Resources

* [General GitHub documentation](http://help.github.com/)
* [GitHub pull request documentation](http://help.github.com/send-pull-requests/)
