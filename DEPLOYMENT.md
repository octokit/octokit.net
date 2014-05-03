## Deploying a new release

When we're ready to deploy a new release, we need to do the following steps:

1. Create a branch named `release`.
2. Update [`ReleaseNotes.md`](ReleaseNotes.md). Note that the format is
important as we parse the version out and use that for the NuGet packages.
3. Push the branch to GitHub and create a pull request. This will kick off the
MyGet build of the NuGet package with this new version. 
   If you're impatient, you can run `.\build CreatePackages` and get the packages locally.
4. Test!
5. When you're satisfied with this release, push the package 
[from MyGet](https://www.myget.org/feed/Packages/octokit) to NuGet.
6. Create a tag `git tag v#.#.#`. For example, to create a tag for 1.0.0 
`git tag v1.0.0`
7. Push the tag to the server. `git push --tags`
8. Accept the pull request.
9. Create a [new release](https://github.com/octokit/octokit.net/releases/new)
using the tag you just created and pasting in the release notes you just wrote up
