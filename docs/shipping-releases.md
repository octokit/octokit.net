# Shipping an Octokit Release

## Pre-requisites

 - Visual Studio 2017 Update 3

### Running Tests

Due to how long it takes to run the integration tests (and GitHub API rate limits), I usually run these in groups in Visual Studio.

If, however, you still want to do this, it's available from the command line:

> `.\build.ps1 -Target IntegrationTests`

If you want to avoid these tests, or get faster feedback, the Unit and Convention tests are run in the default target
> `.\build.ps1`

Or can be run individually:

> `.\build.ps1 -Target UnitTests`
> `.\build.ps1 -Target ConventionTests`

### Signing Assemblies

**TODO**

### Create NuGet Packages

NuGet packages are automatically generated (and SourceLinked) by the csproj configration.
> `.\build.ps1`

The output .nupkg files will be located in the `packaging` directory.
