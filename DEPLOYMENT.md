## Deploying a new release

When we're ready to deploy a new release, we need to do the following steps:

1. Create a branch named `release`.
2. Update [`ReleaseNotes.md`](ReleaseNotes.md). Note that the format is
important as we parse the version out and use that for the NuGet packages.
3. Push the branch to GitHub and create a pull request. This gives everyone a
   chance to review the release notes.
4. Build and run the fast tests by running this script:
```powershell
./script/build.ps1
```
5. Once you're happy with the state of this branch, tag it:
```
git tag v#.#.#
git push --tags
```
6. To create the packages locally:
```powershell
./script/package.ps1
```
7. Push the tag to the server. `git push --tags`
8. Accept the pull request.
9. Create a [new release](https://github.com/octokit/octokit.net/releases/new)
using the tag you just created and pasting in the release notes you just wrote up

TODO: script for setting tokens for testing
TODO: script for running integration tests

TODO: look into using GitHubReleaseNotes to draft the release notes
TODO: look into using paket/octokit for publishing artefacts
TODO: look into creating pre-release packages on AppVeyor
