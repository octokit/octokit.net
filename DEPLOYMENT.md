## Deploying a new release

### Requirements

Creating a new release and deploying the package to nuget are administrative tasks and require that you have/do the following:
1. Admin access to the [GitHub](https://www.nuget.org/profiles/GitHub) NuGet organization
2. Admin rights to the [octokit.net](https://github.com/octokit/octokit.net) repository

### Publish to nuget.org

1. Run test and perform manual tests from `main`
2. When you're satisfied with this release, create a tag `git tag v#.#.#`. For example, to create a tag for 3.0.1
`git tag v3.0.1`
3. Push the tag to the server. `git push --tags`
4. When the tag is successfully pushed, the [publish workflow](https://github.com/octokit/octokit.net/blob/main/.github/workflows/publish.yml) will run and build and push the package to nuget
5. Verify that the package has been uploaded to [nuget.org](https://www.nuget.org/packages/Octokit/)
6. Create a [new release](https://github.com/octokit/octokit.net/releases/new) using the tag you just created and pasting in the release notes you just generated
