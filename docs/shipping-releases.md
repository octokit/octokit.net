# Shipping an Octokit Release

## Pre-requisites

 - Visual Studio 2013 Update 4
 - Debugging Tools for Windows (install via [Chocolatey](http://chocolatey.org) - `cinst windbg`)

### Running Tests

Due to how long it takes to run the integration tests, I usually run these in groups in Visual Studio.

If, however, you still want to do this, it's available from the command line:

> `.\build RunIntegrationTests`

If you want to avoid these tests, or get faster feedback, you can run the other
tests:

> `.\build RunUnitTests`
> `.\build RunConventionTests`

### Signing Assemblies

**TODO**

### Create NuGet Packages

To generate the NuGet packages, you also need to setup the SourceLink tools.

Set this globally:

> git config --global core.autocrlf input

**TODO: ** need to reset the working directory to ensure CRLF files are cleared

Once you've done that, just run this:

> `.\build CreatePackages`

This will build the assemblies, index the symbols and create the packages. The
output .nupkg files will be located in the `packaging` directory.