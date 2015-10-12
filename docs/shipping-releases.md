## Shipping Releases

### The Steps

When we're ready to deploy a new release, we need to do the following steps:

 - Create a release branch (we use `release` but this isn't mandatory).
 - Update [`ReleaseNotes.md`](ReleaseNotes.md). Note that the format is
   important as we parse the version out and use that for the NuGet packages.
 - Push the branch to GitHub and open a pull request. This gives everyone a
   chance to review the release notes.
 - From the root of the repository, build and test the code:

```powershell
./build
```

 - Once you're happy with the state of this branch, tag it:

```
git tag v#.#.#
git push --tags
```

 - Create the packages for this release:

```powershell
./script/package
```

- Merge the pull request.
- Create a [new release](https://github.com/octokit/octokit.net/releases/new)
on GitHub using the tag you just created and pasting in the release notes you
just wrote up

## Future Work

There's still a bunch of manual work in here, so here's what I'd like to
look at incorporating into this process in the future.

 - make it easy to configure and run the integration tests outside of VS -
   this is a bit fiddly currently due to how tests break down when API limits
   are exhausted. I think I can extend xUnit to support this, but there are
   potential workarounds to do this before going down that path.

 - use the [GitReleaseNotes](https://github.com/GitTools/GitReleaseNotes) 
   to generate release notes for the current build
   
 - publishing artifacts to GitHub from script - this has been started in 
   https://github.com/octokit/octokit.net/pull/933

 - use CI to publish pre-release packages (after a PR is merged,
   take whatever is on master - if it builds and tests pass, publish pacakge)
