## Deploying a new release

### Requirements

Creating a new release and deploying the package to nuget are administrative tasks and require that you have/do the following:
1. Admin access to the [GitHub](https://www.nuget.org/profiles/GitHub) NuGet organization
2. Admin rights to the [octokit.net](https://github.com/octokit/octokit.net) repository
3. For any PRs that should be noted in the release notes, each pull request must have a comment formatted like `release_notes: description` where description represents the details of the change.

### Prepare the changeset & publish to nuget.org

1. Create a branch named `release`.
2. Push the branch to GitHub and create a pull request. This will kick off the CI builds to verify the changes.
3. Test!
4. [Generate](https://github.com/shiftkey/octokit.releasenotes) the release notes. `Octokit.ReleaseNotes generate v0.50.0 [BASE] [HEAD]`
  Any PRs that should be noted in the release notes, each pull request must have a comment formatted like `release_notes: description` where description represents the details of the change. Make sure for the commit range any changeset in a PR that should be represented has a comment on the PR prefixed with `release_notes:` and corresponding label (details below)
5. When you're satisfied with this release, create a tag `git tag v#.#.#`. For example, to create a tag for 1.0.0
`git tag v1.0.0`
6. Push the tag to the server. `git push --tags`
7. When the tag is successfully pushed, the [publish workflow](https://github.com/octokit/octokit.net/blob/main/.github/workflows/publish.yml) will run and build and push the package to nuget
8. Verify that the package has been uploaded to [nuget.org](https://www.nuget.org/packages/Octokit/)
8. Accept the pull request
9. Create a [new release](https://github.com/octokit/octokit.net/releases/new)
using the tag you just created and pasting in the release notes you just generated

---

### Details on the release notes generator

Release notes [generator](https://github.com/shiftkey/octokit.releasenotes): this is used to pull details using the GitHub API to generate the release notes.

The generator helps in that it produces release notes that are consistently formatted based on information directly from the change scope (from the previous release to the current tip i.e. the `release` branch).

The nuances are mostly around how it gets and parses release notes from the PRs found in the changeset. The following are the comment prefixes that the generator currently supports:

* `release_notes` - Having this in the comment with a corresponding "category" label on the PR will put the given note under the "Release Notes" section of the output in the group for the given label
* `advisories` - Having this in the comment will put the given note into the "Advisories and Breaking Changes" section of the output

In addition to formatted comment parsing, the generator also looks at the following labels to generate categories with in the release notes.

* `category: bug` - This will add the comment in the PR prefixed with `release_notes` into the `Fixes` group
* `category: docs-and-samples` - This will add the comment in the PR prefixed with `release_notes` into the `Documentation Updates` group
* `category: feature` - This will add the comment in the PR prefixed with `release_notes` into the `Features/Enhancements` group
* `category: housekeeping` - This will add the comment in the PR prefixed with `release_notes` into the `Housekeeping` group

When these labels are added to a given PR the generator will produce and categorize comment in the release notes formatted like:

`Comment content` - `PR Id and link` via `Contributor`
ex. "Cleans up some mentions of `master` branch in codebase - [#2306](https://github.com/octokit/octokit.net/pull/2306) via [@shiftkey](https://github.com/shiftkey)"

----
### Troubleshooting

When running the release notes generator on a mac if dotnet has not been added to the PATH or there is not alias defined in your dotfiles you'll need to execute the command as follows:

`dotnet run generate [BASE] [HEAD]`
(where `[BASE]` is the last release label (i.e. v0.50.0) and `[HEAD]` represents the `release` branch tip that was generated above)

The tool looks for a environment variable named `OCTOKIT_OAUTHTOKEN`. This token requires a minimal scope to execute and pull the data from the repository via API. Keep in mind that if a token is not provided then you will most likely run into rate limit errors because the requests are being made anonymously.
